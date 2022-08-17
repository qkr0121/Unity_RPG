using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ĳ���Ϳ� ���� healthBar�� ǥ���մϴ�.
public sealed class HealthBarUI : MonoBehaviour
{
    [Header("�÷��̾� HealthBarUI")]
    [SerializeField] private Slider _PlayerHealthBarUI;

    [Header("�÷��̾� ManaBarUI")]
    [SerializeField] private Slider _PlayerManaBarUI;

    // ü�¹ٸ� ��Ÿ�� �÷��̾�ĳ����
    private Player player;

    // ü�¹ٸ� ǥ���� ���͵�
    private List<Monster> monsters;

    // ���� ü�¹� UI
    private List<RectTransform> monsterhealthUI;

    // ����ī�޶�
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

    // UI�� ���ϴ� ��ġ�� ǥ���մϴ�.
    public void DisPlayHealthBar()
    {
        // �÷��̾�
        _PlayerHealthBarUI.value = player.characterInfo.health / 100;

        // ����
        for (int i = 0; i < monsters.Count; i++) 
        {
            Slider healthImage = monsterhealthUI[i].GetComponent<Slider>();
            healthImage.value = monsters[i].characterInfo.health / 100;

            monsterhealthUI[i].position = camera.WorldToScreenPoint(monsters[i].transform.position);
        }
    }

    // ü�¹ٸ� ǥ���� ���͸� �߰��մϴ�.
    public void AddMonster(Monster target)
    {
        // ���� �̹� UI�� �����Ǿ��ִٸ� �������� �ʽ��ϴ�.
        if (monsters.Contains(target))
            return;

        monsters.Add(target);

        GameObject obj = (GameObject)Instantiate(Resources.Load("Prefabs/UI/HealthBarUI"), transform);
        monsterhealthUI.Add(obj.GetComponent<RectTransform>());
    }

    // ü�¹ٰ� ǥ�õǾ� �ִ� ������ ü�¹ٸ� �����մϴ�.
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
