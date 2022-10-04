using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBarUI : MonoBehaviour
{
    private Player _Player;

    // 스킬 UI
    [Header("스킬 쿨타임 BackGround")]
    [SerializeField] private List<Image> _SkillCoolBackGround;

    // 남은 쿨타임 Text
    [Header("남은 쿨타임 Text")]
    [SerializeField] private List<TextMeshProUGUI> _LeftTimeText;

    private void Start()
    {
        _Player = PlayerManager.Instance.player;
    }

    // 스킬 쿨타임 UI를 진행시킵니다.
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
            // 남은 시간에 맞춰 BackGround 와 Text 변경
            _SkillCoolBackGround[inputKey - 1].fillAmount =
                _Player.characterInfo.skills[inputKey].skillInfo.leftCoolTime /
                _Player.characterInfo.skills[inputKey].skillInfo.skillCool;

            _LeftTimeText[inputKey - 1].text = ((int)_Player.characterInfo.skills[inputKey].skillInfo.leftCoolTime + 1).ToString();

            // 계속진행
            StartCoroutine(CoolDownUI(inputKey));
        }
        else
            _LeftTimeText[inputKey - 1].gameObject.SetActive(false);
    }
}
