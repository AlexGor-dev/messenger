using System;
using System.IO;
using System.Collections;

namespace CryptoLib
{
    public interface IDigest
    {
        string AlgorithmName { get; }
        int GetDigestSize();
        int GetByteLength();
        void Update(byte input);
        void BlockUpdate(byte[] input, int inOff, int length);
        int DoFinal(byte[] output, int outOff);
        void Reset();
    }

    public interface IMemoable
    {
        IMemoable Copy();
        void Reset(IMemoable other);
    }

    public interface IRandomGenerator
    {
        void AddSeedMaterial(byte[] seed);
        void AddSeedMaterial(long seed);
        void NextBytes(byte[] bytes);
        void NextBytes(byte[] bytes, int start, int len);
    }

    public interface IAsn1Convertible
    {
        Asn1Object ToAsn1Object();
    }

    public interface Asn1TaggedObjectParser : IAsn1Convertible
    {
        int TagNo { get; }

        IAsn1Convertible GetObjectParser(int tag, bool isExplicit);
    }

    public interface IAsn1Choice
    {
        // marker interface
    }

    public interface Asn1SequenceParser
    : IAsn1Convertible
    {
        IAsn1Convertible ReadObject();
    }

    public interface Asn1OctetStringParser
    : IAsn1Convertible
    {
        Stream GetOctetStream();
    }

    public interface IEncoder
    {
        int Encode(byte[] data, int off, int length, Stream outStream);

        int Decode(byte[] data, int off, int length, Stream outStream);

        int DecodeString(string data, Stream outStream);
    }

    public interface Asn1SetParser
    : IAsn1Convertible
    {
        IAsn1Convertible ReadObject();
    }

    public interface ISet
    : ICollection
    {
        void Add(object o);
        void AddAll(IEnumerable e);
        void Clear();
        bool Contains(object o);
        bool IsEmpty { get; }
        bool IsFixedSize { get; }
        bool IsReadOnly { get; }
        void Remove(object o);
        void RemoveAll(IEnumerable e);
    }

    public interface IAsn1ApplicationSpecificParser
    : IAsn1Convertible
    {
        IAsn1Convertible ReadObject();
    }

    public interface IAsn1String
    {
        string GetString();
    }

    public interface PemObjectGenerator
    {
        PemObject Generate();
    }

    public interface IPublicKey
    {
    }

    public interface IPrivateKey
    {
    }

    public interface ICipherParameters
    {
    }

    public interface IAeadBlockCipher
    {
        string AlgorithmName { get; }
        IBlockCipher GetUnderlyingCipher();
        void Init(bool forEncryption, ICipherParameters parameters);
        int GetBlockSize();
        void ProcessAadByte(byte input);
        void ProcessAadBytes(byte[] inBytes, int inOff, int len);
        int ProcessByte(byte input, byte[] outBytes, int outOff);
        int ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff);
        int DoFinal(byte[] outBytes, int outOff);
        byte[] GetMac();
        int GetUpdateOutputSize(int len);
        int GetOutputSize(int len);
        void Reset();
    }

    public interface IBlockCipher
    {
        string AlgorithmName { get; }
        void Init(bool forEncryption, ICipherParameters parameters);
        int GetBlockSize();
        bool IsPartialBlockOkay { get; }
        int ProcessBlock(byte[] inBuf, int inOff, byte[] outBuf, int outOff);
        void Reset();
    }

    public interface IGcmMultiplier
    {
        void Init(byte[] H);
        void MultiplyH(byte[] x);
    }

    public interface IGcmExponentiator
    {
        void Init(byte[] x);
        void ExponentiateX(long pow, byte[] output);
    }

    public interface IDerivationParameters
    {
    }

    public interface IDerivationFunction
    {
        void Init(IDerivationParameters parameters);
        IDigest Digest{get;}
        int GenerateBytes(byte[] output, int outOff, int length);
    }

    public interface IMac
    {
        void Init(ICipherParameters parameters);
        string AlgorithmName { get; }
        int GetMacSize();
        void Update(byte input);
        void BlockUpdate(byte[] input, int inOff, int len);
        int DoFinal(byte[] output, int outOff);
        void Reset();
    }

}
