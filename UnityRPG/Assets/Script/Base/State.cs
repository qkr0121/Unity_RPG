using UnityEngine;

public abstract class State<T> where T : class
{
    /// �ش� ���� ����, ������Ʈ, ����� �����մϴ�.

    public abstract void Enter(T entity);

    public abstract void Execute(T entity);

    public abstract void Exit(T entity);

    // ���� ���� ���ɿ��θ� ��Ÿ���ϴ�.
    public bool tryChangeState = true;
}
