using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public Vector2 _movementInput;
    public float _movementSpeed = 1.0f;
    public float _collisionOffset = 0.05f;
    public ContactFilter2D _movementFilter;
    public bool _canMove = true;
    public SwordAttack swordAttack;

    List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
    Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //Checks if the player can move or not
        if (_canMove)
        {
            //Checks if the player is moving or not
            if (_movementInput != Vector2.zero)
            {
                bool success = TryMove(_movementInput);

                //If the success failed move on the x axes
                if (!success)
                {
                    success = TryMove(new Vector2(_movementInput.x, 0));
                }
                //If the success failed move on the y axes
                if (!success)
                {
                    success = TryMove(new Vector2(0, _movementInput.y));
                }
                //Checks if the player is moving for the animator
                _animator.SetBool("isMoving", success);
            }
            //If the player is not moving for the animator
            else { _animator.SetBool("isMoving", false); }

            //Sets the sprite to the direction the player is moving
            if (_movementInput.x < 0) 
            { 
                _spriteRenderer.flipX = true;
            }  
            else if (_movementInput.x > 0) 
            { 
                _spriteRenderer.flipX = false;
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
                _movementFilter,
                _castCollisions,
                _movementSpeed * Time.fixedDeltaTime + _collisionOffset);

            //Moves the player if their are no collisions
            if (count == 0)
            {
                _rigidbody.MovePosition(_rigidbody.position + direction * _movementSpeed * Time.fixedDeltaTime);
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
        _movementInput = movementValue.Get<Vector2>();
    }

    /// <summary>
    /// Gets the input when the player attacks
    /// </summary>
    void OnFire(){
        //Starts the animator when the player attacks
        _animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    { 
        LockMovement();
        if (_spriteRenderer.flipX == true) 
        {
            swordAttack.AttackLeft();}
        else 
        { 
            swordAttack.AttackRight();}
    }

    /// <summary>
    /// Stops the players movement
    /// </summary>
    public void LockMovement(){
        _canMove = false;
    }

    /// <summary>
    /// Lets the player move
    /// </summary>
    public void UnLockMovement(){
        _canMove = true;
    }
}
