
// ��ų����
public struct SkillInfo
{
    // ����Ÿ��, ��ų�̸�, ��Ÿ��, ����, �ൿ�ð�
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
