
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] float spawnRate;
    float counter = 0;
    [SerializeField] GameObject thingToSpawn;

    [SerializeField] float randomRangeX;
    [SerializeField] float randomRangeY;

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
            if (timer <= 0)
            {
                counter += Time.deltaTime;
                float randomX = UnityEngine.Random.Range(-randomRangeX, randomRangeX);
                if (counter > spawnRate)
                {
                    Instantiate(thingToSpawn, new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z), Quaternion.identity);
                    counter = 0;
                }
            }
        }
    }
}
