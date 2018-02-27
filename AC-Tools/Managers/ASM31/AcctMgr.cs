using ASM31.Model;
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
    public partial class AcctMgr : Form
    {
        private List<Account> accounts;
        private long TOLL_MAX = Properties.Settings.Default.maxToll;
        private Account current, backup;
        private int currentIdx = -1;
        public AcctMgr()
        {
            InitializeComponent();
            setupCBoxes();

            setupEditMap();
            refreshList();

        }
        private void setupCBoxes()
        {
            cbxAccessLvl.DataSource = Enum.GetNames(typeof(AccountType));
            cbxMembership.DataSource = Enum.GetNames(typeof(Membership));
            cbxPMembership.DataSource = Enum.GetNames(typeof(Membership));
            cbxAccessLvl.BackColor = Color.FromKnownColor(KnownColor.Window);
            cbxMembership.BackColor = Color.FromKnownColor(KnownColor.Window);
            cbxPMembership.BackColor = Color.FromKnownColor(KnownColor.Window);
        }

        Dictionary<Control, int> fieldMap = new Dictionary<Control, int>();

        private void setupEditMap()
        {
            fieldMap.Add(txtId, 1);
            fieldMap.Add(txtAcctName,2);
            fieldMap.Add(txtPWHash,3);
            fieldMap.Add(txtEmail,4);
            fieldMap.Add(chkActivated,5);
            fieldMap.Add(cbxAccessLvl,6);
            fieldMap.Add(cbxMembership,7);
            fieldMap.Add(cbxPMembership,8);
            fieldMap.Add(dpExpiry,9);
            fieldMap.Add(txtToll,10);
            fieldMap.Add(txtBalance,11);
            fieldMap.Add(txtLastIP,12);
            fieldMap.Add(txtLastMac,13);
            fieldMap.Add(txtLastSvr,14);
            fieldMap.Add(txtQues,15);
            fieldMap.Add(txtAns, 16);
            fieldMap.Add(txtTollEdit, 17);
        }

        private void acctList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (acctList.SelectedItem == null)
                return;
            Account acct = (Account)acctList.SelectedItem;
            if (acct.Equals(current))
                return;                     // reselected current
            
            if (acctEdited())
            {
                DialogResult result = MessageBox.Show("Changes will be lost. Continue?", "Confirm", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    // put selection back to where it was
                    if (currentIdx != -1)
                        acctList.SelectedIndex = currentIdx;
                    return;
                }
            }
            currentIdx = acctList.SelectedIndex;
            current = acct;
            backup = acct.clone();
            populateWith(current);
        }

        private void populateWith(Account acct)
        {
            txtId.Text = acct.id.ToString();
            txtAcctName.Text = acct.name;
            txtPWHash.Text = acct.password;
            txtEmail.Text = acct.email;
            chkActivated.Checked = (acct.activated == 1);
            if (chkActivated.Checked)
                chkActivated.Text = "Active";
            else
                chkActivated.Text = "Inactive";

            cbxAccessLvl.SelectedIndex = (int)acct.access_level;
            cbxMembership.SelectedIndex = (int)acct.membership;
            cbxPMembership.SelectedIndex = (int)acct.old_membership;
            dpExpiry.Value = acct.expire;
            
            if (acct.toll > TOLL_MAX)
                acct.toll = TOLL_MAX;

            txtToll.Text = acct.toll.ToString();
            txtBalance.Text = Convert.ToString(acct.balance);
            txtLastIP.Text = acct.last_ip;
            txtLastMac.Text = acct.last_mac;
            txtLastSvr.Text = acct.last_server.ToString();
            txtQues.Text = acct.question;
            txtAns.Text = acct.answer;
            txtTollEdit.Text = "";
            resetColors();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            doTollUpdate();
        }

        private void doTollUpdate()
        {

            if (txtTollEdit.BackColor != Color.Yellow)
                return;                         // nothing to do. ignore
            long toll = current.toll;

            string sVal = txtTollEdit.Text;

            if (sVal != null && sVal.Length > 0)
            {
                int op = 0;
                if (sVal.Contains("+"))
                {
                    op = 1;
                    sVal = sVal.Replace("+", string.Empty);
                }
                else if (sVal.Contains("-"))
                {
                    op = 2;
                    sVal = sVal.Replace("-", string.Empty);
                }
                long incr = int.Parse(sVal);
                switch (op)
                {
                    case 0: // replace Toll value
                        txtToll.Text = sVal;
                        toll = (long)int.Parse(sVal);
                        break;
                    case 1: // add to toll
                        if (toll + incr > 1000000)      // cap at one million toll
                            toll = 1000000;
                        else
                            toll += incr;
                        break;
                    case 2: // subtract
                        if (toll <= incr)      // don't underflow
                            toll = 0;
                        else
                            toll -= incr;
                        break;
                }
                txtToll.Text = toll.ToString();
                txtToll.BackColor = Color.Yellow;
                txtTollEdit.Text = "";
                txtTollEdit.BackColor = Color.FromKnownColor(KnownColor.Window);
                
            }
        }


        private void txtTollEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == '+' || ch == '-' && sender.Equals(txtTollEdit))   // sign handling only for tollEditor
            {
                TextBox tb = (TextBox)sender;
                string s = tb.Text;
                string sch = new string(ch,1);
                // remove old sign
                if (s.Contains("+"))
                    s = s.Replace("+", string.Empty);
                else if (s.Contains("-"))
                    s = s.Replace("-", string.Empty);
                // insert new one at start of string
                s = s.Insert(0,sch);
                tb.Text = s;            // update the textbox
                e.Handled = true;       // we handled it.
            }
            else if (char.IsDigit(ch))   // key OK
                e.Handled = false;
            else if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Delete)
                e.Handled = false;
            else if (e.KeyChar == (char)Keys.Down || e.KeyChar == (char)Keys.Up || e.KeyChar == (char)Keys.Left || e.KeyChar == (char)Keys.Right)
                e.Handled = false;
            else
                e.Handled = true;       // we handled it so tell system  to ignore
        }

        private void general_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                Control ctrl = (Control)sender;
                switch(fieldMap[(Control)sender])
                {
                    case 2:
                        if (ctrl.BackColor != Color.Yellow) ctrl.BackColor = Color.Yellow;
                        break;
                    case 3:
                        if (ctrl.BackColor != Color.Yellow) ctrl.BackColor = Color.Yellow;
                        break;
                    case 4:
                        if (ctrl.BackColor != Color.Yellow) ctrl.BackColor = Color.Yellow;
                        break;
                    case 9:
                        if (ctrl.BackColor != Color.Yellow) ctrl.BackColor = Color.Yellow;
                        break;
                    case 10:
                        if (ctrl.BackColor != Color.Yellow) ctrl.BackColor = Color.Yellow;
                        break;
                    case 15:
                        if (ctrl.BackColor != Color.Yellow) ctrl.BackColor = Color.Yellow;
                        break;
                    case 16:
                        if (ctrl.BackColor != Color.Yellow) ctrl.BackColor = Color.Yellow;
                        break;
                    case 17:
                        if (ctrl.BackColor != Color.Yellow) ctrl.BackColor = Color.Yellow;
                        break;
                }
                
            }   
        }

        private void resetColors()
        {
            Control.ControlCollection controls = this.Controls;
            foreach (Control control in controls)
            {
                if (control is TextBox || control is ComboBox)
                    control.BackColor = Color.FromKnownColor(KnownColor.Window);
                else if (control is CheckBox)
                    control.BackColor = Color.FromKnownColor(KnownColor.Window);
                else if (control is Label)
                    control.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
            txtHasExpiry.Text = "";
         
        }

        private bool acctEdited()
        {
            Control.ControlCollection controls = this.Controls;
            foreach (Control control in controls)
            {
                if (control.BackColor == Color.Yellow)
                    return true;
            }
            return false;
        }


        private void general_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
                ((ComboBox)sender).BackColor = Color.Yellow;
        }

        private void undo()
        {
            backup.copyTo(current);         // wipe changes
            populateWith(current);          // redisplay virgin account
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (acctEdited())
            {
                doUpdate();
                resetColors();
                refreshList();
            }
        }

        private void doUpdate()
        {
            Account acct = current;
            if (txtId.BackColor == Color.Yellow)
                acct.id = int.Parse(txtId.Text);

            if (txtAcctName.BackColor == Color.Yellow)
                acct.name = txtAcctName.Text;
            if (txtPWHash.BackColor == Color.Yellow)
                acct.password = txtPWHash.Text;
            
            acct.email = txtEmail.Text;
            acct.activated = (byte)(chkActivated.Checked ? 1 : 0);;
            acct.access_level =  (AccountType) cbxAccessLvl.SelectedIndex;
            acct.membership = (Membership)cbxMembership.SelectedIndex;
            acct.old_membership = (Membership) cbxPMembership.SelectedIndex;
            acct.expire = dpExpiry.Value;
            if (acct.expire.Year == 2050)
                acct.expire = DateTime.MinValue;
            acct.toll = int.Parse(txtToll.Text);
            txtQues.Text = acct.question;
            acct.answer = txtAns.Text;
            acct.question = txtQues.Text;

            if (ASMDB.getInstance().updateAccount(acct))
            {
                resetColors();
                acctList.Items.Remove(current);
                acctList.Items.Add(current);
                acctList.SelectedItem = current;
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (acctEdited())
            {
                DialogResult result = MessageBox.Show("Changes will be lost. Continue?","Confirm",MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                    undo();
            }
        }

        private void chkActivated_CheckedChanged(object sender, EventArgs e)
        {
            chkActivated.BackColor = Color.Yellow;
            if (chkActivated.Checked)
                chkActivated.Text = "Active";
            else
                chkActivated.Text = "Inactive";
        }

        private void btnPwdChange_Click(object sender, EventArgs e)
        {
            PasswordDlg dlg = new PasswordDlg();
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                // generate a new password hash
                string pwd = dlg.password;
                string pwdhash = Utils.encodePassword(pwd);
                txtPWHash.Text = pwdhash;
                txtPWHash.BackColor = Color.Yellow;
            }
        }
        private void refreshList()
        {
            accounts = ASMDB.getInstance().getAccounts();
            acctList.Items.Clear();
            foreach (Account acct in accounts)
                acctList.Items.Add(acct);
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshList();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (acctEdited())
            {
                DialogResult result = MessageBox.Show("Changes will be lost. Continue?","Confirm",MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                    return;
            }
            Dispose();
        }

        private void btnBAN_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Official BAN function is not yet available\nClearing the 'Activated' checkbox effectively bans a player\nby making it impossible to log in from anywhere.\nTo un-ban at a later time, simply check 'Activated' again.\n\nDo you want to do this now?", "Soft BAN.", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                chkActivated.Checked = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtHasExpiry.BackColor = Color.Yellow;
            txtHasExpiry.Text = "Updated!";
        }

        private void btnDateClear_Click(object sender, EventArgs e)
        {
            dpExpiry.Value = DateTimePicker.MinimumDateTime;
        }





    }
}
