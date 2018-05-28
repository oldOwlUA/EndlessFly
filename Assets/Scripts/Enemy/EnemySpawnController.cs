using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CHe;


public class EnemySpawnController : SingletonObj<EnemySpawnController>
{
    [Header("Скрипт отвечает за спавн врагов и лут")]
    public Transform EnemysSpawnPos;
    public List<GameObject> listEnemys;
    public List<GameObject> listBossEnemys;

    #region loot
    [Space]
    public List<GameObject> loot;//варианты лута

    [SerializeField]
    private int[] index;
    public int[] Probability;
    public int[] lootGenerated;

    [SerializeField]
    private int numInst = 0;
    [Space]
    #endregion
    public float spawnTime = 2.5f;

    private float timer;

    //----------------------------------------------------------------
    public int Vaves;

    //----------------------------------------------------------------


    void Start()
    {
        
        CalculateProb();
        timer = spawnTime;
        Vaves = Random.Range(10, 15); 
    }

    public void Restart(float time)
    {
        StartCoroutine("Rest", time);
    }

    IEnumerator Rest(float time)
    {
        yield return new WaitForSeconds(time);
        Vaves = Random.Range(10,15);

    }


    void Update()
    {

        if (GameController.Instance.currentState == GameState.Play)
        {
            int ind = Random.Range(0, listEnemys.Count);


            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {

                if (Vaves > 0)
                {
                    spawnTime -= 0.01f; 
                    timer = (spawnTime);
                    Instantiate(listEnemys[ind], EnemysSpawnPos.position, Quaternion.identity, EnemysSpawnPos);
                    Vaves--;

                }
                else if (Vaves == 0)
                {
                    timer = spawnTime;
                    Instantiate(listBossEnemys[ind], EnemysSpawnPos.position, listBossEnemys[ind].transform.rotation);
                    Vaves--;
                }
                else if (Vaves < 0)
                {
                    return;
                }
            }


        }
        else return;
    }



    //расчитываем вероятности выпадения лута
    public void CalculateProb()
    {

        index = new int[loot.Count];
        for (int i = 0; i < loot.Count; i++)
            index[i] = i;
        lootGenerated = new int[1000];

        Probability[1] = GameController.Instance.Heroes[GameController.Instance.IndCurrentHerro].attribute.ProbabMagnet;
        Probability[2] = GameController.Instance.Heroes[GameController.Instance.IndCurrentHerro].attribute.ProbabPowerUp;
        Probability[0] = 1000 - Probability[1] - Probability[2];

        lootGenerated = RandomPriority.GetRandom(Probability, index, lootGenerated);
    }

    //создаем обьект лута
    public void instantLoot(Vector3 pos)
    {
        /*
         0 - coin
         1 - magnet
         2 - powerUp
        */

        int randomIndex = Random.Range(0, 99);
        GameObject gm = Instantiate(loot[lootGenerated[randomIndex]], pos, Quaternion.identity, GameController.Instance.InstRootObjects[2]);
        Vector3 scale = gm.transform.localScale;

        gm.transform.localScale = new Vector3
            (
                scale.x * ScallerSc.Instance.defaultScele,
                scale.y * ScallerSc.Instance.defaultScele,
                scale.z * ScallerSc.Instance.defaultScele
            );
        if (numInst < 99)
        {
            numInst++;
        }
        else
        {
            CalculateProb();
            numInst = 0;
        }
    }

}
