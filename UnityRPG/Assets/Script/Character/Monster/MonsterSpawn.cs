using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MonsterSpawn : MonoBehaviour
{
    [Header("��ȯ�� ���͹�ȣ")]
    [SerializeField] private List<int> _MonsterNum;

    [Header("���͸� ������ ��Ʈ�ѷ�")]
    [SerializeField] private List<MonsterController> _MonsterControllers; 

    [Header("������ �ϴµ� �ɸ��� �ð�")]
    [SerializeField] private float respawnTime;

    private void Start()
    {
        
        for(int i=0; i< _MonsterControllers.Count; i++)
        {
            // ������Ʈ Ǯ���� �ش� ���͸� �ҷ��ͼ� �����մϴ�.
            GameObject obj = ObjectPoolManager.Instance.Spawn(_MonsterNum[i]);

            obj.transform.SetParent(_MonsterControllers[i].transform);
            obj.transform.localPosition = Vector3.zero;
            _MonsterControllers[i].spawnNum = i;
            _MonsterControllers[i].monster = obj.GetComponent<Monster>();
        }
    }

    // ��ȯ
    IEnumerator Summon(int spawnnum)
    {
        yield return new WaitForSeconds(respawnTime);
        GameObject obj = ObjectPoolManager.Instance.Spawn(_MonsterNum[spawnnum]);
        obj.transform.SetParent(_MonsterControllers[spawnnum].transform);
        obj.transform.localPosition = Vector3.zero;
        _MonsterControllers[spawnnum].monster = obj.GetComponent<Monster>();
    }

    public void StartSummon(int spawnnum)
    {
        StartCoroutine(Summon(spawnnum));
    }
}
