using UnityEngine;

public class LevelViewController : MonoBehaviour
{
    //Панели
    public LevelItem[] items;

    void Start()
    {
        Init();
    }
    void Init()
    {
        //Инициализация всех панелей
        int currLevelIndex = DataManager.Instance.Data.selectedLevel;
        for (int i = 0; i < items.Length; i++)
        {
            items[i].Init(i == currLevelIndex);
        }
    }
    public void ChangeLevel(int newIndex)
    {
        //Изменение уровня
        LevelManager.Instance.ChangeCurrLevel(newIndex);
        //Обновляется информация
        Init();
    }
}
