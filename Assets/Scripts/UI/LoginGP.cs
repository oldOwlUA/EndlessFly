using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;



public class LoginGP : MonoBehaviour {


    public string leaderBoardID;


    private void Start()
    {
       
    }

    public bool Authenticated
    {
        get
        {
            return Social.Active.localUser.authenticated;
        }
    }
    public GameObject SH;

    public void Authenticate()
    {
        leaderBoardID = GPGSIds.leaderboard_leaders;  

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();


        ((PlayGamesPlatform)Social.Active).SetDefaultLeaderboardForUI(leaderBoardID);





        Social.localUser.Authenticate((bool success) =>
        {         
            if (success)
            {
               
                Debug.Log("Login successful!");
                SH.SetActive(true);
            }
            else
            {
             
                Debug.LogWarning("Failed to sign in with Google Play Games.");
                SH.SetActive(false);
            }
        });


    }
    public void ShowLeaderboardUI()
    {

        if (Authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }

    public void SendLeaderBord( int score)
    {
       if (Authenticated)
        {
            Social.ReportScore(score, leaderBoardID, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Send " + score.ToString() + " poitns");                
                }
                else
                {
                    Debug.LogWarning("Failed to send in  Google Play Leader.");                    
                }
            });
        }
    }

}
