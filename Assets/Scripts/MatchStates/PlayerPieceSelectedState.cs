using System.Collections;
using UnityEngine;

public class PlayerPieceSelectedState : MatchState {
    public PlayerPieceSelectedState(BoardController board) : base(board) { }

    public override IEnumerator Start() {
        Board.HighlightPossibleMoves();
        yield break;
    }

    public override IEnumerator Cancel() {
        Board.SetState(new PlayerTurnState(Board));
        yield break;
    }

    public override IEnumerator ChangeTile(Vector2Int t) {
        Board.highlightSystem.SetFocus(t);
        yield break;
    }
}