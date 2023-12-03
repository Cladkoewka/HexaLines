using UnityEngine;

namespace Assets.Scripts.Hex
{
    public class HexGrid : MonoBehaviour
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height = 9;
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private HexCell _cellPrefab;

        private HexCell[] _cells;
        private HexMesh _hexMesh;

        private void Awake()
        {
            _hexMesh = GetComponentInChildren<HexMesh>();

            _cells = new HexCell[_height * _width];

            int i = 0;
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    CreateCell(x, y, i++);
                }
            }
        }

        private void Start() => 
            _hexMesh.Triangulate(_cells);

        private void CreateCell(int x, int y, int i)
        {
            Vector3 position;
            position.x = (x + y * 0.5f - y / 2) * (HexMetrics.InnerRadius * 2f);
            position.y = y * (HexMetrics.OuterRadius * 1.5f);
            position.z = 0;

            HexCell cell = _cells[i] = Instantiate(_cellPrefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;
            cell.HexCoordinates = HexCoordinates.FromOffsetCoordinates(x, y);
            cell.Color = _defaultColor;
            cell.LabelText.text = cell.HexCoordinates.ToStringOnSeparateLines();
        }

        public void ColorCell(Vector3 position, Color color)
        {
            HexCoordinates coordinates = HexCoordinates.FromPosition(position);
            int index = coordinates.X + coordinates.Y * _width + coordinates.Y / 2;
            Debug.Log($"Coloring cell at position {position} with index {index}");
            HexCell cell = _cells[index];
            Debug.Log(cell.HexCoordinates.ToString());
            cell.Color = color;
            _hexMesh.Triangulate(_cells);
        }
    }
}