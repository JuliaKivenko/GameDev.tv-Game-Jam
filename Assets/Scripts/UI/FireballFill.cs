using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireballFill : MonoBehaviour
{
    Image fireBallBar;

    void Start()
    {
        fireBallBar = GetComponent<Image>();
    }

    void Update()
    {
        fireBallBar.fillAmount = PlayerController.instance.fireballRechargeProgress;
    }
}
