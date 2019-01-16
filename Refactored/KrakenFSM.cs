using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenFSM : MonoBehaviour
{
    public enum EnumState { PATROL, CHASE, ATTACK, UNDERWATER };
    private EnumState state;

    public Kraken kraken;
    private Dictionary<EnumState, KrakenState> stateDictionary = new Dictionary<EnumState, KrakenState>();
    private KrakenState currentState;
    private bool isStateInit = false;

    public void InitFSM()
    {
        InitStateMachine();
        SetInitialState();
    }

    void Update()
    {
        if (isStateInit)
        {
            currentState.OnUpdate();
        }
    }

    protected void InitStateMachine()
    {
        stateDictionary.Add(EnumState.PATROL, GetComponent<KrakenPatrolState>());
        stateDictionary.Add(EnumState.CHASE, GetComponent<KrakenChaseState>());
        stateDictionary.Add(EnumState.ATTACK, GetComponent<KrakenAttackState>());
        stateDictionary.Add(EnumState.UNDERWATER, GetComponent<KrakenUnderWaterState>());
    }

    void SetInitialState()
    {
        SetState(EnumState.PATROL);
        isStateInit = true;
    }

    public void SetState(EnumState state)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        this.state = state;

        currentState = stateDictionary[state];
        currentState.OnStart();
    }

    public KrakenState GetCurrentState()
    {
        return currentState;
    }
}

