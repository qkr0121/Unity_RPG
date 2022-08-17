
public class StateMachine<T> where T : class
{
    // StateMachine 소유주
    private T ownerEntity;

    // 현재 상태
    private State<T> currentState;

    // 초기 설정
    public void SetUP(T own, State<T> entryState)
    {
        ownerEntity = own;
        currentState = entryState;
    }

    // 현재상태 실행
    public void Execute()
    {
        currentState.Execute(ownerEntity);
    }

    // 상태를 변화시킵니다.
    public void ChangeState(State<T> nextState)
    {
        // 상태변경 불가능 상태면 실행하지 않습니다.
        if (!nextState.tryChangeState) return;

        // 현재상태의 Exit()를 호출합니다.
        currentState.Exit(ownerEntity);

        // 다음상태로 변경합니다.
        currentState = nextState;

        // 변경된 상태의 Enter()를 호출합니다.
        currentState.Enter(ownerEntity);
    }

}
