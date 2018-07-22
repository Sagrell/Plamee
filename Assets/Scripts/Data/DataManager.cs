using UnityEngine;

public class DataManager : MonoBehaviour {

    public static DataManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (!FileManager.IsSavesFileExists())
            {
                FileManager.CreateSavesFile();
                SaveDefaultData();
            }
            else
            {
                Load();
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    UserData userData;

    public UserData Data
    {
        get { return userData; }
        set
        {
            userData = value;
            userData.hashOfContent = Service.GenerateHashFromUserData(userData);
            FileManager.SaveData(JsonUtility.ToJson(userData));
        }
    }
    //Загружает данные
    public void Load()
    {
        string saveData = FileManager.LoadData();
        UserData tempData = JsonUtility.FromJson<UserData>(saveData);

        string hash = tempData.hashOfContent;
        if (Service.GenerateHashFromUserData(tempData).Equals(hash))
        {
            userData = tempData;
        }
        else
        {
            SaveDefaultData();
        }
    }
    //Сохраняет данные по-умолчанию
    public void SaveDefaultData()
    {
        userData = new UserData()
        {
            selectedCharacter = 0,
            selectedLevel = 0
        };

        userData.hashOfContent = Service.GenerateHashFromUserData(userData);
        FileManager.SaveData(JsonUtility.ToJson(userData));
    }
    
    
    //Сохраняет текующего персонажа
    public void SaveCharacter(int character)
    {
        userData.selectedCharacter = character;
        userData.hashOfContent = Service.GenerateHashFromUserData(userData);
        FileManager.SaveData(JsonUtility.ToJson(userData));
    }
    //Сохраняет текующий уровень
    public void SaveLevel(int level)
    {
        userData.selectedLevel = level;
        userData.hashOfContent = Service.GenerateHashFromUserData(userData);
        FileManager.SaveData(JsonUtility.ToJson(userData));
    }


}
