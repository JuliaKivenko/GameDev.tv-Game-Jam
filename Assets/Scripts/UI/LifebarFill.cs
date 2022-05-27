using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifebarFill : MonoBehaviour
{

    Image healthBar;

    [SerializeField] TextMeshProUGUI healthbarText;

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }
    void Update()
    {
        healthBar.fillAmount = PlayerController.sharedInstance.health.currentHealth / PlayerController.sharedInstance.health.baseHealth;
        healthbarText.text = PlayerController.sharedInstance.health.currentHealth + "/" + PlayerController.sharedInstance.health.baseHealth;
    }
}
