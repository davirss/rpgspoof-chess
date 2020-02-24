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
    public Piece PopulatedBy = null;
    BoardController boardController;

    public int row {get; private set;}
    public int column {get; private set;}
    private bool isHovered = false;

    void Start()
    {
        boardController = GetComponentInParent(typeof(BoardController)) as BoardController;
    }

    void Update()
    {
        UpdateColor();
    }

    void OnMouseOver()
    {
        isHovered = true;
    }

    void OnMouseExit()
    {
        isHovered = false;
    }

    private void UpdateColor() {
        if (isHovered)
        {
            GetComponent<Renderer>().material.color = Color.grey;
        }
        else if (target) 
        {
            GetComponent<Renderer>().material.color = Color.red;
        } 
        else if (current) 
        {
            GetComponent<Renderer>().material.color = Color.green;
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

    public void ResetState() {
        this.target = false;
        this.current = false;
        this.possible = false;
    }

    public void PopulateWith(Piece piece) {
        this.PopulatedBy = piece;
    }

    private void OnMouseDown() {
        boardController.NotifyTileClicked(this);
    }

    public void SetCoordinates(int row, int column) {
        this.row = row;
        this.column = column;
    }
}