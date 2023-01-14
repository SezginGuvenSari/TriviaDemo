using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    #region Struct

    [System.Serializable]
    public struct Pool
    {
        public Queue<GameObject> _pool;

        public GameObject _dataPrefab;

        public int _poolSize;

        public Transform _dataT;

    }

    #endregion

    #region References

    [SerializeField] private Pool[] _pools;

    #endregion

    #region OnEnable / OnDisable

    private void OnEnable()
    {
        EventManager.OnGetEnableObjects += GetEnableObjects;
        EventManager.OnGetObjectsInPool += GetPooledObject;
    }

    private void OnDisable()
    {
        EventManager.OnGetEnableObjects -= GetEnableObjects;
        EventManager.OnGetObjectsInPool -= GetPooledObject;
    }

    #endregion

    private void Awake() => PopulatePool();

    private void PopulatePool()
    {
        for (int j = 0; j < _pools.Length; j++)
        {
            _pools[j]._pool = new Queue<GameObject>();
            for (int i = 0; i < _pools[j]._poolSize; i++)
            {
                GameObject obj = Instantiate(_pools[j]._dataPrefab, _pools[j]._dataT);
                obj.SetActive(false);
                _pools[j]._pool.Enqueue(obj);
            }
        }
    }

    private GameObject GetPooledObject(int objectType)
    {
        if (objectType >= _pools.Length) return null;

        GameObject obj = _pools[objectType]._pool.Dequeue();
        obj.SetActive(true);
        _pools[objectType]._pool.Enqueue(obj);
        return obj;
    }

    private void GetEnableObjects(int length)
    {
        for (int i = 0; i < length; i++)
        {
            GetPooledObject(0);
        }
    }

}
