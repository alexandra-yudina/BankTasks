using System.Collections.Generic;
using System;
using System.Linq;
using CardsTasks;

#region Task 1

Console.WriteLine("Task 1 \n");

var hagrid = new BankClient("Rubeus", "Hagrid", "Wooden cabin", 100f, new[] { 1000f, 2000f });

hagrid.Pay(50f);
hagrid.Pay(550f);
hagrid.Pay(3000f);
hagrid.Pay(5000f);
hagrid.Pay(15000f);
hagrid.Pay(6000f);

Console.WriteLine(hagrid.ToString());

#endregion

#region Task 2

Console.WriteLine("Task 2 \n");

var moony = new BankClient("Remus", "Lupin", "Hogwarts", 100f, new[] { 1000f, 1000f }, 1000f);
var wormtail = new BankClient("Peter", "Pettigrew", "Hogwarts", 0f, new[] { 0f });
var padfoot = new BankClient("Sirius", "Black", "Azkaban", 0f, new[] { 5000f });
var prongs = new BankClient("James", "Potter", "Godrik's Trough", 0f, new[] { 1000f }, 5000f, 10000f, 1f);
var marauders = new List<BankClient> { moony, wormtail, padfoot, prongs };

//Initial order
Print(marauders);

//Sorted by Lastname
marauders.Sort(new ClientComparer(CompareTypeEnum.LastName));
Print(marauders);

//Sorted by address
marauders.Sort(new ClientComparer(CompareTypeEnum.Address));
Print(marauders);

//Sorted by cardscount(cash and bitcoin excluded)
marauders.Sort(new ClientComparer(CompareTypeEnum.CardsCount));
Print(marauders);

//Sorted by Total balance
marauders.Sort(new ClientComparer(CompareTypeEnum.TotalBalance));
Print(marauders);

//Sorted by Max Balance
marauders.Sort(new ClientComparer(CompareTypeEnum.MaxBalance));
Print(marauders);

#endregion

#region Task 3

Console.WriteLine("Task 3 \n");

var harry = new BankClient("Harry", "Potter", "Little Winging", 150f, new[] { 10000f, 20000f }, bitCoinBalance: 1.5f);
var ron = new BankClient("Ron", "Wisley", "The Burrow", 10f, new[] { 50f }, bitCoinBalance: 0f);
var hermione = new BankClient("Hermione", "Granger", "8 Heathgate, Hampsted garden Suburb, London",
    500f, debitBalances: new[] { 500f, 1000f }, bitCoinBalance: 0.5f);

harry.PrintDebitCards();
harry.PrintTotalBalance();

var trio = new List<BankClient> { harry, ron, hermione };

trio.Sort(new ClientComparer(CompareTypeEnum.TotalBalance));

var richest = trio.Last();

Console.WriteLine(richest.ToString());

#endregion

#region Task 4

Console.WriteLine("Task 4 \n");

var dumbledore = new BankClient("Albus", "Dumbledore", "Hogsmeade", 500f, new[] { 10000f, 20000f }, 5000f);
var dumbledore2 = new BankClient("Aberfort", "Dumbledore", "Hogsmeade", 500f, new[] { 10000f, 20000f }, 5000f);

if (dumbledore.Equals(dumbledore2))
{
    Console.WriteLine("They are equal");
}

//TODO: uncomment this to validate propeties setters exceptoins;
/*
 var daenerys = new BankClient("Daenerys", "Targaryen", "Dragonstorm",5000f, new[] {10000000000f});
 daenerys.FirstName = null;
 daenerys.FirstName = "Daenerys Stormborn of House Targaryen, the First of Her Name, Queen of the Andals and the First Men,
    Protector of the Seven Kingdoms, the Mother of Dragons, the Khaleesi of the Great Grass Sea, the Unburnt, the Breaker of Chains."
 */

#endregion

Console.ReadLine();

static void Print(List<BankClient> clients)
{
    Console.WriteLine("\nSorting result:");
    foreach (var client in clients)
    {
        Console.WriteLine($"{client.LastName}, {client.FirstName}");
    }
    Console.WriteLine("End of sorting\n");
}
