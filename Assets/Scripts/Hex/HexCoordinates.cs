using UnityEngine;

namespace Assets.Scripts.Hex
{
    [System.Serializable]
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

        public static HexCoordinates FromOffsetCoordinates(int x, int y)
        {
            int xCoordinate = x;
            
            switch (y)
            {
                case 1:
                    xCoordinate -= 1;
                    break;
                case 2:
                    xCoordinate -= 2;
                    break;
                case 3:
                    xCoordinate -= 3;
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    xCoordinate -= 4;
                    break;
            }
            
            int yCoordinate = y - 4;
            
            return new HexCoordinates(xCoordinate, yCoordinate);
        }

        public static HexCoordinates FromPosition(Vector3 position)
        {
            float spacing = HexMetrics.Spacing;

            float x = (position.x + spacing) / (HexMetrics.InnerRadius * 2f + spacing) - 2;
            float y = -x;

            float offsetY = (position.y + spacing) / (HexMetrics.OuterRadius * 3f + spacing * 2) - 2;
            y -= offsetY;
            x -= offsetY;
            
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

        
        
        public override string ToString () => 
            "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";

        public string ToStringOnSeparateLines () => 
            X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }
}