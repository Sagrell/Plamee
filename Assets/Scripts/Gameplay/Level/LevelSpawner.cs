using UnityEngine;

public abstract class LevelSpawner : MonoBehaviour {

    //Константы
    protected const float TILE_LENGTH = 3;
    protected const float SPAWN_DISTANCE = 8;

    //Данные для определения расстояния при котором нужно спавнить
    protected Vector2 playerPosition;
    protected Vector2 currPosition;
    //Кэшированный transform
    protected Transform _transform;

    //Создает любую платформу,согласно текущему уровню
    protected abstract void Spawn();
    //Создает безопасную платформу
    protected abstract void SpawnSafe();
}
