using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

// структура лікаря
struct Doctor
{
    public string Name { get; set; } // ім'я лікаря
    public int Years { get; set; } // стаж у роках
}

// структура відділення
struct Department
{
    public string Name { get; set; }  // назва відділення
    public List<Doctor> Doctors { get; set; } // список лікарів у цьому відділенні
}

// структура лікарні
struct Hospital
{
    public string Name { get; set; }  // назва лікарні
    public List<Department> Departments { get; set; } // всі відділення лікарні
}

class Program
{
    const string FileName = "hospital.json"; // назва JSON-файлу

    static void Main()
    {
        Hospital hospital;

        // якщо файл існує — читаю його
        if (File.Exists(FileName))
        {
            string json = File.ReadAllText(FileName);  // читаю текст з JSON
            hospital = JsonSerializer.Deserialize<Hospital>(json);  // перетворюю JSON у об'єкт
            Console.WriteLine("Дані лікарні завантажено з JSON.\n");
        }
        else
        {
            // якщо файла немає — створюю стартові дані
            hospital = CreateSampleData();
            Save(hospital);      // одразу зберігаю
            Console.WriteLine("Файл не знайдено — створено нові дані.\n");
        }

        // основне меню програми
        while (true)
        {
            Console.WriteLine(" МЕНЮ ");
            Console.WriteLine("1 — Переглянути всі дані");
            Console.WriteLine("2 — Додати нового лікаря");
            Console.WriteLine("3 — LINQ: кількість лікарів у відділеннях");
            Console.WriteLine("4 — LINQ: відділення з найбільшим досвідом");
            Console.WriteLine("5 — Зберегти в JSON");
            Console.WriteLine("0 — Вихід");
            Console.Write("Вибір: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1": PrintHospital(hospital); break; // показ всіх даних
                case "2": AddDoctor(ref hospital); break; // додавання лікаря
                case "3": Query_DoctorsPerDepartment(hospital); break;  // LINQ 1
                case "4": Query_DepartmentMaxExperience(hospital); break; // LINQ 2
                case "5": Save(hospital); Console.WriteLine("Збережено!\n"); break;
                case "0": return;   // вихід з програми
                default: Console.WriteLine("Невірний вибір.\n"); break;
            }
        }
    }

    // початкові дані для JSON
    static Hospital CreateSampleData()
    {
        return new Hospital
        {
            Name = "City Hospital", // назва лікарні
            Departments = new List<Department> // список відділень
            {
                new Department
                {
                    Name = "Therapy",
                    Doctors = new List<Doctor> // лікарі терапії
                    {
                        new Doctor { Name = "Dr. Ivanov", Years = 10 },
                        new Doctor { Name = "Dr. Petrenko", Years = 7 }
                    }
                },
                new Department
                {
                    Name = "Surgery",
                    Doctors = new List<Doctor>  // лікарі хірургії
                    {
                        new Doctor { Name = "Dr. Shevchenko", Years = 12 },
                        new Doctor { Name = "Dr. Bondar", Years = 5 },
                        new Doctor { Name = "Dr. Kovalenko", Years = 8 }
                    }
                }
            }
        };
    }

    // збереження всієї лікарні у JSON
    static void Save(Hospital hospital)
    {
        var json = JsonSerializer.Serialize(
            hospital,
            new JsonSerializerOptions { WriteIndented = true }  // форматування JSON
        );

        File.WriteAllText(FileName, json); // запис у файл
    }

    // вивід усіх даних лікарні
    static void PrintHospital(Hospital hospital)
    {
        Console.WriteLine($"Лікарня: {hospital.Name}\n");

        foreach (var dep in hospital.Departments)
        {
            Console.WriteLine($"Відділення: {dep.Name}"); // назва відділення

            foreach (var doc in dep.Doctors)
            {
                Console.WriteLine($"  - {doc.Name}, стаж {doc.Years} років");
            }

            Console.WriteLine();
        }
    }

    // додавання лікаря у відділення
    static void AddDoctor(ref Hospital hospital)
    {
        Console.Write("Назва відділення: ");
        string depName = Console.ReadLine();

        // шукаю відділення по назві
        var department = hospital.Departments
            .FirstOrDefault(d =>
                d.Name != null &&
                d.Name.Equals(depName, StringComparison.OrdinalIgnoreCase));

        // якщо відділення немає, створюю
        if (department.Name == null)
        {
            Console.WriteLine("Створюю нове відділення...");
            department = new Department
            {
                Name = depName,
                Doctors = new List<Doctor>()
            };
            hospital.Departments.Add(department);
        }

        Console.Write("Ім'я лікаря: ");
        string doctorName = Console.ReadLine();

        Console.Write("Стаж (роки): ");
        int years = int.Parse(Console.ReadLine());

        // додаю лікаря у список
        department.Doctors.Add(new Doctor
        {
            Name = doctorName,
            Years = years
        });

        // оновлюю відділення у списку (struct)
        hospital.Departments = hospital.Departments
            .Select(d => d.Name.Equals(depName, StringComparison.OrdinalIgnoreCase) ? department : d)
            .ToList();

        Console.WriteLine("Лікаря додано!\n");
    }

    // LINQ 1 кількість лікарів у кожному відділенні
    static void Query_DoctorsPerDepartment(Hospital hospital)
    {
        var query = hospital.Departments
            .Select(d => new
            {
                DepartmentName = d.Name,  // назва відділення
                DoctorCount = d.Doctors.Count  // скільки лікарів
            });

        Console.WriteLine("Кількість лікарів у відділеннях:");
        foreach (var x in query)
        {
            Console.WriteLine($"- {x.DepartmentName}: {x.DoctorCount} лікар(ів)");
        }
        Console.WriteLine();
    }

    // LINQ 2 відділення з найбільшим сумарним досвідом
    static void Query_DepartmentMaxExperience(Hospital hospital)
    {
        var q = hospital.Departments
            .Select(d => new
            {
                d.Name,  // назва
                TotalYears = d.Doctors.Sum(doc => doc.Years) // сума стажу лікарів
            })
            .OrderByDescending(x => x.TotalYears) // сортування по спаданню
            .FirstOrDefault();  // беремо перше

        if (q.Name != null)
            Console.WriteLine($"Найбільший досвід має відділення: {q.Name} ({q.TotalYears} років)\n");
        else
            Console.WriteLine("Немає даних.\n");
    }
}
