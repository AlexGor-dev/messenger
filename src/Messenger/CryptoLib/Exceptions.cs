using System;
using System.IO;

namespace CryptoLib
{
    public class Asn1Exception
        : IOException
    {
        public Asn1Exception()
            : base()
        {
        }

        public Asn1Exception(
            string message)
            : base(message)
        {
        }

        public Asn1Exception(
            string message,
            Exception exception)
            : base(message, exception)
        {
        }
    }

    public class Asn1ParsingException
    : InvalidOperationException
    {
        public Asn1ParsingException()
            : base()
        {
        }

        public Asn1ParsingException(
            string message)
            : base(message)
        {
        }

        public Asn1ParsingException(
            string message,
            Exception exception)
            : base(message, exception)
        {
        }
    }

    public class CryptoException
    : Exception
    {
        public CryptoException()
        {
        }

        public CryptoException(
            string message)
            : base(message)
        {
        }

        public CryptoException(
            string message,
            Exception exception)
            : base(message, exception)
        {
        }
    }

    public class DataLengthException
    : CryptoException
    {
        /**
        * base constructor.
		*/
        public DataLengthException()
        {
        }

        /**
         * create a DataLengthException with the given message.
         *
         * @param message the message to be carried with the exception.
         */
        public DataLengthException(
            string message)
            : base(message)
        {
        }

        public DataLengthException(
            string message,
            Exception exception)
            : base(message, exception)
        {
        }
    }

    public class OutputLengthException
    : DataLengthException
    {
        public OutputLengthException()
        {
        }

        public OutputLengthException(
            string message)
            : base(message)
        {
        }

        public OutputLengthException(
            string message,
            Exception exception)
            : base(message, exception)
        {
        }
    }

    public class InvalidCipherTextException
    : CryptoException
    {
        /**
		* base constructor.
		*/
        public InvalidCipherTextException()
        {
        }

        /**
         * create a InvalidCipherTextException with the given message.
         *
         * @param message the message to be carried with the exception.
         */
        public InvalidCipherTextException(
            string message)
            : base(message)
        {
        }

        public InvalidCipherTextException(
            string message,
            Exception exception)
            : base(message, exception)
        {
        }
    }

    public class SecurityUtilityException
    : Exception
    {
        /**
        * base constructor.
        */
        public SecurityUtilityException()
        {
        }

        /**
         * create a SecurityUtilityException with the given message.
         *
         * @param message the message to be carried with the exception.
         */
        public SecurityUtilityException(
            string message)
            : base(message)
        {
        }

        public SecurityUtilityException(
            string message,
            Exception exception)
            : base(message, exception)
        {
        }
    }

}
