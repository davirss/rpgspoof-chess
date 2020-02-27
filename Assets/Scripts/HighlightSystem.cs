using System.Collections.Generic;
using UnityEngine;

public class HighlightSystem : MonoBehaviour {
    public HighlightPool moveHighlightPool;
    public HighlightPool killHighlightPool;

    private Dictionary<Vector2Int, Highlight> highlights = new Dictionary<Vector2Int, Highlight>();
    private Highlight currentFocus = null;

    public void CreateHighlights(List<Move> moves) {
        foreach (Move m in moves) {
            Highlight highlight = m.kill ? killHighlightPool.Get() : moveHighlightPool.Get();
            highlight.Init(m.kill, m.distance, m.position);
            highlights.Add(m.position, highlight);
        }
    }

    public void ClearHighlights() {
        foreach (Highlight h in highlights.Values) {
            if (h.kill)
                killHighlightPool.ReturnObject(h);
            else
                moveHighlightPool.ReturnObject(h);
        }
        highlights.Clear();
    }

    public void SetFocus(Vector2Int tile) {
        if (currentFocus) {
            currentFocus.ActivateWall(false);
        }
        if (highlights[tile] != null) {
            currentFocus = highlights[tile];
            currentFocus.ActivateWall(true);
        } else {
            currentFocus = null;
        }
    }
}