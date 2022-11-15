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
        _rigidbody= GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Checks if the player is moving or not
        if (_movementInput != Vector2.zero){
            //checks for the potential collisions
            int count = _rigidbody.Cast(
                _movementInput, 
                _movementFilter, 
                _castCollisions, 
                _movementSpeed * Time.fixedDeltaTime + _collisionOffset);
            //Moves the player if their are no collisions
            if (count == 0){
                _rigidbody.MovePosition(_rigidbody.position + _movementInput * _movementSpeed * Time.fixedDeltaTime);
            }
        }
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
