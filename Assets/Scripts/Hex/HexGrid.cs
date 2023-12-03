using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Hex
{
    public class HexGrid : MonoBehaviour
    {
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private HexCell _cellPrefab;

        private HexCell[] _cells;
        private HexMesh _hexMesh;

        private void Awake()
        {
            _hexMesh = GetComponentInChildren<HexMesh>();

            _cells = new HexCell[HexMetrics.CellsCount.Sum()];

            int i = 0;
            int j = 0;
            for (int y = 0; y < HexMetrics.CellsCount.Length; y++)
            {
                for (int x = 0; x < HexMetrics.CellsCount[j]; x++)
                {
                    CreateCell(x, y, i++);
                }

                j++;
            }
        }

        private void Start() => 
            _hexMesh.Triangulate(_cells);

        private void CreateCell(int x, int y, int i)
        {
            Vector3 position;
            position.x = XPosition(x, y);
            position.y = y * (HexMetrics.OuterRadius * 1.5f);
            position.z = 0;

            HexCell cell = _cells[i] = Instantiate(_cellPrefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;
            cell.HexCoordinates = HexCoordinates.FromOffsetCoordinates(x, y);
            cell.Color = _defaultColor;
            cell.LabelText.text = cell.HexCoordinates.ToStringOnSeparateLines();
        }

        private float XPosition(int x, int y)
        {
            float xPos;
            if (y <= HexMetrics.CellsCount.Length / 2)
                xPos = (x - y * 0.5f) * (HexMetrics.InnerRadius * 2f);
            else
                xPos = (x + y * 0.5f - HexMetrics.CellsCount.Length / 2) * (HexMetrics.InnerRadius * 2f);
            return xPos;
        }

        public void ColorCell(Vector3 position, Color color)
        {
            Vector3 localPosition = transform.InverseTransformPoint(position);
            HexCoordinates coordinates = HexCoordinates.FromPosition(localPosition);
            HexCell cell = FindCellByCoordinates(coordinates);
            cell.Color = color;
            cell.IsFilled = true;
            _hexMesh.Triangulate(_cells);
            
            CheckLines();
        }

        private void CheckLines()
        {
            CheckLinesX();
            CheckLinesY();
            CheckLinesZ();
        }

        private void CheckLinesX()
        {
            for (int x = -4; x < 5; x++)
            {
                HexCell[] allCells = _cells.Where(data => data.HexCoordinates.X == x).ToArray();
                HexCell[] filledCells = allCells.Where(data => data.IsFilled == true).ToArray();
                if (allCells.Length == filledCells.Length)
                    StartCoroutine(ClearCells(allCells));
            }
        }
        
        private void CheckLinesY()
        {
            for (int y = -4; y < 5; y++)
            {
                HexCell[] allCells = _cells.Where(data => data.HexCoordinates.Y == y).ToArray();
                HexCell[] filledCells = allCells.Where(data => data.IsFilled == true).ToArray();
                if (allCells.Length == filledCells.Length)
                    StartCoroutine(ClearCells(allCells));
            }
        }
        
        private void CheckLinesZ()
        {
            for (int z = -4; z < 5; z++)
            {
                HexCell[] allCells = _cells.Where(data => data.HexCoordinates.Z == z).ToArray();
                HexCell[] filledCells = allCells.Where(data => data.IsFilled == true).ToArray();
                if (allCells.Length == filledCells.Length)
                    StartCoroutine(ClearCells(allCells));
            }
        }

        private IEnumerator ClearCells(HexCell[] allCells)
        {
            yield return new WaitForSeconds(0.5f);
            
            foreach (HexCell cell in allCells)
            {
                cell.IsFilled = false;
                cell.Color = _defaultColor;
            }
            
            _hexMesh.Triangulate(_cells);
        }

        private HexCell FindCellByCoordinates(HexCoordinates coordinates)
        {
            return _cells.FirstOrDefault
            (
                data => 
                data.HexCoordinates.X == coordinates.X && 
                data.HexCoordinates.Y == coordinates.Y && 
                data.HexCoordinates.Z == coordinates.Z
            );
        }
    }
}