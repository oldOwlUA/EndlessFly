using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CHe;


public class UIController : SingletonObj<UIController>
{
    public Slider Sensitive;
    public Text ValueLb;

    public Text score;// обьект текста куда записываем собраные монетки во время одного раунда


    public GameObject SettMenu;
    public List<GameObject> AuBgBTNs;
    public List<GameObject> AuFxBTNs;

    [Space]
    public Text HerroLevelVal;
    public Text HerroPriceVal;
    public Text HerroPriceUpdateVal;

    public GameObject MaxLevel;
    public GameObject btnBuy;
    public GameObject btnSelect;
    public GameObject btnSelected;
    public GameObject btnUpgrade;
    public GameObject btnDontUpgrade;
    public GameObject BuyLabel;
    public GameObject UpgradeLabel;
    public GameObject PriceUpg;
    public Image bulletIm;
    [Space]
    public GameObject LoseScrean;
    public GameObject GameScrean;
    public void BtnPlay()

    {
        GameController.Instance.currentState = GameState.Play;
        RootCleaner.Instance.CleanRoots();
        EnemySpawnController.Instance.Restart(0.1f);
    }
    public void BtnExit()
    {
        Application.Quit();
    }
    public GameObject ContentHero;




    [Space]
    [Header("Обьекты меню усилителей в игре")]
    public GameObject PowerUp;
    public GameObject CoinUp;
    public GameObject Shild;
    public Text CountPowerUp;
    public Text CountCoinUp;
    public Text CountShild;
    public Text CountMarPowerUp;
    public Text CountMarCoinUp;
    public Text CountMarShild;
    public Text PricePowerUp;
    public Text PriceCoinUp;
    public Text PriceShild;


    #region sensivity
    //обновление чувствительности управления
    public void SetSensivity()
    {
        ValueLb.text = Sensitive.value.ToString();
        GameSetAndStat.Instance._G_SensitiveTouch = (int)Sensitive.value;
    }
    public void GetSensivity()
    {
        Sensitive.value = GameSetAndStat.Instance._G_SensitiveTouch;
        ValueLb.text = Sensitive.value.ToString();
    }
    #endregion

    #region globalCoints
    public void ShowGCoints()
    {
        score.text = GameSetAndStat.Instance._G_Coins.ToString();
    }
    #endregion

    #region herroMarket

    private int tempIndHerro = 0;

    public void UpdateValues(int i)
    {
        tempIndHerro = i;

        if (GameController.Instance.Heroes[i].isOpen)
        {
            btnBuy.SetActive(false);



            if (GameController.Instance.IndCurrentHerro == i)
            {
                btnSelected.SetActive(true);
                btnSelect.SetActive(false);
            }
            else
            {
                btnSelected.SetActive(false);
                btnSelect.SetActive(true);
            }

            if (GameController.Instance.Heroes[i].Level < GameController.Instance.Heroes[i].MaxHeroLevel)
            {
                btnUpgrade.SetActive(true);
                PriceUpg.SetActive(true);
                MaxLevel.SetActive(false);
                btnDontUpgrade.SetActive(false);
                UpgradeLabel.SetActive(true);
            }
            else
            {
                btnUpgrade.SetActive(false);
                btnDontUpgrade.SetActive(true);
                PriceUpg.SetActive(false);
                MaxLevel.SetActive(true);
            }
            UpgradeLabel.SetActive(true);
            BuyLabel.SetActive(false);
            int levelId = GameController.Instance.Heroes[i].Level;
            HerroLevelVal.text = levelId.ToString();
            int tempPrice = GameController.Instance.Heroes[i].PricePerLevel;
            HerroPriceUpdateVal.text = tempPrice.ToString();
            bulletIm.enabled = true;
            bulletIm.sprite = GameController.Instance.Heroes[i].ShotIm[levelId];
        }
        else
        {
            PriceUpg.SetActive(false);
            bulletIm.enabled = false;
            btnBuy.SetActive(true);
            BuyLabel.SetActive(true);
            HerroPriceVal.text = GameController.Instance.Heroes[i].OpenPrice.ToString();
            MaxLevel.SetActive(false);
            btnUpgrade.SetActive(false);
            btnDontUpgrade.SetActive(false);
            UpgradeLabel.SetActive(false);
        }
    }

