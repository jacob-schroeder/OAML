using OAML.Components.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriendChatApp.Forms.Friends
{
    public partial class FriendConfigForm : Form
    {
        public UserNode Friend { get; private set; }
        public FriendConfigForm(UserNode friend, ICollection<string> EncryptionMethods, ICollection<string> SignatureAlgorithms)
        {
            Friend = friend;

            InitializeComponent();

            if(EncryptionMethods?.Any() ?? false)
            {
                dataKeyMethod.Items.Clear();
                EncryptionMethods.ToList().ForEach(k => dataKeyMethod.Items.Add(k));
                dataKeyMethod.SelectedIndex = 0;
            }

            if (SignatureAlgorithms?.Any() ?? false)
            {
                signKeyMethod.Items.Clear();
                SignatureAlgorithms.ToList().ForEach(k => signKeyMethod.Items.Add(k));
                signKeyMethod.SelectedIndex = 0;
            }
        }

        private void FriendConfigForm_Load(object sender, EventArgs e)
        {
            HostInput.Text = Friend.IPAddress;
            PortInput.Text = Friend.Port.ToString();
            DisplayNameInput.Text = Friend.DisplayName;

            dataKeyPath.Text = Friend.DataKeyPath;
            Friend.DataKeyPath = dataKeyPath.Text;

            signKeyPath.Text = Friend.SignKeyPath;
            Friend.SignKeyPath = signKeyPath.Text;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Friend.DisplayName = DisplayNameInput.Text;
            Friend.IPAddress = HostInput.Text;

            Friend.EncryptionMethod = dataKeyMethod.Text;
            Friend.DataKeyPath = dataKeyPath.Text;

            Friend.SignatureAlgo = signKeyMethod.Text;
            Friend.SignKeyPath = signKeyPath.Text;

            if (int.TryParse(PortInput.Text, out int port))
                Friend.Port = port;

            //var result = SaveConfig(config);
            //if (result.Result == null)
            //{

                this.DialogResult = DialogResult.OK;
                this.Close();
            //}
            //else
            //{
            //    result.Result.ForEach(r => MessageBox.Show(r.Value));
            //}
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
