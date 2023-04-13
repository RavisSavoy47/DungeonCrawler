using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public float speed = 0.5f;
    public float viewrange = 1.0f;
    public PlayerControler player;
    public float collisionOffset = 0.55f;
    public float damage = 1;
    private bool isAlive = true;
    public float deathTime = 0;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

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

    public float health = .5f;

    private void Start()
    {
        _rigidbody= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Checks if enemy is still alive
        if (isAlive)
        {
            //Gets the players position
            Vector3 playerPosition = player.GetComponent<Rigidbody2D>().position;
            //Distance to the player
            Vector2 displacement = playerPosition - transform.position;
            displacement = displacement.normalized;
            //if player is in viewrange enemy moves to player
            if (Vector2.Distance(playerPosition, transform.position) < viewrange)
            {
                //Checks if the player is alive
                if(player.Health > 0 )
                {
                    //moves the enemy 
                    bool success = TryMove(transform.position);

                    //If the success failed move on the x axes
                    if (!success)
                    {
                        success = TryMove(new Vector2(transform.position.x, 0));
                    }
                    //If the success failed move on the y axes
                    if (!success)
                    {
                        success = TryMove(new Vector2(0, transform.position.y));
                    }
                    animator.SetBool("isMoving", true);
                }
                else { animator.SetBool("isMoving", false); }
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

    public void Update()
    {
        //if enemy is dead
        if (!isAlive) 
        {
            //Removes the enemy after time passes
            deathTime += .01f;
            if (deathTime > 30) { RemoveEnemy(); }
        }
        
    }
    private bool TryMove(Vector2 direction)
    {
        //Checks if there is no movement
        if (direction != Vector2.zero)
        {
            //checks for the potential collisions
            int count = _rigidbody.Cast(
                direction,
                movementFilter,
                castCollisions,
                speed * Time.fixedDeltaTime + collisionOffset);

            //Gets the players position
            Vector3 playerPosition = player.GetComponent<Rigidbody2D>().position;
            //Distance to the player
            Vector2 displacement = playerPosition - transform.position;
            displacement = displacement.normalized;

            //Moves the enemy if their are no collisions
            if (count == 0)
            {
                _rigidbody.MovePosition(_rigidbody.position + displacement * speed * Time.fixedDeltaTime);
                return true;
            }
            else 
            {
                //moves the enemy along the wall
                _rigidbody.MovePosition(_rigidbody.position + direction * speed * Time.fixedDeltaTime);
                return false; 
            }
        }
        else
        {
            //Returns false if there's no movement
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Checks if enemy is still alive
        if (isAlive)
        {
            //Checks the collision tag for an enemy
            if (other.tag == "Player")
            {
                //Gets the enemy that it collided with
                PlayerControler player = other.GetComponent<PlayerControler>();

                //Checks if the enemy exsits 
                if (player != null)
                {
                    player.Health -= damage;
                }
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
