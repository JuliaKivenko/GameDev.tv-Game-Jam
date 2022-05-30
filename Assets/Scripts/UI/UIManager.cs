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
    [SerializeField] Button continueButton;
    [SerializeField] TextMeshProUGUI bestDistanceText;
    [SerializeField] TextMeshProUGUI bestPointsText;

    [Header("Start Screen")]
    [SerializeField] GameObject startPanel;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] AudioClip pressStartSound;

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
        pointsText.text = "0";
    }

    private void Update()
    {
        hudPointsCounter.text = GameManager.sharedInstance.points.ToString();
        hudDistanceCounter.text = GameManager.sharedInstance.distance.ToString() + "m";
    }
    public void ActivateGameOverPanel()
    {
        continueButton.interactable = false;
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);


        StartCoroutine(WaitBeforeContinue());

        gameOverDistanceCounter.text = GameManager.sharedInstance.distance.ToString() + " m";
        gameOverPointsCounter.text = GameManager.sharedInstance.pointsForThisRun.ToString();

        bestDistanceText.text = GameManager.sharedInstance.bestDistance.ToString() + " m";
        bestPointsText.text = GameManager.sharedInstance.bestPoints.ToString();

    }

    public void ActivateStartPanel()
    {
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);
        pointsText.text = GameManager.sharedInstance.points.ToString();
        Upgrade.onBuyUpgrade += OnBuyUpgrade;
    }

    public void DeactivateStartPanel()
    {
        startPanel.SetActive(false);
        hudPanel.SetActive(true);
        SoundManager.PlaySound(pressStartSound);
        Upgrade.onBuyUpgrade -= OnBuyUpgrade;

        //remove this in the future maybe and make game manager subscribe to onclick if possible?
        GameManager.sharedInstance.StartGame();
    }

    public void OnBuyUpgrade()
    {
        pointsText.text = GameManager.sharedInstance.points.ToString();
    }

    IEnumerator WaitBeforeContinue()
    {
        yield return new WaitForSeconds(2f);
        continueButton.interactable = true;
    }
}
