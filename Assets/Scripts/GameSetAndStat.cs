using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using System;
using CHe;
public class GameSetAndStat : SingletonObj<GameSetAndStat>
{

    public Save SaveObj = new Save(); // обьект данных для записи

    public GameObject GPLeaderBoard;


    [HideInInspector]
    public int _TCoins = 0;
    public int _G_MonstersKill = 0;
    public int _G_BossKill = 0;
    //глобальные поля для управления и сохранения    


    public int _G_SensitiveTouch;
    public int _G_BackgroundAudio;
    public int _G_EffectsAudio;
    public int _G_CurrHerro;
    public int _G_CurrLeft;
    public int _G_CurrRight;
    public int _G_Coins;
    public int _G_SpeedPerCoint  = 20;

    // ----------------------------------
    public List<int> prices;
    /* unit price 
    0 - Shild
    1 - PoverUp
    2 - CoinPowerUp
    */
    // ----------------------------------

    public bool bonusCoin;
    
    private int levGame = 5;

    public void AddBonusCoin()
    {
        bonusCoin = true;
    }

    public void CoinPlus()
    {
        if (bonusCoin)
            _TCoins += 2;
        else _TCoins++;
        GameController.Instance.coinsText.text = _TCoins.ToString();
        levGame = 5 + _TCoins / _G_SpeedPerCoint;
        GameController.Instance.enemyMoveSpeed = levGame;       
    }

    

    public void Reset()
    {
        GameController.Instance.enemyMoveSpeed = 5;
        _G_Coins += _TCoins;
       
        _G_MonstersKill = 0;
        _G_BossKill = 0;
        bonusCoin = false;
        EnemySpawnController.Instance.spawnTime = 2.5f;
        GameController.Instance.f = 1;
        GameController.Instance.levelInGame = 1;
        GameController.Instance.coinsText.text = _TCoins.ToString();
        GPLeaderBoard.GetComponent<LoginGP>().SendLeaderBord(_TCoins);
        SaveData();
        _TCoins = 0;
    }

    public void Del()
    {
        PlayerPrefs.DeleteAll();
        _G_SensitiveTouch = 30;
        _G_BackgroundAudio = 1;
        _G_EffectsAudio = 1;
        _G_Coins = 0;
        _G_CurrHerro = 0;
        _G_CurrLeft = 0;
        _G_CurrRight = 0;
        _G_BossKill = 0;
        NullHeroSideData();
       
        GameController.Instance.IndCurrentHerro = 0;
        GameController.Instance.IndCurrentLeft = 0;
        GameController.Instance.IndCurrentRight = 0;
       
        if (!PlayerPrefs.HasKey("Save"))
        {
            print("No date");
        }
        
        SaveData();
        GetData();

    }
    void NullHeroSideData()
    {
        for (int i = 0; i < GameController.Instance.Heroes.Count; i++)
        {
            if (i == 0)
            {
                GameController.Instance.Heroes[i].Level = 0;
            }
            else
            {
                GameController.Instance.Heroes[i].Level = 0;
                GameController.Instance.Heroes[i].isOpen = false;
            }
        }

        for (int i = 0; i < GameController.Instance.SideKicks.Count; i++)
        {
                GameController.Instance.SideKicks[i].Level = 0;
                GameController.Instance.SideKicks[i].isOpen = true;          
        }
    }





    //--Debug-------------
    public void Million()
    {

        _G_Coins = 1000000;
        SaveData();
    }
    //--EndDebug-------------


    public void SaveData()
    {
        

        SaveObj.SensitiveTouch = _G_SensitiveTouch;
        SaveObj.BackgroundAudio = _G_BackgroundAudio;
        SaveObj.EffectsAudio = _G_EffectsAudio;
        SaveObj.PlayerCoins = _G_Coins;
        SaveObj.CurrHerro = _G_CurrHerro = GameController.Instance.IndCurrentHerro ;
        SaveObj.CurrLeft = _G_CurrLeft = GameController.Instance.IndCurrentLeft;
        SaveObj.CurrRight = _G_CurrRight = GameController.Instance.IndCurrentRight;


        SetHeroesDate();
        SetLeftDate();
        SetRightDate();

        PlayerPrefs.SetString("Save", JsonUtility.ToJson(SaveObj));
        Debug.Log("load BGAu->" + SaveObj.BackgroundAudio.ToString() +
                   " __Sens->" + SaveObj.SensitiveTouch.ToString() +
                   " __FX->" + SaveObj.EffectsAudio.ToString() +
                   " __Coins->" + SaveObj.PlayerCoins.ToString() +
                   " _Herro->" + SaveObj.CurrHerro.ToString() +
                   " _Left->" + SaveObj.CurrLeft.ToString() +
                   " _Right->" + SaveObj.CurrRight.ToString()
                   );

    }

