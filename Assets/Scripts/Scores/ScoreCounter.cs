using Assets.Scripts.Hex;
using Assets.Scripts.Static;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Scores
{
    public class ScoreCounter
    {
        private TMP_Text _currentScoreText;
        private TMP_Text _highestScoreText;
        private int _currentScore;

        public void Init(HexGrid hexGrid, TMP_Text currentScoreText, TMP_Text highestScoreText)
        {
            hexGrid.LineFilled += AddScoresByFilledLine;
            hexGrid.CellFilled += AddScoresByFilledCells;
            _currentScoreText = currentScoreText;
            _highestScoreText = highestScoreText;
        }


        private void AddScoresByFilledCells()
        {
            _currentScore += Constants.ScoresByFilledCell;
            UpdateText();
        }
        
        private void AddScoresByFilledLine(int lineLength)
        {
            _currentScore += lineLength * Constants.ScoresByCellWhenLineFilled;
            UpdateText();
        }

        private void UpdateText()
        {
            _currentScoreText.text = _currentScore.ToString();

            if (_currentScore >= PlayerProgress.HighestScores) 
                UpdateHighestScore();
        }

        private void UpdateHighestScore()
        {
            PlayerProgress.HighestScores = _currentScore;
            _highestScoreText.text = PlayerProgress.HighestScores.ToString();
        }
    }
}