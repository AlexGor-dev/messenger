﻿using System;

namespace CryptoLib
{
    internal static class ByteIntegerConverter
    {
        #region Individual

        public static UInt32 LoadLittleEndian32(byte[] buf, int offset)
        {
            return
                (UInt32)(buf[offset + 0])
            | (((UInt32)(buf[offset + 1])) << 8)
            | (((UInt32)(buf[offset + 2])) << 16)
            | (((UInt32)(buf[offset + 3])) << 24);
        }

        public static void StoreLittleEndian32(byte[] buf, int offset, UInt32 value)
        {
            buf[offset + 0] = unchecked((byte)value);
            buf[offset + 1] = unchecked((byte)(value >> 8));
            buf[offset + 2] = unchecked((byte)(value >> 16));
            buf[offset + 3] = unchecked((byte)(value >> 24));
        }

        public static UInt64 LoadBigEndian64(byte[] buf, int offset)
        {
            return
                (UInt64)(buf[offset + 7])
                | (((UInt64)(buf[offset + 6])) << 8)
                | (((UInt64)(buf[offset + 5])) << 16)
                | (((UInt64)(buf[offset + 4])) << 24)
                | (((UInt64)(buf[offset + 3])) << 32)
                | (((UInt64)(buf[offset + 2])) << 40)
                | (((UInt64)(buf[offset + 1])) << 48)
                | (((UInt64)(buf[offset + 0])) << 56);
        }

        public static void StoreBigEndian64(byte[] buf, int offset, UInt64 value)
        {
            buf[offset + 7] = unchecked((byte)value);
            buf[offset + 6] = unchecked((byte)(value >> 8));
            buf[offset + 5] = unchecked((byte)(value >> 16));
            buf[offset + 4] = unchecked((byte)(value >> 24));
            buf[offset + 3] = unchecked((byte)(value >> 32));
            buf[offset + 2] = unchecked((byte)(value >> 40));
            buf[offset + 1] = unchecked((byte)(value >> 48));
            buf[offset + 0] = unchecked((byte)(value >> 56));
        }

        #endregion

        #region Array8

        public static void Array8LoadLittleEndian32(out Array8<UInt32> output, byte[] input, int inputOffset)
        {
            output.x0 = LoadLittleEndian32(input, inputOffset + 0);
            output.x1 = LoadLittleEndian32(input, inputOffset + 4);
            output.x2 = LoadLittleEndian32(input, inputOffset + 8);
            output.x3 = LoadLittleEndian32(input, inputOffset + 12);
            output.x4 = LoadLittleEndian32(input, inputOffset + 16);
            output.x5 = LoadLittleEndian32(input, inputOffset + 20);
            output.x6 = LoadLittleEndian32(input, inputOffset + 24);
            output.x7 = LoadLittleEndian32(input, inputOffset + 28);
        }

        #endregion

        public static void Array16LoadBigEndian64(out Array16<UInt64> output, byte[] input, int inputOffset)
        {
            output.x0 = LoadBigEndian64(input, inputOffset + 0);
            output.x1 = LoadBigEndian64(input, inputOffset + 8);
            output.x2 = LoadBigEndian64(input, inputOffset + 16);
            output.x3 = LoadBigEndian64(input, inputOffset + 24);
            output.x4 = LoadBigEndian64(input, inputOffset + 32);
            output.x5 = LoadBigEndian64(input, inputOffset + 40);
            output.x6 = LoadBigEndian64(input, inputOffset + 48);
            output.x7 = LoadBigEndian64(input, inputOffset + 56);
            output.x8 = LoadBigEndian64(input, inputOffset + 64);
            output.x9 = LoadBigEndian64(input, inputOffset + 72);
            output.x10 = LoadBigEndian64(input, inputOffset + 80);
            output.x11 = LoadBigEndian64(input, inputOffset + 88);
            output.x12 = LoadBigEndian64(input, inputOffset + 96);
            output.x13 = LoadBigEndian64(input, inputOffset + 104);
            output.x14 = LoadBigEndian64(input, inputOffset + 112);
            output.x15 = LoadBigEndian64(input, inputOffset + 120);
        }

        // ToDo: Only used in tests. Remove?
        public static void Array16LoadLittleEndian32(out Array16<UInt32> output, byte[] input, int inputOffset)
        {
            output.x0 = LoadLittleEndian32(input, inputOffset + 0);
            output.x1 = LoadLittleEndian32(input, inputOffset + 4);
            output.x2 = LoadLittleEndian32(input, inputOffset + 8);
            output.x3 = LoadLittleEndian32(input, inputOffset + 12);
            output.x4 = LoadLittleEndian32(input, inputOffset + 16);
            output.x5 = LoadLittleEndian32(input, inputOffset + 20);
            output.x6 = LoadLittleEndian32(input, inputOffset + 24);
            output.x7 = LoadLittleEndian32(input, inputOffset + 28);
            output.x8 = LoadLittleEndian32(input, inputOffset + 32);
            output.x9 = LoadLittleEndian32(input, inputOffset + 36);
            output.x10 = LoadLittleEndian32(input, inputOffset + 40);
            output.x11 = LoadLittleEndian32(input, inputOffset + 44);
            output.x12 = LoadLittleEndian32(input, inputOffset + 48);
            output.x13 = LoadLittleEndian32(input, inputOffset + 52);
            output.x14 = LoadLittleEndian32(input, inputOffset + 56);
            output.x15 = LoadLittleEndian32(input, inputOffset + 60);
        }

        public static void Array16StoreLittleEndian32(byte[] output, int outputOffset, ref Array16<UInt32> input)
        {
            StoreLittleEndian32(output, outputOffset + 0, input.x0);
            StoreLittleEndian32(output, outputOffset + 4, input.x1);
            StoreLittleEndian32(output, outputOffset + 8, input.x2);
            StoreLittleEndian32(output, outputOffset + 12, input.x3);
            StoreLittleEndian32(output, outputOffset + 16, input.x4);
            StoreLittleEndian32(output, outputOffset + 20, input.x5);
            StoreLittleEndian32(output, outputOffset + 24, input.x6);
            StoreLittleEndian32(output, outputOffset + 28, input.x7);
            StoreLittleEndian32(output, outputOffset + 32, input.x8);
            StoreLittleEndian32(output, outputOffset + 36, input.x9);
            StoreLittleEndian32(output, outputOffset + 40, input.x10);
            StoreLittleEndian32(output, outputOffset + 44, input.x11);
            StoreLittleEndian32(output, outputOffset + 48, input.x12);
            StoreLittleEndian32(output, outputOffset + 52, input.x13);
            StoreLittleEndian32(output, outputOffset + 56, input.x14);
            StoreLittleEndian32(output, outputOffset + 60, input.x15);
        }
    }
}
