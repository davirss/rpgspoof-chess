using UnityEngine;

[RequireComponent(typeof(BoardController))]
public class BoardInput : MonoBehaviour {
    private BoardController board;
    private Vector2Int currentTile = Vector2Int.zero;
    public static readonly float TILE_SIZE = 2f;
    public static readonly float TILE_OFFSET = 4f;

    private void Start() {
        board = GetComponent<BoardController>();
    }

    private void Update() {
        Vector2 mouse = Input.mousePosition;
        Plane plane = new Plane(Vector3.up, Vector3.up * transform.position.y);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance)) {
            Vector3 pos = ray.GetPoint(distance);
            Vector2Int t = GetTile(pos);
            if (t != currentTile) {
                currentTile = t;
            }
        }
    }

    public Vector2Int GetTile(Vector3 pos) {
        Vector2 t = new Vector2(pos.x, pos.z) / TILE_SIZE + new Vector2(TILE_OFFSET, TILE_OFFSET);
        return new Vector2Int(Mathf.FloorToInt(t.x), Mathf.FloorToInt(t.y));
    }

    public void Select() {
        board.Select(currentTile);
    }

    public void Cancel() {

    }
}