using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM31
{
    public class ChatView : ServerView
    {
        public ChatView(ASM asm) : base(asm)
        {
            type = ServerType.Chat;
            state = State.Stopped;
            Text = type.ToString() + " - " + state.ToString();

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatView));
            this.SuspendLayout();
            // 
            // ChatView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(780, 478);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
