using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsPool : MonoBehaviour {
    //Структура описания для пула определенного типа
    [System.Serializable]
    public struct PoolSettings
    {
        //Тип пула
        public string type;
        //Его размер
        public int size;
        //Префаб
        public GameObject prefab;
    }
    //Массив пулов
    public PoolSettings[] pools;
    //Куда помещаются все префабы (необязатально)
    public Transform container;

    //Словарь пулов (тип)=>(очередь из префабов)
    static Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start () {
        //Инициализируется словарь пула и наполняется
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        for (int i = 0; i < pools.Length; ++i)
        {
            PoolSettings poolSettings = pools[i];
            Queue<GameObject> pool = new Queue<GameObject>();

            for(int j = 0; j < poolSettings.size; ++j)
            {
                GameObject obj = Instantiate(poolSettings.prefab, container);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
            poolDictionary.Add(poolSettings.type, pool);
        }
	}
    
    //Берет объект из пула заданного типа и помещается в заданное место
    public static GameObject PickFromPool(string type, Vector3 position, Quaternion rotation = default(Quaternion), Transform parent = null)
    {
        // Если данного типо не существует, возвращается null
        if(!poolDictionary.ContainsKey(type))
        {
            return null;
        }
        //Берется первый объект из очереди
        GameObject obj = poolDictionary[type].Dequeue();
        //И сразу помещается в конец
        poolDictionary[type].Enqueue(obj);
        Transform _transform = obj.transform;
        //Активируется
        obj.SetActive(true);
        //Помещается в нужное место
        _transform.position = position;
        _transform.rotation = rotation;
        //Инициализация объекта (если есть)
        IPooledObject pooledObj = obj.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        //Назначается родительский элемент (если есть)
        if (parent!=null)
        {
            _transform.parent = parent;
        } 
        return obj;
    }

}
