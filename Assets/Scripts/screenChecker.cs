using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenChecker : MonoBehaviour
{

    public GameObject[] Points;


    private void Awake()
    {
        calc();
    }
    

    void calc()
    {
        float scl = (((Camera.main.orthographicSize * Camera.main.aspect) * 2)/ 5);
       // print(scl);
        float v = ((float)Screen.width / 5f);
       // print(v);

        float f2 = 0;

        for (int i = 0; i < 5; i++)
        {

            Vector2 wig;

            if (i == 0)
            {
                f2 = v/2;
               // print(f2);
            }
            else
            {
                f2 = (v / 2) + v * i;
            }

            wig = new Vector2(f2,0);

           // print(wig);
            var TMP2 = Camera.main.ScreenToWorldPoint(wig);
            Points[i].transform.localPosition = new Vector2
            (
                TMP2.x,
                transform.localPosition.y

            );
            Points[i].transform.localScale = new Vector3
            (
                scl,
                scl,
                scl
            );

            //print(TMP2.x);
        }
    }

}
