using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MatthiWare.Networking
{
    public class UdpReader
    {
        private Encoding encoding;

        private byte[] m_buffer;

        public int Length { get; private set; }
        public int Position { get; private set; }

        public UdpReader()
        {
            encoding = Encoding.UTF8;
            m_buffer = new byte[1024];
        }

        public void SetData(byte[] buffer)
        {
            if (buffer.Length > m_buffer.Length)
                m_buffer = buffer;
            else
                Buffer.BlockCopy(buffer, 0, m_buffer, 0, buffer.Length);

            Position = 0;
            Length = buffer.Length;
        }

        public int Read(byte[] dest, int offset, int len)
        {
            if (dest.Length - offset < len)
                throw new ArgumentException("Invalid offset length");

            int n = Length - Position;
            if (n > len) n = len;
            if (n <= 0)
                return 0;

            Buffer.BlockCopy(m_buffer, Position, dest, offset, len);

            Position += n;

            return n;
        }

        private int ReadByte()
        {
            if (Position + 1 > Length)
                return -1;
            else
                return m_buffer[Position++];
        }

        public byte ReadUInt8()
        {
            int value = ReadByte();
            if (value == -1)
                throw new EndOfStreamException();

            return (byte)value;
        }

        public sbyte ReadInt8()
        {
            return (sbyte)ReadUInt8();
        }

        public ushort ReadUInt16()
        {
            return (ushort)(
                (ReadUInt8() << 8) |
                ReadUInt8());
        }

        public short ReadInt16()
        {
            return (short)ReadUInt16();
        }

        public uint ReadUInt32()
        {
            return (uint)(
                (ReadUInt8() << 24) |
                (ReadUInt8() << 16) |
                (ReadUInt8() << 8) |
                 ReadUInt8());
        }


        public int ReadInt32()
        {
            return (int)ReadUInt32();
        }

        public ulong ReadUInt64()
        {
            return unchecked(
                   ((ulong)ReadUInt8() << 56) |
                   ((ulong)ReadUInt8() << 48) |
                   ((ulong)ReadUInt8() << 40) |
                   ((ulong)ReadUInt8() << 32) |
                   ((ulong)ReadUInt8() << 24) |
                   ((ulong)ReadUInt8() << 16) |
                   ((ulong)ReadUInt8() << 8) |
                    (ulong)ReadUInt8());
        }

        public long ReadInt64()
        {
            return (long)ReadUInt64();
        }

        public byte[] ReadUInt8Array(int length)
        {
            var result = new byte[length];
            if (length == 0) return result;
            Read(result, 0, length);
            return result;
        }

        public sbyte[] ReadInt8Array(int length)
        {
            return (sbyte[])(Array)ReadUInt8Array(length);
        }

        public ushort[] ReadUInt16Array(int length)
        {
            var result = new ushort[length];
            if (length == 0) return result;
            for (int i = 0; i < length; i++)
                result[i] = ReadUInt16();
            return result;
        }

        public short[] ReadInt16Array(int length)
        {
            return (short[])(Array)ReadUInt16Array(length);
        }

        public uint[] ReadUInt32Array(int length)
        {
            var result = new uint[length];
            if (length == 0) return result;
            for (int i = 0; i < length; i++)
                result[i] = ReadUInt32();
            return result;
        }

        public int[] ReadInt32Array(int length)
        {
            return (int[])(Array)ReadUInt32Array(length);
        }

        public ulong[] ReadUInt64Array(int length)
        {
            var result = new ulong[length];
            if (length == 0) return result;
            for (int i = 0; i < length; i++)
                result[i] = ReadUInt64();
            return result;
        }

        public long[] ReadInt64Array(int length)
        {
            return (long[])(Array)ReadUInt64Array(length);
        }

        public unsafe float ReadSingle()
        {
            uint value = ReadUInt32();
            return *(float*)&value;
        }


        public unsafe double ReadDouble()
        {
            ulong value = ReadUInt64();
            return *(double*)&value;
        }

        public bool ReadBoolean()
        {
            return ReadUInt8() != 0;
        }

        public string ReadString()
        {
            ushort length = ReadUInt16();
            byte[] data = ReadUInt8Array(length);
            return encoding.GetString(data);
        }


    }
}
