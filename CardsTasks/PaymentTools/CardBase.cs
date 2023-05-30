using System;

namespace CardsTasks.PaymentTools;
public abstract class CardBase : PaymentMethod
{
    public string CardNumber { get; }

    protected CardBase()
    {
        CardNumber = GenerateRandomCardNumber();
    }

    private static string GenerateRandomCardNumber()
    {
        int[] sectionRangers = { 4, 1000, 1000, 1000 };
        //Generate random digits for each section
        var sections = new int[4];
        var rand = new Random();
        for (var i = 0; i < 4; i++)
        {
            sections[i] = rand.Next(sectionRangers[i]);
        }
        return $"{sections[0]:0000}{sections[1]:0000}{sections[2]:0000}{sections[3]:0000}";
    }
}