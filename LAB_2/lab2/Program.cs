using System;
using System.IO;
using System.Text;

internal class Program
{
    // Коренева папка: поточний каталог запуску
    static readonly string Root = Directory.GetCurrentDirectory();

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        try
        {
            // базовий рівень
            Task1_CreateInfoTxt();
            Task2_CreateDataDir();
            Task3_DeleteInfoTxtIfExists();
            Task4_DeleteDataDirIfExists();
            Task5_CreateNumbersTxt();

            // середній рівень
            Task6_RenameNumbersToDigits();
            Task7_MoveDigitsToBackup();
            Task8_CopyFromBackupToCopyData();

            // Завдання 9: інформація про файл
            Task9_PrintFileInfoFromUser();

            // підвищений рівень
            // Рекурсивне копіювання каталогів
            Task10_CopyDirectoryInteractive();

            Console.WriteLine("\nВиконання ЛР-2 завершено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Сталася помилка: " + ex.Message);
        }
    }

    // базовий рівень

    // 1. Створіть файл info.txt у корені проєкту (якщо існує — повідомити)
    static void Task1_CreateInfoTxt()
    {
        string path = Path.Combine(Root, "info.txt");
        if (!File.Exists(path))
        {
            File.Create(path).Close();
            Console.WriteLine("Файл створено: info.txt");
        }
        else
        {
            Console.WriteLine("Файл вже існує: info.txt");
        }
    }

    // 2. Створіть каталог Data у корені проєкту (якщо існує — повідомити)
    static void Task2_CreateDataDir()
    {
        string dir = Path.Combine(Root, "Data");
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
            Console.WriteLine("Каталог створено: Data");
        }
        else
        {
            Console.WriteLine("Каталог вже існує: Data");
        }
    }

    // 3. Видаліть файл info.txt, якщо він існує
    static void Task3_DeleteInfoTxtIfExists()
    {
        string path = Path.Combine(Root, "info.txt");
        if (File.Exists(path))
        {
            File.Delete(path);
            Console.WriteLine("Файл info.txt видалено.");
        }
        else
        {
            Console.WriteLine("Файл info.txt не існує.");
        }
    }

    // 4. Видаліть каталог Data разом із вмістом
    static void Task4_DeleteDataDirIfExists()
    {
        string dir = Path.Combine(Root, "Data");
        if (Directory.Exists(dir))
        {
            Directory.Delete(dir, true);
            Console.WriteLine("Каталог Data видалено разом з усім вмістом.");
        }
        else
        {
            Console.WriteLine("Каталогу Data не знайдено.");
        }
    }

    // 5. Створіть файл numbers.txt і запишіть числа 1..10 (кожне з нового рядка)
    static void Task5_CreateNumbersTxt()
    {
        string path = Path.Combine(Root, "numbers.txt");
        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            for (int i = 1; i <= 10; i++)
            {
                writer.WriteLine(i);
            }
        }
        Console.WriteLine("Числа записані у файл: numbers.txt");
    }

    // середній рівень

    // 6. Перейменуйте numbers.txt у digits.txt
    static void Task6_RenameNumbersToDigits()
    {
        string src = Path.Combine(Root, "numbers.txt");
        string dst = Path.Combine(Root, "digits.txt");

        if (File.Exists(src))
        {
            if (File.Exists(dst)) File.Delete(dst);
            File.Move(src, dst);
            Console.WriteLine("Файл перейменовано: numbers.txt → digits.txt");
        }
        else
        {
            Console.WriteLine("Файл numbers.txt не знайдено — перейменування неможливе.");
        }
    }

    // 7. Перемістіть digits.txt у каталог Backup (створити, якщо немає)
    static void Task7_MoveDigitsToBackup()
    {
        string file = Path.Combine(Root, "digits.txt");
        string backupDir = Path.Combine(Root, "Backup");
        Directory.CreateDirectory(backupDir);

        if (File.Exists(file))
        {
            string dest = Path.Combine(backupDir, "digits.txt");
            if (File.Exists(dest)) File.Delete(dest);
            File.Move(file, dest);
            Console.WriteLine("Файл переміщено до каталогу Backup.");
        }
        else
        {
            Console.WriteLine("digits.txt не знайдено в корені проєкту.");
        }
    }

    // 8. Скопіюйте файл з Backup у CopyData (папку створити автоматично)
    static void Task8_CopyFromBackupToCopyData()
    {
        string src = Path.Combine(Root, "Backup", "digits.txt");
        string copyDir = Path.Combine(Root, "CopyData");
        Directory.CreateDirectory(copyDir);

        if (File.Exists(src))
        {
            string dst = Path.Combine(copyDir, "digits.txt");
            File.Copy(src, dst, true);
            Console.WriteLine("Файл скопійовано з Backup у CopyData.");
        }
        else
        {
            Console.WriteLine("У каталозі Backup немає файлу digits.txt для копіювання.");
        }
    }

    // 9. Програма приймає шлях до файлу і виводить ім'я, розмір, дати
    static void Task9_PrintFileInfoFromUser()
    {
        Console.Write("\nВведіть шлях до файлу: ");
        string? filePath = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
        {
            FileInfo fi = new FileInfo(filePath);
            Console.WriteLine($"Ім'я файлу: {fi.Name}");
            Console.WriteLine($"Розмір: {fi.Length} байт");
            Console.WriteLine($"Дата створення: {fi.CreationTime}");
            Console.WriteLine($"Останній доступ: {fi.LastAccessTime}");
        }
        else
        {
            Console.WriteLine("Файл не існує!");
        }
    }

    // підвищений рівень

    // 10. Рекурсивне копіювання каталогу (усі підкаталоги й файли)
    static void Task10_CopyDirectoryInteractive()
    {
        Console.Write("\nВведіть шлях вихідного каталогу для рекурсивного копіювання: ");
        string? sourceDir = Console.ReadLine();

        Console.Write("Введіть шлях каталогу-призначення: ");
        string? destDir = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(sourceDir) && !string.IsNullOrWhiteSpace(destDir))
        {
            if (!Directory.Exists(sourceDir))
            {
                Console.WriteLine("Вихідний каталог не існує.");
                return;
            }

            CopyDirectory(sourceDir, destDir);
            Console.WriteLine("Рекурсивне копіювання завершено.");
        }
        else
        {
            Console.WriteLine("Невірні шляхи.");
        }
    }


    static void CopyDirectory(string sourceDir, string destDir)
    {
        Directory.CreateDirectory(destDir);

        foreach (string file in Directory.GetFiles(sourceDir))
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(destDir, fileName);
            File.Copy(file, destFile, true);
        }

        foreach (string subDir in Directory.GetDirectories(sourceDir))
        {
            string dirName = Path.GetFileName(subDir);
            string destSubDir = Path.Combine(destDir, dirName);
            CopyDirectory(subDir, destSubDir);
        }
    }
}
