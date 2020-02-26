using System.Collections;
using UnityEngine;

class PlayerTurnState : MatchState {

    public PlayerTurnState(BoardController board) : base(board) { }

    public override IEnumerator Start() {
        return base.Start();
    }

    public override IEnumerator Select(Vector2Int t) {
        Piece p = Board.tiles[t.x, t.y];
        if (p != null && p.team == Board.PlayerTeam) {
            Board.SelectedPiece = p;
            Board.SetState(new PlayerPieceSelectedState(Board));
        }
        yield break;
    }

}