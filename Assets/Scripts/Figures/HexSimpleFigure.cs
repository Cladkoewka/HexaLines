using Assets.Scripts.Hex;
using UnityEngine;

namespace Assets.Scripts.Figures
{
    public class HexSimpleFigure : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Color Color
        {
            get => _color;
            set => _color = value;
        }
        
        private HexGrid _hexGrid;
        private HexCell _currentCell;
        private HexCell _lastCell;

        private void Start()
        {
            _hexGrid = FindObjectOfType<HexGrid>();
            _spriteRenderer.color = _color;
        }

        public bool CanBePlaced()
        {
            Vector3 rayStart = transform.position - Vector3.forward;
            Ray inputRay = new Ray(rayStart, Vector3.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(inputRay, out hit))
            {
                _currentCell = _hexGrid.CellByCoordinates(hit.point);

                if (!_currentCell.IsFilled)
                    return true;

            }
            
            return false;
        }

        public void ColorCell()
        {
            if (_currentCell != null)
                _hexGrid.ColorCell(_currentCell, _color);

            ClearLastCell();
        }

        public void ClearCell()
        {
            if (_currentCell != null && !_currentCell.IsFilled)
                _hexGrid.ColorCell(_currentCell, _hexGrid.DefaultColor);
            
            ClearLastCell();
        }

        public void Place()
        {
            _hexGrid.ColorCell(_currentCell, _color);
            _hexGrid.FillCell(_currentCell);
        }

        private void ClearLastCell()
        {
            if (_lastCell != _currentCell && _lastCell != null && !_lastCell.IsFilled)
                _hexGrid.ColorCell(_lastCell, _hexGrid.DefaultColor);
            _lastCell = _currentCell;
        }
    }
}