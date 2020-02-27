using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour {
    public T prefab;
    public int size;
    private List<T> freeObjects;
    private List<T> usedObjects;

    void Awake() {
        freeObjects = new List<T>();
        usedObjects = new List<T>();
        for (int i = 0; i < size; i++) {
            T obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            freeObjects.Add(obj);
        }
    }

    public virtual T Get() {
        if (freeObjects.Count == 0) {
            T newObj = Instantiate(prefab, transform);
            usedObjects.Add(newObj);
            return newObj;
        } else {
            T obj = freeObjects[0];
            freeObjects.Remove(obj);
            usedObjects.Add(obj);
            return obj;
        }
    }

    public virtual IEnumerator ReturnObject(T obj) {
        if (!usedObjects.Contains(obj))
            yield break;
        usedObjects.Remove(obj);
        freeObjects.Add(obj);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        yield break;
    }
}