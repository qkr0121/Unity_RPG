using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBarUI : MonoBehaviour
{
    private Player _Player;

    // ��ų UI
    [Header("��ų ��Ÿ�� BackGround")]
    [SerializeField] private List<Image> _SkillCoolBackGround;

    // ���� ��Ÿ�� Text
    [Header("���� ��Ÿ�� Text")]
    [SerializeField] private List<TextMeshProUGUI> _LeftTimeText;

    private void Start()
    {
        _Player = PlayerManager.Instance.player;
    }

    // ��ų ��Ÿ�� UI�� �����ŵ�ϴ�.
    public void CoolDown(int inputKey)
    {
        _SkillCoolBackGround[inputKey-1].fillAmount = 1;
        _LeftTimeText[inputKey - 1].gameObject.SetActive(true);

        StartCoroutine(CoolDownUI(inputKey));
    }

    private IEnumerator CoolDownUI(int inputKey)
    {
        yield return null;

        if (_SkillCoolBackGround[inputKey - 1].fillAmount > 0)
        {
            // ���� �ð��� ���� BackGround �� Text ����
            _SkillCoolBackGround[inputKey - 1].fillAmount =
                _Player.characterInfo.skills[inputKey].skillInfo.leftCoolTime /
                _Player.characterInfo.skills[inputKey].skillInfo.skillCool;

            _LeftTimeText[inputKey - 1].text = ((int)_Player.characterInfo.skills[inputKey].skillInfo.leftCoolTime + 1).ToString();

            // �������
            StartCoroutine(CoolDownUI(inputKey));
        }
        else
            _LeftTimeText[inputKey - 1].gameObject.SetActive(false);
    }
}
