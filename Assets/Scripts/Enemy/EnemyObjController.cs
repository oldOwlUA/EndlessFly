using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class EnemyObjController : MonoBehaviour
{
    public float DayHP = 100;
    public GameObject CanSl;
    public Slider HPStatBar;
    public List<GameObject> Explode;



    private float holdTime = 2f;
    [SerializeField]
    private float HitPoint = 0;
    private bool isDead = false;
    private Coroutine cs;

    private void Start()
    {
        DayHP = (GameController.Instance.EnemyHP + GameSetAndStat.Instance._TCoins) - GameController.Instance.Heroes[GameController.Instance.IndCurrentHerro].Level;
    }

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
            Instantiate(Explode[0], transform.position, Quaternion.identity, GameController.Instance.InstRootObjects[3]);
            if (!isDead)
            {
                AudioController.Instance.source[1].Play();
                EnemySpawnController.Instance.instantLoot(transform.position);
                isDead = true;
                GameSetAndStat.Instance._G_MonstersKill++;
            }
            StopCoroutine("HideHP");
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

        if (col.tag == "Shild")
        {
            Instantiate(Explode[0], transform.position, Quaternion.identity, GameController.Instance.InstRootObjects[3]);
            if (!isDead)
            {
                AudioController.Instance.source[1].Play();
                EnemySpawnController.Instance.instantLoot(transform.position);
                isDead = true;
                GameSetAndStat.Instance._G_MonstersKill++;
            }
            StopCoroutine("HideHP");
            Destroy(gameObject);
        }
    }
    IEnumerator HideHP()
    {
        yield return new WaitForSeconds(holdTime);
        CanSl.SetActive(false);
    }

}
