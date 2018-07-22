using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//Решение не мое, взято из лекции на YouTube канале Unity: https://www.youtube.com/watch?v=wbmjturGbAQ

// Класс менеджер, отвечает за логику меню. Singleton
public class MenuManager : MonoBehaviour {

    #region Singleton
    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        MainMenu.Show();
    }

    private void OnDestroy()
    {
        Instance = null;
    }
    #endregion
    [Header("Menu prefabs")]
    public MainMenu MainMenuPrefab;
    public ChangeLevel ChangeLevelPrefab;
    public ChangeCharacter ChangePlayerPrefab;

    //Стек меню
    private Stack<Menu> menuStack = new Stack<Menu>();

    //Открыть меню типа T
    public void OpenMenu<T>() where T : Menu
    {
        //Получает префаб
        var prefab = GetPrefab<T>();
        //Создает его
        var menuToOpen = Instantiate(prefab, transform);

        //Закрывает предыдущее, если есть
        if (menuStack.Count > 0)
        {
            menuStack.Peek().gameObject.SetActive(false);
        }
        //Пушит в стек
        menuStack.Push(menuToOpen);
    }

    //Метод получения префаба меню T
    T GetPrefab<T>() where T : Menu
    {
        //Получает поля по флагу
        var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        //Ищет нужный и возвращает его
        foreach (var field in fields)
        {
            var prefab = field.GetValue(this) as T;
            if(prefab!=null)
            {
                return prefab;
            }
        }
        //Если не нашли
        throw new MissingReferenceException("Префаб с типом "+typeof(T)+" не был найден");
    }

    //Метод закрытия меню
    public void CloseMenu()
    {
        //Берет из стека текущее меню и уничтожает
        var menuToClose = menuStack.Pop();
        Destroy(menuToClose.gameObject);

        //Показывает предыдущее, если есть
        if (menuStack.Count > 0 )
        {
            menuStack.Peek().gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        //Обработка нажатия кнопки Back на Android
        if(Input.GetKeyDown(KeyCode.Escape) && menuStack.Count > 0 )
        {
            menuStack.Peek().OnBackPressed();
        }
    }

}
