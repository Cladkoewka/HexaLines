using Assets.Scripts.Hex;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Skins
{
    public class SkinChanger : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private HexGrid _hexGrid;
        [SerializeField] private Image[] _buttons;
        [SerializeField] private Image[] _shopButtons;
        [SerializeField] private TMP_Text[] _text;
        
        public void SetSkin(Skin skin)
        {
            _camera.backgroundColor = skin.BackGroundColor;
            _hexGrid.SetCellsDefaultColor(skin.GridColor);
            _hexGrid.SetCellsClearColor(skin.CellsClearColor);
            
            foreach (var button in _buttons) 
                button.material.color = skin.ButtonsColor;

            foreach (var shopButton in _shopButtons) 
                shopButton.material.color = skin.ShopButtonsColor;

            foreach (var text in _text) 
                text.color = skin.TextColor;
        }
    }
}