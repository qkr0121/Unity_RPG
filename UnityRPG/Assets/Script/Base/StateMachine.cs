
public class StateMachine<T> where T : class
{
    // StateMachine ������
    private T ownerEntity;

    // ���� ����
    private State<T> currentState;

    // �ʱ� ����
    public void SetUP(T own, State<T> entryState)
    {
        ownerEntity = own;
        currentState = entryState;
    }

    // ������� ����
    public void Execute()
    {
        currentState.Execute(ownerEntity);
    }

    // ���¸� ��ȭ��ŵ�ϴ�.
    public void ChangeState(State<T> nextState)
    {
        // ���º��� �Ұ��� ���¸� �������� �ʽ��ϴ�.
        if (!nextState.tryChangeState) return;

        // ��������� Exit()�� ȣ���մϴ�.
        currentState.Exit(ownerEntity);

        // �������·� �����մϴ�.
        currentState = nextState;

        // ����� ������ Enter()�� ȣ���մϴ�.
        currentState.Enter(ownerEntity);
    }

}
