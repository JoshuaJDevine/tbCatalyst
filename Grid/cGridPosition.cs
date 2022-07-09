namespace DBS.Catalyst
{
    public struct cGridPosition
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
    }
}
