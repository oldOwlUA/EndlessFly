using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHe;

public class ScallerSc : SingletonObj<ScallerSc>
{
    // проверяем росширение экрана и рассщитываем розмер обьектов относительно экрана
    public float defaultScele;
    public float leftBorder;
    public float rightBorder;
    public float part = 5;


    private void Awake()
    {        
        float scl = (Camera.main.orthographicSize * Camera.main.aspect);
        defaultScele = (scl * 2) / part;

        leftBorder = -scl;
        rightBorder = scl;
    }    
}
