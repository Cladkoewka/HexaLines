using Assets.Scripts.Audio;
using Assets.Scripts.Static;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class MoneyService : MonoBehaviour
    {
        private const string CoinsCountPFKey = "CoinsCount";
        
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private TMP_Text _coinsText;

        private int _currentCoins;

        public int CurrentCoins => _currentCoins;

        public void Init()
        {
            _currentCoins = PlayerPrefs.GetInt(CoinsCountPFKey);
            UpdateText();
        }
        
        public void SpawnCoin()
        {
            if (CalculateChance())
            {
                Instantiate(_coinPrefab, _spawnPosition.position - Vector3.forward * 5, Quaternion.identity);
                _currentCoins++;
                PlayerPrefs.SetInt(CoinsCountPFKey, _currentCoins);
                UpdateText();
                AudioManager.Instance.PlayCoinSound();
            }
        }

        private void UpdateText() => 
            _coinsText.text = _currentCoins.ToString();

        public void RemoveCoins(int value)
        {
            _currentCoins -= value;
            PlayerPrefs.SetInt(CoinsCountPFKey, _currentCoins);
            UpdateText();
        }

        private static bool CalculateChance() => 
            Random.Range(0f, 1f) < Constants.CoinDropChance;
    }
}