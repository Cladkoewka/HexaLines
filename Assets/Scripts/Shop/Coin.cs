using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class Coin : MonoBehaviour
    {    
        [SerializeField] private float _jumpForce = 5f;       
        [SerializeField] private float _lifetime = 3f;         

        private void Start()
        {
            StartCoroutine(JumpAndDestroy());
        }

        private IEnumerator JumpAndDestroy()
        {
            Vector2 jumpDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;

            float elapsedTime = 0f;

            while (elapsedTime < _lifetime)
            {
                transform.Translate(jumpDirection * _jumpForce * Time.deltaTime, Space.World);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}