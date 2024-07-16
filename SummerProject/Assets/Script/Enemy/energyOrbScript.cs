using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyOrbScript : Projectile
{
    // Start is called before the first frame update
    [SerializeField] Vector3 colidePosition;
    GameObject skillHolder;
    void Start()
    {
        skillHolder = GameObject.Find("2ndSkill").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // colidePosition = skillHolder.transform.position;
        // transform.position = Vector3.MoveTowards(transform.position, colidePosition, 5f * Time.deltaTime);
    }
}
