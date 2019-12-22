using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib
{
    public struct FieldElement
    {
        internal int x0;
        internal int x1;
        internal int x2;
        internal int x3;
        internal int x4;
        internal int x5;
        internal int x6;
        internal int x7;
        internal int x8;
        internal int x9;

        internal FieldElement(params int[] elements)
        {
            x0 = elements[0];
            x1 = elements[1];
            x2 = elements[2];
            x3 = elements[3];
            x4 = elements[4];
            x5 = elements[5];
            x6 = elements[6];
            x7 = elements[7];
            x8 = elements[8];
            x9 = elements[9];
        }
    }

    public struct GroupElementP3
    {
        public FieldElement X;
        public FieldElement Y;
        public FieldElement Z;
        public FieldElement T;
    };

    public struct GroupElementP1P1
    {
        public FieldElement X;
        public FieldElement Y;
        public FieldElement Z;
        public FieldElement T;
    };

    public struct GroupElementP2
    {
        public FieldElement X;
        public FieldElement Y;
        public FieldElement Z;
    };

    public struct GroupElementPreComp
    {
        public FieldElement yplusx;
        public FieldElement yminusx;
        public FieldElement xy2d;

        public GroupElementPreComp(FieldElement yplusx, FieldElement yminusx, FieldElement xy2d)
        {
            this.yplusx = yplusx;
            this.yminusx = yminusx;
            this.xy2d = xy2d;
        }
    };

    internal struct Array8<T>
    {
        public T x0;
        public T x1;
        public T x2;
        public T x3;
        public T x4;
        public T x5;
        public T x6;
        public T x7;
    }

    internal struct Array16<T>
    {
        public T x0;
        public T x1;
        public T x2;
        public T x3;
        public T x4;
        public T x5;
        public T x6;
        public T x7;
        public T x8;
        public T x9;
        public T x10;
        public T x11;
        public T x12;
        public T x13;
        public T x14;
        public T x15;
    }

}
