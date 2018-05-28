using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

    [Header("Задежка перед удалением")]
    public float DestroyDeley = 1f;
    private void Awake()
    {
        StartCoroutine("Destroy");
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(DestroyDeley);
        Destroy(gameObject);
    }
}
