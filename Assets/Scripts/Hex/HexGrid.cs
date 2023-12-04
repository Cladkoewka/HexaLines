using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Hex
{
    public class HexGrid : MonoBehaviour
    {
        public Color DefaultColor = Color.white;
        
        [SerializeField] private Color _clearColor = Color.gray;
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
            position.y = y * (HexMetrics.OuterRadius * 1.5f + HexMetrics.Spacing);
            position.z = 0;

            HexCell cell = _cells[i] = Instantiate(_cellPrefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;
            cell.HexCoordinates = HexCoordinates.FromOffsetCoordinates(x, y);
            cell.Color = DefaultColor;
            //cell.LabelText.text = cell.HexCoordinates.ToStringOnSeparateLines();
        }

        private float XPosition(int x, int y)
        {
            float xPos;
            if (y <= HexMetrics.CellsCount.Length / 2)
                xPos = (x - y * 0.5f) * (HexMetrics.InnerRadius * 2f + HexMetrics.Spacing);
            else
                xPos = (x + y * 0.5f - HexMetrics.CellsCount.Length / 2) * (HexMetrics.InnerRadius * 2f + HexMetrics.Spacing);
            return xPos;
        }


        public HexCell CellByCoordinates(Vector3 position)
        {
            HexCoordinates coordinates = HexCoordinates.FromPosition(position);
            Debug.Log(coordinates.ToString());
            HexCell cell = FindCellByCoordinates(coordinates);
            return cell;
        }
        
        public void ColorCell(HexCell cell, Color color)
        {
            cell.Color = color;
            _hexMesh.Triangulate(_cells);
        }

        public void FillCell(HexCell cell)
        {
            cell.IsFilled = true;
            CheckLines();
            _hexMesh.Triangulate(_cells);
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
            foreach (HexCell cell in allCells) 
                cell.Color = _clearColor;
            
            _hexMesh.Triangulate(_cells);

            yield return new WaitForSeconds(0.5f);
            
            foreach (HexCell cell in allCells)
            {
                cell.IsFilled = false;
                cell.Color = DefaultColor;
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