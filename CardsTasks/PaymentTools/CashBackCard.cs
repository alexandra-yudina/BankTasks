namespace CardsTasks.PaymentTools;

public class CashBackCard : CardBase
{
    public float CashBackPercent { get; set; }

    public CashBackCard(float percent)
    {
        CashBackPercent = percent;
    }

    public override void MakePayment(float amount)
    {
        var cashBack = amount / 100 * CashBackPercent;
        _balance -= amount;
        TopUp(cashBack);
    }
}