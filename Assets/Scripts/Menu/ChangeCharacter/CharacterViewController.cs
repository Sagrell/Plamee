using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewController : MonoBehaviour {

    //Панели
    public CharacterItem[] items;

	void Start () {
        Init();
    }
    void Init()
    {
        //Инициализация всех панелей
        int currCharacterIndex = DataManager.Instance.Data.selectedCharacter;
        for (int i = 0; i < items.Length; i++)
        {
            items[i].Init(i == currCharacterIndex);
        }
    }
    public void ChangeCharacter(int newIndex)
    {
        //Изменение персонажа
        CharacterManager.Instance.ChangeCurrCharacter(newIndex);
        //Обновляется информация
        Init();
    }
}
