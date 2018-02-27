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
    public partial class ASMStats : Form
    {
        public GameView gv;

        public ASMStats(GameView gv)
        {
            InitializeComponent();
            this.gv  = gv;
            refresh();
        }

        private void refresh()
        {
            string version = Globals.instance.getGameVersion();
            if (version != null && !version.Equals(""))
                txtVersion.Text = version;
            else return;
            if (gv != null)
            {
                if (gv.engine != null)
                {
                    TimeSpan ts = gv.engine.timeRunning();
                    string timeRun  = String.Format("{0} Days, {1} Hrs, {2} Mins, {3} Secs", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
                    txtUptime.Text = timeRun;
                }
            }
            else return;
            // check db availabil8ity. if not available, go no furhter
            if (!NetUtils.isConnected(Globals.instance.getDbHost(), Globals.instance.getDbPort()))
                return;
            ASMDB.getInstance().refreshAll();
            txtFree.Text = ASMDB.getInstance().getPlayerCount(Membership.Free).ToString();
            txtPremium.Text = ASMDB.getInstance().getPlayerCount(Membership.Premium).ToString();
            txtVIP.Text = ASMDB.getInstance().getPlayerCount(Membership.VIP).ToString();

            txtChars.Text = ASMDB.getInstance().getToonCount(Races.All).ToString();
            txtAChars.Text = ASMDB.getInstance().getToonCount(Races.Asmo).ToString();
            txtEChars.Text = ASMDB.getInstance().getToonCount(Races.Elyo).ToString();

            txtOChars.Text = ASMDB.getInstance().getOnlineToonCount(Races.All).ToString();
            txtOAChars.Text = ASMDB.getInstance().getOnlineToonCount(Races.Asmo).ToString();
            txtOEChars.Text = ASMDB.getInstance().getOnlineToonCount(Races.Elyo).ToString();

            toonList.Items.Clear();
            List<Toon> onlineToons = ASMDB.getInstance().getOnlineToons();
            foreach (Toon toon in onlineToons)
                toonList.Items.Add(toon.name);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

    }
}
