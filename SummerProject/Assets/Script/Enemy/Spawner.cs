using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float timer;
    protected float counter = 0;
    [SerializeField] GameObject thingToSpanwn;

    [SerializeField] float  randomRangeX;
    [SerializeField] float  randomRangeY;
    
    [SerializeField] GameObject player;
    [SerializeField] bool isFollowPlayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowPlayer){
            transform.LookAt(player.transform.position);
            // transform.rotation = Quaternion.Euler(transform.rotation.x,180,transform.rotation.z);
        }
        counter += Time.deltaTime;
        float randomX = UnityEngine.Random.Range(-randomRangeX,randomRangeX);
        float randomY = UnityEngine.Random.Range(-randomRangeY,randomRangeY);
        if(counter > timer){
            Instantiate(thingToSpanwn,new Vector3(transform.position.x + randomX,transform.position.y + randomY,transform.position.z),Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z));
            counter = 0;
        }
    }
}
