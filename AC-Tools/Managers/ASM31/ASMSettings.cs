using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM31
{
    public partial class ASMSettings : Form
    {
        public ASMSettings()
        {
            InitializeComponent();
            loadSettings();
        }

        private void loadSettings()
        {
            DataGridViewRowCollection rows = settingsTable.Rows;
            rows.Clear();
            SettingsPropertyValueCollection settings = Properties.Settings.Default.PropertyValues;

            foreach(SettingsPropertyValue setting in settings)
            {
                string name = setting.Name;
                object value = setting.PropertyValue;
                string typeName = value.GetType().Name;
                object[] row = new object[] { name, typeName, value };
                settingsTable.Rows.Add(row);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveSettings();
        }

        private void saveSettings()
        {
            bool dirty = false;
            DataGridViewRowCollection rows = settingsTable.Rows;
            SettingsPropertyValueCollection settings = Properties.Settings.Default.PropertyValues;
            foreach (DataGridViewRow row in rows)
            {
                String name = (String)row.Cells[0].Value;
                try
                {
                    SettingsPropertyValue setting = settings[name];
                    Object oValue = setting.PropertyValue;
                    Object nValue = row.Cells[2].Value;
                    if (oValue is Int32)
                    {
                        nValue = Int32.Parse((string)nValue);
                    }

                    if (oValue.Equals(nValue))
                        continue;
                    setting.PropertyValue = nValue;     // Update the setting
                    setting.IsDirty = true;             // flag as neerds saving
                    dirty = true;                       // we will trigger a save
                }
                catch (Exception)
                {
                    // not found or numeric data conversion exception - not saved.
                }
            }
            if(dirty)
            {
                Properties.Settings.Default.Save();
                dirty = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
