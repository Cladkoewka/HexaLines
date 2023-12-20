
using UnityEngine;

namespace Assets.Scripts.Scores
{
    [System.Serializable]
    public class Player
    {
        public string Name;
        public int Score;

        public Player() { }

        public Player(string name)
        {
            Name = name;
        }

        public Player(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public static Player CreateFromJSON(string jsonString) => 
            JsonUtility.FromJson<Player>(jsonString.Trim('\"').Replace("\\\"", "\""));
    }
}