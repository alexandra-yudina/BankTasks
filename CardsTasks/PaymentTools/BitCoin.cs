
namespace CardsTasks.PaymentTools;

public class BitCoin : PaymentMethod
{
    public const float Rate = 20000f;

    public override void MakePayment(float amount)
    {
        _balance = (_balance * Rate - amount) / Rate;
    }

    public override float Balance()
    {
        return _balance * Rate;
    }

    public float BalanceInBtc()
    {
        return Balance() / Rate;
    }
}