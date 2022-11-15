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

    List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
    Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Checks if the player is moving or not
        if (_movementInput != Vector2.zero)
        {
            bool success = TryMove(_movementInput);

            //If the success failed move on the x axes
            if (!success)
            {
                success = TryMove(new Vector2(_movementInput.x, 0));
                //If success failed move on the y axes
                if (!success)
                {
                    success = TryMove(new Vector2(0, _movementInput.y));
                }
            }
        }
    }
    private bool TryMove(Vector2 direction)
    {
        //checks for the potential collisions
        int count = _rigidbody.Cast(
            direction,
            _movementFilter,
            _castCollisions,
            _movementSpeed * Time.fixedDeltaTime + _collisionOffset);

        //Moves the player if their are no collisions
        if (count == 0){
            _rigidbody.MovePosition(_rigidbody.position + direction * _movementSpeed * Time.fixedDeltaTime);
            return true;
        }
        else { return false; }
    }

    /// <summary>
    /// Gets player input and moves the player
    /// </summary>
    /// <param name="movementValue">how much to move the player by</param>
    void OnMove(InputValue movementValue)
    {
        _movementInput = movementValue.Get<Vector2>();
    }

}
