using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarUI : MonoBehaviour
{
    private Player _Player;

    // ����� ��ų ����Ʈ
    private List<Skill> _SkillImage;

    private void Start()
    {
        _Player = PlayerManager.Instance.player;

        
    }
}
