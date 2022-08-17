using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : ManagerClassBase<ObjectPoolManager>
{
    [Header("Ǯ���� ������Ʈ")]
    [SerializeField] private List<GameObject> _PoolingObject;

    // Ǯ
    private Dictionary<int, Queue<GameObject>> _PoolDict;

    // ������ Ǯ
    private Dictionary<int, GameObject> _SampleDict;

    private void Awake()
    {
        int count = _PoolingObject.Count;

        _PoolDict = new Dictionary<int, Queue<GameObject>>(count);
        _SampleDict = new Dictionary<int, GameObject>(count);

        // ����ϱ�
        for(int i=0; i<count; i++)
        {
            Register(i, _PoolingObject[i]);
        }
    }

    // Ǯ�� ����ϱ�
    public void Register(int key, GameObject poolObject)
    {
        // �ش� Ǯ�� �����ϸ� ������� �ʽ��ϴ�.
        if (_PoolDict.ContainsKey(key)) return;

        GameObject sample = Instantiate(poolObject);
        sample.SetActive(false);

        Queue<GameObject> queue = new Queue<GameObject>();

        // ���
        _SampleDict.Add(key, sample);
        _PoolDict.Add(key, queue);
    }

    // ���� ������Ʈ���� �����ϱ�
    private GameObject CloneSample(int key)
    {
        if (!_SampleDict.TryGetValue(key, out GameObject sample)) return null;

        return Instantiate(sample);
    }

    // Ǯ���� ��������
    public GameObject Spawn(int key)
    {
        // Ǯ�� Ű�� �������� ������ �������� �ʽ��ϴ�.
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
