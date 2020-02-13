using UnityEngine;

public class Tile : MonoBehaviour {
    
    public bool IsPopulated {
        get {
            return PopulatedBy != null;
        }
    }
    public bool target = false;
    public bool current = false;
    public bool possible = false;
    Piece PopulatedBy = null;
    BoardController boardController;

    void Start()
    {
        boardController = GetComponentInParent(typeof(BoardController)) as BoardController;
    }

    void Update()
    {   
    }

    /// </summary>
    void OnMouseOver()
    {
        GetComponent<Renderer>().material.color = Color.grey;
    }

    void OnMouseExit()
    {
        UpdateColor();
    }

    private void UpdateColor() {
        if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        } 
        else if (current) 
        {
            GetComponent<Renderer>().material.color = Color.red;
        } 
        else if (possible)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        } 
        else 
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void PopulateWith(Piece piece) {
        this.PopulatedBy = piece;
    }

    private void OnMouseDown() {
        boardController.NotifyTileClicked(this);
    }
}