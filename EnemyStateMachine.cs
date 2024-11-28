using System;
using UnityEngine;
using Library;

public class EnemyStateMachine : StateMachine
{
    public IState Idle;
    public IState Attack0;

    [SerializeField]private float _changeStateTime;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        Idle = new EnemyIdle(this);
        Attack0 = new EnemyStraightAttack(this, Resources.Load("Enemt Attacck Mode/BossStraightAtk 0") as EnemyAttackMode);
        
        
        ChangeState(Idle);
    }

    private void LateUpdate()
    {
        _changeStateTime -= Time.deltaTime;
    }

    public override void ChangeState(IState state)
    {
        base.ChangeState(state);

        _changeStateTime = currentStateName == Idle.ToString() ? 15 : 10;
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
    private bool isExit;

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
        {
            Vector3 pos = _sm.transform.position;
            
            if (Vector3.Distance(new Vector3(0, pos.y),
                    new Vector3(pos.x, pos.y)) >= .1f)
            {
                Vector3 dir = new Vector3(0, pos.y) - _sm.transform.position;
                pos += new Vector3(Mathf.Sign(dir.x), 0) * Time.deltaTime * 3;
                _sm.transform.position = pos;
            }
            else
            {
                _sm.ChangeState(_sm.Idle);
            }
        }

        if (_atkCoolTime <= 0 & _sm.GetChangeTime() >= 0)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 dir = player.position - _sm.transform.position;

            if (Vector3.Distance(new Vector3(player.transform.position.x, 0),
                    new Vector3(_sm.transform.position.x, 0)) >= .1f)
            {
                Vector3 pos = _sm.transform.position;
                pos += new Vector3(Mathf.Sign(dir.x), 0) * Time.deltaTime * 3;
                _sm.transform.position = pos;
            }
            else
            {
                EnemyAttack.Attack(_mode, _sm.transform);
                _atkCoolTime = _mode.rate;
            }
        }
    }

    void ExitState()
    {
        _sm.ChangeState(_sm.Idle);
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