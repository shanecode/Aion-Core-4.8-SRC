using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM31
{
    public class GameView : ServerView
    {
        public GameView(ASM asm) : base(asm)
        {
            type = ServerType.Game;
            state = State.Stopped;
            Text = type.ToString() + " - " + state.ToString();

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameView));
            this.SuspendLayout();
            // 
            // GameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(780, 478);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GameView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
