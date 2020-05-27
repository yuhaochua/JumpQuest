using UnityEngine;

public class Elp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("elp");
        }
    }
}
