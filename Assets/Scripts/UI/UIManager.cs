using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("HUD")]
    [SerializeField] GameObject hudPanel;
    [SerializeField] TextMeshProUGUI hudDistanceCounter;
    [SerializeField] TextMeshProUGUI hudPointsCounter;

    [Header("Game Over Screen")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI gameOverDistanceCounter;
    [SerializeField] TextMeshProUGUI gameOverPointsCounter;
    [SerializeField] Button continueButton;
    [SerializeField] float waitBeforeEnableContinue = 1.5f;
    [SerializeField] TextMeshProUGUI bestDistanceText;
    [SerializeField] TextMeshProUGUI bestPointsText;

    [Header("Start Screen")]
    [SerializeField] GameObject startPanel;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] AudioClip pressStartSound;
    [SerializeField] UpgradeDisplay[] upgradeDisplays;

    [Header("Tutorial")]
    [SerializeField] GameObject tutorialCanvas;
    [SerializeField] Button tutorialProceedButton;

    bool isRunStarted;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnEnable() => Upgrade.onBuyUpgrade += OnBuyUpgrade;
    private void OnDisable() => Upgrade.onBuyUpgrade -= OnBuyUpgrade;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(false);
        startPanel.SetActive(true);

        hudPointsCounter.text = "0";
        pointsText.text = "0";

        isRunStarted = false;
    }

    private void Update()
    {
        if (isRunStarted)
        {
            hudPointsCounter.text = GameManager.instance.points.ToString();
            hudDistanceCounter.text = GameManager.instance.distance.ToString() + "m";
        }
    }

    public void ActivateGameOverPanel()
    {
        isRunStarted = false;

        continueButton.interactable = false;
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        StartCoroutine(WaitBeforeContinue(continueButton));

        gameOverDistanceCounter.text = GameManager.instance.distance.ToString() + " m";
        gameOverPointsCounter.text = GameManager.instance.pointsForThisRun.ToString();

        bestDistanceText.text = GameManager.instance.bestDistance.ToString() + " m";
        bestPointsText.text = GameManager.instance.bestPoints.ToString();
    }

    IEnumerator WaitBeforeContinue(Button buttonToEnable)
    {
        yield return new WaitForSeconds(waitBeforeEnableContinue);
        buttonToEnable.interactable = true;
    }

    public void ActivateStartPanel()
    {
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);
        pointsText.text = GameManager.instance.points.ToString();
    }

    public void BeginRun()
    {
        startPanel.SetActive(false);
        hudPanel.SetActive(true);
        SoundManager.PlaySound(pressStartSound);

        if (GameManager.instance.isFirstRun)
        {
            tutorialCanvas.SetActive(true);
            tutorialProceedButton.interactable = false;
            GameManager.instance.isFirstRun = false;
            StartCoroutine(WaitBeforeContinue(tutorialProceedButton));
            return;
        }
        else
        {
            tutorialCanvas.SetActive(false);
        }

        isRunStarted = true;
        GameManager.instance.StartGame();
    }

    public void OnBuyUpgrade()
    {
        pointsText.text = GameManager.instance.points.ToString();
    }

    public void UpdateVisualsAfterLoad()
    {
        pointsText.text = GameManager.instance.points.ToString();
        foreach (UpgradeDisplay upgradeDisplay in upgradeDisplays)
        {
            upgradeDisplay.UpdateVisual();
        }
    }

}
