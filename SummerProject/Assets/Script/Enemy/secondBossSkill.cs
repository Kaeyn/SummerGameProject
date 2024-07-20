using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class secondBossSkill : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject upperOrb, belowOrb, laser;
    [SerializeField] Vector3 upperPosition = new Vector3(5, 4, 0);
    [SerializeField] Vector3 belowPosition = new Vector3(5, -4, 0);
    [SerializeField] Vector3 collidePosition = new Vector3(5, 0, 0);
    GameObject orbA, orbB, laserBeam;
    private bool orbsCollided = false;
    private float collisionTimer = 1f;
    private float moveSpeed = 3f;
    private float threshold = 0.1f;
    void Start()
    {
        orbA = Instantiate(upperOrb, upperPosition, Quaternion.identity);
        orbB = Instantiate(belowOrb, belowPosition, Quaternion.identity);
    }
    void Update()
    {
        if (!orbsCollided)
        {
            if (orbA && orbB)
                ActivateLaser();
        }
        else
        {
            HandleCollisionTimer();
        }
    }
    private void ActivateLaser()
    {
        orbA.transform.position = Vector3.MoveTowards(orbA.transform.position, collidePosition, moveSpeed * Time.deltaTime);
        orbB.transform.position = Vector3.MoveTowards(orbB.transform.position, collidePosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(orbA.transform.position, collidePosition) < threshold &&
            Vector3.Distance(orbB.transform.position, collidePosition) < threshold)
        {
            orbsCollided = true;
        }
    }
    private void HandleCollisionTimer()
    {
        collisionTimer -= Time.deltaTime;
        if (collisionTimer <= 0)
        {
            Destroy(orbA);
            Destroy(orbB);
            laserBeam = Instantiate(laser, new Vector3(-2, 0, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
            Destroy(laserBeam, 2f);  // Destroy the laserBeam after 2 seconds
            orbsCollided = false;  // Reset if you want to repeat this behavior.
        }
    }
}
