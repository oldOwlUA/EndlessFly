using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPanelData : MonoBehaviour
{
    public GameObject closeMask;
    public bool isOpenHero;
    public int id;

    private void Update()
    {      
        if (isOpenHero)
            closeMask.SetActive(false);
        else
            closeMask.SetActive(true);
    }
}
