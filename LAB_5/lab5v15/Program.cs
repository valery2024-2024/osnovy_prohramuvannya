using System;
using System.Linq;

// структура покупця
struct Customer
{
    public string Name;  // ім'я
    public string Status;  // VIP або Regular
    public int Purchases;  // кількість покупок
}

class Program
{
    static void Main()
    {
        // масив із 10 покупців
        Customer[] customers =
        {
            new Customer { Name="Ivan",    Status="VIP",     Purchases=7 },
            new Customer { Name="Anna",    Status="Regular", Purchases=3 },
            new Customer { Name="Petro",   Status="VIP",     Purchases=12 },
            new Customer { Name="Oleh",    Status="Regular", Purchases=5 },
            new Customer { Name="Ira",     Status="VIP",     Purchases=2 },
            new Customer { Name="Mark",    Status="VIP",     Purchases=9 },
            new Customer { Name="Liza",    Status="Regular", Purchases=11 },
            new Customer { Name="Roman",   Status="VIP",     Purchases=4 },
            new Customer { Name="Olena",   Status="Regular", Purchases=8 },
            new Customer { Name="Taras",   Status="VIP",     Purchases=6 }
        };

        // знайти VIP з покупками > 5
        var vipBig = customers
            .Where(c => c.Status == "VIP" && c.Purchases > 5);  
            // фільтрую тільки VIP і тільки тих, хто купив більше 5 разів

        Console.WriteLine("VIP клієнтів з покупками > 5:");
        foreach (var c in vipBig)
            Console.WriteLine($"{c.Name} – {c.Status}, покупок: {c.Purchases}");
            // виводжу підходящих покупців
            
        Console.WriteLine();      

        // групування за статусом + середня кількість покупок
        var avgByStatus = customers
            .GroupBy(c => c.Status)  
            // групую всіх по статусу, VIP / Regular
            .Select(g => new
            {
                Status = g.Key,  // ключ групи, статус
                Average = g.Average(x => x.Purchases)  
                // рахую середню кількість покупок
            });

        Console.WriteLine("Середні покупки за статусом:");
        foreach (var s in avgByStatus)
            Console.WriteLine($"{s.Status}: {s.Average:F2}");
            // виводжу середні значення

        Console.WriteLine();

        // хто має більшу середню кількість покупок, VIP або Regular
        var bestStatus = avgByStatus
            .OrderByDescending(s => s.Average)  
            // сортуємо від більшого до меншого
            .First();  
            // беру той статус, у якого середня найбільша

        Console.WriteLine($"Найкраща група: {bestStatus.Status} ({bestStatus.Average:F2})");
        // показую, який статус найактивніший
    }
}
