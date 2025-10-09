using System;

// Структура Student — зберігає дані про студента
struct Student
{
    public string Name;     // ПІБ (повне ім’я студента)
    public string Group;    // Назва групи, до якої належить студент
    public double Average;  // Середній бал студента
}

// Структура Book — описує одну книгу
struct Book
{
    public string Title;    // Назва книги
    public string Author;   // Автор книги
    public int Year;        // Рік видання
}

// Структура Worker — описує одного робітника
struct Worker
{
    public string Name;      // Ім’я робітника
    public string Position;  // Посада (ким працює)
    public int Experience;   // Стаж у роках
}

class Program
{
    // Допоміжні функції для коректного введення даних

    // Функція ReadText() — читає текст, не дозволяє залишити порожнє поле
    static string ReadText(string prompt)
    {
        while (true) // нескінченний цикл, поки користувач не введе щось правильне
        {
            Console.Write(prompt); // показує підказку (назву поля)
            var s = Console.ReadLine(); // читає рядок із клавіатури

            // якщо рядок не порожній і не складається лише з пробілів
            if (!string.IsNullOrWhiteSpace(s))
                return s; // повертаємо введений текст

            // якщо поле порожнє — виводимо повідомлення і повторюємо введення
            Console.WriteLine("Поле не може бути порожнім. Спробуйте ще раз.");
        }
    }

    // Функція ReadInt() — читає ціле число (int)
    static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt); // підказка для користувача
            var s = Console.ReadLine(); // зчитуємо введення
            if (int.TryParse(s, out var n)) // пробуємо перетворити у тип int
                return n; // якщо вдалося — повертаємо значення
            Console.WriteLine("Потрібно ввести ціле число. Спробуйте ще раз."); // повідомлення про помилку
        }
    }

    // Функція ReadDouble() — читає дійсне число (double)
    static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var s = Console.ReadLine();
            if (double.TryParse(s, out var x)) // перевірка правильності вводу
                return x;
            Console.WriteLine("Потрібно ввести число. Спробуйте ще раз.");
        }
    }

    // головний метод програми
    static void Main()
    {
        // Увімкнення підтримки українських символів у консолі
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Завдання №1
        Console.WriteLine("Завдання 1: Студенти із середнім балом > 90");

        // створюємо масив із 5 студентів
        Student[] students = new Student[5];

        // цикл введення даних для кожного студента
        for (int i = 0; i < students.Length; i++)
        {
            Console.WriteLine($"\nСтудент №{i + 1}");
            students[i].Name = ReadText("ПІБ: ");           // зчитуємо ПІБ
            students[i].Group = ReadText("Група: ");        // зчитуємо групу
            students[i].Average = ReadDouble("Середній бал: "); // зчитуємо середній бал
        }

        // виводимо студентів, у яких середній бал > 90
        Console.WriteLine("\nРезультат (студенти з балом > 90):");
        for (int i = 0; i < students.Length; i++)
        {
            if (students[i].Average > 90) // перевіряємо умову
            {
                Console.WriteLine($"{students[i].Name} — група {students[i].Group}, бал {students[i].Average}");
            }
        }

        // Завдання №2
        Console.WriteLine("\nЗавдання 2: Книги, видані після 2010 року");

        // створюємо масив із 5 книг
        Book[] books = new Book[5];

        // цикл введення даних
        for (int i = 0; i < books.Length; i++)
        {
            Console.WriteLine($"\nКнига №{i + 1}");
            books[i].Title = ReadText("Назва: ");   // зчитуємо назву книги
            books[i].Author = ReadText("Автор: ");  // зчитуємо автора
            books[i].Year = ReadInt("Рік: ");       // зчитуємо рік видання
        }

        // виводимо книги, які видані після 2010 року
        Console.WriteLine("\nКниги після 2010 року:");
        for (int i = 0; i < books.Length; i++)
        {
            if (books[i].Year > 2010)
            {
                Console.WriteLine($"{books[i].Title} ({books[i].Author}), {books[i].Year}");
            }
        }

        // Завдання №3
        Console.WriteLine("\nЗавдання 3: Робітники зі стажем понад 10 років");

        // створюємо масив із 5 робітників
        Worker[] workers = new Worker[5];

        // цикл введення даних про кожного робітника
        for (int i = 0; i < workers.Length; i++)
        {
            Console.WriteLine($"\nРобітник №{i + 1}");
            workers[i].Name = ReadText("Ім'я: ");             // зчитуємо ім’я
            workers[i].Position = ReadText("Посада: ");       // зчитуємо посаду
            workers[i].Experience = ReadInt("Стаж (років): "); // зчитуємо стаж
        }

        // виводимо лише тих робітників, у яких стаж > 10
        Console.WriteLine("\nРезультат (стаж понад 10 років):");
        for (int i = 0; i < workers.Length; i++)
        {
            if (workers[i].Experience > 10) // перевіряємо умову
            {
                // виводимо результат у зрозумілому форматі
                Console.WriteLine($"{workers[i].Name} — {workers[i].Position}, стаж {workers[i].Experience} р.");
            }
        }

        // Кінець програми
        Console.WriteLine("\nРоботу завершено. Натисніть Enter для виходу.");
        Console.ReadLine(); // пауза, щоб вікно не закривалося відразу
    }
}
