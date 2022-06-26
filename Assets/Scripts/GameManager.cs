using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] AudioClip pointCollectSfx;
    [SerializeField] SaveData saveData;

    public int points { get { return _points; } set { } }
    int _points = 0;
    public int pointsForThisRun { get { return _points; } set { } }
    int _pointsForThisRun = 0;

    public float distance { get { return _distance; } set { } }
    float _distance = 0;

    public float bestDistance { get { return _distance; } set { } }
    float _bestDistance = 0;

    public float bestPoints { get { return _distance; } set { } }
    float _bestPoints = 0;

    public bool isFirstRun { get { return _isFirstRun; } set { } }
    bool _isFirstRun = true;

    public delegate void GameOverAction();
    public static event GameOverAction onGameOver;

    public delegate void GameStartAction();
    public static event GameStartAction onGameStart;

    bool isRunStarted;
    float gameTimePassed;


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

    private void Start()
    {
        gameTimePassed = 0;
        saveData.Load();
        StopGame();
        UIManager.instance.UpdateVisualsAfterLoad();
    }

    private void Update()
    {
        if (isRunStarted)
        {
            gameTimePassed += Time.deltaTime;
            _distance = Mathf.Round(gameTimePassed * LevelManager.instance.GetLevelSpeed());
        }
    }

    public void StopGame()
    {
        isRunStarted = false;
        LevelManager.instance.StopLevelGeneration();
    }

    public void StartGame()
    {
        PlayerController.instance.ActivatePlayerCharacter();
        LevelManager.instance.StartLevelGeneration();
        _distance = 0;
        _pointsForThisRun = 0;
        gameTimePassed = 0;
        isRunStarted = true;

        saveData.Save();

        if (onGameStart != null)
            onGameStart.Invoke();

    }

    public void AddPoints(int pointsToAdd)
    {
        _points += pointsToAdd;
        _pointsForThisRun += pointsToAdd;
        SoundManager.PlaySound(pointCollectSfx);
    }

    public void SubstractPoints(int pointsToSubstract)
    {
        _points -= pointsToSubstract;
    }

    public void LoadDataFromSave(int savedPoints, float savedbestDistance, float savedbestPoints, bool isFirstRun)
    {
        _points = savedPoints;
        _bestDistance = savedbestDistance;
        _bestPoints = savedbestPoints;
        _isFirstRun = isFirstRun;
    }

    public void GameOver()
    {
        if (_distance > bestDistance)
        {
            bestDistance = _distance;
        }
        if (_pointsForThisRun > bestPoints)
        {
            bestPoints = _pointsForThisRun;
        }

        UIManager.instance.ActivateGameOverPanel();

        foreach (GameObject enemyGameObject in EnemyObjectPool.sharedInstance.pooledObjects)
        {
            enemyGameObject.GetComponent<EnemyHealth>().ResetHealth();
        }

        isRunStarted = false;

        saveData.Save();

        if (onGameOver != null)
            onGameOver.Invoke();
    }





}
