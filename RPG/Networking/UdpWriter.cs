using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MatthiWare.Networking
{
    public class UdpWriter
    {
        private Encoding encoding;
        private UdpClient client;

        private byte[] m_buffer;
        private int m_length, m_position;

        public UdpWriter(UdpClient client)
        {
            this.client = client;
            encoding = Encoding.UTF8;
            m_buffer = new byte[1024];
            m_length = 0;
            m_position = 0;
        }

        public void Flush(IPEndPoint ep = null)
        {
            byte[] dgram = new byte[m_length];
            Buffer.BlockCopy(m_buffer, 0, dgram, 0, m_length);

            if (ep != null)
                client.Send(dgram, m_length, ep);
            else
                client.Send(dgram, m_length);

            m_length = 0;
            m_position = 0;
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            if (count <= 8)
            {
                int i = -1;
                while (++i < count)
                    m_buffer[m_position + i] = buffer[offset + i];
            }
            else
            {
                Buffer.BlockCopy(buffer, offset, m_buffer, m_position, count);
            }

            m_position += count;
            m_length += count;
        }

        public void WriteUInt8(byte value)
        {
            m_buffer[m_position++] = value;
            m_length++;
        }

        public void WriteInt8(sbyte value)
        {
            WriteUInt8((byte)value);
        }

        public void WriteUInt16(ushort value)
        {
            Write(new[]
            {
                (byte)((value & 0xFF00) >> 8),
                (byte)(value & 0xFF)
            }, 0, 2);
        }

        public void WriteInt16(short value)
        {
            WriteUInt16((ushort)value);
        }

        public void WriteUInt32(uint value)
        {
            Write(new[]
            {
                (byte)((value & 0xFF000000) >> 24),
                (byte)((value & 0xFF0000) >> 16),
                (byte)((value & 0xFF00) >> 8),
                (byte)(value & 0xFF)
            }, 0, 4);
        }

        public void WriteInt32(int value)
        {
            WriteUInt32((uint)value);
        }

        public void WriteUInt64(ulong value)
        {
            Write(new[]
            {
                (byte)((value & 0xFF00000000000000) >> 56),
                (byte)((value & 0xFF000000000000) >> 48),
                (byte)((value & 0xFF0000000000) >> 40),
                (byte)((value & 0xFF00000000) >> 32),
                (byte)((value & 0xFF000000) >> 24),
                (byte)((value & 0xFF0000) >> 16),
                (byte)((value & 0xFF00) >> 8),
                (byte)(value & 0xFF)
            }, 0, 8);
        }

        public void WriteInt64(long value)
        {
            WriteUInt64((ulong)value);
        }

        public void WriteUInt8Array(byte[] value)
        {
            Write(value, 0, value.Length);
        }

        public void WriteInt8Array(sbyte[] value)
        {
            Write((byte[])(Array)value, 0, value.Length);
        }

        public void WriteUInt16Array(ushort[] value)
        {
            for (int i = 0; i < value.Length; i++)
                WriteUInt16(value[i]);
        }

        public void WriteInt16Array(short[] value)
        {
            WriteUInt16Array((ushort[])(Array)value);
        }

        public void WriteUInt32Array(uint[] value)
        {
            for (int i = 0; i < value.Length; i++)
                WriteUInt32(value[i]);
        }

        public void WriteInt32Array(int[] value)
        {
            WriteUInt32Array((uint[])(Array)value);
        }

        public void WriteUInt64Array(ulong[] value)
        {
            for (int i = 0; i < value.Length; i++)
                WriteUInt64(value[i]);
        }

        public void WriteInt64Array(long[] value)
        {
            WriteUInt64Array((ulong[])(Array)value);
        }

        public unsafe void WriteSingle(float value)
        {
            WriteUInt32(*(uint*)&value);
        }

        public unsafe void WriteDouble(double value)
        {
            WriteUInt64(*(ulong*)&value);
        }

        public void WriteBoolean(bool value)
        {
            if (value) WriteUInt8(1);
            else WriteUInt8(0);
        }

        public void WriteString(string value)
        {
            WriteUInt16((ushort)value.Length);
            WriteUInt8Array(encoding.GetBytes(value));
        }
    }
}
