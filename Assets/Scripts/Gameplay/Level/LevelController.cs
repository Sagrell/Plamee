using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public float speed = 5f;
    public float maxSpeed = 20f;
    public float timeToGetMaximum = 60f;

    public GameObject spawner;
    //Текущее положение
    Vector2 currentPosition;
    //Текущая скорость
    float currentSpeed;

    bool isTriangle;
    //Переменные "кэша" для быстроты обращения
    Transform _transform;
    
	void Start () {
        //Удаляем все компоненты со spawner и добавляем нужный
        Destroy(spawner.GetComponent<LevelSpawner>());
        switch (LevelManager.currentLevel.index)
        {
            case 0:
                spawner.AddComponent<Level01Spawner>();
                break;
            case 1:
                spawner.AddComponent<Level02Spawner>();
                break;
            case 2:
                spawner.AddComponent<Level03Spawner>();
                break;
        }
        //Меняется задний фон и гравитация 
        Camera.main.backgroundColor = LevelManager.currentLevel.background;
        Physics2D.gravity = LevelManager.currentLevel.gravity;

        //Инициализация
        _transform = transform;
        currentPosition = _transform.position;
        currentSpeed = speed;
        //Запускаем со-программу по контролю за скоростью
        StartCoroutine("ControllSpeed");
        isTriangle = CharacterManager.currentCharacter.type == CharacterType.Triangle;
    }
	
	void Update () {
        //Уровень передвигается влево со скоростью currentSpeed
        currentPosition.x -= currentSpeed * Time.deltaTime;
        _transform.position = currentPosition;
	}

    //Со-программа по контролю за скоростью 
    //(можно было реализовать более производительно изменяя скорость не каждый кадр)
    IEnumerator ControllSpeed()
    {
        float triangleT;
        float traingleStartSpeed = (speed + maxSpeed) / 2;
        for (float t = 0; t <= 1 && currentSpeed < maxSpeed; t+=Time.deltaTime/timeToGetMaximum)
        {
            //Если выбран треугольник и прошла половина времени, скорость изменяется квадратично
            if( isTriangle && t >0.5f )
            {
                //t приводится к диапазону от 0 до 1
                triangleT = (t - .5f) * 2;
                //Интерполируется скорость от traingleStartSpeed до maxSpeed
                currentSpeed = Mathf.Lerp(traingleStartSpeed, maxSpeed, triangleT* triangleT);
            } else
            {
                //Интерполируется скорость от speed до maxSpeed
                currentSpeed = Mathf.Lerp(speed, maxSpeed, t);
            }  
            //Ждет следующий кадр
            yield return null;
        }
        //Присваивает максимальное значение
        currentSpeed = maxSpeed;
    }
}
