using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health
    {
        set{
            health = value;

            if (health <= 0)
            {
                Defeated();
            }
        }
        get { return health; }
    }

    public float health = 1;

    public void TakeDamage(float damage) 
    { 
        Health -= damage;
    }

    public void Defeated() 
    {
        //Destroy(gameobject);
    }
}