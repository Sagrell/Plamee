using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Спавнер для 2 уровня
public class Level02Spawner : LevelSpawner {

    bool isFireball;

    void Start()
    {
        //Инициализация
        isFireball = true;
        _transform = transform;
        playerPosition = FindObjectOfType<PlayerController>().transform.position;
        currPosition = _transform.localPosition;
        //Спавнит первые платформы безопасными
        while (currPosition.x - playerPosition.x <= SPAWN_DISTANCE * TILE_LENGTH)
        {
            SpawnSafe();
        }
    }
    void Update()
    {
        //Если дистанция меньше, чем должна быть, спавнит новый элемент 
        float distance = _transform.position.x - playerPosition.x;
        if (distance <= SPAWN_DISTANCE * TILE_LENGTH)
        {
            Spawn();
        }
    }
    override protected void SpawnSafe()
    {
        //Спавн обычной платформы из пула
        ElementsPool.PickFromPool("Simple_2", _transform.position, parent: _transform.parent);
        //Двигается спавнер
        currPosition.x += TILE_LENGTH;
        _transform.localPosition = currPosition;
    }
    override protected void Spawn()
    {
        //50% шанс выпадения огненного шара, при том, что два раза подряд выпасть он не может
        isFireball = Random.value < .5f && !isFireball ? true : false;
        if (isFireball)
        {
            //Рандомно выбирается какой (сверху или снизу)
            switch (Random.Range(0, 2))
            {
                case 0:
                    ElementsPool.PickFromPool("WithBottomFireBall", _transform.position, parent: _transform.parent);
                    break;
                case 1:
                    ElementsPool.PickFromPool("WithTopFireBall", _transform.position, parent: _transform.parent);
                    break;
            }   
        }   
        else
            ElementsPool.PickFromPool("Simple_2", _transform.position, parent: _transform.parent);

        //Двигается спавнер
        currPosition.x += TILE_LENGTH;
        _transform.localPosition = currPosition;
    }
}
