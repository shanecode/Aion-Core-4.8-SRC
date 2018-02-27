using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM31
{
    /*
     *  Properties
     *  a class that provides similar cuntion to Java properties.
     *  gives read compatibility only
     */
    public class JProperties
    {
        private Dictionary<string, string> props = new Dictionary<string, string>();

        public JProperties(string propsFile)
        {
            load(propsFile);
        }

        public void load(string propsFile)
        {
            try
            {
                string[] lines = File.ReadAllLines(propsFile);
                foreach (string line in lines)
                {
                    if (line.Length == 0)
                        continue;                   //
                    if (line.StartsWith("#"))
                        continue;                  // comment
                    string[] bits = line.Split(new char[] { '=' });
                    if (bits.Length <= 1)
                        continue;
                    props.Add(bits[0].Trim(), bits[1].Trim());
                }
            }
            catch (IOException)
            {
                // do nothing with it. just allow to return
            }
        }

        public string getValue(string key)
        {
            try
            {
                return props[key];
            }
            catch (Exception)
            {
                return null;                //   returns null if not found
            }
        }

        public bool isEmpty()
        {
            return (props.Count == 0);
        }

        public void clear()
        {
            props.Clear();          // relkease memory held by this
        }
    }
}
