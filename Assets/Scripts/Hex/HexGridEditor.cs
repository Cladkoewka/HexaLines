using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Hex
{
    public class HexGridEditor : MonoBehaviour
    {
        private Color _color = Color.cyan;

        [SerializeField] private HexGrid hexGrid;


        private void Update ()
        {
            if(Input.GetMouseButtonDown(0)) 
                HandleInput();
        }

        private void HandleInput () {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit)) 
                hexGrid.ColorCell(hit.point, _color);
        }

    }
}