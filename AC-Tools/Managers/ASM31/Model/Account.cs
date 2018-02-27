using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM31.Model
{
    public class Account
    {
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public byte activated { get; set; }
        public AccountType access_level { get; set; }
        public Membership membership { get; set; }
        public Membership old_membership { get; set; }
        public byte last_server { get; set; }
        public string last_ip { get; set; }
        public string last_mac { get; set; }
        public string ip_force { get; set; }
        public DateTime expire { get; set; }
        public long toll { get; set; }
        public double balance { get; set; }
        public string email { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public override string ToString()
        {
            return name;
        }
        public override bool Equals(object obj)
        {
            if (obj is Account)
                return ((Account)obj).name.Equals(name);
            return base.Equals(obj);
        }
        public Account clone()
        {
            //deep copy this account object into a new one
            return copyTo(new Account());
        }

        public Account copyTo(Account nacct)
        {
            nacct.id = id;
            nacct.name = name;
            nacct.password = password;
            nacct.activated = activated;
            nacct.access_level = access_level;
            nacct.membership = membership;
            nacct.old_membership = old_membership;
            nacct.last_server = last_server;
            nacct.last_ip = last_ip;
            nacct.last_mac = last_mac;
            nacct.ip_force = ip_force;
            nacct.expire = expire;
            nacct.toll = toll;
            nacct.balance = balance;
            nacct.email = email;
            nacct.question = question;
            nacct.answer = answer;
            return nacct;
        }

        public bool hasExpiryDate()
        {
            return (expire.Year != 2050);
        }
    }
}
