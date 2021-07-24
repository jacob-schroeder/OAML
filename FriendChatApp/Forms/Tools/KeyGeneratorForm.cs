using OAML.Components.Security;
using OAML.Components.Security.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriendChatApp.Forms.Tools
{
    public partial class KeyGeneratorForm : Form
    {
        Dictionary<string, IEncryptionManager> _managers;
        IReadOnlyDictionary<string, ISignatureManager> _signManagers;

        //public KeyPair<string> Keys;
        public KeyPair Keys;

        public KeyGeneratorForm(Dictionary<string, IEncryptionManager> managers, Dictionary<string, ISignatureManager> signManagers)
        {
            InitializeComponent();

            _managers = managers;
            _signManagers = signManagers;

            if (_managers?.Keys.Any() ?? false)
            {
                dataKeyTypes.Items.Clear();
                managers.Keys.ToList().ForEach(k => dataKeyTypes.Items.Add(k));
                dataKeyTypes.SelectedIndex = 0;
            }

            if(_signManagers?.Keys.Any() ?? false)
            {
                signKeyTypes.Items.Clear();
                signManagers.Keys.ToList().ForEach(k => signKeyTypes.Items.Add(k));
                signKeyTypes.SelectedIndex = 0;
            }
        }

        private void generateDataKeysBtn_Click(object sender, EventArgs e)
        {
            string key = dataKeyTypes.SelectedItem as string;

            if (_managers.ContainsKey(key))
            {
                var manager = _managers[key];

                Keys = manager.GenerateKeys();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void generateSignKeysBtn_Click(object sender, EventArgs e)
        {
            string key = signKeyTypes.SelectedItem as string;

            if (_signManagers.ContainsKey(key))
            {
                var manager = _signManagers[key];

                Keys = manager.GenerateKeys();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
