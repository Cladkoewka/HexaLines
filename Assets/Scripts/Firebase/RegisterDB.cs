using System.Collections;
using Assets.Scripts.Scores;
using Assets.Scripts.Static;
using Firebase.Database;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Firebase
{
    public class RegisterDB : MonoBehaviour
    {
        private DatabaseReference _database;

        public void Start()
        {
            _database = FirebaseDatabase.DefaultInstance.RootReference;
        }

        public async void LoginPlayer(string playerName)
        {
            var playerTask = _database.Child("Players").Child(playerName).GetValueAsync();
            await playerTask;
            
            if(playerTask.IsFaulted)
                Debug.LogError(playerTask.Exception);
            else if (!playerTask.Result.Exists)
            {
                Debug.Log("Register Player");
                RegisterNewPlayer(playerName);
            }
            else
                Debug.Log("Player is already exist");
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public async void RegisterNewPlayer(string playerName)
        {
            Player newPlayer = new Player(playerName);
            string newPlayerJson = JsonUtility.ToJson(newPlayer);
            await _database.Child("Players").Child(playerName).SetValueAsync(newPlayerJson);
        }

        public async void UpdatePlayerMaxScore(string playerName)
        {
            var playerTask = _database.Child("Players").Child(playerName).GetValueAsync();
            await playerTask;

            if (playerTask.IsFaulted)
            {
                Debug.LogError(playerTask.Exception);
            }

            DataSnapshot snapshot = playerTask.Result;

            if (snapshot != null && snapshot.Exists)
            {
                Player player = Player.CreateFromJSON(snapshot.GetRawJsonValue());
                Debug.Log($"Max Score {PlayerPrefs.GetInt(Constants.MaxScorePFKey)}");
                PlayerPrefs.SetInt(Constants.MaxScorePFKey, player.Score);
            }
            else
            {
                Debug.LogWarning("Player data not found for: " + playerName);
                PlayerPrefs.SetInt(Constants.MaxScorePFKey, 0);
            }
        }

    }
}