    public void GetData()
    {
        if (!PlayerPrefs.HasKey("Save"))
        {

            print("No date");
            _G_SensitiveTouch = 50;
            _G_BackgroundAudio = 1;
            _G_EffectsAudio = 1;
            _G_Coins = 0;
            _G_CurrHerro = 0;
            Del();
        }
        else
        {
            SaveObj = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
            Debug.Log("load BGAu->" + SaveObj.BackgroundAudio.ToString() +
                    " __Sens->" + SaveObj.SensitiveTouch.ToString() +
                    " __FX->" + SaveObj.EffectsAudio.ToString() +
                    " __Coins->" + SaveObj.PlayerCoins.ToString() +
                    " _Herro->" + SaveObj.CurrHerro.ToString()+
                    " _Left->" + SaveObj.CurrLeft.ToString()+
                    " _Right->" + SaveObj.CurrRight.ToString()
                    );

            _G_SensitiveTouch = SaveObj.SensitiveTouch;
            _G_BackgroundAudio = SaveObj.BackgroundAudio;
            _G_EffectsAudio = SaveObj.EffectsAudio;
            _G_Coins = SaveObj.PlayerCoins;
            _G_CurrHerro = SaveObj.CurrHerro;
            _G_CurrLeft = SaveObj.CurrLeft;
            _G_CurrRight = SaveObj.CurrRight;

            GetHeroesDate();
            GetLeftDate();
            GetRightDate();

        }

        AudioController.Instance.Set(_G_BackgroundAudio);
        AudioController.Instance.SetFX(_G_EffectsAudio);
        SetCurrHero();
       

    }

    public void ChangeBGAudio()
    {
        if (AudioController.Instance.source[0].isPlaying)
            _G_BackgroundAudio = 1;
        else _G_BackgroundAudio = 0;
    }

    public void ChangeFXAudio()
    {
        if (_G_EffectsAudio == 1)
        {
            _G_EffectsAudio = 0;
        }

        else
        {
            _G_EffectsAudio = 1;
        }
    }
    
    public void SetCurrHero()
    {
        GameController.Instance.IndCurrentHerro = _G_CurrHerro;
        PlayerController.Instance.SetAv();
        SetLeftHero();
        SetRightHero();
    }


    public void SetLeftHero()
    {
        GameController.Instance.IndCurrentLeft = _G_CurrLeft;
        PlayerController.Instance.SetLeft();
    }

    public void SetRightHero()
    {
        GameController.Instance.IndCurrentRight = _G_CurrRight;
        PlayerController.Instance.SetRight();
    }

    /*--------------------------------------------*/
    void SetHeroesDate()
    {
        if (SaveObj.heroeslist.Count < GameController.Instance.Heroes.Count)
        {
            for (int i = 0; i < GameController.Instance.Heroes.Count; i++)
            {
                SaveObj.heroeslist.Add(new heroes());
            }
        }
        for (int i = 0; i < GameController.Instance.Heroes.Count; i++)
        {
            SaveObj.heroeslist[i].isOpen = GameController.Instance.Heroes[i].isOpen;
            SaveObj.heroeslist[i].Level = GameController.Instance.Heroes[i].Level;
        }
    }

    void GetHeroesDate()
    {
        if (SaveObj.heroeslist.Count < GameController.Instance.Heroes.Count)
        {
            for (int i = 0; i < GameController.Instance.Heroes.Count; i++)
            {
                SaveObj.heroeslist.Add(new heroes());
            }
        }

        for (int i = 0; i < GameController.Instance.Heroes.Count; i++)
        {
            GameController.Instance.Heroes[i].isOpen = SaveObj.heroeslist[i].isOpen;
            GameController.Instance.Heroes[i].Level = SaveObj.heroeslist[i].Level;
        }
    }
    /*--------------------------------------------*/

