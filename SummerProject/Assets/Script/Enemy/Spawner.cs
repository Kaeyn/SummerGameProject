using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] float spawnRate;
    float counter = 0;
    [SerializeField]  GameObject thingToSpanwn;

    [SerializeField] float  randomRangeX;
    [SerializeField] float  randomRangeY;

    LogicGameHandler logicGameHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        logicGameHandler = GameObject.Find("GameLogicHandler").GetComponent<LogicGameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!logicGameHandler.isBossSpawn)
        {
            timer -= Time.deltaTime;
            if(timer <= 0){
                counter += Time.deltaTime;
                float randomX = UnityEngine.Random.Range(-randomRangeX, randomRangeX);
                float randomY = UnityEngine.Random.Range(-randomRangeY, randomRangeY);
                if (counter > spawnRate)
                {
                    Instantiate(thingToSpanwn, new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z), Quaternion.identity);
                    counter = 0;
                }
            }
        }
        
    }
}
