
// ��ų����
public struct SkillInfo
{
    // ����Ÿ��, ��ų�̸�, ��Ÿ��, �����ð�, ����, �ൿ�ð�
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
