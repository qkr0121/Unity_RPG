
// 스킬정보
public struct SkillInfo
{
    // 공격타입, 스킬이름, 쿨타임, 레벨, 행동시간
    public AttackType attackType;
    public string skillName;
    public float skillCool;
    public int skillLevel;
    public float actTime;

    public bool useable;

    public SkillInfo(AttackType attacktype, string skillname, float skillcool, int skilllevel, float acttime, bool useAble)
    {
        attackType = attacktype;
        skillName = skillname;
        skillCool = skillcool;
        skillLevel = skilllevel;
        actTime = acttime;

        useable = useAble;
    }
}
