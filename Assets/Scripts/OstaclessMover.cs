using UnityEngine;
using System.Collections;

public class OstaclessMover : MonoBehaviour
{


    private Transform gmTransform;

    [SerializeField]
    private float moveSpeed;

    private void Start()
    {
        gmTransform = gameObject.transform;
    }
    // Update is called once per frame
    void Update()
    {
        moveSpeed = GameController.Instance.enemyMoveSpeed;

        gmTransform.localPosition = new Vector3(
            gmTransform.localPosition.x,
            gmTransform.localPosition.y   - (moveSpeed * Time.deltaTime),
            0
            );
    }

 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "end")
        {
            Destroy(gameObject);
        }

      
    }
}
