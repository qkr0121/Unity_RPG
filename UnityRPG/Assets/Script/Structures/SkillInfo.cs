
// 스킬정보
public struct SkillInfo
{
    // 공격타입, 스킬이름, 쿨타임, 남은시간, 레벨, 행동시간
    public AttackType attackType;
    public string skillName;
    public float skillCool;
    public float leftCoolTime;
    public int skillLevel;
    public float actTime;

    public bool useable;

    public SkillInfo(AttackType attacktype, string skillname, float skillcool, int skilllevel, float acttime, bool useAble)
    {
        attackType = attacktype;
        skillName = skillname;
        skillCool = skillcool;
        leftCoolTime = 0;
        skillLevel = skilllevel;
        actTime = acttime;

        useable = useAble;
    }
}
