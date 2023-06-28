using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    Vector2 attackOffset;
    public float damage = 3;

    // Start is called before the first frame update
    void Start()
    {
            attackOffset = transform.localPosition;
    }

    /// <summary>
    /// Enables the collider on the right
    /// </summary>
    public void AttackRight()
    {
        swordCollider.enabled = true;

        if (swordCollider.GetComponentInParent<PlayerControler>())
            transform.localPosition = attackOffset;

        if (swordCollider.GetComponentInParent<Enemy>())
            transform.localPosition = new Vector3(attackOffset.x * -1, attackOffset.y);
    }

    /// <summary>
    /// Enables the collider on the left
    /// </summary>
    public void AttackLeft()
    {
        swordCollider.enabled = true;
        if (swordCollider.GetComponentInParent<PlayerControler>())
            transform.localPosition = new Vector3(attackOffset.x * -1, attackOffset.y);

        if (swordCollider.GetComponentInParent<Enemy>())
            transform.localPosition = attackOffset;
    }

    /// <summary>
    /// Disables  the collider
    /// </summary>
    public void StopAttack()
    { 
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Checks the parent to see if its the enemy
        if (swordCollider.GetComponentInParent<Enemy>())
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
                    player.healthBar.SetHealth(player.maxHealth, player.health);
                }
            }
        }
        //Checks the parent to see if its the player
        if (swordCollider.GetComponentInParent<PlayerControler>())
        {
            //Checks the collision tag for an enemy
            if (other.tag == "Enemy")
            {
                //Gets the enemy that it collided with
                Enemy enemy = other.GetComponent<Enemy>();
                //Checks if the enemy exsits 
                if (enemy != null)
                {
                    enemy.Health -= damage;
                    enemy.healthBar.SetHealth(enemy.maxHealth, enemy.health);                   
                }
            }
        }
    }
}
