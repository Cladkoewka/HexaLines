using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

namespace Assets.Scripts.Scores
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private GameObject _leaderboardElementPrefab;

        private DatabaseReference _database;
        private List<GameObject> _leaderboardElements = new List<GameObject>();

        public void Start()
        {
            _database = FirebaseDatabase.DefaultInstance.RootReference;
            SetPlayersLeaderboard();
        }
        
        public async void SetPlayersLeaderboard()
        {
            foreach (var element in _leaderboardElements) 
                Destroy(element.gameObject);
            _leaderboardElements.Clear();
            
            var playersTask = _database.Child("Players").OrderByChild("Score").LimitToLast(5).GetValueAsync();
            await playersTask;

            if (playersTask.IsFaulted)
                Debug.LogError(playersTask.Exception);
            else if (playersTask.IsCompleted)
            {
                DataSnapshot snapshot = playersTask.Result;

                foreach (DataSnapshot playerSnapshot in snapshot.Children)
                {
                    Player player = Player.CreateFromJSON(playerSnapshot.GetRawJsonValue());
                    if (player != null) 
                        AddLeadearboardElement(player);
                }
            }
        }

        private void AddLeadearboardElement(Player player)
        {
            LeaderboardElement leaderboardElement = 
                Instantiate(_leaderboardElementPrefab, transform).GetComponent<LeaderboardElement>();
            _leaderboardElements.Add(leaderboardElement.gameObject);
            leaderboardElement.SetScore(player.Score);
            leaderboardElement.SetName(player.Name);
        }

        //Kostyl
        public void UpdateLeaderboard()
        {
            StopAllCoroutines();
            StartCoroutine(UpdateLeaderBoardCoroutine());
        }

        private IEnumerator UpdateLeaderBoardCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            SetPlayersLeaderboard();
        }
    }
}