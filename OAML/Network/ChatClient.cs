using OAML.Components.IO;
using OAML.Components.IO.Types;
using OAML.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Network
{
    public class ChatClient
    {

        private IPAddress _host;

        private int _port;

        private TcpClient _client;

        public Action<IPAddress, int, string> SendMessageEvent { get; set; }

        public Action<string> ResponseEvent { get; set; }

        public ChatClient(string ip, int port)
        {
            _port = port;

            if (IPAddress.TryParse(ip, out _host))
            {

            }
        }

        public ServiceResult<SocketError> Start()
        {
            var result = new ServiceResult<SocketError>();

            try
            {
                _client = new TcpClient(_host.ToString(), _port);
            }
            catch(SocketException ex)
            {
                result.SetResult(ex.SocketErrorCode);

                if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    result.AddError("SocketError", "Connection Refused");
                else if (ex.SocketErrorCode == SocketError.HostUnreachable)
                    result.AddError("SocketError", "Host Unreachable");
                else
                    result.AddError("SocketError", ex.SocketErrorCode.ToString());
            }

            return result;
        }

        public void Stop()
        {
            if(_client != null)
                _client.Close();
        }

        public ServiceResult<SocketError> SendMessage(MessageBuilder msg)
        {
            var result = Start();

            if (_client != null)
            {
                //---create a TCPClient object at the IP and port no.---
                NetworkStream nwStream = _client.GetStream();

                string displayMessage = string.Empty;
                switch(msg._type)
                {
                    case MessageType.Message:
                        displayMessage = Encoding.UTF8.GetString(msg._data);
                        break;
                    case MessageType.Image:
                        displayMessage = "*Sent image*";
                        break;
                    case MessageType.Binary:
                        displayMessage = "*Sent file*";
                        break;
                }

                byte[] bytesToSend = msg.Build();

                //---send the text---
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                //this still needs to be implemented, just have to figure out how to get the raw data, and pass it back if it's been encrypted..
                if (SendMessageEvent != null)
                    SendMessageEvent(_host, _port, displayMessage);

                //---read back the text---
                byte[] bytesToRead = new byte[_client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, _client.ReceiveBufferSize);
            }

            return result;
        }

        public ServiceResult<SocketError> SendMessage(byte[] msg)
        {
            var result = Start();

            if (_client != null)
            {
                //---create a TCPClient object at the IP and port no.---
                NetworkStream nwStream = _client.GetStream();
                byte[] bytesToSend = msg;

                //---send the text---
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                //this still needs to be implemented, just have to figure out how to get the raw data, and pass it back if it's been encrypted..
                //if (SendMessageEvent != null)
                    //SendMessageEvent(_host, _port, msg);

                //---read back the text---
                byte[] bytesToRead = new byte[_client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, _client.ReceiveBufferSize);
            }

            return result;
        }

        public ServiceResult<SocketError> SendMessage(string msg)
        {
            var result = Start();

            if (_client != null)
            {
                //---create a TCPClient object at the IP and port no.---
                NetworkStream nwStream = _client.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(msg);

                //---send the text---
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                if (SendMessageEvent != null)
                    SendMessageEvent(_host, _port, msg);

                //---read back the text---
                byte[] bytesToRead = new byte[_client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, _client.ReceiveBufferSize);
            }

            return result;
        }

    }
}
