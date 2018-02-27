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
    public partial class PasswordDlg : Form
    {
        bool valid = false;

        public string password { get; set; }
        public PasswordDlg()
        {
            InitializeComponent();
            btnOK.Enabled = valid;
        }

        private void txtPwd1_TextChanged(object sender, EventArgs e)
        {
            string s = txtPwd1.Text;
            if (s.Length > 0)
            {
                if (btnp1.BackgroundImage != Properties.Resources.small_check)
                    btnp1.BackgroundImage = Properties.Resources.small_check;
            }
            else
                btnp1.BackgroundImage = Properties.Resources.small_cross;
            btnOK.Enabled = valid;
        }

        private void txtPwd2_TextChanged(object sender, EventArgs e)
        {
            string s1 = txtPwd1.Text;
            string s2 = txtPwd2.Text;
            if (s1.Length > 0)
            {
                if (s1.Equals(s2))
                {
                    if (btnp2.BackgroundImage != Properties.Resources.small_check)
                        btnp2.BackgroundImage = Properties.Resources.small_check;
                    valid = true;
                    password = s1;
                }
                else
                {
                    if (btnp2.BackgroundImage != Properties.Resources.small_cross)
                        btnp2.BackgroundImage = Properties.Resources.small_cross;
                    valid = false;
                }
            }
            else
                btnp2.BackgroundImage = Properties.Resources.small_cross;
            btnOK.Enabled = valid;
        }

    }
}
