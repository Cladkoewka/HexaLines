using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _placeSounds;
        [SerializeField] private AudioSource _lineSound;
        [SerializeField] private AudioSource _coinSound;


        public static AudioManager Instance;

        public void Init()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        public void PlayLineFilledSound()
        {
            _lineSound.Play();
        }

        public void PlayPlaceFigureSound()
        {
            foreach (var source in _placeSounds)
            {
                if (!source.isPlaying)
                {
                    source.Play();
                    break;
                }
            }
        }

        public void PlayCoinSound() => 
            _coinSound.Play();
    }
}