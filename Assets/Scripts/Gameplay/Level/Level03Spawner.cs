using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Спавнер для 3 уровня
public class Level03Spawner : LevelSpawner {

    bool isEnemy;

    void Start()
    {
        //Инициализация
        isEnemy = true;
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
        ElementsPool.PickFromPool("Simple_3", _transform.position, parent: _transform.parent);
        //Двигается спавнер
        currPosition.x += TILE_LENGTH;
        _transform.localPosition = currPosition;
    }
    override protected void Spawn()
    {
        //60% шанс выпадения врага, при том, что два раза подряд выпасть он не может
        isEnemy = Random.value < .6f && !isEnemy ? true : false;
        if (isEnemy)
        {
            //Рандомно выбирается какой (змеи, робот или мина)
            switch (Random.Range(0, 3))
            {
                case 0:
                    ElementsPool.PickFromPool("WithSnakes", _transform.position, parent: _transform.parent);
                    break;
                case 1:
                    ElementsPool.PickFromPool("WithRobot", _transform.position, parent: _transform.parent);
                    break;
                case 2:
                    ElementsPool.PickFromPool("WithMine", _transform.position, parent: _transform.parent);
                    break;
            }
        }
        else
            ElementsPool.PickFromPool("Simple_3", _transform.position, parent: _transform.parent);
        //Двигается спавнер
        currPosition.x += TILE_LENGTH;
        _transform.localPosition = currPosition;
    }
}