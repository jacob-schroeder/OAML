using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace OAML.Network
{
    public class ChatServer
    {
        private IPAddress _host;

        private int _ServerPort;

        private TcpListener _server;

        public TcpClient _client { get; private set; }

        public bool Configured { get; set; }
        public bool isOn { get; set; }

        private Action<Socket, byte[]> Callback = null;

        public Action<Socket> ConnectedCallback = null;

        public ChatServer(int ServerPort)
        {
            _ServerPort = ServerPort;
                
            Configured = true;
            _server = new TcpListener(IPAddress.Any, _ServerPort);
        }

        public void UpdatePort(int port)
        {
            isOn = false;
            _server.Stop();

            _ServerPort = port;

            _server = new TcpListener(IPAddress.Any, _ServerPort);
        }

        public ChatServer(string ip, int ServerPort)
        {
            _ServerPort = ServerPort;
           // _ClientPort = ClientPort;

            if(IPAddress.TryParse(ip, out _host))
            {
                Configured = true;
                _server = new TcpListener(_host, _ServerPort);
            }
            else
            {
                Configured = false;
            }
        }

        public void SetCallback(Action<Socket, byte[]> action)
        {
            Callback = action;
        }

        public void StartListener()
        {
            _server.Start();
            isOn = true;

            while(isOn)
            {
                //---incoming client connected---
                _client = _server.AcceptTcpClient();

                if (_client.Connected)
                {
                    if (ConnectedCallback != null)
                        ConnectedCallback(_client.Client);

                    //---get the incoming data through a network stream---
                    NetworkStream nwStream = _client.GetStream();
                    byte[] buffer = new byte[_client.ReceiveBufferSize];

                    //---read incoming stream---
                    int bytesRead = nwStream.Read(buffer, 0, _client.ReceiveBufferSize);

                    if (bytesRead > 0)
                    {
                        //---convert the data received into a string---
                        //string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Callback(_client.Client, buffer);

                        //---write back the text to the client---
                        //nwStream.Write(buffer, 0, bytesRead);
                    }
                }

                _client.Close();
            }
        }

        public void StopListener()
        {
            _server.Stop();
            isOn = false;
        }
    }
}
