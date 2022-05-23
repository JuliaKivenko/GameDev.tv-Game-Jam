using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager sharedInstance;
    [SerializeField] GameObject hudPanel;
    [SerializeField] GameObject gameOverPanel;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(true);
    }
    public void ActivateGameOverPanel()
    {
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
