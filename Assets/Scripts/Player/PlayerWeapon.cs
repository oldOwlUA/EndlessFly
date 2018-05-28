using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CHe;
public class PlayerWeapon : SingletonObj<PlayerWeapon>
{


    [Space]
    [Header("Этот скрипт отвечает за аспекты стрельбы персонажа")]


    private float ShotPower;

    [Range(0.01f, 0.05f)]
    public float fireRate = 0.25f;
    public bool isFire = false;

    public List<GameObject> ShotAvatar;
    public List<Transform> ShotSpawn;
    public Transform parentInstantiate;
    public SpriteRenderer Rend;
    public List<GameObject> PlayerHelps;
    public float PoverUp = 0;
    public int bonus = 0;
    public void ResetPA()
    {
        PoverUp = 0;
        bonus = 0;
    }
    public void AddBonus()
    {
        bonus = 2;
    }
    private float nextFire;
    private int tempLev;
    private void Start()
    {
        Rend = ShotAvatar[0].GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameController.Instance.currentState == GameState.Play)
            Shot();
        else return;
    }

    private void Shot()
    {
        if (isFire)
        {
            changeShot(GameController.Instance.Heroes[GameController.Instance.IndCurrentHerro].Level);
            changeShotSideL(GameController.Instance.SideKicks[GameController.Instance.IndCurrentLeft].Level);
            changeShotSideR(GameController.Instance.SideKicks[GameController.Instance.IndCurrentRight].Level);
            if (Time.time > nextFire)
            {
                Instantiate(ShotAvatar[0], ShotSpawn[0].position, ShotSpawn[0].rotation, parentInstantiate);
                if (PlayerHelps.Count > 0)
                {
                    if (PlayerHelps[0] != null)
                    {
                        GameObject g = Instantiate(ShotAvatar[1], ShotSpawn[1].position, ShotSpawn[1].rotation, parentInstantiate);
                        g.GetComponent<BoltMover>().shotPow = (int)GameController.Instance.ShotHelperPowerL;
                    }
                    if (PlayerHelps[1] != null)
                    {
                        GameObject g = Instantiate(ShotAvatar[1], ShotSpawn[2].position, ShotSpawn[2].rotation, parentInstantiate);
                        g.GetComponent<BoltMover>().shotPow = (int)GameController.Instance.ShotHelperPowerR;
                    }
                }

                nextFire = Time.time + fireRate;
            }
        }
    }

    void changeShotSideL(int i)
    {
        var shot = GameController.Instance.BasicShotSide;

        ShotPower = shot * (i + 1);

        GameController.Instance.ShotHelperPowerL = ShotPower;
    }

    void changeShotSideR(int i)
    {
        var shot = GameController.Instance.BasicShotSide;

        ShotPower = shot * (i + 1);

        GameController.Instance.ShotHelperPowerR = ShotPower;
    }

    void changeShot(int i)
    {
        var shot = GameController.Instance.BasicShot;

        ShotPower = shot * (i + 1);

        GameController.Instance.ShotPower = ShotPower + PoverUp + (ShotPower * bonus);
        GameController.Instance.EnemyHP = ShotPower;
      Rend.sprite = GameController.Instance.Heroes[GameController.Instance.IndCurrentHerro].ShotIm[i];

    }
}
