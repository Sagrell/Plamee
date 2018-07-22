using UnityEngine;

//Тип персонажа (влияет на изменение скорости)
public enum CharacterType { Simple, Triangle }

[System.Serializable]
public class Character
{
    //Его имя
    public string name;
    //Количество вершин
    public float verticesCount;
    //Тип
    public CharacterType type;
    //Спрайт
    public Sprite sprite;
    //Цвета (на которые он будет изменяться)
    public Color[] colors;
}
