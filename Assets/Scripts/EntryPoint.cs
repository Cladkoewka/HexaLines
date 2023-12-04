using Assets.Scripts.Hex;
using Assets.Scripts.Scores;
using UnityEngine;
using Assets.Scripts.Spawner;
using TMPro;

namespace Assets.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private FiguresSpawner _figuresSpawner;
        [SerializeField] private HexGrid _hexGrid;
        [SerializeField] private TMP_Text _currentScoreText;
        [SerializeField] private TMP_Text _highestScoreText;

        private ScoreCounter _scoreCounter;

        private void Start() => 
            Initialize();

        private void Initialize()
        {
            _figuresSpawner.Init();
            _hexGrid.Init();

            _scoreCounter = new ScoreCounter();
            _scoreCounter.Init(_hexGrid, _currentScoreText, _highestScoreText);
        }
    }
}