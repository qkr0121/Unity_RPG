using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarUI : MonoBehaviour
{
    private Player _Player;

    // 사용할 스킬 리스트
    private List<Skill> _SkillImage;

    private void Start()
    {
        _Player = PlayerManager.Instance.player;

        
    }
}
