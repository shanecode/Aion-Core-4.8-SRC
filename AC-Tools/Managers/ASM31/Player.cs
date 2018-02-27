using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM31
{
    public class Player
    {
        public string name { get; set; }
        public int access { get; set; }
        public int membership { get; set; }

        public Player(string name,int access,int membership)
        {
            this.name = name;
            this.access = access;
            this.membership = membership;
        }
    }
}
