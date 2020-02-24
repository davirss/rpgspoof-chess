using UnityEngine;

public class Piece : MonoBehaviour {

    public bool isSelected = false;
    public BoardController controller;

    public Tile currentTile {
        get;
        private set;
    }

    public void SetCurrentTile(Tile tile) {
        if (currentTile != null) {
            currentTile.PopulateWith(null);
        }
        this.currentTile = tile;
    }

    public BaseMovement[,] movement = {{new BaseMovement(Direction.UP_LEFT, 5), new BaseMovement(Direction.UP, 1), new BaseMovement(Direction.UP_RIGHT, 4)}, 
                              {new BaseMovement(Direction.LEFT, 4), new BaseMovement(Direction.CENTER, 0), new BaseMovement(Direction.RIGHT, -1)},
                              {new BaseMovement(Direction.DOWN_LEFT, 3), new BaseMovement(Direction.DOWN, 2), new BaseMovement(Direction.DOWN_RIGHT, -1)}};

    public Owner owner = Owner.FRIEND;
    


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
}