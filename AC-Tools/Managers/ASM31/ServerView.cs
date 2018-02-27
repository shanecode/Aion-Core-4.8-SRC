using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM31
{
    public partial class ServerView : Form
    {
        private ASM asm { set; get; }
        public Engine engine { get; set; }
        public State state { get; set; }

        public ServerType type { get; set; }

        public ServerView()
        {
            InitializeComponent();
        }

        public ServerView(ASM asm)
        {
            this.asm = asm;
            InitializeComponent();
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            console.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // check to see if server is running. if it is, warn!
            if (state == State.Starting || state == State.Running)
            {
                DialogResult answer = MessageBox.Show(this, "Server is currently running or starting.\nClosing this window will stop this server.\n\nContinue?", "Server Running!", MessageBoxButtons.OKCancel);
                if (answer == DialogResult.Cancel)
                    return;
            }
            //contiuing after user oked. kill server.

            // finally exit
            stop();         // stop the server and all processes/hreads associated with it
            Dispose();      // shut the window
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            stop();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string cmd = commandInput.Text;
            if (cmd.Length > 0 && !cmd.StartsWith("?"))
            {
                //send command to server.
            }
        }

        private void addConsoleText(string line)
        {
            if (!IsDisposed)
            {
                console.AppendText(line + Environment.NewLine);
                console.ScrollToCaret();
            }
        }

        private void addConsoleTextError(string line)
        {
            if (!IsDisposed)
            {
                console.SelectionStart = console.TextLength;
                console.SelectionLength = 0;
                console.SelectionColor = Color.Red;
                console.AppendText(line + Environment.NewLine) ;
                console.SelectionColor = console.ForeColor;
                console.ScrollToCaret();
            }
        }

        //  threadsafe
        public void updateConsole(string line)
        {
            
                if (InvokeRequired)
                    try
                    {
                        if (!IsDisposed)
                            Invoke((MethodInvoker)delegate { addConsoleText(line); });
                    }
                    catch (Exception)
                    { }
                else
                    addConsoleText(line);
        }

        public void updateConsoleErr(string line)
        {

            if (InvokeRequired)
                try
                {
                    if (!IsDisposed)
                        Invoke((MethodInvoker)delegate { addConsoleTextError(line); });
                }
                catch (Exception)
                { }
            else
                addConsoleTextError(line);
        }

        private void updateStateViews()
        {
            Text = type.ToString() + " - " + state.ToString();
            Icon anIcon = null;
            switch(state)
            {
                case State.Running :
                    anIcon = Properties.Resources.green_dot_png;        // running
                    break;
                case State.Starting :
                    anIcon = Properties.Resources.yellow_dot_png;       // starting
                    break;
                default :
                    anIcon = Properties.Resources.red_dot_png;          // stopped
                    break;
            }
            Icon = anIcon;
            asm.updateState(type, state);            
        }

        //  threadsafe
        public void updateState(State newState)
        {
            state = newState;
            if (InvokeRequired)
            {
                if (!IsDisposed)
                    Invoke((MethodInvoker)delegate { updateStateViews(); });
            }
            else
                updateStateViews();
        }

        public void start()
        {
            console.Text = "\n +++ "+type.ToString() + " Server is Starting +++\n";
            engine = new Engine(type, this);

            string args = null;
            string path = null;

            switch (type)
            {
                case ServerType.Chat :
                    if (Properties.Settings.Default.runMode == 1)
                        args = Properties.Settings.Default.argsChatMode1;
                    else
                        args = Properties.Settings.Default.argsChatMode2;
                    path = Properties.Settings.Default.chatPath;
                    break;
                case ServerType.Login :
                    if (Properties.Settings.Default.runMode == 1)
                        args = Properties.Settings.Default.argsLoginMode1;
                    else
                        args = Properties.Settings.Default.argsLoginMode2;
                    path = Properties.Settings.Default.loginPath;
                    break;
                case ServerType.Game:
                    if (Properties.Settings.Default.runMode == 1)
                        args = Properties.Settings.Default.argsGameMode1;
                    else
                        args = Properties.Settings.Default.argsGameMode2;
                    int iz = Properties.Settings.Default.gameMemSize;
                    string memSize = iz.ToString();
                    args = args.Replace("%MEMSIZE%", memSize);
                    path = Properties.Settings.Default.gamePath ;
                    break;
            }
            if (args == null || path == null)
                return;

            engine.args = args;
            engine.startPath = path;
            engine.start();
        }

        public void stop()
        {
            engine.shutDown();   // stop all engine threads
            updateState(State.Stopped);
            updateConsole("\n --- "+type.ToString()+ " Server has stopped ---");
        }

        private void ServerView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (state == State.Running || state == State.Starting)
            {
                DialogResult answer = MessageBox.Show(this, "Servers is currently running or starting.\nClosing this window will stop it.\n\nContinue?", "Server Running!", MessageBoxButtons.OKCancel);
                if (answer == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                stop();
            }
        }

        private void console_MultilineChanged(object sender, EventArgs e)
        {

        }



    }
}
