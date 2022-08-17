using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : ManagerClassBase<ObjectPoolManager>
{
    [Header("풀링할 오브젝트")]
    [SerializeField] private List<GameObject> _PoolingObject;

    // 풀
    private Dictionary<int, Queue<GameObject>> _PoolDict;

    // 복제용 풀
    private Dictionary<int, GameObject> _SampleDict;

    private void Awake()
    {
        int count = _PoolingObject.Count;

        _PoolDict = new Dictionary<int, Queue<GameObject>>(count);
        _SampleDict = new Dictionary<int, GameObject>(count);

        // 등록하기
        for(int i=0; i<count; i++)
        {
            Register(i, _PoolingObject[i]);
        }
    }

    // 풀에 등록하기
    public void Register(int key, GameObject poolObject)
    {
        // 해당 풀이 존재하면 등록하지 않습니다.
        if (_PoolDict.ContainsKey(key)) return;

        GameObject sample = Instantiate(poolObject);
        sample.SetActive(false);

        Queue<GameObject> queue = new Queue<GameObject>();

        // 등록
        _SampleDict.Add(key, sample);
        _PoolDict.Add(key, queue);
    }

    // 샘플 오브젝트에서 복제하기
    private GameObject CloneSample(int key)
    {
        if (!_SampleDict.TryGetValue(key, out GameObject sample)) return null;

        return Instantiate(sample);
    }

    // 풀에서 꺼내오기
    public GameObject Spawn(int key)
    {
        // 풀에 키가 존재하지 않을시 실행하지 않습니다.
        if (!_PoolDict.TryGetValue(key, out var pool)) return null;

        GameObject spawnObject;

        if(pool.Count > 0)
        {
            spawnObject = pool.Dequeue();
        }
        else
        {
            spawnObject = CloneSample(key);
        }

        spawnObject.SetActive(true);
        spawnObject.transform.SetParent(null);

        return spawnObject;
    }

    public void Despawn(int key, GameObject despawnObject)
    {
        if (!_PoolDict.TryGetValue(key, out var pool)) return;
        despawnObject.transform.SetParent(null);
        despawnObject.SetActive(false);
        pool.Enqueue(despawnObject);

    }
}
