using System.Collections;

public interface IPlayerUI
{
    void ChangeTxCoin(int value);
    void ChangeTxKnifeCount(int value);
    void Reset();
    void DisableTapToStart();
    void EnableTapToStart();
    IEnumerator TransparencyBlinking();
}