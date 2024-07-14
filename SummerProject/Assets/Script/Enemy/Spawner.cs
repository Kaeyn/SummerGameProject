using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float timer;
    float counter = 0;
    [SerializeField]  GameObject thingToSpanwn;

    [SerializeField] float  randomRangeX;
    [SerializeField] float  randomRangeY;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
        void Update()
    {
        counter += Time.deltaTime;
        float randomX = UnityEngine.Random.Range(-randomRangeX,randomRangeX);
        float randomY = UnityEngine.Random.Range(-randomRangeY,randomRangeY);
        if(counter > timer){
            Instantiate(thingToSpanwn,new Vector3(transform.position.x + randomX,transform.position.y + randomY,transform.position.z),Quaternion.identity);
            counter = 0;
        }
    }
}
