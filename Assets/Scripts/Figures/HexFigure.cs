using System;
using Assets.Scripts.FiguresSpawner;
using Assets.Scripts.Hex;
using Assets.Scripts.Static;
using UnityEngine;

namespace Assets.Scripts.Figures
{
    public class HexFigure : MonoBehaviour
    {
        [SerializeField] private HexSimpleFigure[] _hexSimpleFigures;
        
        private bool _isDragging = false;
        private Vector3 _startPosition;

        public event Action Placed;
        public FigureSpawnPoint FigureSpawnPoint;

        private void OnMouseDown() => 
            StartDrugging();

        private void OnMouseDrag()
        {
            if (_isDragging)
            {
                Move();

                if (CanBePlaced()) 
                    ShowThatCanBePlaced();
                else
                    ShowThatCantBePlaced();
                
                Debug.Log($"Can be placed {CanBePlaced()}");
            }
        }

        private void OnMouseUp()
        {
            _isDragging = false;
            
            if (CanBePlaced())
                Place();
            else
                BackToStartPosition();
        }

        public void SetColor(Color color)
        {
            foreach (HexSimpleFigure simpleFigure in _hexSimpleFigures) 
                simpleFigure.Color = color;
        }

        private bool CanBePlaced()
        {
            bool canBePlaced = true;

            foreach (HexSimpleFigure simpleFigure in _hexSimpleFigures)
            {
                if (!simpleFigure.CanBePlaced())
                    canBePlaced = false;
            }

            return canBePlaced;
        }

        private void ShowThatCanBePlaced()
        {
            foreach (HexSimpleFigure simpleFigure in _hexSimpleFigures) 
                simpleFigure.ColorCell();
        }
        
        private void ShowThatCantBePlaced()
        {
            foreach (HexSimpleFigure simpleFigure in _hexSimpleFigures) 
                simpleFigure.ClearCell();
        }

        private void Move()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        }

        private void BackToStartPosition()
        {
            ShowThatCantBePlaced();
            
            transform.position = _startPosition;
            transform.localScale = Vector3.one * Constants.InactiveScaleFactor;
        }

        private void Place()
        {
            FigureSpawnPoint.IsEmpty = true;
            Placed?.Invoke();

            foreach (HexSimpleFigure simpleFigure in _hexSimpleFigures)
                simpleFigure.Place();

            Destroy(gameObject);
        }

        private void StartDrugging()
        {
            transform.localScale = Vector3.one;
            
            _isDragging = true;
            _startPosition = transform.position;
        }
    }
}