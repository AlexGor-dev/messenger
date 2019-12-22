using System;
namespace Messenger
{
    public struct Gram
    {
        public Gram(long value)
        {
            this.value = value;
        }

        private long value;

        public double Value => value / 1000000000.0;

        public static bool operator ==(Gram a, Gram b)
        {
            return a.value == b.value;
        }

        public static bool operator >=(Gram a, Gram b)
        {
            return a.value >= b.value;
        }

        public static bool operator <=(Gram a, Gram b)
        {
            return a.value <= b.value;
        }

        public static bool operator >(Gram a, Gram b)
        {
            return a.value > b.value;
        }

        public static bool operator <(Gram a, Gram b)
        {
            return a.value < b.value;
        }

        public static bool operator !=(Gram a, Gram b)
        {
            return a.value != b.value;
        }

        public static implicit operator Gram(long value)
        {
            return new Gram(value);
        }

        public override string ToString()
        {
            return string.Format("{0:N3}", this.Value);
        }

        public override bool Equals(object obj)
        {
            return this == (Gram)obj;
        }
    }
}
