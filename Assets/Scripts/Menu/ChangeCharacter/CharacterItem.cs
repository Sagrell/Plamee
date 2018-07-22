using UnityEngine;

//Класс панели с выбором персонажа
public class CharacterItem : MonoBehaviour {

    //Галочка, что выбрано
    public GameObject selected;
    //Кнопка выбрать
    public GameObject select;

    //Инициализация, согласно тому, выбран ли данный персонаж
    public void Init(bool isSelected)
    {
        select.SetActive(!isSelected);
        selected.SetActive(isSelected);
    }
	
}
