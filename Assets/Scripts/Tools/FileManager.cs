using System.IO;
using UnityEngine;

public static class FileManager {

    //Путь к сохранениям
    public static string savePath;

    //Статический конструктор, для определения пути сохранений
    static FileManager()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saves");
    }

    //Существует ли файл сохранений?
    public static bool IsSavesFileExists()
    {
        return File.Exists(savePath);
    }
    //Создает файл сохранений
    public static void CreateSavesFile()
    {
        File.Create(savePath).Dispose();
    }
    //Сохраняет данные из файла
    public static void SaveData(string content)
    {
        File.WriteAllText(savePath, Service.StringToBinary(content));
    }
    //Загружает данные из файла
    public static string LoadData()
    {
        return Service.BinaryToString(File.ReadAllText(savePath));
    }
}
