using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected float speed;
    protected Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
}