    public void UpgradeHerro()
    {
        int myMoney = GameSetAndStat.Instance._G_Coins;
        int price = GameController.Instance.Heroes[tempIndHerro].PricePerLevel;

        if (myMoney < price)
            return;
        else
        {
            GameSetAndStat.Instance._G_Coins -= price;
            GameController.Instance.Heroes[tempIndHerro].Level++;
            GameSetAndStat.Instance.SaveData();
            UpdateValues(tempIndHerro);
        }
    }

    public void BuyHerro()
    {
        int myMoney = GameSetAndStat.Instance._G_Coins;
        int price = GameController.Instance.Heroes[tempIndHerro].OpenPrice;

        if (myMoney < price)
            return;
        else
        {
            GameSetAndStat.Instance._G_Coins -= price;
            GameController.Instance.Heroes[tempIndHerro].isOpen = true;
            ContentHero.GetComponent<SnapScroling>().instPan[tempIndHerro].GetComponent<HeroPanelData>().isOpenHero = true;
            GameSetAndStat.Instance.SaveData();
            UpdateValues(tempIndHerro);
        }
    }

    #endregion

    #region LoseScrean
    [Space]
    [Header("Lose Screen")]
    public Text monsters;
    public Text boss;
    public Text coins;
    public GameObject player;
    public bool revardOk;
    public GameObject BTNShowVid;
    public void ShowLoseScrean()
    {
        GameScrean.SetActive(false);
        LoseScrean.SetActive(true);
        SetTotal(
            GameSetAndStat.Instance._G_MonstersKill,
            GameSetAndStat.Instance._G_BossKill,
            GameSetAndStat.Instance._TCoins
            );
        if (revardOk) BTNShowVid.SetActive(true);
        else BTNShowVid.SetActive(false);
    }

        public void SetTotal(int monst, int bos, int coin)
    {
        monsters.text = monst.ToString();
        boss.text = bos.ToString();
        coins.text = coin.ToString();
    }

    public void BackToMain()
    {
        GameController.Instance.currentState = GameState.MainMenu;
        GameScrean.SetActive(true);
        LoseScrean.SetActive(false);
        RootCleaner.Instance.CleanRoots();
        EnemySpawnController.Instance.Restart(0.1f);
    }


  
    public void RevardedAds()
    {      
        GameScrean.SetActive(true);
        LoseScrean.SetActive(false);
        player.GetComponent<ShildActive>().ShakeContinue(3);
        if (EnemySpawnController.Instance.Vaves == 0)
        {
            EnemySpawnController.Instance.Vaves = Random.Range(2, 5);

            RootCleaner.Instance.CleanRoots();
        }
        GameController.Instance.currentState = GameState.Play;
      
    }

    #endregion

    #region control audio battons
    public void SetABGBTN(int play)
    {
        if (play > 0)
        {
            AuBgBTNs[0].SetActive(true);
            AuBgBTNs[1].SetActive(false);
        }

        else
        {
            AuBgBTNs[1].SetActive(true);
            AuBgBTNs[0].SetActive(false);
        }
    }

    public void SetAFXBTN(int play)
    {
        if (play > 0)
        {
            AuFxBTNs[0].SetActive(true);
            AuFxBTNs[1].SetActive(false);
        }

        else
        {
            AuFxBTNs[1].SetActive(true);
            AuFxBTNs[0].SetActive(false);
        }
    }
    #endregion

    #region PoverUp's BTN in Game
    void updBtn()
    {
        /* unit price 
  0 - Shild
  1 - PoverUp
  2 - CoinPowerUp
  */
        CountMarPowerUp.text = CountPowerUp.text = GameSetAndStat.Instance.SaveObj.CountPowerUp.ToString();
        CountMarCoinUp.text = CountCoinUp.text = GameSetAndStat.Instance.SaveObj.CountCoinPowerUp.ToString();
        CountMarShild.text = CountShild.text = GameSetAndStat.Instance.SaveObj.CountShild.ToString();
        PricePowerUp.text = GameSetAndStat.Instance.prices[1].ToString();
        PriceCoinUp.text = GameSetAndStat.Instance.prices[2].ToString();
        PriceShild.text = GameSetAndStat.Instance.prices[0].ToString();

        if (GameSetAndStat.Instance.SaveObj.CountPowerUp == 0 || PlayerWeapon.Instance.bonus == 2)
            PowerUp.SetActive(false);
        else
            PowerUp.SetActive(true);

        if (GameSetAndStat.Instance.SaveObj.CountCoinPowerUp == 0 || GameSetAndStat.Instance.bonusCoin)
            CoinUp.SetActive(false);
        else
            CoinUp.SetActive(true);

        if (GameSetAndStat.Instance.SaveObj.CountShild == 0)
            Shild.SetActive(false);
        else
            Shild.SetActive(true);
    }
    #endregion



