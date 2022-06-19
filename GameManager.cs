using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Linq;

public class GameManager : MonoBehaviour, IPointerClickHandler
{
    public static GameManager instance;

    public Action OnStartGame;
    public Action OnRestartGame;
    public Action<int> OnCoinAdd;
    public Action<int> OnKnifeChange;
    public Action OnLevelChange;

    private List<PoolObject> poolData;
    private List<Stage> stages;
    private Stage activeStage;

    private ISpawner spawner;
    private IPlayerUI playerUI;
    private ILog log;
    private IWallet wallet;

    private bool gameStarted = false;
    void Awake()
    {
        stages = Resources.LoadAll<Stage>("Stages").ToList();
        poolData = Resources.LoadAll<PoolObject>("PoolObject").ToList();

        spawner = FindObjectOfType<Spawner>();
        log = FindObjectOfType<Log>();
        playerUI = FindObjectOfType<PlayerUI>();

        wallet = new Wallet();

        instance = this;

        spawner.PreparationPool(poolData[0]);
        spawner.PreparationPool(poolData[1]);
    }

    public void StartGame()
    {
        gameStarted = true;
        OnStartGame?.Invoke();
        LevelGeneration();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ThrowKnife();
    }
    private void OnEnable()
    {
        OnRestartGame += wallet.Reset;
        OnRestartGame += playerUI.Reset;
        OnRestartGame += CleanLevel;
        OnRestartGame += log.StopRotation;
        OnStartGame += playerUI.DisableTapToStart;
        OnStartGame += log.StartGame;
        OnCoinAdd += wallet.AddCoin;
        OnCoinAdd += UpdateTxCoin;
        OnKnifeChange += playerUI.ChangeTxKnifeCount;
        OnLevelChange += CleanLevel;
        OnLevelChange += LevelGeneration;
    }
    private void OnDisable()
    {
        OnRestartGame -= wallet.Reset;
        OnRestartGame -= CleanLevel;
        OnRestartGame -= playerUI.Reset;
        OnRestartGame -= log.StopRotation;
        OnStartGame -= playerUI.DisableTapToStart;
        OnStartGame -= log.StartGame;
        OnCoinAdd -= wallet.AddCoin;
        OnCoinAdd -= UpdateTxCoin;
        OnKnifeChange -= playerUI.ChangeTxKnifeCount;
        OnLevelChange -= LevelGeneration;
        OnLevelChange -= CleanLevel;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameStarted != false)
        {
            ThrowKnife();
        }
    }
    private void LevelGeneration()
    {
        activeStage = stages[UnityEngine.Random.Range(0, stages.Count)];
        SpawnApple(activeStage.countApple);
        log.SetMaxHits(activeStage.countKnife);
        OnKnifeChange?.Invoke(activeStage.countKnife);
    }
    private void CleanLevel()
    {
        spawner.DispawnAll();
    }
    private void SpawnApple(int appleCount)
    {
        for (int i = 0; i < appleCount; i++)
        {
            spawner.SpawnObject(poolData[0]).TryGetComponent(out Apple apple);
            apple.Stand();
        }
    }
    private void ThrowKnife()
    {
        spawner.SpawnObject(poolData[1]);
    }
    private void UpdateTxCoin(int unused)
    {
        playerUI.ChangeTxCoin(wallet.GetCoinCount());
    }
}

