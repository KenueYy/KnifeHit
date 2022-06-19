public interface IWallet
{
    void AddCoin(int value);
    int GetCoinCount();
    void Reset();
    void SpendCoin(int value);
}