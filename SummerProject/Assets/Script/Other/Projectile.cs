using System.Collections;
using UnityEngine;

    public class Projectile : MonoBehaviour
    {
        public float speed = 10f;
        private Vector3 direction;
        protected LogicGameHandler logicGameHandler;
        void Start() {
            logicGameHandler = GameObject.Find("GameLogicHandler").GetComponent<LogicGameHandler>();
        }

        void Update()
        {
            if(logicGameHandler.isBossSpawn){
                Destroy(gameObject);
            }
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
            if (other.CompareTag("blockable"))
            {
                Destroy(gameObject);
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "blockable")
            {
                Destroy(gameObject);
            }
        }
    }