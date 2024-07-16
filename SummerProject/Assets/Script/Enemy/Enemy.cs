using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float health;
    
    protected LogicGameHandler logicGameHandler;
    
    protected Rigidbody2D rigidbody2D;
    [SerializeField] public float timer;
    float counter = 0;
    public void takeDamage(float damage){
        health -= damage;
    }   
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        logicGameHandler = GameObject.Find("GameLogicHandler").GetComponent<LogicGameHandler>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        counter += Time.deltaTime;
        if(counter > timer){
            Destroy(gameObject);
        }
    }
}
