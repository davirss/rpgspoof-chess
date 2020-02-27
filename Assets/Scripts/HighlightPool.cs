using System.Collections;
using UnityEngine;

public class HighlightPool : ObjectPool<Highlight> {
    public override IEnumerator ReturnObject(Highlight obj) {
        float delay = obj.Disable();
        yield return new WaitForSeconds(delay);
        base.ReturnObject(obj);
        yield break;
    }
}