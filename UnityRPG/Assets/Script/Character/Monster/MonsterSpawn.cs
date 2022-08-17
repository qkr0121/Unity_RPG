using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MonsterSpawn : MonoBehaviour
{
    [Header("소환할 몬스터번호")]
    [SerializeField] private List<int> _MonsterNum;

    [Header("몬스터를 조종할 컨트롤러")]
    [SerializeField] private List<MonsterController> _MonsterControllers; 

    [Header("리스폰 하는데 걸리는 시간")]
    [SerializeField] private float respawnTime;

    private void Start()
    {
        
        for(int i=0; i< _MonsterControllers.Count; i++)
        {
            // 오브젝트 풀에서 해당 몬스터를 불러와서 저장합니다.
            GameObject obj = ObjectPoolManager.Instance.Spawn(_MonsterNum[i]);

            obj.transform.SetParent(_MonsterControllers[i].transform);
            obj.transform.localPosition = Vector3.zero;
            _MonsterControllers[i].spawnNum = i;
            _MonsterControllers[i].monster = obj.GetComponent<Monster>();
        }
    }

    // 소환
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
