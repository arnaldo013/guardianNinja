using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;
    private Collider2D coll;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int fire = 0;
    [SerializeField] private Text fireText;
    [SerializeField] private float hurtForce = 10f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state);

        if (Input.GetKey(KeyCode.X))
        {
            TakeDamage(20);
        }
    }

    public void Damage(int dmg)
    {
        currentHealth -= dmg;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable") 
        {
            Destroy(collision.gameObject);
            fire += 1;
            fireText.text = fire.ToString();   
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "enemy")
        {
            state = State.hurt;
            TakeDamage(20);
            
            if (other.gameObject.transform.position.x > transform.position.x)
            {
               rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else
            {
               rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }
        }
    }


        void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Movement() 
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }
        else
        {
            anim.SetBool("running", false);
        }
        if (Input.GetKey(KeyCode.W) && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
        }
    }

    private void AnimationState()
    {
        if(state == State.jumping)
        {
            if (rb.velocity.y < 1f)
            {
                state = State.falling;
            }

        else if(state == State.falling)
            {
                if (coll.IsTouchingLayers(ground))
                {
                    state = State.idle;
                } 
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x)> 0)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
}

    
