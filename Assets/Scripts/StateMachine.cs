using UnityEngine;

public abstract class StateMachine : MonoBehaviour {
    protected MatchState State;

    public void SetState(MatchState state) {
        State = state;
        StartCoroutine(State.Start());
    }
}