using System.Collections.Generic;
using System.Linq;

namespace CardsTasks;

public class ClientComparer : IComparer<BankClient>
{
    private readonly CompareTypeEnum _compareType;

    public ClientComparer(CompareTypeEnum compareType)
    {
        _compareType = compareType;
    }

    int IComparer<BankClient>.Compare(BankClient x, BankClient y)
    {
        if (x == null && y == null) return 0;
        if (x != null && y == null) return 1;
        if (x == null) return -1;

        return _compareType switch
        {
            CompareTypeEnum.LastName => x.LastName.CompareTo(y.LastName),
            CompareTypeEnum.Address => x.Address.CompareTo(y.Address),
            CompareTypeEnum.CardsCount => ClientCardsCount(x).CompareTo(ClientCardsCount(y)),
            CompareTypeEnum.TotalBalance => x.TotalBalance.CompareTo(y.TotalBalance),
            CompareTypeEnum.MaxBalance => ClientMaxBalance(x).CompareTo(ClientMaxBalance(y)),
            _ => default
        };
    }

    private static int ClientCardsCount(BankClient client)
    {
        return client == null ? 0 : client.PaymentMethods.Count(pm => pm.Type != "Cash" && pm.Type != "BitCoin");
    }

    private static float ClientMaxBalance(BankClient client)
    {
        return client == null ? 0 : client.PaymentMethods.Select(pm => pm.Balance()).Max();
    }
}