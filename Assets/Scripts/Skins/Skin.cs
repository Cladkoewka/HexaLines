using UnityEngine;

namespace Assets.Scripts.Skins
{
    [CreateAssetMenu(fileName = "Skin", menuName = "Skin")]
    public class Skin : ScriptableObject
    {
        public Color BackGroundColor;
        public Color GridColor;
        public Color CellsClearColor;
        public Color ButtonsColor;
        public Color ShopButtonsColor;
        public Color TextColor;
        public Sprite SkinPreview;
        public bool IsOpened;
        public int Price;
    }
}
