using Assets.Scripts.Audio;
using Assets.Scripts.Hex;
using Assets.Scripts.Static;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Scores
{
    public class ScoreCounter
    {
        private const string MaxScorePFKey = "MaxScore";
        
        private TMP_Text _currentScoreText;
        private TMP_Text _highestScoreText;
        private int _currentScore;

        public void Init(HexGrid hexGrid, TMP_Text currentScoreText, TMP_Text highestScoreText)
        {
            hexGrid.LineFilled += AddScoresByFilledLine;
            hexGrid.CellFilled += AddScoresByFilledCells;
            _currentScoreText = currentScoreText;
            _highestScoreText = highestScoreText;
            _highestScoreText.text = PlayerPrefs.GetInt(MaxScorePFKey).ToString();
        }


        private void AddScoresByFilledCells()
        {
            _currentScore += Constants.ScoresByFilledCell;
            UpdateText();
        }
        
        private void AddScoresByFilledLine(int lineLength)
        {
            _currentScore += lineLength * Constants.ScoresByCellWhenLineFilled;
            AudioManager.Instance.PlayLineFilledSound();
            UpdateText();
        }

        private void UpdateText()
        {
            _currentScoreText.text = _currentScore.ToString();

            if (_currentScore >= PlayerPrefs.GetInt(MaxScorePFKey))
                UpdateHighestScore();
        }

        private void UpdateHighestScore()
        {
            PlayerPrefs.SetInt(MaxScorePFKey, _currentScore);
            _highestScoreText.text = _currentScore.ToString();
        }
    }
}