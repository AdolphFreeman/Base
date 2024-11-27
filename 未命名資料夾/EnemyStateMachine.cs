using System;
using UnityEngine;
using Library;

public class EnemyStateMachine : StateMachine
{
    public IState Idle;
    public IState Attack0;

    private float _changeStateTime;

    private void Awake()
    {
        Idle = new EnemyIdle(this);
        Attack0 = new EnemyStraightAttack(this, Resources.Load("Enemy Attack Mode/Boss 0 -  1") as EnemyAttackMode);
        
        
        ChangeState(Idle);
    }

    private void LateUpdate()
    {
        _changeStateTime -= Time.deltaTime;
    }

    public override void ChangeState(IState state)
    {
        base.ChangeState(state);

        _changeStateTime = currentStateName == Idle.ToString() ? 15 : 30;
    }

    public float GetChangeTime()
    {
        return _changeStateTime;
    }
}

public class EnemyIdle : IState
{
    readonly EnemyStateMachine _sm;

    public EnemyIdle(EnemyStateMachine sm)
    {
        _sm = sm;
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
        if(_sm.GetChangeTime() <= 0)
            _sm.ChangeState(_sm.Attack0);
    }

    public void Exit()
    {
        
    }
}

public class EnemyStraightAttack : IState
{
    readonly EnemyStateMachine _sm;
    readonly EnemyAttackMode _mode;
    
    private float _atkCoolTime;

    public EnemyStraightAttack(EnemyStateMachine sm, EnemyAttackMode mode)
    {
        _sm = sm;
        _mode = mode;
    }

    public void Enter()
    { 
        _atkCoolTime = _mode.rate;
    }

    public void Update()
    {
        _atkCoolTime -= Time.deltaTime;
        
        if(_sm.GetChangeTime() <= 0)
            _sm.ChangeState(_sm.Idle);

        if (_atkCoolTime <= 0)
        {
            EnemyAttack.Attack(_mode, _sm.transform);
            _atkCoolTime = _mode.rate;
        }
    }

    public void Exit()
    {
        
    }
}

public class EnemyRadialAttack : IState
{
    private EnemyStateMachine _sm;

    public EnemyRadialAttack(EnemyStateMachine sm)
    {
        _sm = sm;
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        
    }
}