    #region Statistic Screen

    public GameObject Screen;
    public Text TimeMagnet;
    public Text TimeShild;
    public Text ShotPower;
    public Text ProbCoin;
    public Text ProbMagnet;
    public Text ProbPA;
    public GameObject ContentSc;

    void UpdateState()
    {
        if (Screen.activeSelf)
        {
            int i = ContentSc.GetComponent<SnapScroling>().selectedPanId;

            TimeMagnet.text = GameController.Instance.Heroes[i].attribute.TimeMagnet.ToString();
            TimeShild.text = GameController.Instance.Heroes[i].attribute.TimeShild.ToString();

            var shot = GameController.Instance.BasicShot * (GameController.Instance.Heroes[i].Level + 1); ;

            ShotPower.text = shot.ToString();

            ProbMagnet.text = GameController.Instance.Heroes[i].attribute.ProbabMagnet.ToString();
            ProbPA.text = GameController.Instance.Heroes[i].attribute.ProbabPowerUp.ToString();

            int s = 100 - GameController.Instance.Heroes[i].attribute.ProbabMagnet - GameController.Instance.Heroes[i].attribute.ProbabPowerUp;

            ProbCoin.text = s.ToString();
        }
        else return;
    }

    #endregion

    #region  Sidekicks Choice 

    [Space]
    [Header("Обьекты меню помощников")]
    public GameObject ContentLeft;
    public GameObject ContentRight;
    public GameObject selectSideLeft;
    public GameObject selectSideRight;
    public GameObject menuSides;

    void UpdateSideChoice()
    {

        if (!menuSides.activeSelf)
            return;
        else
        {
            if (GameController.Instance.IndCurrentLeft == ContentLeft.GetComponent<SnapScroling>().selectedPanId)
            {
                selectSideLeft.SetActive(false);
            }
            else selectSideLeft.SetActive(true);

            if (GameController.Instance.IndCurrentRight == ContentRight.GetComponent<SnapScroling>().selectedPanId)
            {
                selectSideRight.SetActive(false);
            }
            else selectSideRight.SetActive(true);
        }

    }


    #endregion




    #region marketSides

    [Space]
    [Header("Обьекты меню магазина помощников")]
    public GameObject BTNUpdate;
    public GameObject LBGOLevel;
    public GameObject LBMaxLev;
    public Text LBPrice;
    public Text LBLevel;

    private int tempIndSides = 0;

    public void UpdateSides()
    {
        Debug.Log("Update " + tempIndSides);


        int myMoney = GameSetAndStat.Instance._G_Coins;
        int price = GameController.Instance.SideKicks[tempIndSides].PricePerLevel;

        if (myMoney < price)
            return;
        else
        {
            GameSetAndStat.Instance._G_Coins -= price;
            GameController.Instance.SideKicks[tempIndSides].Level++;
            GameSetAndStat.Instance.SaveData();
            UpdateValuesSides(tempIndSides);
        }


    }

    public void UpdateValuesSides(int i)
    {
        tempIndSides = i;


        if (GameController.Instance.SideKicks[i].Level < GameController.Instance.SideKicks[i].MaxHeroLevel)
        {
            BTNUpdate.SetActive(true);
            LBGOLevel.SetActive(true);
            LBMaxLev.SetActive(false);

        }
        else
        {
            BTNUpdate.SetActive(false);

            LBGOLevel.SetActive(false);
            LBMaxLev.SetActive(true);
        }
        int levelId = GameController.Instance.SideKicks[i].Level + 1;
        LBLevel.text = levelId.ToString();
        int tempPrice = GameController.Instance.SideKicks[i].PricePerLevel;
        LBPrice.text = tempPrice.ToString();
    }

    #endregion marketSides


    private void Update()
    {
        if (score.IsActive())
            ShowGCoints();
        if (SettMenu.activeSelf)
        {
            SetAFXBTN(GameSetAndStat.Instance._G_EffectsAudio);
            SetABGBTN(GameSetAndStat.Instance._G_BackgroundAudio);
        }
        updBtn();
        UpdateState();
        UpdateSideChoice();


        if (Input.GetKeyDown(KeyCode.Space))
            RevardedAds();
    }
}
