using UnityEngine;

public class Piece : MonoBehaviour {
    public bool isSelected = false;
    public BoardController controller;
    protected Tile currentTile;


    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        Debug.Log("Piece mouse down");
        controller.NotifyPieceSelected(this);
    }
    void Update()
    {
        GetComponent<Renderer>().material.color = isSelected ? Color.grey : Color.white;        
    }

    public void SetCurrentTile(Tile tile) 
    {
        if (currentTile != null)
        {
            this.currentTile.PopulateWith(null);
        }
        this.currentTile = tile;
    }



}