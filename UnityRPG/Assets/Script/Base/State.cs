using UnityEngine;

public abstract class State<T> where T : class
{
    /// 해당 상태 시작, 업데이트, 종료시 실행합니다.

    public abstract void Enter(T entity);

    public abstract void Execute(T entity);

    public abstract void Exit(T entity);

    // 상태 변경 가능여부를 나타냅니다.
    public bool tryChangeState = true;
}
