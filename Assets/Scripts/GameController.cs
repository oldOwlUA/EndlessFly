using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


using CHe;

public class GameController : SingletonObj<GameController>
{

    public GameState currentState;

    public float ShotPower = 0;
    public float ShotHelperPowerL = 0;
    public float ShotHelperPowerR = 0;
    public float BasicShot = 5;
    public float BasicShotSide = 2;

    public List<Transform> InstRootObjects;

    public int IndCurrentHerro = 0;
    public int IndCurrentLeft = 0;
    public int IndCurrentRight = 0;


    [Header("Список героев")]
    public List<HeroClass> Heroes;

    public List<SideClass> SideKicks;


    [Header("ГО главного меню")]
    public GameObject MainUI;

    public Text coinsText;


    [Header("ГО игрового меню")]
    public GameObject GameUI;
    public GameObject InGameUI;
    public GameObject OutGameUI;
    public GameObject player;


    public int levelInGame = 1;

    public float enemyMoveSpeed = 1;
    public float f = 1;

    public float EnemyHP = 0;

    private void Start()
    {
        GameSetAndStat.Instance.GetData();
    }
    

    private void Update()
    {


        switch ((int)currentState)
        {

            case 0:
                {
                    Time.timeScale = 1;
                    MainUI.SetActive(true);
                }
                break;
            case 1:
                {

                    Time.timeScale = f;
                    MainUI.SetActive(false);
                    player.SetActive(true);
                }
                break;
            case 2:
                {
                    if (Time.timeScale > 0.1)
                        Time.timeScale -= Time.fixedDeltaTime * 0.5f;
                    else
                    {
                        Time.timeScale = 0;
                        CameraShake.stop = true;
                        UIController.Instance.ShowLoseScrean();/* в этом методе логика показа кнопки продолжения за просмотр рекламы*/
                       

                        return;
                    }

                }
                break;
            case 3:

                break;
            default:
                break;
        }


    }


}
