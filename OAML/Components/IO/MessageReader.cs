using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Components.IO
{
    public class MessageReader : MemoryStream
    {
        public MessageReader(byte[] buffer) : base(buffer)
        {

        }

        public byte[] ReadBytes(int len)
        {
            byte[] buffer = new byte[len];
            this.Read(buffer, 0, len);

            return buffer;
        }

        public string ReadString(int len)
        {
            byte[] buffer = new byte[len];
            this.Read(buffer, 0, len);

            return System.Text.Encoding.UTF8.GetString(buffer);
        }

        public int ReadInt32()
        {
            byte[] buffer = new byte[4];
            this.Read(buffer, 0, 4);

            if (BitConverter.IsLittleEndian)
              Array.Reverse(buffer);

            return BitConverter.ToInt32(buffer, 0);
        }

        public float ReadFloat()
        {
            byte[] buffer = new byte[4];
            this.Read(buffer, 0, 4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            return BitConverter.ToSingle(buffer, 0);
        }

        public bool ReadBoolean()
        {
            return this.ReadByte() > 0;
        }
    }
}
