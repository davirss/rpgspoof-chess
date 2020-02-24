using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BoardController : MonoBehaviour {

    private Piece selectedPiece = null;
    private GameObject[,] tileArray = new GameObject[ROW_SIZE, COLUMN_SIZE];

    private static int ROW_SIZE = 8;
    private static int COLUMN_SIZE = 8;

    private List<GameObject> previousHighlight = new List<GameObject>();

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Cube");

        for(int i = 0; i < ROW_SIZE * COLUMN_SIZE; i++) {
            var column = i % COLUMN_SIZE;
            var row = i / ROW_SIZE;
            var tile = GameObject.Instantiate(prefab, new Vector3(column, 0, row), Quaternion.identity);
            tile.name = "Tile " + row + "/" + column;
            tile.transform.parent = this.transform;
            var currentTile = tile.GetComponent<Tile>();
            currentTile.SetCoordinates(row, column);
            tileArray[row, column] = tile;
        }
    }

    public void NotifyPieceSelected(Piece piece)
    {
        ClearHighlighted();
        ToggleSelectedPiece(piece);

        if (piece.currentTile != null) {
            HighlightPossibleMoves();
        }
    }

    public void ToggleSelectedPiece(Piece piece)
    {
        if (selectedPiece != null)
        {
            selectedPiece.isSelected = false;
        }
        selectedPiece = piece;
        selectedPiece.isSelected = true;
    }

    public void HighlightPossibleMoves()
    {   
        for (int column = 0; column < 3; column++) {
            for (int row = 0; row < 3; row++) {
                var movement = selectedPiece.movement[column, row];
                highlighDirection(movement);
            }
        }
    }

    private void highlighDirection(BaseMovement movement) {
        if (movement.amount == 0) return;

        var currentPieceRow = selectedPiece.currentTile.row;
        var currentPieceColumn = selectedPiece.currentTile.column;
        var direction = movement.direction;

        int rowDirectionOperation = direction.isUp() ? -1 : 1;
        int columnDirectionOperation = direction.isRight() ?  1 : -1;

        var movementAmount = movement.amount == -1 ? 8 : movement.amount;

        if (direction.IsDiagonalDirection()) {
            for (int x = movement.minAmount; x <= movementAmount; x++) {
                var rowIndex = currentPieceRow + (x * rowDirectionOperation);
                var columnIndex = currentPieceColumn + (x * columnDirectionOperation);

                if (rowIndex >= 8 || columnIndex >= 8 || columnIndex < 0 || rowIndex < 0) continue;

                Debug.Log("Moving this " + rowIndex + " " + columnIndex);
                var tileObject = this.tileArray[rowIndex, columnIndex];
                var highlightedTile = tileObject.GetComponent<Tile>();

                if (highlightedTile.PopulatedBy == null) {
                    highlightedTile.possible = true;
                } else if (highlightedTile.PopulatedBy.owner == Owner.FRIEND) {
                    highlightedTile.possible = false;
                    break;
                } else if (highlightedTile.PopulatedBy.owner == Owner.FOE) {
                    highlightedTile.target = true;
                    break;
                }

                previousHighlight.Add(tileObject);
            }
        } else {
            for (int x = movement.minAmount; x <= movementAmount; x++) {

                var rowIndex = currentPieceRow;
                var columnIndex = currentPieceColumn;
                if (movement.direction == Direction.UP || movement.direction == Direction.DOWN) {
                    rowIndex = currentPieceRow + (x * rowDirectionOperation);
                } else if (movement.direction == Direction.LEFT || movement.direction == Direction.RIGHT) {
                    columnIndex = currentPieceColumn + (x * columnDirectionOperation);
                }

                if (rowIndex >= 8 || columnIndex >= 8 || columnIndex < 0 || rowIndex < 0) continue;

                Debug.Log("Moving this " + rowIndex + " " + columnIndex);
                var tileObject = this.tileArray[rowIndex, columnIndex];
                var highlightedTile = tileObject.GetComponent<Tile>();

                if (highlightedTile.PopulatedBy == null) {
                    highlightedTile.possible = true;
                } else if (highlightedTile.PopulatedBy.owner == selectedPiece.owner) {
                    highlightedTile.possible = false;
                    break;
                } else if (highlightedTile.PopulatedBy.owner != selectedPiece.owner) {
                    highlightedTile.target = true;
                    break;
                }

                previousHighlight.Add(tileObject);
            }
        }
    }

    public void NotifyTileClicked(Tile tile) {
        if (selectedPiece != null && (selectedPiece.currentTile == null || tile.possible || tile.target)) {

            if (tile.IsPopulated && tile.PopulatedBy.owner == selectedPiece.owner) {
                Debug.Log(tile.PopulatedBy.owner + " " + selectedPiece.owner);
                return;
            }

            if (tile.IsPopulated) {
                var piece = tile.PopulatedBy;
                tile.PopulateWith(null);
                piece.gameObject.SetActive(false);
            } 

            var piecePosition = selectedPiece.transform.position;
            var tilePosition = tile.transform.position;
            selectedPiece.transform.position = new Vector3(tilePosition.x, piecePosition.y, tilePosition.z);

            tile.PopulateWith(selectedPiece);
            selectedPiece.SetCurrentTile(tile);
            ClearHighlighted();
            HighlightPossibleMoves();
        }
    }

    private void ClearHighlighted() {
        foreach(GameObject tile in previousHighlight) {
            tile.GetComponent<Tile>().ResetState();
        }
    }

    public void SetTileArray(GameObject[,] array)
    {
        this.tileArray = array;
    }

}