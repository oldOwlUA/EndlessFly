using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRotate : MonoBehaviour
{
    // - public---------------------
    // - private--------------------

    public List<Texture> BGMaterial;

    private Material tx;
    [Range(0.1f, 1f)]
    public float speedRotate = 0.1f;
    

    // - methods--------------------
    void Start()
    {

        tx = GetComponent<MeshRenderer>().material;
        var i = Random.Range(0, BGMaterial.Count - 1);
        tx.mainTexture = BGMaterial[i];
    }


    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * -speedRotate);
        tx.mainTextureOffset = offset;
    }

    private void RandomBackground()
    {


    }

}
