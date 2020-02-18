using UnityEngine;
public class BoardController : MonoBehaviour {

    private Piece selectedPiece = null;
    private GameObject[,] tileArray = new GameObject[ROW_SIZE, COLUMN_SIZE];

    private static int ROW_SIZE = 8;
    private static int COLUMN_SIZE = 8;

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
        ToggleSelectedPiece(piece);
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
        //peça vai ter uma matriz de movimentação
        //Dada a matriz de movimentação, percorrer os tiles refente ao movimento
        //Atualizar o status to tile (movimento possivel, n sei)
        //Checar se tem algum inimigo, se tiver, ativa a target
    }

    private void highlighDirection(BaseMovement movement) {
        if (movement.amount == 0) return;

        var currentPieceRow = selectedPiece.currentTile.row;
        var currentPieceColumn = selectedPiece.currentTile.column;
        var direction = movement.direction;

        if (direction.IsDiagonalDirection()) {
            int rowDirectionOperation = direction.isUp() ? -1 : 1;
            int columnDirectionOperation = direction.isRight() ?  1 : -1;            

            for (int x = movement.minAmount; x <= movement.amount; x++) {
                var rowIndex = currentPieceRow + (x * rowDirectionOperation);
                var columnIndex = currentPieceColumn + (x * columnDirectionOperation);

                Debug.Log("Moving this " + rowIndex + " " + columnIndex);
                var highlightedTile = this.tileArray[rowIndex, columnIndex];
                highlightedTile.GetComponent<Tile>().possible = true;

                // var rowDirection = currentPieceRow + (x * rowDirectionOperation);
                // var columnDirection = currentPieceColumn + (x * columnDirectionOperation);
            
                // int rowIndex = 0;
                // int columnIndex = 0;

                // if (rowDirection < 0) {
                //     rowIndex = 0;
                // } else if (rowDirection >= 8) {
                //     rowIndex = 7;
                // } else {
                //     rowIndex = rowDirection;
                // }

                // if (columnDirection < 0) {
                //     columnIndex = 0;
                // } else if (columnDirection >= 8) {
                //     columnIndex = 7;
                // } else {
                //     columnIndex = columnDirection;
                // }
                // Debug.Log("Fetching this tile: Row "  + rowIndex + " Column: " + columnIndex);

                // var highlightedTile = tileArray[rowIndex, columnIndex];

                // highlightedTile.possible = true;
            }
        } else {
            //Highlight Cardinal (up, down, left, right)
        }
    }

    public void NotifyTileClicked(Tile tile) {
        if (selectedPiece != null && !tile.IsPopulated) {
            var piecePosition = selectedPiece.transform.position;
            var tilePosition = tile.transform.position;
            selectedPiece.transform.position = new Vector3(tilePosition.x, piecePosition.y, tilePosition.z);

            tile.PopulateWith(selectedPiece);
            selectedPiece.SetCurrentTile(tile);
            HighlightPossibleMoves();
        }
    }

    public void SetTileArray(GameObject[,] array)
    {
        this.tileArray = array;
    }

}