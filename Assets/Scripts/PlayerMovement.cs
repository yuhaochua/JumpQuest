using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
//These are variables related to the character's movement, interactions with the platform
    public float speed;
    public float jumpForce;
    private float moveInput;
    public Joystick joystick;
    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool isClimbing;
    public float distance;
    public LayerMask whatIsRope;
    private float inputVertical;

//This variable is referring to the character
    private Rigidbody2D rb;

//These variables are responsible for the animation, health and damage taking for the character
    private Animator animator;
    public int currentHealth;
    public int maxHealth;
    public float stopwatch = 0;
    
//function to call when character dies
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check if the character is on the ground, mainly to see if it is possible for it the jump.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);



        //controls orientation of character
        void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }

        //This part is for character to climb ropes
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsRope);

        if(hitInfo.collider != null)
        {
            if (joystick.Vertical >= .2f)
            {
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
        }

        if (isClimbing == true)
        {
            inputVertical = joystick.Vertical;
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * speed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 3;
        }
    }

    void Update()
    {
        //Movement script for character(left right)
        if(joystick.Horizontal >= .2f)
        {
            moveInput = 1;
        }else if (joystick.Horizontal <= -.2f)
        {
            moveInput = -1;
        }
        else
        {
            moveInput = 0;
        }

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //animation for character's movement
        if (moveInput == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        if (isClimbing == true)
        {
            animator.SetBool("isClimbing", true);
        }
        else
        {
            animator.SetBool("isClimbing", false);
        }

        //For character to die when health count reaches 0
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

    }

    //To minus heart count as damage and animation for taking damage
    public void Damage(int dmg)
    {
        currentHealth -= dmg;
        FindObjectOfType<AudioManager>().Play("pain");
        animator.Play("PikachuDamage");
    }


    public IEnumerator Knockback(float knockDuration, float knockPower, Vector3 direction)
    {
        float timer = 0;
        while(knockDuration > timer)
        {
            timer += Time.deltaTime;
            rb.AddForce(new Vector3(direction.x, direction.y * knockPower, transform.position.z));
        }

        yield return 0;
    }

    //Script for character to jump
    public void Jump()
    {
        if (isGrounded == true)
        {
            animator.SetTrigger("JumpTrigger");
            animator.SetBool("isJumping", true);
            rb.velocity = Vector2.up * jumpForce;
            FindObjectOfType<AudioManager>().Play("pika");
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }
}
