using UnityEngine;

public class LevelManager : MonoBehaviour
{

    #region Singleton
    public static LevelManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    #endregion

    //Набор уровней
    public Level[] levels;
    //Текущий уровень
    public static Level currentLevel;

    //Менеджер сохраненных данных
    DataManager dataManager;

    void Start()
    {
        dataManager = DataManager.Instance;
        //Получает из сохраненных данных текущий уровень
        int currentLevelIndex = dataManager.Data.selectedLevel;
        currentLevel = levels[currentLevelIndex];
    }

    //Изменяет текущего уровень на новый
    public void ChangeCurrLevel(int newIndex)
    {
        if (newIndex < 0 || newIndex >= levels.Length)
        {
            return;
        }
        //Изменяет текущий уровень
        currentLevel = levels[newIndex];
        //Сохраняет данные
        dataManager.SaveLevel(newIndex);
    }
}
