﻿using UnityEngine;
using FiniteStateMachine;

public class AI_Suicide : State<AI>
{
    private static AI_Suicide _instance;
    public static string _name = "suicide";
    private AI_Suicide()
    {
        if (_instance != null)
            return;
        _instance = this;
    }
    //get instance of the state
    public static AI_Suicide instance
    {
        get
        {
            //if there is no instance
            if (_instance == null)
                new AI_Suicide();      //create one
            return _instance;
        }
    }

    public static string name
    {
        get { return _name; }
        set { _name = value; }
    }

    public override void EnterState(AI _owner)
    {
        Debug.Log("Entering Suicide State");
        _owner.seek.Target = _owner.seek.Enemy;
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Suicide State");
    }

    public override void UpdateState(AI _owner)
    {
        if (_owner.IsDead()) { _owner.stateMachine.ChangeState(AI_Dead.instance); }
        else if (_owner.IsCloseEnough()) { _owner.stateMachine.ChangeState(AI_Evade.instance); }
        Suicide(_owner);
    }

    void Suicide(AI _owner)
    {
        var direction = _owner.seek.Target.transform.position - _owner.transform.position;
        _owner.transform.rotation = Quaternion.Slerp(_owner.transform.rotation,
                                    Quaternion.LookRotation(direction),
                                    _owner.critter.speed * Time.deltaTime);
        _owner.transform.Translate(0, 0, Time.deltaTime * _owner.critter.speed);
    }

}
