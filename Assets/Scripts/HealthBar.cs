using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthbar;

    public void SetHealth(float maxHealth, float currentHealth)
    {
        healthbar.fillAmount = currentHealth / maxHealth;
    }

    private void Update()
    {
        //Checks if the parent is the enemy
        if (GetComponentInParent<Enemy>())
        {
            //Sets the canvis to follow the enemy position
            healthbar.canvas.transform.position = GetComponentInParent<Enemy>().transform.position;
            //Removes the healthbar when enemy dies
            if (GetComponentInParent<Enemy>().health <= 0)
            { Destroy(gameObject); }
        }
        //Checks if the parent is the player
        if (GetComponentInParent<PlayerControler>())
        {
            //Sets the canvis to follow the enemy position
            healthbar.canvas.transform.position = GetComponentInParent<PlayerControler>().transform.position;
            //Removes the healthbar when enemy dies
            if (GetComponentInParent<PlayerControler>().health <= 0)
            { Destroy(gameObject); }
        }
    }
}
