using Assets.Scripts.Audio;
using Assets.Scripts.Hex;
using Assets.Scripts.Static;
using Firebase.Database;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Scores
{
    public class ScoreCounter
    {
        private TMP_Text _currentScoreText;
        private TMP_Text _highestScoreText;
        private int _currentScore;
        private DatabaseReference _database;
        private Leaderboard _leaderboard;


        public void Init(HexGrid hexGrid, TMP_Text currentScoreText, TMP_Text highestScoreText, Leaderboard leaderboard)
        {
            hexGrid.LineFilled += AddScoresByFilledLine;
            hexGrid.CellFilled += AddScoresByFilledCells;
            _currentScoreText = currentScoreText;
            _highestScoreText = highestScoreText;
            _highestScoreText.text = PlayerPrefs.GetInt(Constants.MaxScorePFKey).ToString();
            
            _database = FirebaseDatabase.DefaultInstance.RootReference;
            _leaderboard = leaderboard;
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

            if (_currentScore >= PlayerPrefs.GetInt(Constants.MaxScorePFKey))
                UpdateHighestScore();
        }

        private void UpdateHighestScore()
        {
            PlayerPrefs.SetInt(Constants.MaxScorePFKey, _currentScore);
            _highestScoreText.text = _currentScore.ToString();

            UpdateLeaderboard();
        }

        private void UpdateLeaderboard()
        {
            string playerName = PlayerPrefs.GetString(Constants.PlayerNamePFKey);

            Player player = new Player(playerName, _currentScore);

            _database.Child("Players").Child(playerName).SetRawJsonValueAsync(JsonUtility.ToJson(player));
            _leaderboard.UpdateLeaderboard();
        }
    }
}