using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] BossController bossController;
    [SerializeField] GameObject player;
    public State activeState;

    [Header("States")]
    public State defaultState;
    public State slashState;
    public State flameWaveState;
    public State stunnedState;
    public State deathState;
    public State unblockableState;

    private void Start()
    {
        defaultState.enabled = false;
        slashState.enabled = false;
        flameWaveState.enabled = false;
        stunnedState.enabled = false;

        ActivateState(defaultState);
    }

    public void RequestState(State state)
    {
        if(state == deathState && activeState == deathState)
        {
            return;
        }

        if(state.Priority >= activeState.Priority)
        {
            activeState.Interrupt();
            activeState.enabled = false;

            ActivateState(state);

            if(state == slashState)
            {
                bossController.slashCounter++;
            }
            if(state == stunnedState)
            {
                bossController.parryCounter++;
            }
        }
    }

    void ActivateState(State state)
    {
        activeState = state;
        state.enabled = true;
        state.OnCall();
    }

    public void ActionComplete()
    {
        activeState.enabled = false;
        ActivateState(defaultState);

        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-6, 6, 6);
        }
        else
        {
            transform.localScale = new Vector3(6, 6, 6);
        }
    }

}
