namespace CardsTasks.PaymentTools;

public abstract class PaymentMethod : IPayment
{
    protected float _balance;

    public string Type { get; set; }

    protected PaymentMethod()
    {
        Type = GetType().Name;
    }

    public virtual void MakePayment(float amount)
    {
        _balance -= amount;
    }

    public virtual void TopUp(float amount)
    {
        _balance += amount;
    }

    public virtual float Balance()
    {
        return _balance;
    }
}