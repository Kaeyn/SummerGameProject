using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pipe : Enemy
{
    LogicGameHandler logicGameHandler;
    protected override void Start()
    {
        base.Start();
        logicGameHandler = GameObject.Find("GameLogicHandler").GetComponent<LogicGameHandler>();
        CreatePipe();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (!logicGameHandler.isBossSpawn)
        {
            if (!logicGameHandler.isGameOver)
            {
                rigidbody2D.velocity = new Vector2(Vector2.left.normalized.x * speed, rigidbody2D.velocity.y);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(0, 0);
            }
        }
    }

    void CreatePipe()
    {

    }
}
