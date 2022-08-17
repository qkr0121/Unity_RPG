using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 캐릭터와 몬스터 healthBar를 표시합니다.
public sealed class HealthBarUI : MonoBehaviour
{
    [Header("플레이어 HealthBarUI")]
    [SerializeField] private Slider _PlayerHealthBarUI;

    [Header("플레이어 ManaBarUI")]
    [SerializeField] private Slider _PlayerManaBarUI;

    // 체력바를 나타낼 플레이어캐릭터
    private Player player;

    // 체력바를 표시할 몬스터들
    private List<Monster> monsters;

    // 몬스터 체력바 UI
    private List<RectTransform> monsterhealthUI;

    // 메인카메라
    private Camera camera;

    private Image healthBar;

    private void Start()
    {
        player = PlayerManager.Instance.player;
        monsters = new List<Monster>();
        monsterhealthUI = new List<RectTransform>();
        camera = Camera.main;
    }

    private void Update()
    {
        DisPlayHealthBar();
    }

    // UI를 원하는 위치에 표시합니다.
    public void DisPlayHealthBar()
    {
        // 플레이어
        _PlayerHealthBarUI.value = player.characterInfo.health / 100;

        // 몬스터
        for (int i = 0; i < monsters.Count; i++) 
        {
            Slider healthImage = monsterhealthUI[i].GetComponent<Slider>();
            healthImage.value = monsters[i].characterInfo.health / 100;

            monsterhealthUI[i].position = camera.WorldToScreenPoint(monsters[i].transform.position);
        }
    }

    // 체력바를 표시할 몬스터를 추가합니다.
    public void AddMonster(Monster target)
    {
        // 만약 이미 UI가 생성되어있다면 실행하지 않습니다.
        if (monsters.Contains(target))
            return;

        monsters.Add(target);

        GameObject obj = (GameObject)Instantiate(Resources.Load("Prefabs/UI/HealthBarUI"), transform);
        monsterhealthUI.Add(obj.GetComponent<RectTransform>());
    }

    // 체력바가 표시되어 있는 몬스터의 체력바를 제거합니다.
    public void RemoveMonster(Monster target)
    {
        if (monsters.Contains(target))
        {
            int i = monsters.IndexOf(target);

            monsters.Remove(target);

            Destroy(monsterhealthUI[i].gameObject);

            monsterhealthUI.RemoveAt(i);
        }

    }
}
