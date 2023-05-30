using CardsTasks;

namespace CardsTests;

[TestClass]
public class BankClientTests
{
    [TestMethod]
    public void ToString_ReturnsExpectedString()
    {
        var client = new BankClient("Lucius", "Malfoy", "Wilthshire", 1000f,
            new[] { 1500f, 3000f, 1500f }, 5000f, 10000f, 1000f);
        const string expected = "Bank Client: Lucius Malfoy" +
                                "\r\nRemaining Cash Balance: 1000" +
                                "\r\nRemaining Debit Cards Balances: 6000" +
                                "\r\nRemaining Cash Back Card Balance: 5000" +
                                "\r\nRemaining Credit Card Balance: 10000" +
                                "\r\nRemaining BTC Balance: 1000BTC";

        Assert.AreEqual(expected, client.ToString());
    }

    [TestMethod]
    public void Equals_ReturnsTrue()
    {
        var client1 = new BankClient("Fred", "Wisley", "The Burrow", 100f, new[] { 1000f, 2000f });
        var client2 = new BankClient("George", "Wisley", "The Burrow", 100f, new[] { 1300f, 1700f });

        Assert.IsTrue(client1.Equals(client2));
    }

    [TestMethod]
    public void TestPayments()
    {
        var teacher = new BankClient("Minerva", "McGonagall", "Gryffindor House", 1000f, new[] { 1000f, 2000f });

        teacher.Pay(600f);

        Assert.AreEqual(400f, teacher.PaymentMethods.First().Balance());
    }

    [TestMethod]
    public void Pay_WithCash_ReturnsTrue()
    {
        var bankClient = new BankClient("Severus", "Snape", "Spinner's End",
            cashBalance: 100, debitBalances: new List<float> { 50 });

        var result = bankClient.Pay(75);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Pay_WithDebitCard_ReturnsTrue()
    {
        var bankClient = new BankClient("Severus", "Snape", "Spinner's End",
            cashBalance: 0, debitBalances: new List<float> { 100 });

        var result = bankClient.Pay(50);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Pay_WithCashBackCard_ReturnsTrue()
    {
        var bankClient = new BankClient("Severus", "Snape", "Spinner's End",
            cashBalance: 0, debitBalances: new List<float>(), cashBackBalance: 100);

        var result = bankClient.Pay(75);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Pay_WithCreditCard_ReturnsTrue()
    {
        var bankClient = new BankClient("Severus", "Snape", "Spinner's End",
            cashBalance: 0, debitBalances: new List<float>(), creditBalance: 200);

        var result = bankClient.Pay(150);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Pay_WithBitCoin_ReturnsTrue()
    {
        var bankClient = new BankClient("Severus", "Snape", "Spinner's End",
            cashBalance: 0, debitBalances: new List<float>(), bitCoinBalance: 0.5f);

        var result = bankClient.Pay(0.25f);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Pay_WithInsufficientFunds_ReturnsFalse()
    {
        var bankClient = new BankClient("Severus", "Snape", "Spinner's End",
            cashBalance: 50, debitBalances: new List<float> { 25 });

        var result = bankClient.Pay(100);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Pay_WithoutFunds_ReturnsFalse()
    {
        var bankClient = new BankClient("Severus", "Snape", "Spinner's End",
            cashBalance: 0, debitBalances: new List<float>());

        var result = bankClient.Pay(50);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Pay_WithMultiplePaymentMethods_CorrectlySelectsFirstAvailable()
    {
        var bankClient = new BankClient("Severus", "Snape", "Spinner's End",
            cashBalance: 50, debitBalances: new List<float> { 100 },
            cashBackBalance: 75, creditBalance: 200, bitCoinBalance: 0.5f);

        var result1 = bankClient.Pay(25); // Cash
        var result2 = bankClient.Pay(75); // Debit Card
        var result3 = bankClient.Pay(50); // CashBack Card
        var result4 = bankClient.Pay(150); // Credit Card
        var result5 = bankClient.Pay(0.25f); // BitCoin

        Assert.IsTrue(result1);
        Assert.IsTrue(result2);
        Assert.IsTrue(result3);
        Assert.IsTrue(result4);
        Assert.IsTrue(result5);
    }
}