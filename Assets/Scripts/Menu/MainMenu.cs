using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Главное меню
public class MainMenu : Menu<MainMenu>
{
    //Отобразить
    public static void Show()
    {
        Open();
    }
    //Обработка нажатия выбора уровня
    public void OnChangeLevelPressed()
    {
        ChangeLevel.Show();
    }
    //Обработка нажатия выбора игрока
    public void OnChangePlayerPressed()
    {
        ChangeCharacter.Show();
    }
    public void OnPlayPressed()
    {
        SceneManager.LoadScene("Gameplay");
    }
    //Обработка нажатия кнопки назад
    public override void OnBackPressed()
    {
        Application.Quit();
    }
}
