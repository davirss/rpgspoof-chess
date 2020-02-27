using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HighlightSystem))]
public class BoardController : StateMachine {

    public static readonly int ROW_SIZE = 8;
    public static readonly int COLUMN_SIZE = 8;

    public Piece[, ] tiles { get; set; }
    public Piece SelectedPiece { get; set; }
    public bool PlayerTeam { get; private set; }
    public HighlightSystem highlightSystem { get; private set; }

    void Awake() {
        highlightSystem = GetComponent<HighlightSystem>();
    }

    void Start() {
        tiles = new Piece[ROW_SIZE, COLUMN_SIZE];
        for (int i = 0; i < ROW_SIZE * COLUMN_SIZE; i++) {
            int column = i % COLUMN_SIZE;
            int row = i / ROW_SIZE;
            tiles[row, column] = null;
        }
        PlayerTeam = false;
        SelectedPiece = null;
    }

    public void Select(Vector2Int t) {
        StartCoroutine(State.Select(t));
    }

    public void Cancel() {
        StartCoroutine(State.Cancel());
    }

    public void ChangeTile(Vector2Int t) {
        StartCoroutine(State.ChangeTile(t));
    }

    public void NotifyPieceSelected(Piece piece) {
        HighlightPossibleMoves();
    }

    public void HighlightPossibleMoves() {

    }

    private void highlighDirection(BaseMovement movement) {

    }

    // public void NotifyTileClicked(Tile tile) {
    //     if (selectedPiece != null && (selectedPiece.currentTile == null || tile.possible || tile.target)) {

    //         if (tile.IsPopulated && tile.piece.owner == selectedPiece.owner) {
    //             Debug.Log(tile.piece.owner + " " + selectedPiece.owner);
    //             return;
    //         }

    //         if (tile.IsPopulated) {
    //             Piece piece = tile.piece;
    //             tile.SetPiece(null);
    //             piece.gameObject.SetActive(false);
    //         }

    //         Vector3 piecePosition = selectedPiece.transform.position;
    //         //selectedPiece.transform.position = new Vector3(tilePosition.x, piecePosition.y, tilePosition.z);

    //         tile.SetPiece(selectedPiece);
    //         selectedPiece.SetCurrentTile(tile);
    //         ClearHighlighted();
    //         HighlightPossibleMoves();
    //     }
    // }
}