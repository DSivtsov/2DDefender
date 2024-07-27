using System.Data;

namespace Game.Modules.Utils
{
    public sealed class SolveQuadratic
    {
        private readonly float a;
        private readonly float b;
        private readonly float c;
        private readonly float D;

        public SolveQuadratic(float a, float b, float c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.D = Discriminant();
        }

        private float Discriminant() => b * b - 4 * a * c;

        public bool IsNoRoot => D < 0;
        public int GetNumberRoot() =>
            D switch
            {
                < 0 => 0,
                > 0 => 2,
                _ => 1
            };

        public float[] GetRoots()
        {
            switch (GetNumberRoot())
            {
                case 0 :
                    return new float[] {-b / (2 * a)};
                case 2 :
                    float sqrtD = (float)System.Math.Sqrt(D);
                    return new float[] {(-b + sqrtD) / (2 * a) , (-b - sqrtD) / (2 * a)};
                default:
                    throw new DataException("No Roots");
            }
        }
    }
}