using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class firstBossSkill : Projectile
{
    // Start is called before the first frame update
    [SerializeField] float counterTime = 6f;
    private Vector3 targetPosition;
    private GameObject player;
    void Start()
    {
        
        if (player != null)
        {
            targetPosition = new Vector3(player.transform.position.x - 5, player.transform.position.y, player.transform.position.z);
        }
        else{
            targetPosition = new Vector3(-12,0,0);
        }
    }
    void Update()
    {
        HandleCounter();
    }
    private void HandleCounter()
    {
        counterTime -= Time.deltaTime;
        if (counterTime <=3)
        {
            Activate();
        }
    }
    private void Activate()
    {
        if (counterTime <= 0)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            // Check if out of bounds
            if (transform.position.x < -10)
            {
                Destroy(gameObject);
            }
        }
    }
}
