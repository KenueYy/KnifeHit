using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : IWallet
{
    private int coins;

    public void AddCoin(int value)
    {
        coins += value;
    }
    public void SpendCoin(int value)
    {
        coins -= value;
    }
    public int GetCoinCount()
    {
        return coins;
    }
    public void Reset()
    {
        coins = 0;
    }
}
