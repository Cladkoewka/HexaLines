using TMPro;
using UnityEngine;

namespace Assets.Scripts.Scores
{
    public class LeaderboardElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _scoreText;

        public void SetName(string name) => 
            _nameText.text = name;

        public void SetScore(int score) => 
            _scoreText.text = score.ToString();
    }
}