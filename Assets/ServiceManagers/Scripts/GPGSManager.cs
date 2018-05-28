using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
#endif

public class GPGSManager : MonoBehaviour {

    public static GPGSManager Instance;
    [Header("IOS Settings")]

    public string leaderBoardID;
    public bool LogEnagles = false;


    private bool mAuthenticating = false;
      

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogWarning("You have duplicate Google Managers GPGS in your scene!");
        }

#if UNITY_ANDROID
        leaderBoardID = GPGSIds.leaderboard_leaders;
#endif
    }

    //чек на авторизацию в плей сервис платформе
    public bool Authenticated
    {
        get
        {
            return Social.Active.localUser.authenticated;
        }
    }


    // авторизация в гуглплейсервисах
    public void Authenticate()
    {
        if (Authenticated || mAuthenticating)
        {
            Debug.LogWarning("Ignoring repeated call to Authenticate().");
            return;
        }

#if UNITY_ANDROID
        // Enable/disable logs on the PlayGamesPlatform
        Debug.LogWarning("One");
        PlayGamesPlatform.DebugLogEnabled = LogEnagles;

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        Debug.LogWarning("Two");
        // Activate the Play Games platform. This will make it the default
        // implementation of Social.Active
        PlayGamesPlatform.Activate();
        Debug.LogWarning("Three");
        // Set the default leaderboard for the leaderboards UI
        ((PlayGamesPlatform)Social.Active).SetDefaultLeaderboardForUI(leaderBoardID);
#endif

        // Sign in to Servises
        mAuthenticating = true;
        Social.localUser.Authenticate((bool success) =>
        {
            mAuthenticating = false;
            if (success)
            {
                // if we signed in successfully, load data from cloud
                Debug.Log("Login successful!");
            }
            else
            {
                // no need to show error message (error messages are shown automatically
                // by plugin)
                Debug.LogWarning("Failed to sign in with Google Play Games.");
            }
        });
    }

    //Показать лидерборд
    public void ShowLeaderboardUI()
    {
        if (Authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }
    
    // запостить лидерборд
    public void PostToLeaderboard(int score)
    {
        Social.ReportScore(score, leaderBoardID, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Reported score successfully");
            }
            else
            {
                Debug.Log("Failed to report score");
            }
            Debug.Log(success ? "Reported score successfully" : "Failed to report score"); Debug.Log("New Score:" + score);
        });
    }

}
