using System;
using System.Linq;

// структура лікаря
struct Doctor
{
    public string Name;  // ім'я лікаря
    public int Years;  // стаж роботи
}

// структура відділення
struct Department
{
    public string Name;  // назва відділення
    public Doctor[] Doctors;  // масив лікарів
}

// структура лікарні
struct Hospital
{
    public string Name;  // назва лікарні
    public Department[] Departments; // всі відділення
}

class Program
{
    static void Main()
    {
        // створюю лікарню з 3 відділеннями і лікарями
        Hospital hospital = new Hospital
        {
            Name = "City Hospital",
            Departments = new Department[]
            {
                new Department
                {
                    Name = "Surgery", // хірургія
                    Doctors = new Doctor[]
                    {
                        new Doctor { Name = "Petrenko", Years = 10 },
                        new Doctor { Name = "Shevchenko", Years = 7 },
                        new Doctor { Name = "Ivanov", Years = 5 }
                    }
                },
                new Department
                {
                    Name = "Therapy", // терапія
                    Doctors = new Doctor[]
                    {
                        new Doctor { Name = "Koval", Years = 12 },
                        new Doctor { Name = "Melnyk", Years = 8 },
                        new Doctor { Name = "Bondar", Years = 6 },
                        new Doctor { Name = "Tkachenko", Years = 4 }
                    }
                },
                new Department
                {
                    Name = "Pediatrics", // педіатрія
                    Doctors = new Doctor[]
                    {
                        new Doctor { Name = "Horobets", Years = 9 },
                        new Doctor { Name = "Oliynyk", Years = 3 },
                        new Doctor { Name = "Danyliuk", Years = 2 }
                    }
                }
            }
        };

        // порахувати кількість лікарів у кожному відділенні
        var doctorsCount =
            hospital.Departments
            .Select(d => new  // вибираю кожне відділення
            {
                d.Name,  // назва відділення
                Count = d.Doctors.Count()  // рахує кількість лікарів
            });

        Console.WriteLine("Лікарі розраховуються по відділеннях:");
        foreach (var d in doctorsCount) // вивід результату
            Console.WriteLine($"{d.Name}: {d.Count}");

        Console.WriteLine();

        // знайти відділення з найбільшим досвідом
        var experienceByDepartment =
            hospital.Departments
            .Select(d => new
            {
                d.Name,  // назва відділення
                TotalExperience = d.Doctors.Sum(doc => doc.Years) // сума років роботи
            });

        // сортування за спаданням і вибір першого
        var bestDepartment =
            experienceByDepartment
            .OrderByDescending(d => d.TotalExperience)
            .First(); // відділення з найбільшим досвідом

        Console.WriteLine($"Відділ з максимальним досвідом: {bestDepartment.Name} ({bestDepartment.TotalExperience} років)");
    }
}
