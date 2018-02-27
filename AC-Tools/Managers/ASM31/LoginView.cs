using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM31
{
    public class LoginView : ServerView
    {
        public LoginView(ASM asm) : base(asm)
        {
            type = ServerType.Login;
            state = State.Stopped;
            Text = type.ToString() + " - " + state.ToString();

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginView));
            this.SuspendLayout();
            // 
            // LoginView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(780, 478);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
