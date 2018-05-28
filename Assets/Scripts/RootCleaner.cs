using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHe;

public class RootCleaner : SingletonObj<RootCleaner>
{
    public List<GameObject> gm;
    public List<string> tags;
    public void CleanRoots()
    {
        for (int i = 0; i < tags.Count; i++)
        {
            gm.AddRange(GameObject.FindGameObjectsWithTag(tags[i]));

            for (int j = 0; j < gm.Count; j++)
            {
                Destroy(gm[j]);

            }
            gm.Clear();
        }
    }
}
