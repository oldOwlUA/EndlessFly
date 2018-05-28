using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BossObjController : MonoBehaviour
{
    public float DayHP = 100;
    public GameObject CanSl;
    public Slider HPStatBar;
    public List<GameObject> Explode;
    [SerializeField]
    private bool isFire;

    private float holdTime = 2f;
    [SerializeField]
    private float HitPoint = 0;
    private bool isDead = false;
    private Coroutine cs;
    private float scale;



    [SerializeField]
    private float moveSpeed = 0.1f;

    public int nextPoint = 0;
    //---------------------------------
    private float nextFire;
    public List<GameObject> ShotAvatar;
    public List<Transform> ShotSpawn;
    public Transform parentInstantiate;
    [Range(0.6f, 1f)]
    public float fireRate = 1f;
    //--------------------------------------
    public List<Vector3> points; // точки для передвижения

    Collider2D cl;

    private void OnTriggerEnter2D(Collider2D col)
    {
        HPStatBar.value = (HitPoint / DayHP);

        {
            if (col.tag == "bolt")
            {
                HitPoint += GameController.Instance.ShotPower;
                Destroy(col.gameObject);
            }
            else if (col.tag == "boltHelp")
            {
                HitPoint += col.GetComponent<BoltMover>().shotPow;
                Destroy(col.gameObject);
            }

        }

        if (HitPoint > DayHP)
        {
            int lootCount = Random.Range(10, 20);
            Instantiate(Explode[0], transform.position, Quaternion.identity, GameController.Instance.InstRootObjects[3]);
            if (!isDead)
            {
                GameSetAndStat.Instance._G_BossKill++;
                GameController.Instance.levelInGame++;
                AudioController.Instance.source[1].Play();
                for (int i = 0; i < lootCount; i++)
                {
                    Vector3 randPos = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y);

                    if (randPos.x>PlayerController.Instance.boundary.xMax)
                        randPos.x = PlayerController.Instance.boundary.xMax;
                    if (randPos.x < PlayerController.Instance.boundary.xMin)
                        randPos.x = PlayerController.Instance.boundary.xMin;

                    EnemySpawnController.Instance.instantLoot(randPos);
                }
                isDead = true;
            }
            StopCoroutine("HideHP");

            EnemySpawnController.Instance.Restart(3f);
            Destroy(gameObject);
        }

        if (col.tag == "bolt" || col.tag == "boltHelp")
        {
            holdTime++;
            CanSl.SetActive(true);
            if (cs == null)
                cs = StartCoroutine("HideHP");
            else return;
        }

    }
    IEnumerator HideHP()
    {
        yield return new WaitForSeconds(holdTime);
        CanSl.SetActive(false);
    }




    void pointCalculate()
    {
        points[0] = new Vector3(
            0,
             Camera.main.orthographicSize * 0.75f,
            0);
        points[1] = new Vector3
              (ScallerSc.Instance.leftBorder,
               Camera.main.orthographicSize * 0.75f,
               0);
        points[2] = new Vector3
              (ScallerSc.Instance.rightBorder,
               Camera.main.orthographicSize * 0.75f,
               0);
    }

    private void Start()
    {
        cl = GetComponent<Collider2D>();
        
        scale = ScallerSc.Instance.defaultScele;
        parentInstantiate = GameController.Instance.InstRootObjects[0];
        pointCalculate();
        DayHP = GameController.Instance.ShotPower * 100*GameController.Instance.levelInGame;
    }
    // Update is called once per frame

    int ind;
    void Update()
    {

        if (transform.localPosition != points[nextPoint])
        {
            transform.localPosition = Vector3.MoveTowards(transform.position, points[nextPoint], moveSpeed * Time.deltaTime);
        }
        else
        {
            cl.enabled = true;
            isFire = true;
            if (nextPoint >= points.Count - 1)
                nextPoint = 0;
            else nextPoint++;
        }


        if (isFire)
        {

            if (Time.time > nextFire)
            {
                ind = (GameController.Instance.levelInGame - 1);

                if (ind > (ShotAvatar.Count - 1))
                {

                    Instantiate(ShotAvatar[Random.Range(0,ShotAvatar.Count - 1)], ShotSpawn[0].position, ShotSpawn[0].rotation, parentInstantiate);
                }
                else
                    Instantiate(ShotAvatar[ind], ShotSpawn[0].position, ShotSpawn[0].rotation, parentInstantiate);
                // shot.transform.localScale = new Vector3(scale, scale, scale);
                nextFire = Time.time + fireRate;
            }
        }




    }



}
