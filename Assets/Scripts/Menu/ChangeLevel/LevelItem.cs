using UnityEngine;

//Класс панели с выбором уровня
public class LevelItem : MonoBehaviour {

    //Галочка, что выбрано
    public GameObject selected;
    //Кнопка выбрать
    public GameObject select;

    //Инициализация, согласно тому, выбран ли данный уровень
    public void Init(bool isSelected)
    {
        select.SetActive(!isSelected);
        selected.SetActive(isSelected);
    }

}
