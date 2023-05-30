namespace CardsTasks;

internal interface IPayment
{
    void MakePayment(float amount);
    void TopUp(float amount);
}