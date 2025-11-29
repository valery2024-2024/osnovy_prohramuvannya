using System;
// структура для показників здоров'я тварини
struct HealthStats
{
    public double Weight;  // вага тварини
    public double Temperature;  // температура тіла тварини 
    public int HealthIndex;  // загальний індекс здоров'я від 0 до 100
}
// структура тварини
struct Animal
{
    public string Name; // ім'я тварини
    public string Species; // вид тварини
    public HealthStats Stats; // вкладена структура з показниками здоров'я
}
// структура ферми
struct Farm
{
    public string Name; // назва ферми
    public Animal[] Animals; // масив тварин на фермі
}

class Program
{
    static void Main()
    {
        // створюю масив із двох ферм, як вимагає завдання
        Farm[] farms = new Farm[]
        {
            // перша ферма
            new Farm
            {
                Name = "Milk Valley", // назва
                Animals = new Animal[] // список тварин
                {
                    new Animal
                    {
                        Name = "Milka",
                        Species = "Cow",
                        Stats = new HealthStats
                        {
                            Weight = 520,
                            Temperature = 38.6,
                            HealthIndex = 92
                        }
                    },
                    new Animal
                    {
                        Name = "Buryonka",
                        Species = "Cow",
                        Stats = new HealthStats
                        {
                            Weight = 510,
                            Temperature = 38.5,
                            HealthIndex = 88
                        }
                    },
                    new Animal
                    {
                        Name = "Bobik",
                        Species = "Dog",
                        Stats = new HealthStats
                        {
                            Weight = 25,
                            Temperature = 38.7,
                            HealthIndex = 95
                        }
                    }
                }
            },
            // друга ферма
            new Farm
            {
                Name = "Sunny Farm",
                Animals = new Animal[]
                {
                    new Animal
                    {
                        Name = "Orlyk",
                        Species = "Horse",
                        Stats = new HealthStats
                        {
                            Weight = 430,
                            Temperature = 37.9,
                            HealthIndex = 85
                        }
                    },
                    new Animal
                    {
                        Name = "Snow",
                        Species = "A sheep",
                        Stats = new HealthStats
                        {
                            Weight = 70,
                            Temperature = 39.0,
                            HealthIndex = 90
                        }
                    },
                    new Animal
                    {
                        Name = "Belka",
                        Species = "Goat",
                        Stats = new HealthStats
                        {
                            Weight = 55,
                            Temperature = 38.9,
                            HealthIndex = 87
                        }
                    }
                }
            }
        };

        // виводжу всі ферми та їхніх тварин
        Console.WriteLine("Статистика здоров'я ферм і тварин:\n");

        foreach (var farm in farms) // проходжу по кожній фермі
        {
            Console.WriteLine($"{farm.Name}:"); // вивід назви ферми

            foreach (var animal in farm.Animals) // всі тварини ферми
            {
                // вивід інформації про тварину
                Console.WriteLine(
                    $"- {animal.Name} ({animal.Species}): " +
                    $"Вага {animal.Stats.Weight} kg, " +
                    $"температура {animal.Stats.Temperature}°C, " +
                    $"індекс здоров'я {animal.Stats.HealthIndex}");
            }

        }

        // пошук ферми з найвищим середнім індексом здоров'я
        double maxAverage = -1; // сюди зберігатиму найбільше середнє значення
        string bestFarm = ""; // назва найкращої ферми

        foreach (var farm in farms) // перебір ферм
        {
            int totalHealth = 0; // сума показників здоров'я
            int count = 0; // кількість тварин

            foreach (var animal in farm.Animals)
            {
                totalHealth += animal.Stats.HealthIndex; // додаю індекс
                count++; // рахую тварин
            }

            double average = (double)totalHealth / count; // обчислюю середнє

            if (average > maxAverage) // перевірка, чи це новий максимум
            {
                maxAverage = average;
                bestFarm = farm.Name; // зберігаю назву ферми
            }
        }
        // фінальний вивід результату
        Console.WriteLine(
            $"Ферма з найвищим середнім індексом здоров'я: " +
            $"{bestFarm} ({maxAverage:F1} балів)");
    }
}
