using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth;

    [SerializeField] Image healthbarFillImage;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        healthbarFillImage.fillAmount = 1f;
    }


    public void OnGetHit()
    {
        if (enemyHealth.currentHealth <= 0)
        {
            OnEnemyDeath();
            return;
        }

        healthbarFillImage.fillAmount = enemyHealth.currentHealth / enemyHealth.baseHealth;
    }

    public void OnEnemyDeath()
    {
        //Reset the graphic to full fill.
        healthbarFillImage.fillAmount = 1f;

        //Deactivate the bar. 
        gameObject.SetActive(false);

    }
}
