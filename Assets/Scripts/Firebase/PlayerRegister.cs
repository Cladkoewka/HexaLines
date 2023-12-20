using Assets.Scripts.Static;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Firebase
{
    public class PlayerRegister : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputNameField;
        [SerializeField] private Button _playButton;
        [SerializeField] private RegisterDB _registerDb;
        
        

        public void Start()
        {
            _inputNameField.onValueChanged.AddListener(ShowButton);
            _playButton.onClick.AddListener(StartGame);
            
            _playButton.gameObject.SetActive(false);
        }

        private void StartGame()
        {
            Register();
        }

        private void Register()
        {
            string playerName = _inputNameField.text;
            _registerDb.LoginPlayer(playerName);
            PlayerPrefs.SetString(Constants.PlayerNamePFKey, playerName);
            _registerDb.UpdatePlayerMaxScore(playerName);
        }

        private void ShowButton(string inputString) => 
            _playButton.gameObject.SetActive(inputString.Length > 0);
    }
}