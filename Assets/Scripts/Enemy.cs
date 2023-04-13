using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public float speed = 0.5f;
    public float viewrange = 1.0f;
    public PlayerControler player;
    public float collisionOffset = 0.55f;

    private bool isAlive = true;


    SpriteRenderer spriteRenderer;
    Rigidbody2D _rigidbody;
    public float Health
    {
        set{
            health = value;
            //if the enemy died
            if (health <= 0)
            {
                Defeated();
                isAlive = false;
            }
        }
        get { return health; }
    }

    public float health = 1;

    private void Start()
    {
        _rigidbody= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isAlive)
        {
            //Gets the players position
            Vector3 playerPosition = player.GetComponent<Rigidbody2D>().position;

            Vector3 displacement = playerPosition - transform.position;
            displacement = displacement.normalized;

            if (Vector2.Distance(playerPosition, transform.position) < viewrange)
            {
                transform.position += (displacement * speed * Time.deltaTime);
                //Checks if the player is moving for the animator
                animator.SetBool("isMoving", true);
            }
            else { animator.SetBool("isMoving", false); }


            //Sets the sprite to the direction the enemy is moving
            if (displacement.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (displacement.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    /// <summary>
    /// Called when enemy is defeated
    /// </summary>
    public void Defeated() 
    {
        animator.SetTrigger("Defeated");
    }

    /// <summary>
    /// Called after the enemy is defeated
    /// </summary>
    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
