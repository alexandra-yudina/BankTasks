namespace CardsTasks.PaymentTools;

public class CreditCard : CardBase
{
    public float Interest { get; set; }

    public CreditCard(float interest)
    {
        Interest = interest;
    }

    public override void MakePayment(float amount)
    {
        var transaction = amount + amount / 100 * Interest;
        if (_balance - transaction < 0)
        {
            _balance -= transaction;
        }
    }
}