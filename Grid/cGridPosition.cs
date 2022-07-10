using System;
using Unity.VisualScripting.Dependencies.NCalc;

namespace DBS.Catalyst
{
    public struct cGridPosition : IEquatable<cGridPosition>
    {
        public int x;
        public int z;

        public cGridPosition(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
        public override string ToString()
        {
            return $"{x} : {z}";
        }
        public static bool operator == (cGridPosition a, cGridPosition b)
        {
            return a.x == b.x && a.z == b.z;
        }
        public static bool operator != (cGridPosition a, cGridPosition b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            return obj is cGridPosition other && Equals(other);
        }
        public bool Equals(cGridPosition other)
        {
            return x == other.x && z == other.z;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(x, z);
        }

        public static cGridPosition operator +(cGridPosition a, cGridPosition b)
        {
            return new cGridPosition(a.x + b.x, a.z + b.z);
        }
        public static cGridPosition operator -(cGridPosition a, cGridPosition b)
        {
            return new cGridPosition(a.x - b.x, a.z - b.z);
        }

    }
}
