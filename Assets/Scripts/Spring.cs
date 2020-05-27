using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float springForce;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        if(col.tag == "Player")
        {
            rb.velocity = Vector2.up * springForce;
        }
    }
}
