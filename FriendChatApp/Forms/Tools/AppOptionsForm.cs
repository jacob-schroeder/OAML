using OAML.Components.Configuration;
using OAML.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriendChatApp.Forms.Tools
{
    public partial class AppOptionsForm : Form
    {
        public Options config = null;

        public AppOptionsForm()
        {
            InitializeComponent();
        }

        public AppOptionsForm(Options existingConfig, ICollection<string> EncryptionMethods, ICollection<string> SignatureAlgorithms)
        {
            InitializeComponent();

            config = existingConfig ?? new Options();

            if (EncryptionMethods?.Any() ?? false)
            {
                dataKeyMethod.Items.Clear();
                EncryptionMethods.ToList().ForEach(k => dataKeyMethod.Items.Add(k));
                dataKeyMethod.SelectedIndex = 0;
            }

            if(SignatureAlgorithms?.Any() ?? false)
            {
                signKeyMethod.Items.Clear();
                SignatureAlgorithms.ToList().ForEach(k => signKeyMethod.Items.Add(k));
                signKeyMethod.SelectedIndex = 0;
            }
        }

        private void AppOptionsForm_Load(object sender, EventArgs e)
        {
            textBox2.Text = config?.Port.ToString();

            dataKeyPath.Text = config?.DataKeyPath;
            dataKeyMethod.Text = config?.EncryptionMethod; //selected option maybe??

            signKeyPath.Text = config?.SignKeyPath;
            signKeyMethod.Text = config?.SignatureAlgo; //selected option maybe??
        }

        private void SaveFormBtn_Click(object sender, EventArgs e)
        {
            var result = SaveConfig(config);
            if (result.Success)
            {

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                result.Errors.ToList().ForEach(r => MessageBox.Show(r.Value));
            }
        }

        private void CancelFormBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private ServiceResult<int> SaveConfig(Options options)
        {
            var result = new ServiceResult<int>();

            //validate config
            if(Int32.TryParse(textBox2.Text, out int port))
            {
                if (port < 0 || port > 255)
                    result.Errors.Add(new KeyValuePair<string, string>("Port", "Port out of range. Choose between 0 and 255"));
                else
                    options.Port = port;
            }
            else
                result.Errors.Add(new KeyValuePair<string, string>("Port", "Could not convert port to integer"));

            options.DataKeyPath = dataKeyPath.Text;
            options.EncryptionMethod = dataKeyMethod.Text;

            options.SignKeyPath = signKeyPath.Text;
            options.SignatureAlgo = signKeyMethod.Text;

            return result;
        }

        private void userDataKeyBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;

                dataKeyPath.Text = selectedFileName;
            }
        }

        private void userSignKeyBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;

                signKeyPath.Text = selectedFileName;
            }
        }
    }
}
