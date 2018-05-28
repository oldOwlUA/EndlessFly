using UnityEngine;
using System.Collections;

public class OstaclessRotator : MonoBehaviour
{



    [SerializeField]
    private float rotateSpeed;

    void Update()
    {
       transform.Rotate(
           0f,
           0f,
            rotateSpeed * Time.deltaTime
            );
     
        
    }

 


}
