using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM31
{
    public class Toon
    {
        public string name { get; set; }
        public bool isOnline { get; set; }
        public string race { get; set; }

        public Toon(string name, string race, bool online)
        {
            this.name = name;
            this.race = race;
            this.isOnline = online;
        }
    }
}
