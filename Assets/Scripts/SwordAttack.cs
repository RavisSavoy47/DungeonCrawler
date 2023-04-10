using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Collider2D swordCollider;
    Vector2 attackOffset;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        attackOffset = transform.position;
    }

    /// <summary>
    /// Enables the collider on the right
    /// </summary>
    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.position = attackOffset;
    }

    /// <summary>
    /// Enables the collider on the left
    /// </summary>
    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.position = new Vector3(attackOffset.x * -1, attackOffset.y);
    }

    /// <summary>
    /// Disables  the collider
    /// </summary>
    public void StopAttack()
    { 
        swordCollider.enabled = false;
    }
}
