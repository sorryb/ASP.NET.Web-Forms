using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Configuration;

namespace BugsTrackersScreenCapture
{
    public partial class ConfigForm : Form
    {
        private class Item
        {
            public string Name;
            public int Value;
            public Item(string name, int value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }


        public ConfigForm()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            InitializeComponent();

            textBoxUrl.Text = Program.url;
            textBoxUsername.Text = Program.username;
            textBoxPassword.Text = Program.password;
            //textBoxDomain.Text = Program.domain;
            checkBoxSavePassword.Checked = (Program.save_password == "1");
            textBoxProjectNumber.Text = Convert.ToString(Program.project_id);

            BugTrackerWebService.BugTrackerWebService trackerWService = new BugTrackerWebService.BugTrackerWebService();
         
            BugTrackerWebService.ProjectInfoBase[] objArray = trackerWService.GetProjectByMemberUsername(Program.username);

            foreach (BugTrackerWebService.ProjectInfoBase obj in objArray)
            {
                comboBoxProjects.Items.Add(new Item(obj.Name,Convert.ToInt32(obj.Key)));
            }

        }

        public static void WriteConfig()
        {

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Clear();

            config.AppSettings.Settings.Add("main_window_width", Convert.ToString(Program.main_window_width));
            config.AppSettings.Settings.Add("main_window_height", Convert.ToString(Program.main_window_height));
            config.AppSettings.Settings.Add("url", Program.url);
            config.AppSettings.Settings.Add("username", Program.username);
            config.AppSettings.Settings.Add("domain", Program.domain);
            config.AppSettings.Settings.Add("project_id", Convert.ToString(Program.project_id));
            //config.AppSettings.Settings.Add("project_id", Convert.ToString(Program.url));

            if (Program.save_password == "1")
            {
                config.AppSettings.Settings.Add("password", Program.password);
                config.AppSettings.Settings.Add("save_password", "1");
            }
            else
            {
                config.AppSettings.Settings.Add("password", "");
                config.AppSettings.Settings.Add("save_password", "0");
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            Properties.Settings.Default.ScreenCapture_BugTrackerWebService_BugTrackerWebService = Program.url;
            Properties.Settings.Default.Save();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

            Program.url = textBoxUrl.Text;
            Program.username = textBoxUsername.Text;
            Program.password = textBoxPassword.Text;
           // Program.domain = textBoxDomain.Text;
            Program.save_password = checkBoxSavePassword.Checked ? "0" : "1";
            try
            {
                Program.project_id = Convert.ToInt32(textBoxProjectNumber.Text);
            }
            catch (Exception)
            {
                Program.project_id = 0;
            }

            ConfigForm.WriteConfig();

            this.Close();
        }

        private void comboBoxProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxProjectNumber.Text = ((ConfigForm.Item)(((System.Windows.Forms.ComboBox)(sender)).SelectedItem)).Value.ToString();
        }
    }
}
