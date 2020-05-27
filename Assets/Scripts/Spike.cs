using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private PlayerMovement pikachu;

    // Start is called before the first frame update
    void Start()
    {
        pikachu = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            pikachu.Damage(2);
            StartCoroutine(pikachu.Knockback(0.02f, 20, pikachu.transform.position));
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
