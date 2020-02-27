using DG.Tweening;
using UnityEngine;

public class Highlight : MonoBehaviour {

    public MeshRenderer wall;
    public float wallFadeTime = 0.1f;
    public float scaleTime = 0.15f;
    public float scaleInterval = 0.05f;
    public float maxScaleDelay = 0.4f;

    private int distance;
    private bool active = false;
    private static readonly string Intensity = "_Intensity";

    public Vector2Int tile { get; set; }
    public bool kill { get; set; }

    public void Init(bool kill, int distance, Vector2Int tile) {
        this.kill = kill;
        this.distance = distance;
        this.tile = tile;
        this.transform.position = BoardInput.TileToWorld(tile);
        ActivateWall(false);
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, scaleTime).SetDelay(scaleInterval * distance);
    }

    public float Disable() {
        float delay = maxScaleDelay - (scaleInterval * distance);
        transform.DOScale(0f, scaleTime).SetDelay(delay);
        return delay;
    }

    public void ActivateWall(bool a) {
        if (a != active) {
            active = a;
            float amount = active ? 1f : 0f;
            wall.material.DOFloat(amount, Intensity, wallFadeTime);
        }
    }
}