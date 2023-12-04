using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class FlyScores : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 2f;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private TMP_Text _text;


        public void Init(int scores)
        {
            _text.text = scores.ToString();
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            float timer = 0;
            while (timer < _lifeTime)
            {
                transform.position += Vector3.up * _speed * Time.deltaTime;
                timer += Time.deltaTime;
                _text.alpha = _lifeTime - timer;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}