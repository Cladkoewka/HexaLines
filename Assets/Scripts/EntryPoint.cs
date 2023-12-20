using Assets.Scripts.Audio;
using Assets.Scripts.Hex;
using Assets.Scripts.Scores;
using Assets.Scripts.Shop;
using Assets.Scripts.Skins;
using UnityEngine;
using Assets.Scripts.Spawner;
using TMPro;

namespace Assets.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private FiguresSpawner _figuresSpawner;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private MoneyService _moneyService;
        [SerializeField] private SkinChanger _skinChanger;
        [SerializeField] private Leaderboard _leaderboard;
        [SerializeField] private Shop.Shop _shop;
        [SerializeField] private HexGrid _hexGrid;
        [SerializeField] private TMP_Text _currentScoreText;
        [SerializeField] private TMP_Text _highestScoreText;

        private ScoreCounter _scoreCounter;

        private void Start() => 
            Initialize();

        private void Initialize()
        {
            _figuresSpawner.Init(_moneyService);
            _hexGrid.Init();
            _moneyService.Init();

            _scoreCounter = new ScoreCounter();
            _scoreCounter.Init(_hexGrid, _currentScoreText, _highestScoreText, _leaderboard);
            _audioManager.Init();
            _shop.Init(_moneyService, _skinChanger);
        }
    }
}