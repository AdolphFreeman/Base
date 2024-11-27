using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState _currentState;
    public string currentStateName;

    // Update is called once per frame
    void Update()
    {
        _currentState.Update();
    }

    public virtual void ChangeState(IState state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState?.Enter();
        
        currentStateName = state.ToString();
    }
}

public interface IState
{
    public void Enter(){}
    public void Update(){}
    public void Exit(){}
}