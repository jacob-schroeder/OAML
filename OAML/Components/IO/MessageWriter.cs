using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Components.IO
{
    public class MessageWriter : StreamWriter
    {
        public MessageWriter(Stream Input) : base(Input)
        {

        }

        public void WriteInt32(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            this.BaseStream.Write(bytes, 0, 4);
        }

        public void WriteFloat(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            this.BaseStream.Write(bytes, 0, 4);
        }

        public void WriteString(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            this.BaseStream.Write(bytes, 0, value.Length);
        }

        public void WriteBoolean(bool value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.BaseStream.Write(bytes, 0, 1);
        }

        public void WriteBytes(byte[] value)
        {
            this.BaseStream.Write(value, 0, value.Length);
        }
    }
}
