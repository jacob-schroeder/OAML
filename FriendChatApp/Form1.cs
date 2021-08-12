using FriendChatApp.Components;
using FriendChatApp.Forms.Friends;
using FriendChatApp.Forms.Tools;
using FriendChatApp.Utilities.Extensions;
using OAML.Network;
using OAML.Components.Configuration;
using OAML.Components.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAML.Services;
using OAML.Components.Security;
using OAML.Components.IO;
using OAML.Security.Encryption;
using OAML.Security.Signature;

namespace FriendChatApp
{
    public partial class Form1 : Form
    {
        private readonly string CONFIG_FILE = "config.json";

        private Thread serverCtx;
        //private Thread n_client;
        //private Thread n_send_server;

        ChatServer _srv;
        Dictionary<long, ChatClient> Clients = new Dictionary<long, ChatClient>();

        Dictionary<string, IEncryptionManager> CryptoManagers = new Dictionary<string, IEncryptionManager>
        {
            { "RSA", new RSAManager() }
        };

        Dictionary<string, ISignatureManager> SignatureManagers = new Dictionary<string, ISignatureManager>
        {
            { "ECDSA", new ECDSAManager() }
        };

        List<MessageQueueItem> Queue;  
        UserNode CurrentFriend = null;

        public Form1()
        {
            InitializeComponent();
            InitializeChatComponents();

            TaskResult[] tasks =
            {
                LoadConfiguration(),
                InitializeServer()
            };

            LoadFriends();

            ProcessTasks(tasks);
        }

        #region UIComponents

        private void ProcessTasks(IEnumerable<TaskResult> Results)
        {
            if(Results != null && Results.Any(r => !r.Success))
                Results.Where(r => !r.Success).ForEach(r => ProcessTask(r));
        }

        private void ProcessTask(TaskResult Result)
        {
            if (Result != null && !Result.Success)
                Result.Errors.ForEach(kv => MessageBox.Show(kv.Value));
        }

        private void InitializeChatComponents()
        {
            Queue = new List<MessageQueueItem>();
        }

        private void RunQueue()
        {
            LoadMessages(CurrentFriend?.ID ?? 0);
        }

        private void LoadMessages(long id)
        {
            if (Queue?.Any() ?? false)
            {
                richTextBox1.Text = "";

                var query = Queue.Where(q => q.Friend?.ID == id || q.isSelf);

                foreach (var item in query.OrderBy(q => q.Timestamp))
                {
                    if (item.isSelf && item.Friend == null)
                    {
                        richTextBox1.Invoke(x => x.Text += item.Message + Environment.NewLine);
                    }
                    else if (item.isSelf && item.Friend?.ID == id)
                    {
                        richTextBox1.Invoke(x => x.Text += "Self to {" + item.Friend?.DisplayName + "}: " + item.Message + Environment.NewLine);
                    }
                    else if (item.isSelf == false)
                        richTextBox1.Invoke(x => x.Text += item.Friend.DisplayName + ": " + item.Message + Environment.NewLine);
                }
            }
        }
        #endregion

        #region Options 
        public Options config = new Options
        {
            Port = 220,
            Friends = new List<UserNode>()
        };

        private TaskResult LoadConfiguration()
        {
            TaskResult Result;

            if (!File.Exists(CONFIG_FILE))
            {
                config = new Options
                {
                    Port = 220,
                    Friends = new List<UserNode>()
                };

                Result = SaveConfiguration();
            }
            else
            {
                Result = ReadConfiguration();
            }

            return Result;
        }

