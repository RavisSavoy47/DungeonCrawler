using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthbar;
    public Image expBar;

    /// <summary>
    /// Set the bar to the health stats
    /// </summary>
    /// <param name="maxHealth">the max amount of health the object has</param>
    /// <param name="currentHealth">the current amount of health the object has</param>
    public void SetHealth(float maxHealth, float currentHealth)
    {
        healthbar.fillAmount = currentHealth / maxHealth;
    }

    public void GainExp(float maxExp, float currentExp)
    {
        expBar.fillAmount = currentExp / maxExp;
    }

    private void Start()
    {
        expBar.fillAmount = 0;
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
            //Sets the canvis to follow the player position
            healthbar.canvas.transform.position = GetComponentInParent<PlayerControler>().transform.position;
            expBar.canvas.transform.position = GetComponentInParent<PlayerControler>().transform.position;
            //Removes the healthbar when player dies
            if (GetComponentInParent<PlayerControler>().health <= 0)
            { Destroy(gameObject); }
        }
    }
}
