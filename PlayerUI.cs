using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class PlayerUI : MonoBehaviour, IPlayerUI
{
    [SerializeField] private TextMeshProUGUI txCoin;
    [SerializeField] private TextMeshProUGUI txKnifeCount;
    [SerializeField] private TextMeshProUGUI txTapToStart;
    [SerializeField] private Button btnTapToStart;

    private void OnEnable()
    {
        StartCoroutine(TransparencyBlinking());
    }
    private void OnDisable()
    {
        StopCoroutine(TransparencyBlinking());
    }
    public void ChangeTxCoin(int value)
    {
        txCoin.text = value.ToString();
    }
    public void ChangeTxKnifeCount(int value)
    {
        txKnifeCount.text = value.ToString();
    }
    public void DisableTapToStart()
    {
        btnTapToStart.interactable = false;
        btnTapToStart.gameObject.SetActive(false);
    }
    public void EnableTapToStart()
    {
        btnTapToStart.interactable = true;
        btnTapToStart.gameObject.SetActive(true);
    }
    public void Reset()
    {
        ChangeTxCoin(0);
        EnableTapToStart();
    }
    public IEnumerator TransparencyBlinking()
    {
        while (true)
        {
            if (txTapToStart.alpha == 1)
                txTapToStart.alpha = 0.7f;
            else
                txTapToStart.alpha = 1;
            yield return new WaitForSeconds(1);
        }
    }
}
