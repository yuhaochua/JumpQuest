using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsCount : MonoBehaviour
{
    public Sprite[] HeartSprites;
    public Image HeartUI;
    private PlayerMovement pikachu;

    // Start is called before the first frame update
    void Start()
    {
        pikachu = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        HeartUI.sprite = HeartSprites[pikachu.currentHealth];
    }
}
