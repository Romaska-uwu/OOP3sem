using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

// 1. Класс XXXLog
public static class AKPLog
{
    private const string LogFilePath = "akplogfile.txt";

    // a. Запись в файл
    public static void WriteLog(string action, string details)
    {
        try
        {
            using (var writer = new StreamWriter(LogFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {action} - {details}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
        }
    }

    // a. Чтение из файла
    public static IEnumerable<string> ReadLog()
    {
        try
        {
            return File.Exists(LogFilePath) ? File.ReadAllLines(LogFilePath) : Array.Empty<string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка чтения лога: {ex.Message}");
            return Array.Empty<string>();
        }
    }

    // a. Поиск в файле
    public static IEnumerable<string> SearchLog(string keyword)
    {
        return ReadLog().Where(line => line.Contains(keyword));
    }

    // a. Очистка старых записей
    public static void ClearOldLogs(DateTime threshold)
    {
        try
        {
            var logs = ReadLog().Where(line => DateTime.Parse(line.Split(':')[0]) >= threshold).ToArray();
            File.WriteAllLines(LogFilePath, logs);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка очистки лога: {ex.Message}");
        }
    }
}

// 2. Класс XXXDiskInfo
public static class AKPDiskInfo
{
    public static void PrintFreeSpace(string driveName)
    {
        try
        {
            var drive = DriveInfo.GetDrives().FirstOrDefault(d => d.Name == driveName);
            if (drive != null && drive.IsReady)
            {
                AKPLog.WriteLog("Free space check", driveName);
                Console.WriteLine($"Свободное место на диске {driveName}: {drive.AvailableFreeSpace} байт");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка вывода информации о свободном месте: {ex.Message}");
        }
    }

    public static void PrintFileSystemInfo(string driveName)
    {
        try
        {
            var drive = DriveInfo.GetDrives().FirstOrDefault(d => d.Name == driveName);
            if (drive != null && drive.IsReady)
            {
                AKPLog.WriteLog("File system info", driveName);
                Console.WriteLine($"Файловая система: {drive.DriveFormat}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка вывода информации о файловой системе: {ex.Message}");
        }
    }

    public static void PrintAllDrivesInfo()
    {
        try
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Диск: {drive.Name}, Общий объем: {drive.TotalSize}, Доступный объем: {drive.AvailableFreeSpace}, Метка: {drive.VolumeLabel}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка вывода информации о дисках: {ex.Message}");
        }
    }
}

// 3. Класс XXXFileInfo
public static class AKPFileInfo
{
    public static void PrintFileInfo(string filePath)
    {
        try
        {
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                Console.WriteLine($"Полный путь: {fileInfo.FullName}");
                Console.WriteLine($"Размер: {fileInfo.Length}, Расширение: {fileInfo.Extension}, Имя: {fileInfo.Name}");
                Console.WriteLine($"Дата создания: {fileInfo.CreationTime}, Дата изменения: {fileInfo.LastWriteTime}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка вывода информации о файле: {ex.Message}");
        }
    }
}

// 4. Класс XXXDirInfo
public static class AKPDirInfo
{
    public static void PrintDirectoryInfo(string dirPath)
    {
        try
        {
            var dirInfo = new DirectoryInfo(dirPath);
            if (dirInfo.Exists)
            {
                Console.WriteLine($"Количество файлов: {dirInfo.GetFiles().Length}");
                Console.WriteLine($"Время создания: {dirInfo.CreationTime}");
                Console.WriteLine($"Количество поддиректорий: {dirInfo.GetDirectories().Length}");
                Console.WriteLine($"Родительские директории: {dirInfo.Parent}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка вывода информации о директории: {ex.Message}");
        }
    }
}

// 5. Класс XXXFileManager
public static class AKPFileManager
{
    public static void InspectDrive(string driveName)
    {
        try
        {
            var dir = Path.Combine(driveName, "AKPInspect");
            Directory.CreateDirectory(dir);

            var filePath = Path.Combine(dir, "akpdirinfo.txt");
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var file in Directory.GetFiles(driveName))
                {
                    writer.WriteLine(file);
                }

                foreach (var subDir in Directory.GetDirectories(driveName))
                {
                    writer.WriteLine(subDir);
                }
            }

            var copyPath = Path.Combine(dir, "copy_akpdirinfo.txt");
            File.Copy(filePath, copyPath);
            File.Delete(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в методе InspectDrive: {ex.Message}");
        }
    }

    public static void CopyFilesWithExtension(string sourceDir, string extension)
    {
        try
        {
            var targetDir = Path.Combine(sourceDir, "AKPFiles");
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir, "*" + extension))
            {
                var destFile = Path.Combine(targetDir, Path.GetFileName(file));
                File.Copy(file, destFile);
            }

            var inspectDir = Path.Combine(sourceDir, "AKPInspect");
            Directory.CreateDirectory(inspectDir);
            Directory.Move(targetDir, Path.Combine(inspectDir, "AKPFiles"));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в методе CopyFilesWithExtension: {ex.Message}");
        }
    }

    public static void ArchiveFiles(string sourceDir)
    {
        try
        {
            var zipPath = Path.Combine(sourceDir, "AKPFiles.zip");
            ZipFile.CreateFromDirectory(sourceDir, zipPath);

            var extractPath = Path.Combine(sourceDir, "ExtractedAKPFiles");
            ZipFile.ExtractToDirectory(zipPath, extractPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в методе ArchiveFiles: {ex.Message}");
        }
    }
}

// 6. Основная программа
class Program
{
    static void Main()
    {
        try
        {
            // Логирование
            AKPLog.WriteLog("Program started", "Демонстрация работы классов");

            // Работа с дисками
            AKPDiskInfo.PrintFreeSpace("D:\\");
            AKPDiskInfo.PrintFileSystemInfo("D:\\");
            AKPDiskInfo.PrintAllDrivesInfo();

            // Работа с файлами
            AKPFileInfo.PrintFileInfo("D:\\example.txt");

            // Работа с директориями
            AKPDirInfo.PrintDirectoryInfo("D:\\Temp");

            // Работа с файловым менеджером
            AKPFileManager.InspectDrive("D:\\");
            AKPFileManager.CopyFilesWithExtension("D:\\Temp", ".txt");
            AKPFileManager.ArchiveFiles("D:\\Temp\\AKPInspect");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в программе: {ex.Message}");
        }
    }
}