    /*--------------------------------------------*/
    void SetLeftDate()
    {
        if (SaveObj.heroesSides.Count < GameController.Instance.SideKicks.Count)
        {
            for (int i = 0; i < GameController.Instance.SideKicks.Count; i++)
            {
                SaveObj.heroesSides.Add(new heroes());
            }
        }
        for (int i = 0; i < GameController.Instance.SideKicks.Count; i++)
        {
            SaveObj.heroesSides[i].isOpen = GameController.Instance.SideKicks[i].isOpen;
            SaveObj.heroesSides[i].Level = GameController.Instance.SideKicks[i].Level;
        }
    }
    void GetLeftDate()
    {
        if (SaveObj.heroesSides.Count < GameController.Instance.SideKicks.Count)
        {
            for (int i = 0; i < GameController.Instance.SideKicks.Count; i++)
            {
                SaveObj.heroesSides.Add(new heroes());
            }
        }

        for (int i = 0; i < GameController.Instance.SideKicks.Count; i++)
        {
            GameController.Instance.SideKicks[i].isOpen = SaveObj.heroesSides[i].isOpen;
            GameController.Instance.SideKicks[i].Level = SaveObj.heroesSides[i].Level;
        }
    }
    /*--------------------------------------------*/

    /*--------------------------------------------*/
    void SetRightDate()
    {
        if (SaveObj.heroesSides.Count < GameController.Instance.SideKicks.Count)
        {
            for (int i = 0; i < GameController.Instance.SideKicks.Count; i++)
            {
                SaveObj.heroesSides.Add(new heroes());
            }
        }
        for (int i = 0; i < GameController.Instance.SideKicks.Count; i++)
        {
            SaveObj.heroesSides[i].isOpen = GameController.Instance.SideKicks[i].isOpen;
            SaveObj.heroesSides[i].Level = GameController.Instance.SideKicks[i].Level;
        }
    }
    void GetRightDate()
    {
        if (SaveObj.heroesSides.Count < GameController.Instance.SideKicks.Count)
        {
            for (int i = 0; i < GameController.Instance.SideKicks.Count; i++)
            {
                SaveObj.heroesSides.Add(new heroes());
            }
        }

        for (int i = 0; i < GameController.Instance.Heroes.Count; i++)
        {
            GameController.Instance.SideKicks[i].isOpen = SaveObj.heroesSides[i].isOpen;
            GameController.Instance.SideKicks[i].Level = SaveObj.heroesSides[i].Level;
        }
    }
    /*--------------------------------------------*/

    #region market
    public void BuyPA(int i)
    {
        if (_G_Coins - prices[i] > 0)
        {
            _G_Coins -= prices[i];
            switch (i)
            {
                case 0:
                    SaveObj.CountShild++;
                    break;
                case 1:
                    SaveObj.CountPowerUp++;
                    break;
                case 2:
                    SaveObj.CountCoinPowerUp++;
                    break;
                default:
                    break;
            }
            SaveData();
        }
        else return;
    }

    public void UsePA(int i)
    {
           switch (i)
            {
                case 0:
                    SaveObj.CountShild--;
                    break;
                case 1:
                    SaveObj.CountPowerUp--;
                    break;
                case 2:
                    SaveObj.CountCoinPowerUp--;
                    break;
                default:
                    break;
            }
            SaveData();    
    }

    #endregion

    #region sidemarket

    #endregion

}



[Serializable]
public class Save
{
    //gameSetting
    public int SensitiveTouch;

    public int BackgroundAudio;

    public int EffectsAudio;

    public int CountShild;

    public int CountPowerUp;

    public int CountCoinPowerUp;

    public int PlayerCoins;
    public int MonstersKill;

    public int CurrHerro;
    public int CurrLeft;
    public int CurrRight;

    public List<heroes> heroeslist;
    public List<heroes> heroesSides;
    
}

[Serializable]
public class heroes
{
    public bool isOpen;
    public int Level;
}


