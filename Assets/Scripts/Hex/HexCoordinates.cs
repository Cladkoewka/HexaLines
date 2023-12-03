using UnityEngine;

namespace Assets.Scripts.Hex
{
    public struct HexCoordinates
    {
        [SerializeField] private int _x;
        [SerializeField] private int _y;

        public int X => _x;
        public int Y => _y;
        public int Z => -X - Y;

        public HexCoordinates(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public static HexCoordinates FromOffsetCoordinates(int x, int y) => 
            new(x - y / 2, y);

        public static HexCoordinates FromPosition(Vector3 position)
        {
            float x = position.x / (HexMetrics.InnerRadius * 2f);
            float y = -x;

            float offset = position.y / (HexMetrics.OuterRadius * 3f);
            x -= offset;
            y -= offset;

            int iX = Mathf.RoundToInt(x);
            int iY = Mathf.RoundToInt(y);
            int iZ = Mathf.RoundToInt(-x -y);
            
            if (iX + iY + iZ != 0) {
                float dX = Mathf.Abs(x - iX);
                float dY = Mathf.Abs(y - iY);
                float dZ = Mathf.Abs(-x -y - iZ);

                if (dX > dY && dX > dZ) {
                    iX = -iY - iZ;
                }
                else if (dZ > dY) {
                    iZ = -iX - iY;
                }
            }

            return new HexCoordinates(iX, iZ);
        }
        
        public override string ToString () {
            return "(" +
                   X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
        }

        public string ToStringOnSeparateLines () {
            return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
        }
    }
}