        private TaskResult ReadConfiguration()
        {
            var Result = new TaskResult();

            if (string.IsNullOrEmpty(CONFIG_FILE))
                Result.AddError("Configuration", "Configuration File Path Empty");

            if (Result.Success)
            {
                try
                {
                    using (StreamReader r = new StreamReader(CONFIG_FILE))
                    {
                        string json = r.ReadToEnd();
                        config = JsonConvert.DeserializeObject<Options>(json);
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Result.AddError("FileNotFound", ex.Message);
                }
                catch (DirectoryNotFoundException ex)
                {
                    Result.AddError("DirectoryNotFound", ex.Message);
                }
                catch (IOException ex)
                {
                    Result.AddError("IO", ex.Message);
                }
            }

            return Result;
        }

        private TaskResult SaveConfiguration()
        {
            var Result = new TaskResult();

            if (string.IsNullOrEmpty(CONFIG_FILE))
                Result.AddError("Configuration", "Configuration File Path Empty");

            if (Result.Success)
            {
                try
                {
                    using (StreamWriter r = new StreamWriter(CONFIG_FILE, false))
                    {
                        string json = JsonConvert.SerializeObject(config);

                        r.Write(json);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    Result.AddError("UnauthorizedAccess", ex.Message);
                }
                catch (DirectoryNotFoundException ex)
                {
                    Result.AddError("DirectoryNotFound", ex.Message);
                }
                catch (IOException ex)
                {
                    Result.AddError("IO", ex.Message);
                }
            }

            return Result;
        }
        #endregion

        #region Friends
        private void LoadFriends()
        {
            if(config?.Friends?.Any() ?? false)
            {
                foreach(var Friend in config.Friends)
                {
                    userAddressList.Items.Add(new ListViewItem()
                    {
                        Text = Friend.DisplayName,
                        Tag = Friend.ID
                    });
                }
            }
        }

        private long? GetSelectedFriend()
        {
            long? FriendID = null;

            if (userAddressList.SelectedIndices.Count > 0)
            {
                var item = userAddressList.SelectedItems[0];
                FriendID = (long?)item.Tag;
            }

            return FriendID;
        }

        private long? GetSelectedFriend(out ListViewItem item)
        {
            item = null;
            long? FriendID = null;

            if (userAddressList.SelectedIndices.Count > 0)
            {
                item = userAddressList.SelectedItems[0];
                FriendID = (long?)item.Tag;
            }

            return FriendID;
        }

        private void ChangeFriendView(UserNode friend)
        {
            if (friend == null)
            {
                CurrentFriend = null;
                label1.Text = "Current Friend: N/A";
                LoadMessages(0);
            }
            else
            {
                CurrentFriend = friend;
                label1.Text = "Current Friend: " + friend.DisplayName;
                LoadMessages(friend.ID);
            }
        }
        #endregion

        #region Server/Client Callbacks
        private void ProcessConnected(Socket socket)
        {
            var ip = (IPEndPoint)socket.LocalEndPoint;
            var xip = (IPEndPoint)socket.RemoteEndPoint;

            /*
            string msg = string.Format("IP: {0}:{1}\r\nX IP: {2}:{3}", ip.Address.ToString(), ip.Port, xip.Address.ToString(), xip.Port);

            Queue.Add(new MessageQueueItem
            {
                Message = msg,
                Timestamp = DateTime.Now,
                Processed = false,

                isSelf = true
            });
            */
        }

        private void ProcessCommand(Socket socket, byte[] input)
        {
            var ip = (IPEndPoint)socket.LocalEndPoint;
            var xip = (IPEndPoint)socket.RemoteEndPoint;

            UserNode f = null;

            //Find Friend based on IP
            f = config.Friends.FirstOrDefault(r => r.IPAddress == ip.Address.ToString());  // && r.Port == ip.Port);
            if (f == null)
            {
                f = config.Friends.FirstOrDefault(r => r.IPAddress == xip.Address.ToString()); // && r.Port == xip.Port);
            }

            //Message Interpret
            var message = new MessageBuilder().FromBuffer(input);

            var payload = input;
            string path = Environment.CurrentDirectory;
            File.WriteAllBytes(Path.Combine(path, "receive.bin"), payload);



            if (CryptoManagers.ContainsKey(f.EncryptionMethod))
            {
                var manager = CryptoManagers[f.EncryptionMethod];
                string key = File.ReadAllText(config.DataKeyPath); //set this to your private key, not friends

                message = message.UseEncryption(manager, key);
            }

            if (SignatureManagers.ContainsKey(f.SignatureAlgo))
            {
                var manager = SignatureManagers[f.SignatureAlgo];
                byte[] key = File.ReadAllBytes(f.SignKeyPath); //set this to your friends public key

                message = message.UseSignatureManager(manager, key);
            }

            string msg = message.ReadMessage().ToString();
            if (message.SignatureValid)
                msg += "*verified*";

            Queue.Add(new MessageQueueItem
            {
                Friend = f, //subject to change, add in port as well...

                Message = msg,
                Timestamp = DateTime.Now,
                Processed = false
            });
        }

        private void ClientSendMessage(IPAddress host, int port, string msg)
        {
            var friend = config.Friends.FirstOrDefault(f => f.IPAddress == host.ToString()); // && f.Port == port);

            Queue.Add(new MessageQueueItem
            {
                isSelf = true,
                Friend = friend,

                Message = msg,
                Timestamp = DateTime.Now,
                Processed = false
            });
        }

        #endregion

        #region Server/Client Components
        private TaskResult InitializeServer()
        {
            TaskResult Result = new TaskResult();

            if(config == null)
            {
                Result.AddError("App", "This application is not configured");
                return Result;
            }

            _srv = new ChatServer(config.Port);
            _srv.SetCallback(ProcessCommand);
            _srv.ConnectedCallback = ProcessConnected;

            serverCtx = new Thread(new ThreadStart(_srv.StartListener));
            serverCtx.IsBackground = true;
            serverCtx.Start();

            return Result;
        }

        private ChatClient GetChatClient(long ID)
        {
            ChatClient client = null;

            if (Clients.ContainsKey(CurrentFriend.ID))
                client = Clients[CurrentFriend.ID];
            else
            {
                client = new ChatClient(CurrentFriend.IPAddress, CurrentFriend.Port);
                client.SendMessageEvent = ClientSendMessage;
                Clients.Add(CurrentFriend.ID, client);
            }

            return client;
        }

        private void SendMessageBtn_Click(object sender, EventArgs e)
        {
            if (CurrentFriend == null)
                return;

            var client = GetChatClient(CurrentFriend.ID);
            var message = new MessageBuilder(MessageInput.Text)
                                .SetMessageType(OAML.Components.IO.Types.MessageType.Message);

            if (CryptoManagers.ContainsKey(CurrentFriend.EncryptionMethod))
            {
                var manager = CryptoManagers[CurrentFriend.EncryptionMethod]; //Key not found exception...

                string key = Encoding.UTF8.GetString(File.ReadAllBytes(CurrentFriend.DataKeyPath)); //file not found, argument null exception...

                message = message.UseEncryption(manager, key);
            }

            if (CurrentFriend.SignatureAlgo != null && SignatureManagers.ContainsKey(CurrentFriend.SignatureAlgo))
            {
                var hashManager = SignatureManagers[CurrentFriend.SignatureAlgo]; //Key not found exception...

                byte[] key = File.ReadAllBytes(config.SignKeyPath); //file not found, argument null exception...

                message = message.UseSignatureManager(hashManager, key);
            }

            //Send Message
            var result = client.SendMessage(message);

            if (result.Success)
                MessageInput.Text = "";
            else
                ProcessTask(result);
        }

        private void OnPoll_Tick(object sender, EventArgs e)
        {
            RunQueue();
        }

        private void RefreshMsgBtn_Click(object sender, EventArgs e)
        {
            RunQueue();
        }
        #endregion

        #region ToolsForms
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new AppOptionsForm(config, CryptoManagers.Keys, SignatureManagers.Keys))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    config = form.config;

                    SaveConfiguration();

                    _srv.UpdatePort(config.Port);

                    serverCtx.Abort();

                    serverCtx = new Thread(new ThreadStart(_srv.StartListener));
                    serverCtx.IsBackground = true;
                    serverCtx.Start();
                }
            }
        }

        private void keyGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new KeyGeneratorForm(CryptoManagers, SignatureManagers))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var keys = form.Keys;

                    //private
                    SaveFileDialog savefile = new SaveFileDialog();
                    // set a default file name
                    savefile.FileName = "private_key.bin";
                    // set filters - this can be done in properties as well
                    savefile.Filter = "All files (*.*)|*.*";

                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(savefile.FileName, keys.PrivateKey);
                    }

                    //public
                    SaveFileDialog savefile2 = new SaveFileDialog();
                    // set a default file name
                    savefile2.FileName = "public_key.bin";
                    // set filters - this can be done in properties as well
                    savefile2.Filter = "All files (*.*)|*.*";

                    if (savefile2.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(savefile2.FileName, keys.PublicKey);
                    }
                }
            }
        }
        #endregion

        #region FriendForms
        private void userAddressList_SelectedIndexChanged(object sender, EventArgs e)
        {
            long? FriendID = GetSelectedFriend();

            if (FriendID.HasValue)
            {
                var NewFriend = config.Friends.FirstOrDefault(f => f.ID == FriendID);

                if (NewFriend != null && NewFriend != CurrentFriend)
                    ChangeFriendView(NewFriend);
            }
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long? FriendID = GetSelectedFriend();

            if(FriendID.HasValue)
            {
                var Friend = config.Friends.FirstOrDefault(f => f.ID == FriendID);

                using (var form = new FriendConfigForm(Friend, CryptoManagers.Keys, SignatureManagers.Keys))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Friend = form.Friend;

                        //Save Config
                        SaveConfiguration();

                        //Smart do this, but reload clients...
                        Queue.RemoveAll(f => f.Friend.ID == Friend.ID);
                        Clients.Remove(Friend.ID);

                        //Update UI
                        var uiitem = userAddressList.Items.Cast<ListViewItem>().FirstOrDefault(f => (long)f.Tag == Friend.ID);
                        if (uiitem != null)
                            uiitem.Text = Friend.DisplayName;
                    }
                }
            }
        }

        private void removeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long? FriendID = GetSelectedFriend(out ListViewItem item);

            if (FriendID.HasValue)
            {
                userAddressList.Items.Remove(item);

                Queue.RemoveAll(f => f.Friend.ID == FriendID);
                Clients.Remove(FriendID.Value);

                ChangeFriendView(null);
                SaveConfiguration();
            }
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var NewFriend = new UserNode
            {
                ID = config.Friends.Select(f => f.ID).DefaultIfEmpty(0).Max() + 1
            };

            using (var form = new FriendConfigForm(NewFriend, CryptoManagers.Keys, SignatureManagers.Keys))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var Friend = form.Friend;

                    config.Friends.Add(Friend);

                    userAddressList.Items.Add(new ListViewItem { Text = Friend.DisplayName, Tag = Friend.ID });

                    SaveConfiguration();
                }
            }
        }
        #endregion
        
    }
}
