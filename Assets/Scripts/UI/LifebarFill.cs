using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifebarFill : MonoBehaviour
{

    Image healthBar;

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }
    void Update()
    {
        healthBar.fillAmount = PlayerController.sharedInstance.health.currentHealth / PlayerController.sharedInstance.health.baseHealth;
    }
}
