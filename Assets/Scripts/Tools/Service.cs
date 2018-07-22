using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

// Класс помощник 
public static class Service {

    // Строку в двоичный вид (каждый "бит" имеет тип char) 
    public static string StringToBinary(string data)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char c in data.ToCharArray())
        {
            sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
        }
        return sb.ToString();
    }

    // Обратный процесс 
    public static string BinaryToString(string data)
    {
        List<byte> byteList = new List<byte>();

        for (int i = 0; i < data.Length; i += 8)
        {
            byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
        }
        return Encoding.ASCII.GetString(byteList.ToArray());
    }

    // Генерация хэша исходя из сохраненных данных
    public static string GenerateHashFromUserData(UserData userData)
    {
        userData.hashOfContent = "0105199627041997VLADUESMYOCUDONTEVENTRY";
        string saveContent = JsonUtility.ToJson(userData, true);
        SHA256Managed crypt = new SHA256Managed();
        string hash = string.Empty;

        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(saveContent), 0, Encoding.UTF8.GetByteCount(saveContent));

        for (int i = 0; i < crypto.Length; i++)
        {
            hash += (crypto[i] + i * (i - 1)).ToString("x2");
        }

        return hash;
    }
}
