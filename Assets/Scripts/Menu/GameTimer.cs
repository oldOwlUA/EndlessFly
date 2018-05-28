using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace SylokHiddenCat {
    public class GameTimer : MonoBehaviour {
        
        public static GameTimer Instance;
        public float multiplyEnergy = 120;

        [Tooltip("Время отката энергии в сек")]
        public float restoreTime = 120;
        public static float firstTime;
        public static float secondTime;
        public float difference;
        
        [Tooltip("общее время отката энергии")]
        public int playTime;
        public bool startCount = false;
        public int energyCount;
        private static bool isStartApp = true;
        public static bool rechargeActivate = false;
        public static bool PlusEnergy = false;
        public System.DateTime InfBuyDate = new System.DateTime();
        public System.DateTime curentDate = new System.DateTime();
        public System.DateTime lastTime = new System.DateTime();
        
        private TimeSpan delta = new TimeSpan();
        private int seconds = 0;
        private int minutes = 0;
        private int hours = 0;
        private int days = 0;
        private float currentTime;
        public int EnergyCount
        {
            get { return energyCount; }
            set
            {
                energyCount = value;
                SetCubeCount(energyCount);
                if (energyCount < 10 && isStartApp)
                    RestorCubeFromOff(); // востанавлимвает кубики если игрок выключил игру и зашел в нее через время 
                                         //if (cubeText != null) cubeText.text = energyCount.ToString() + "/15";
                                         // если кубиков меньше нужного количества то начинается востановниесе
                if(energyCount <= 10)
                {
                    if(playTime == 0)
                    {
                        playTime =(int) restoreTime;
                    }
                    else
                    {
                        //playTime = SaveData.EndTime;
                    }
                    if (PlusEnergy)
                    {
                        //playTime += (int)restoreTime;
                    }
                    if (Application.loadedLevelName == "Menu")
                    {
                        print("Change count");
                        //MenuController.instance.SetCountOfEnergy();
                    }
                }
                
                if (energyCount < 10 && !rechargeActivate)
                {
                    startCount = true;
                    //print("StartTimer");
                    rechargeActivate = true;
                    StartCoroutine("PlayTimer");
                }
                if(energyCount == 10)
                {
                    isStartApp = false;
                    multiplyEnergy = 120;
                    playTime = 0;
                    if (Application.loadedLevelName == "Menu")
                    {
                        print("Change count");
                        //MenuController.instance.SetCountOfEnergy();
                    }
                }
                
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }



        void Start() {

            firstTime = DateTime.Now.Second;
            //print("firstTime " + firstTime);
            //print("date_time " + DateTime.Now.Second.GetType());
           // playTime = SaveData.EndTime;
            //if(playTime > 0)
            //{
            //    multiplyEnergy = SaveData.MultiplyEnergy;
            //}
            //else
            //{
            //    multiplyEnergy = 120;
            //}

            GetGubeFromPref();
            //StartCoroutine("PlayTimer");
        }


        void Update() {
            //if(playTime > 0)
            //{
            //    multiplyEnergy -= Time.deltaTime * 0.98f;
            //    if(multiplyEnergy < 0.5)
            //    {
            //        EnergyCount++;
            //        multiplyEnergy = 120;
            //        //SaveData.CurrentEnergy += 1;
            //        if (Application.loadedLevelName == "Menu")
            //        MenuController.instance.SetCountOfEnergy();
            //    }
            //}
        }

        public void RestorCubeFromOff()
        {
            print("RestoreFro,OFF");
            isStartApp = false;
            curentDate = System.DateTime.Now;
            var txt = LoadStartRestData();
            lastTime = System.DateTime.Parse(txt);
            delta = curentDate - lastTime;
            print("DELTA " + (int)delta.TotalSeconds);
            multiplyEnergy = 120;
            var count = (int)delta.TotalSeconds / restoreTime;
            print("COUNT    " + count);
            rechargeActivate = false;
            if ((Instance.EnergyCount + count) > 10)
            {
                Instance.EnergyCount = 10;
                playTime = 0;
                multiplyEnergy = 120;
            }
            else
            {

                Instance.EnergyCount += (int)count;
                if(Instance.EnergyCount > 10)
                {
                    Instance.EnergyCount = 10;
                }
               // playTime -= (int)delta.TotalSeconds;
                //multiplyEnergy -= (int)delta.TotalSeconds;
                //if (multiplyEnergy > playTime)
                //{
                //    multiplyEnergy = playTime - 0.5f;
                //}
            }
            if(playTime < 0)
            {
                
                
                    playTime = 0;
                    multiplyEnergy = 120;
                    Instance.EnergyCount = 10;
               
            }
          //  MenuController.instance.SetCountOfEnergy();
            
        }
        public void Stoptimer()
        {
            StopCoroutine("PlayTimer");
        }
        public void StartPlayTimer()
        {
            StartCoroutine("PlayTimer");
        }
        IEnumerator PlayTimer()
        {
            
            while (startCount)
            {

                yield return new WaitForSeconds(1);
                playTime -= 1;
                seconds = (playTime % 60);
                minutes = (playTime / 60) % 60;
                hours = (playTime / 3600) % 24;
                days = (playTime / 86400) % 365;
                if(playTime % restoreTime == 0)
                {
                    
                    EnergyCount++;
                    
                }
                if (Application.loadedLevelName == "Menu")
                {
                    //if (seconds < 10)
                    //{

                    //    MenuController.instance.timer.text = minutes.ToString() + ":" + "0" + seconds.ToString();
                    //}
                    //else if (hours > 0)
                    //{
                    //    if (minutes < 10)
                    //    {
                    //        MenuController.instance.timer.text = hours.ToString() + ":" + "0" + minutes.ToString() + ":" + seconds.ToString();
                    //    }
                    //    else
                    //    {
                    //        MenuController.instance.timer.text = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
                    //    }

                    //}
                    //else
                    //{
                    //    MenuController.instance.timer.text = minutes.ToString() + ":" + seconds.ToString();
                    //}

                    if (minutes < 10)
                    {
                        //MenuController.instance.timer.text = "0" + minutes.ToString() + ":" + seconds.ToString();
                        if (seconds < 10)
                        {
                          //  MenuController.instance.timer.text = "0" + minutes.ToString() + ":" + "0" + seconds.ToString();
                        }
                    }
                    else
                    {
                        if (seconds < 10)
                        {
                            //MenuController.instance.timer.text = minutes.ToString() + ":" + "0" + seconds.ToString();
                        }
                        else
                        {
                            //MenuController.instance.timer.text = minutes.ToString() + ":" + seconds.ToString();
                        }
                    }
                    SaveStartRestTime();
                }
                if (playTime > 0)
                {
                    //SaveData.MultiplyEnergy = multiplyEnergy;
                    //SaveData.EndTime = playTime;
                }
                if (playTime < 0)
                {
                    startCount = false;
                    playTime = 0;
                    multiplyEnergy = 120;
                }
                if (Instance.EnergyCount < 10)
                {
                    //StartCoroutine(cAutoRestoreCube());
                }
                else
                {
                    playTime = 0;
                    //MenuController.instance.timer.text = "00:00";
                    startCount = false;
                    rechargeActivate = false;
                }
            }
           // StartCoroutine("PlayTimer");
        }
        private void OnLevelWasLoaded(int level)
        {
            //print("level " + level);
            if (level == 0)
            {
                firstTime = 0.0f;

                firstTime += DateTime.Now.Hour * 3600;
                firstTime += DateTime.Now.Minute * 60;
                firstTime += DateTime.Now.Second;
                //print("firstTime " + firstTime);
                difference = secondTime - firstTime;
                //print("difference " + difference);
            }

            if (level == 1)
            {
                secondTime = 0.0f;

                secondTime += DateTime.Now.Hour * 3600;
                secondTime += DateTime.Now.Minute * 60;
                secondTime += DateTime.Now.Second;
                //print("secondTime " + secondTime);
            }

        }

        private void OnApplicationQuit()
        {
            if(playTime > 0)
            {
                //SaveData.MultiplyEnergy = multiplyEnergy;
                //SaveData.EndTime = playTime;
            }
            
        }


        private string LoadStartRestData()
        {
            if (!PlayerPrefs.HasKey("lasttime"))
            {
                SaveStartRestTime();
            }
            return PlayerPrefs.GetString("lasttime");
        }

        private void SaveStartRestTime()
        {
            PlayerPrefs.SetString("lasttime", System.DateTime.Now.ToString("G"));
        }
        public static void GetGubeFromPref()
        {
            if (PlayerPrefs.HasKey("CurrentEnergyIdKey"))
            {
              //  Instance.EnergyCount = SaveData.CurrentEnergy;
            }
            else
            {
                Instance.EnergyCount = 10;
            }
        }
        public static void SetCubeCount(int value)
        {
           // SaveData.CurrentEnergy = value;
        }
       
    }
}
