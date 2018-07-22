using UnityEngine;

//Решение не мое, взято из лекции на YouTube канале Unity: https://www.youtube.com/watch?v=wbmjturGbAQ

//Абстрактный класс меню
public abstract class Menu<T> : Menu where T : Menu<T> {

    #region Singleton
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = (T)this;
    }
    protected virtual void OnDestroy()
    {
        Instance = null;
    }
    #endregion

    //Метод, открывающий меню T
    protected static void Open()
    {
        if(Instance != null)
        {
            return;
        }

        MenuManager.Instance.OpenMenu<T>();
    }
    //Метод, зыкрывающий меню T
    protected static void Close()
    {
        if (Instance == null)
        {
            return;
        }

        MenuManager.Instance.CloseMenu();
    }

    //Обработка нажатия кнопки Back по-умолчанию.
    public override void OnBackPressed()
    {
        Close();
    }
}

//Абстрактный класс меню, наследуюемый от MonoBehaviour
public abstract class Menu : MonoBehaviour
{
    public abstract void OnBackPressed();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnBackPressed();
    }
}