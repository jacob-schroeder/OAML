using OAML.Components.IO.Types;
using OAML.Components.Security;
using OAML.Exceptions.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Components.IO
{
    public class MessageBuilder : Message
    {

        //Usability
        public IEncryptionManager _manager { get; private set; }
        public string _key { get; private set; }

        public ISignatureManager _hashManager { get; private set; }
        public byte[] _hashKey { get; private set; }

        public bool SignatureValid { get; private set; }

        public MessageBuilder() : base()
        {

        }

        public MessageBuilder(string message)
        {
            _data = Encoding.UTF8.GetBytes(message);
        }

        public MessageBuilder ReadMessage()
        {
            try
            {
                if (_manager != null)
                {
                    //if(_type == MessageType.Message)
                    _data = _manager.Decrypt(_key, _data); //Exception Handling here...
                }

                if (_hashManager != null)
                {
                    SignatureValid = _hashManager.Verify(_hashKey, _data, _signature); //Exception Handling here..
                }

            }
            catch (CryptographicException ex) //do something
            {

            }

            return this;
        }

        public MessageBuilder SetMessageType(MessageType type)
        {
            _type = type;

            return this;
        }

        public MessageBuilder FromBuffer(byte[] data)
        {
            int len = data.Length;

            using (MessageReader reader = new MessageReader(data))
            {
                _magic = reader.ReadString(4);
                _version = reader.ReadFloat();

                if (_version > Metadata.Version)
                    throw new OAMLVersionException("Message version greater than OAML application version.", _version);

                _type = (MessageType)reader.ReadInt32();
                _encrypted = reader.ReadBoolean();

                _data = reader.ReadBytes(reader.ReadInt32());

                _signature = reader.ReadBytes(reader.ReadInt32());
            }

            return this;
        }

        public MessageBuilder UseEncryption(IEncryptionManager manager, string key)
        {
            _manager = manager;
            _key = key;

            return this;
        }

        public MessageBuilder UseSignatureManager(ISignatureManager hashManager, byte[] key)
        {
            _hashManager = hashManager;
            _hashKey = key;

            return this;
        }

        public byte[] Build()
        {
            using (MemoryStream stream = new MemoryStream())
            { 
                using (MessageWriter sb = new MessageWriter(stream))
                {
                    _encrypted = _manager != null;
                    _signed = _hashManager != null;

                    //header
                    sb.WriteString(Metadata.Magic);
                    sb.WriteFloat(Metadata.Version);
                    sb.WriteInt32((int)_type);
                    sb.WriteBoolean(_encrypted);

                    if (_signed)
                        _signature = _hashManager.Sign(_hashKey, _data);

                    //data
                    if(_encrypted)
                        _data = _manager.Encrypt(_key, _data);

                    sb.WriteInt32(_length);
                    sb.WriteBytes(_data);

                    //signature
                    if (_signature != null)
                    {
                        sb.WriteInt32(_siglength);
                        sb.WriteBytes(_signature);
                    }
                }

                stream.Flush();
                
                return stream.ToArray();
            }
        }

        public override string ToString()
        {
            return Encoding.UTF8.GetString(_data);
        }

    }
}
