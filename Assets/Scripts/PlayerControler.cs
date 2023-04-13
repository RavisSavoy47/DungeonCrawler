using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public Vector2 movementInput;
    public float movementSpeed = 1.0f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public bool canMove = true;
    public SwordAttack swordAttack;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Rigidbody2D _rigidbody;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //Checks if the player can move or not
        if (canMove)
        {
            //Checks if the player is moving or not
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                //If the success failed move on the x axes
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                //If the success failed move on the y axes
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                //Checks if the player is moving for the animator
                animator.SetBool("isMoving", success);
            }
            //If the player is not moving for the animator
            else { animator.SetBool("isMoving", false); }

            //Sets the sprite to the direction the player is moving
            if (movementInput.x < 0) 
            { 
                spriteRenderer.flipX = true;
            }  
            else if (movementInput.x > 0) 
            { 
                spriteRenderer.flipX = false;
            }
        }
    }
    private bool TryMove(Vector2 direction){
        //Checks if there is no movement
        if (direction != Vector2.zero)
        {
            //checks for the potential collisions
            int count = _rigidbody.Cast(
                direction,
                movementFilter,
                castCollisions,
                movementSpeed * Time.fixedDeltaTime + collisionOffset);

            //Moves the player if their are no collisions
            if (count == 0)
            {
                _rigidbody.MovePosition(_rigidbody.position + direction * movementSpeed * Time.fixedDeltaTime);
                return true;
            }
            else { return false; }
        }
        else { 
            //Returns false if there's no movement
            return false; }
    }

    /// <summary>
    /// Gets player input and moves the player
    /// </summary>
    /// <param name="movementValue">how much to move the player by</param>
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    /// <summary>
    /// Gets the input when the player attacks
    /// </summary>
    void OnFire(){
        //Starts the animator when the player attacks
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    { 
        //Stops the players movement
        LockMovement();
        //Gets what direction the sprite is facing
        if (spriteRenderer.flipX == true) 
        {
            swordAttack.AttackLeft();}
        else 
        { 
            swordAttack.AttackRight();}
    }

    public void EndSwordAttack()
    {
        //Unlocks the players movement
        UnLockMovement();
        //Stops the attack 
        swordAttack.StopAttack();
    }

    /// <summary>
    /// Stops the players movement
    /// </summary>
    public void LockMovement(){
        canMove = false;
    }

    /// <summary>
    /// Lets the player move
    /// </summary>
    public void UnLockMovement(){
        canMove = true; 
    }
}
