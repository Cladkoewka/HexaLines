using Assets.Scripts.Skins;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Skin[] _skins;

        [Header("UI Elements")]
        [SerializeField] private Button _setSkinButton;

        [SerializeField] private Button _buySkinButton;
        [SerializeField] private TMP_Text _priceSkinText;
        [SerializeField] private Button _openShopButton;
        [SerializeField] private Button _closeShopButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _prevButton;
        [SerializeField] private Image _skinPreviewImage;
        [SerializeField] private GameObject _shopWindow;
        
        private const string CurrentSettedSkin = "CurrentSettedSkin";

        private int _currentSkinIndex;
        private MoneyService _moneyService;
        private SkinChanger _skinChanger;
        
        

        public void Init(MoneyService moneyService, SkinChanger skinChanger)
        {
            _moneyService = moneyService;
            _skinChanger = skinChanger;
            
            Subscribe();
            SetButtons();
            SetSkinPreview();
            SetCurrentSkin();
        }

        private void SetCurrentSkin() => 
            _skinChanger.SetSkin(_skins[PlayerPrefs.GetInt(CurrentSettedSkin)]);

        private void OnDestroy() => 
            Unsubscribe();

        private void Subscribe()
        {
            _setSkinButton.onClick.AddListener(SetSkin);
            _buySkinButton.onClick.AddListener(BuySkin);
            _openShopButton.onClick.AddListener(OpenShop);
            _closeShopButton.onClick.AddListener(CloseShop);
            _nextButton.onClick.AddListener(NextSkin);
            _prevButton.onClick.AddListener(PrevSkin);
        }

        private void Unsubscribe()
        {
            _setSkinButton.onClick.RemoveListener(SetSkin);
            _buySkinButton.onClick.RemoveListener(BuySkin);
            _openShopButton.onClick.RemoveListener(OpenShop);
            _closeShopButton.onClick.RemoveListener(CloseShop);
            _nextButton.onClick.RemoveListener(NextSkin);
            _prevButton.onClick.RemoveListener(PrevSkin);
        }

        private void SetButtons()
        {
            SetSwitchButtons();
            SetBuySkinButton();
            SetSetSkinButton();
        }

        private void SetSwitchButtons()
        {
            _prevButton.gameObject.SetActive(true);
            _nextButton.gameObject.SetActive(true);

            if (_currentSkinIndex == 0)
                _prevButton.gameObject.SetActive(false);
            if (_currentSkinIndex == _skins.Length - 1)
                _nextButton.gameObject.SetActive(false);
        }

        private void SetSetSkinButton() => 
            _setSkinButton.gameObject.SetActive(CurrentSkin().IsOpened);

        private void SetBuySkinButton()
        {
                _priceSkinText.text = CurrentSkin().Price.ToString();
                _buySkinButton.gameObject.SetActive(!CurrentSkin().IsOpened);
        }

        private void SetSkinPreview() => 
            _skinPreviewImage.sprite = CurrentSkin().SkinPreview;

        private Skin CurrentSkin() => 
            _skins[_currentSkinIndex];

        private void NextSkin()
        {
            _currentSkinIndex++;
            SetButtons();
            SetSkinPreview();
        }

        private void PrevSkin()
        {
            _currentSkinIndex--;
            SetButtons();
            SetSkinPreview();
        }

        private void OpenShop() => 
            _shopWindow.SetActive(true);

        private void CloseShop() => 
            _shopWindow.SetActive(false);

        private void BuySkin()
        {
            if (_moneyService.CurrentCoins >= CurrentSkin().Price)
            {
                _moneyService.RemoveCoins(CurrentSkin().Price);
                CurrentSkin().IsOpened = true;
                SetBuySkinButton();
                SetSetSkinButton();
            }
        }

        private void SetSkin()
        {
            _skinChanger.SetSkin(_skins[_currentSkinIndex]);
            PlayerPrefs.SetInt(CurrentSettedSkin, _currentSkinIndex);
        }
    }
}