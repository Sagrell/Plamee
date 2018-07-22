using UnityEngine;

public class CharacterManager : MonoBehaviour {

    #region Singleton
    public static CharacterManager Instance;

    void Awake()
    {
        if(Instance==null)
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

    //Здесь хранятся все персонажи
    public Character[] characters;
    
    //Текущий персонаж
    public static Character currentCharacter;

    //Менеджер сохраненных данных
    DataManager dataManager;

    void Start () {
        dataManager = DataManager.Instance;
        //Получает из сохраненных данных текущего персонажа
        int currCharacterIndex = dataManager.Data.selectedCharacter;
        currentCharacter = characters[currCharacterIndex];
    }

    //Изменяет текущего персонажа на нового
    public void ChangeCurrCharacter(int newIndex)
    {
        if(newIndex<0 || newIndex>=characters.Length)
        {
            return;
        }
        //Изменяет текущего персонажа
        currentCharacter = characters[newIndex];
        //Сохраняет данные
        dataManager.SaveCharacter(newIndex);
    }
}
