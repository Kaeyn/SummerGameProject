using System.Collections;
using UnityEngine;

    public class Projectile : MonoBehaviour
    {
        public float speed = 10f;
        private Vector3 direction;

        void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        public void SetDirection(Vector3 newDirection)
        {
            direction = newDirection;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            /*if (other.CompareTag("Player"))
            {
                // Handle collision with player
                other.GetComponent<LogicGameHandler>().gameover(); // Destroy the projectile
            }*/
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
    }