using TMPro;
using UnityEngine;

namespace Assets.Scripts.Hex
{
    public class HexCell : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private HexCoordinates _hexCoordinates;
        [SerializeField] private TMP_Text _labelText;
    
        public bool IsFilled { get; set; }

        public TMP_Text LabelText
        {
            get => _labelText;
            set => _labelText = value;
        }
    
        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public HexCoordinates HexCoordinates
        {
            get => _hexCoordinates;
            set => _hexCoordinates = value;
        }
    }
}
