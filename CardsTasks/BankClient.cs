using CardsTasks.PaymentTools;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CardsTasks;

public class BankClient
{
    private string _firstName;
    private string _lastName;
    private string _address;

    public Cash Cash { get; } = new();

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrEmpty(value) || value.Length > 50)
            {
                throw new ArgumentException("First name cannot be null or empty, and must be at most 50 characters long.");
            }
            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrEmpty(value) || value.Length > 50)
            {
                throw new ArgumentException("Last name cannot be null or empty, and must be at most 50 characters long.");
            }
            _lastName = value;
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Address cannot be null or empty");
            }
            _address = value;
        }
    }

    public List<PaymentMethod> PaymentMethods { get; } = new();

    public float TotalBalance => PaymentMethods.Sum(pm => pm.Balance());

    public BankClient(string firstName, string lastName, string address,
        float cashBalance, IEnumerable<float> debitBalances, float cashBackBalance = default,
        float creditBalance = default, float bitCoinBalance = default)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;

        Cash.TopUp(cashBalance);
        PaymentMethods.Add(Cash);

        foreach (var debit in debitBalances.Where(db => db >= 0))
        {
            var temp = new DebitCard();
            temp.TopUp(debit);
            PaymentMethods.Add(temp);
        }

        if (cashBackBalance >= 0f)
        {
            var temp = new CashBackCard(3f);
            temp.TopUp(cashBackBalance);
            PaymentMethods.Add(temp);
        }
        if (creditBalance != 0f)
        {
            var temp = new CreditCard(13f);
            temp.TopUp(creditBalance);
            PaymentMethods.Add(temp);
        }
        if (bitCoinBalance != 0f)
        {
            var temp = new BitCoin();
            temp.TopUp(bitCoinBalance);
            PaymentMethods.Add(temp);
        }
    }

    public bool Pay(float amount)
    {
        var cashToPay = PaymentMethods
            .OfType<Cash>()
            .FirstOrDefault(c => c.Balance() - amount >= 0);
        if (cashToPay != null)
        {
            cashToPay.MakePayment(amount);
            return true;
        }

        var debitToPay = PaymentMethods
            .OfType<DebitCard>()
            .FirstOrDefault(c => c.Balance() - amount >= 0);
        if (debitToPay != null)
        {
            debitToPay.MakePayment(amount);
            return true;
        }

        var cashBackToPay = PaymentMethods
            .OfType<CashBackCard>()
            .FirstOrDefault(c => c.Balance() - amount >= 0);
        if (cashBackToPay != null)
        {
            cashBackToPay.MakePayment(amount);
            return true;
        }

        var creditToPay = PaymentMethods
            .OfType<CreditCard>()
            .FirstOrDefault(c => c.Balance() - amount >= 0);
        if (creditToPay != null)
        {
            creditToPay.MakePayment(amount);
            return true;
        }

        var bitCoinToPay = PaymentMethods
            .OfType<BitCoin>()
            .FirstOrDefault(btc => btc.Balance() - amount >= 0);
        if (bitCoinToPay != null)
        {
            bitCoinToPay.MakePayment(amount);
            return true;
        }

        Console.WriteLine("Insufficient Funds");
        return false;
    }

    public void PrintDebitCards()
    {
        Console.WriteLine($"CardBase Holder name: {_lastName}, {_firstName}");
        PaymentMethods.OfType<DebitCard>()
            .Select(card => $"CardBase Number: {card.CardNumber} Remaining Balance: {card.Balance()}")
            .ToList()
            .ForEach(Console.WriteLine);
    }
    public void PrintTotalBalance()
    {
        Console.WriteLine($"Client Name: {_lastName}, {_firstName}");
        Console.WriteLine("Total Balance: " + TotalBalance);
    }
    public override string ToString()
    {
        return "Bank Client: " + FirstName + " " + LastName +
           "\r\nRemaining Cash Balance: " + Cash.Balance() +
           "\r\nRemaining Debit Cards Balances: " + PaymentMethods.OfType<DebitCard>().Sum(c => c.Balance()) +
           "\r\nRemaining Cash Back Card Balance: " + PaymentMethods.OfType<CashBackCard>().Sum(c => c.Balance()) +
           "\r\nRemaining Credit Card Balance: " + PaymentMethods.OfType<CreditCard>().Sum(c => c.Balance()) +
           "\r\nRemaining BTC Balance: " + PaymentMethods.OfType<BitCoin>().Sum(c => c.BalanceInBtc()) + "BTC";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        var other = (BankClient)obj;
        return _lastName == other._lastName && _address == other._address && TotalBalance == other.TotalBalance;
    }

    public override int GetHashCode()
    {
        return (_lastName?.GetHashCode() ?? 0) + (_address?.GetHashCode() ?? 0) + TotalBalance.GetHashCode();
    }
}