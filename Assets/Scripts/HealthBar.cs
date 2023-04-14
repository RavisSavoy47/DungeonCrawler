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
        healthbar.canvas.transform.position = GetComponentInParent<Enemy>().transform.position;
        if(GetComponentInParent<Enemy>().health <= 0 )
        { Destroy(gameObject); }
    }
}
