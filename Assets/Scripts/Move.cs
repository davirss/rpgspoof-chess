using UnityEngine;

[System.Serializable]
public class Move {

    public Vector2Int position { get; set; }
    public bool kill { get; set; }
    public int distance { get; set; }

    public Move(Vector2Int position, bool kill, int distance) {
        this.position = position;
        this.kill = kill;
        this.distance = distance;
    }
}