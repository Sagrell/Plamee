using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    //Ссылка на текст
    public GameObject gameOverText;

    //Игра оконченна?
    bool isGameOver;

    void Start()
    {
        isGameOver = false;
        //Подписываемся на смерть персонажа
        EventManager.StartListening("Die", GameOver);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.SetActive(true);
    }
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }


    void Update()
    {
        //Обрабатываем тап по экрану, если игрок погиб
#if UNITY_ANDROID && !UNITY_EDITOR
        if(isGameOver && ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)||Input.GetKeyDown(KeyCode.Escape)))
#else
        if (isGameOver && (Input.GetKeyDown("space")|| Input.GetKeyDown(KeyCode.Escape)))
#endif
        {
            Exit();
        }
	}
}
