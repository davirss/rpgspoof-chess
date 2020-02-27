using System.Collections;
using UnityEngine;

public abstract class MatchState {

    protected BoardController Board;

    public MatchState(BoardController board) {
        Board = board;
    }

    public virtual IEnumerator Start() {
        yield break;
    }
    public virtual IEnumerator Select(Vector2Int t) {
        yield break;
    }
    public virtual IEnumerator Cancel() {
        yield break;
    }
    public virtual IEnumerator ChangeTile(Vector2Int t) {
        yield break;
    }
}