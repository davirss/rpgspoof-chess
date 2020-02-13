using UnityEngine;

public class BoardController : MonoBehaviour {

    private Piece selectedPiece = null;
    public Tile[,] tileArray;

    public void NotifyPieceSelected(Piece piece)
    {
        ToggleSelectedPiece(piece);
        HighlightPossibleMoves();
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
        
    }

    public void NotifyTileClicked(Tile tile) {
        if (selectedPiece != null && !tile.IsPopulated) {
            var piecePosition = selectedPiece.transform.position;
            var tilePosition = tile.transform.position;
            selectedPiece.transform.position = new Vector3(tilePosition.x, piecePosition.y, tilePosition.z);

            tile.PopulateWith(selectedPiece);
            selectedPiece.SetCurrentTile(tile);
        }
    }

    public void SetTileArray(Tile[,] array)
    {
        tileArray = array;
    }

}