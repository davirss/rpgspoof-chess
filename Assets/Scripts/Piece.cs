using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    private List<BaseMovement> movements = new List<BaseMovement>();

    private static readonly List<Direction> UpDirections =
        new List<Direction> { Direction.UP, Direction.UP_LEFT, Direction.UP_RIGHT };
    private static readonly List<Direction> DownDirections =
        new List<Direction> { Direction.DOWN, Direction.DOWN_LEFT, Direction.DOWN_RIGHT };
    private static readonly List<Direction> LeftDirections =
        new List<Direction> { Direction.LEFT, Direction.UP_LEFT, Direction.DOWN_LEFT };
    private static readonly List<Direction> RightDirections =
        new List<Direction> { Direction.RIGHT, Direction.UP_RIGHT, Direction.DOWN_RIGHT };

    public BoardController board { get; set; }
    public bool team { get; set; }
    public Vector2Int currentTile { get; private set; }

    public void SetCurrentTile(Vector2Int tile) {
        if (currentTile != null) {
            board.tiles[tile.x, tile.y] = null;
        }
        this.currentTile = tile;
    }

    public List<Move> GetPossibleMoves(BoardController board) {
        List<Move> moves = new List<Move>();
        foreach (BaseMovement m in movements) {
            if (m.amount == 0) break;

            Direction direction = m.direction;

            int rowDirectionOperation = 0;
            int columnDirectionOperation = 0;
            if (UpDirections.Contains(direction)) {
                columnDirectionOperation = 1;
            }
            if (DownDirections.Contains(direction)) {
                columnDirectionOperation = -1;
            }
            if (LeftDirections.Contains(direction)) {
                rowDirectionOperation = -1;
            }
            if (RightDirections.Contains(direction)) {
                rowDirectionOperation = 1;
            }

            int movementAmount = m.amount == -1 ? BoardController.ROW_SIZE : m.amount;

            for (int x = m.minAmount; x <= movementAmount; x++) {
                int rowIndex = currentTile.x + (x * rowDirectionOperation);
                int columnIndex = currentTile.y + (x * columnDirectionOperation);

                if (rowIndex >= BoardController.ROW_SIZE || columnIndex >= BoardController.COLUMN_SIZE ||
                    columnIndex < 0 || rowIndex < 0) break;

                if (board.tiles[rowIndex, columnIndex] == null) {
                    moves.Add(new Move(new Vector2Int(rowIndex, columnIndex), false, x));
                } else if (board.tiles[rowIndex, columnIndex].team == team || !m.canKill) {
                    break;
                } else if (board.tiles[rowIndex, columnIndex].team != team) {
                    moves.Add(new Move(new Vector2Int(rowIndex, columnIndex), true, x));
                    break;
                }
            }
        }
        return moves;
    }
}