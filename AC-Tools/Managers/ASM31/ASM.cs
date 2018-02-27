using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM31
{
    public partial class ASM : Form
    {
        bool shutdown { set; get; }
        Thread watchThread;
        ChatView cv { set; get; }
        LoginView lv { set; get; }
        public GameView gv { set; get; }

        public ASM()
        {
            shutdown = false;
            InitializeComponent();
            showSettings();
            watchThread = new Thread(new ThreadStart(webDbStatusMon));
            watchThread.Start();
        }

        private void showSettings()
        {

            this.Text = "AION System Manager - v" + Properties.Settings.Default.version;

            serverPath.Text = Properties.Settings.Default.serverBase;
            gamePath.Text = Properties.Settings.Default.gamePath;
            loginPath.Text = Properties.Settings.Default.loginPath;
            chatPath.Text = Properties.Settings.Default.chatPath;
            string jh = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (jh != null && jh.Length > 0)
            {
                javaHome.Text = jh;
                if (!jh.Equals(Properties.Settings.Default.javaHome))
                {
                    Properties.Settings.Default.javaHome = javaHome.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                jh = Properties.Settings.Default.javaHome;
                if (jh.Equals("***"))
                    javaHome.Text = "Set JAVA_HOME env variable or javaHome setting";
                else
                    javaHome.Text = jh;
            }
                
            
            if (Properties.Settings.Default.javaHome.Equals("***"))
            {
                Properties.Settings.Default.javaHome = javaHome.Text;
                Properties.Settings.Default.Save();
            }
        }



        private void updateStateView(ServerType type, State state)
        {
            switch (type)
            {
                case ServerType.DB:
                    dbStatus.Text = state.ToString();
                    if (state == State.Stopped)
                        dbStatus.ForeColor = Color.Red;
                    else
                        dbStatus.ForeColor = Color.DarkGreen;
                    break;
                case ServerType.Web:
                    webStatus.Text = state.ToString();
                    if (state == State.Stopped)
                        webStatus.ForeColor = Color.Red;
                    else
                        webStatus.ForeColor = Color.DarkGreen;
                    break;
                case ServerType.Chat:
                    if (state == State.Running)
                    {
                        chatState.BackgroundImage = Properties.Resources.green_dot;
                        chatControl.BackgroundImage = Properties.Resources.button_stop;
                        chatControl.DialogResult = DialogResult.No;
                    }
                    else if (state == State.Starting)
                    {
                        chatState.BackgroundImage = Properties.Resources.yellow_dot;
                        chatControl.BackgroundImage = Properties.Resources.button_stop;
                        chatControl.DialogResult = DialogResult.No;
                    }
                    else if (state == State.Stopped)
                    {
                        chatState.BackgroundImage = Properties.Resources.red_dot;
                        chatControl.BackgroundImage = Properties.Resources.button_start;
                        chatControl.DialogResult = DialogResult.Yes;
                    }
                    break;
                case ServerType.Login:
                    if (state == State.Running)
                    {
                        loginState.BackgroundImage = Properties.Resources.green_dot;
                        loginControl.BackgroundImage = Properties.Resources.button_stop;
                        loginControl.DialogResult = DialogResult.No;
                    }
                    else if (state == State.Starting)
                    {
                        loginState.BackgroundImage = Properties.Resources.yellow_dot;
                        loginControl.BackgroundImage = Properties.Resources.button_stop;
                        loginControl.DialogResult = DialogResult.No;
                    }
                    else if (state == State.Stopped)
                    {
                        loginState.BackgroundImage = Properties.Resources.red_dot;
                        loginControl.BackgroundImage = Properties.Resources.button_start;
                        loginControl.DialogResult = DialogResult.Yes;
                    }
                    break;
                case ServerType.Game:
                    if (state == State.Running)
                    {
                        gameState.BackgroundImage = Properties.Resources.green_dot;
                        gameControl.BackgroundImage = Properties.Resources.button_stop;
                        gameControl.DialogResult = DialogResult.No;
                    }
                    else if (state == State.Starting)
                    {
                        gameState.BackgroundImage = Properties.Resources.yellow_dot;
                        gameControl.BackgroundImage = Properties.Resources.button_stop;
                        gameControl.DialogResult = DialogResult.No;
                    }
                    else if (state == State.Stopped)
                    {
                        gameState.BackgroundImage = Properties.Resources.red_dot;
                        gameControl.BackgroundImage = Properties.Resources.button_start;
                        gameControl.DialogResult = DialogResult.Yes;
                    }
                    break;
            }

        }

        //  threadsafe
        public void updateState(ServerType type, State state)
        {
            if (!IsDisposed)
                if (InvokeRequired)
                    try
                    {
                        Invoke((MethodInvoker)delegate { updateStateView(type, state); });
                    }
                    catch (Exception)
                    {}
                else
                    updateStateView(type, state);
        }

        private void webDbStatusMon()
        {
            string webHost = Properties.Settings.Default.webHost;
            int webPort = Properties.Settings.Default.webPort;
            string dbHost = Globals.instance.getDbHost();
            int dbPort = Globals.instance.getDbPort();

            while (!shutdown)
            {
                if (IsDisposed)
                    return;
                if (NetUtils.isConnected(webHost, webPort))
                    updateState(ServerType.Web, State.Running);
                else
                    updateState(ServerType.Web, State.Stopped);
                if (NetUtils.isConnected(dbHost, dbPort))
                    updateState(ServerType.DB, State.Running);
                else
                    updateState(ServerType.DB, State.Stopped);
                Thread.Sleep(2000);
            }
        }

        private void chatControl_Click(object sender, EventArgs e)
        {
            if (chatControl.DialogResult == DialogResult.Yes)
            {
                if (cv == null || cv.IsDisposed)
                {
                    // of not created yet or has been closed
                    cv = new ChatView(this);
                    cv.Show();
                }
                if (cv != null && !cv.IsDisposed)
                    cv.start();
            }
            else
            {
                if (cv != null && !cv.IsDisposed)
                    cv.stop();
            }
            ASMDB db = ASMDB.getInstance();
        }

        private void loginControl_Click(object sender, EventArgs e)
        {
            if (loginControl.DialogResult == DialogResult.Yes)
            {
                if (lv == null || lv.IsDisposed)
                {
                    // of not created yet or has been closed
                    lv = new LoginView(this);
                    lv.Show();
                }
                if (lv != null && !lv.IsDisposed)
                    lv.start();
            }
            else
            {
                if (lv != null && !lv.IsDisposed)
                    lv.stop();
            }
        }

        private void gameControl_Click(object sender, EventArgs e)
        {
            if (gameControl.DialogResult == DialogResult.Yes)
            {
                if (gv == null || gv.IsDisposed)
                {
                    // of not created yet or has been closed
                    gv = new GameView(this);
                    gv.Show();
                }
                if (gv != null && !gv.IsDisposed)
                    gv.start();
            }
            else
            {
                if (gv != null && !gv.IsDisposed)
                    gv.stop();
            }
        }

        private void chatState_Click(object sender, EventArgs e)
        {
            if (cv != null && !cv.IsDisposed)
                cv.BringToFront();
        }

        private void loginState_Click(object sender, EventArgs e)
        {
            if (lv != null && !lv.IsDisposed)
                lv.BringToFront();
        }

        private void gameState_Click(object sender, EventArgs e)
        {
            if (gv != null && !gv.IsDisposed)
                gv.BringToFront();
        }

        private void startAll_Click(object sender, EventArgs e)
        {
            // chat server
            if (chatControl.DialogResult == DialogResult.Yes)
            {
                if (cv == null || cv.IsDisposed)
                {
                    // of not created yet or has been closed
                    cv = new ChatView(this);
                    cv.Show();
                }
                if (cv != null && !cv.IsDisposed)
                    cv.start();
            } 
            if (loginControl.DialogResult == DialogResult.Yes)
            {
                if (lv == null || lv.IsDisposed)
                {
                    // of not created yet or has been closed
                    lv = new LoginView(this);
                    lv.Show();
                }
                if (lv != null && !lv.IsDisposed)
                    lv.start();
            }
            if (gameControl.DialogResult == DialogResult.Yes)
            {
                if (gv == null || gv.IsDisposed)
                {
                    // of not created yet or has been closed
                    gv = new GameView(this);
                    gv.Show();
                }
                if (gv != null && !gv.IsDisposed)
                    gv.start();
            }
        }

        private void stopAll_Click(object sender, EventArgs e)
        {
            if (chatControl.DialogResult == DialogResult.No)
            {
                if (cv != null && !cv.IsDisposed)
                    cv.stop();
            }
            if (loginControl.DialogResult == DialogResult.No)
            {
                if (lv != null && !lv.IsDisposed)
                    lv.stop();
            }
            if (gameControl.DialogResult == DialogResult.No)
            {
                if (gv != null && !gv.IsDisposed)
                    gv.stop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool stuffRunning = false;
            if (cv != null && cv.state == State.Running)
                stuffRunning = true;
            if (lv != null && lv.state == State.Running)
                stuffRunning = true;
            if (gv != null && gv.state == State.Running)
                stuffRunning = true;
            if (stuffRunning)
            {
                DialogResult answer = MessageBox.Show(this, "One or more servers is currently running or starting.\nClosing this window will stop them all and exit ASM.\n\nContinue?", "Servers Running!", MessageBoxButtons.OKCancel);
                if (answer == DialogResult.Cancel)
                    return;
                stopAll_Click(sender, e);
            }
            Application.Exit();
        }

        private void ASM_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool stuffRunning = false;
            if (cv != null && cv.state == State.Running)
                stuffRunning = true;
            if (lv != null && lv.state == State.Running)
                stuffRunning = true;
            if (gv != null && gv.state == State.Running)
                stuffRunning = true;
            if (stuffRunning)
            {
                DialogResult answer = MessageBox.Show(this, "One or more servers is currently running or starting.\nClosing this window will stop them all and exit ASM.\n\nContinue?", "Servers Running!", MessageBoxButtons.OKCancel);
                if (answer == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                stopAll_Click(sender, e);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ASMSettings settings = new ASMSettings();
            settings.Show();
        }
        private ASMStats stats;
        private void btnStatsS_Click(object sender, EventArgs e)
        {
            if (stats == null || stats.IsDisposed)
            {
                stats = new ASMStats(gv);
                stats.Show();
            }
            else
            {
                stats.BringToFront();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void editSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void generateClientBatchFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = fbd.SelectedPath;
                string host = Globals.instance.getGameHost();
                int port = Globals.instance.getLoginPort();
                string content = Globals.launchContent;
                content = content.Replace("%IP%", host);
                content = content.Replace("%PORT%", port.ToString());
                string filename = path + "\\aionstart.cmd";
                System.IO.File.WriteAllText(filename, content);

                MessageBox.Show("AION client start batch file was saved to\n" + filename);
            }
        }

        private void clearDatabaseErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ASMDB asmdb = ASMDB.getInstance();
            asmdb.clearMySQLErrors();
        }

        private void accountManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcctMgr acctMgr = new AcctMgr();
            acctMgr.Show();
        }
    }
}
