using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager sharedInstance;

    [Header("HUD")]
    [SerializeField] GameObject hudPanel;
    [SerializeField] TextMeshProUGUI hudDistanceCounter;
    [SerializeField] TextMeshProUGUI hudPointsCounter;

    [Header("Game Over Screen")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI gameOverDistanceCounter;
    [SerializeField] TextMeshProUGUI gameOverPointsCounter;

    [Header("Start Screen")]
    [SerializeField] GameObject startPanel;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(false);
        startPanel.SetActive(true);

        //in the future this will hold value of the points player has overall
        hudPointsCounter.text = "0";
    }

    private void Update()
    {
        hudPointsCounter.text = GameManager.sharedInstance.points.ToString();
        hudDistanceCounter.text = GameManager.sharedInstance.distance.ToString() + "m";
    }
    public void ActivateGameOverPanel()
    {
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverDistanceCounter.text = GameManager.sharedInstance.distance.ToString() + "m";
        gameOverPointsCounter.text = GameManager.sharedInstance.pointsForThisRun.ToString();
    }

    public void ActivateStartPanel()
    {
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void DeactivateStartPanel()
    {
        startPanel.SetActive(false);
        hudPanel.SetActive(true);

        //remove this in the future maybe and make game manager subscribe to onclick if possible?
        GameManager.sharedInstance.StartGame();
    }
}
