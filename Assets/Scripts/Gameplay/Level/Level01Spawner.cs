using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Спавнер для 1 уровня
public class Level01Spawner : LevelSpawner {

    bool isSpike;

    void Start()
    {
        //Инициализация
        isSpike = true;
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
        ElementsPool.PickFromPool("Simple_1", _transform.position, parent: _transform.parent);
        //Двигается спавнер
        currPosition.x += TILE_LENGTH;
        _transform.localPosition = currPosition;
    }
    override protected void Spawn()
    {
        //40% шанс выпадения шипа, при том, что два раза подряд выпасть он не может
        isSpike = Random.value < .4f && !isSpike ? true : false;
        if (!isSpike)
            ElementsPool.PickFromPool("Simple_1", _transform.position, parent: _transform.parent);
        else
            ElementsPool.PickFromPool("WithSpike", _transform.position, parent: _transform.parent);
        //Двигается спавнер
        currPosition.x += TILE_LENGTH;
        _transform.localPosition = currPosition;
    }
}
