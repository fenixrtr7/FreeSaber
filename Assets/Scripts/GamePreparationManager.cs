#define FCM_PRESENT
 
#define MULTIPLE_SCENES_LEVELS
 
#define LOADING_SCENE
 
using System.Collections.Generic;
using System.Linq;
using EazeGamesSDK;
#if FCM_PRESENT
//using Firebase.Messaging;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
 
 
public class GamePreparationManager : MonoBehaviour, EAZGamePreparationDelegate, EAZGamePlayDelegate
{
    public static GamePreparationManager instance;
    public static event System.Action DidEndGameEvent;
    public static event System.Action WaitingForOpponentEvent;
    public static event System.Action<long> DidReceiveOpponentsScoreEvent;
    public static event System.Action<EAZGameStartInfo> DidReceiveStartGameInfo;
    public static List<int> levelList = new List<int>();
    public static int currentLevel = 0;
    public static long currentScore = 0;
    public static long oppScore = 0;
    public static bool playing = false;
    public static float timeGame;
    public float defectTime = 20;
    //public static int endDate = 0;
    //public static int gameDuration = 0;
 
    private void Awake()
    {
        timeGame = defectTime;

        PlayerPrefs.DeleteAll();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
 
#if FCM_PRESENT
            //FirebaseMessaging.TokenReceived += OnTokenReceived;
#endif
 
// #if MULTIPLE_SCENES_LEVELS
//             SceneManager.sceneLoaded += OnSceneLoaded;
// #endif
   
            EazeGames.shared.gamePreparationManager.setPreparationDelegate(this); 
        }
        else if (instance != this)
            //Prevents multiple API calls on Initial scene reload
            Destroy(gameObject);
    }
 
#if MULTIPLE_SCENES_LEVELS
    public void OnSceneLoaded()
    {
        #if LOADING_SCENE
        if (SceneManager.GetActiveScene().name == "LoadScene")
        {
            SceneManager.LoadSceneAsync("GameScene");
        }
        #endif
 
        if (SceneManager.GetActiveScene().name == "GameScene")
        {

            
            Debug.Log("Pasamos");
            didStartPlaying();
            playing = true;
        }
    }
#endif
 
    #region GamePreparationDelegate
 
    private void gameDidLoad()
    {
        EazeGames.shared.gamePreparationManager.gameDidLoad();
    }
 
    public void startLoadingGameResourcesFor(string gameId)
    {
        gameDidLoad();
    }
    public void provideGameControllerWithStartGameInfo(
     EAZGameStartInfo startGameInfo)
    {
        // Level Selected
        currentLevel = Random.Range(0,9999);
        Debug.Log("Current level: " + currentLevel);
        
        // Select seed
        Random.seed = currentLevel;

        //gameDuration = startGameInfo.gameDuration;
 
    // #if MULTIPLE_SCENES_LEVELS
    //     levelList = startGameInfo.levels.Split('-').Select(int.Parse).ToList();
    // #endif
 
        EazeGames.shared.gamePlayManager.setGamePlayDelegate(this);
        
   
        //endDate = Epoch.Current() + gameDuration;
        instance.didStartPlaying();
        playing = true; 
        //Debug.Log("Empezamos Juego: " + gameDuration + " endDate: " + endDate);
    }
    #endregion
 
    #region EAZGamePlayDelegate
 
    public void didEndGame()
    {
        if (DidEndGameEvent != null)
        {
            DidEndGameEvent();
        }
    }
 
    public void didReceiveOpponentsScore(long opponentScore)
    {
        oppScore = opponentScore;
        if (DidReceiveOpponentsScoreEvent != null)
        {
            DidReceiveOpponentsScoreEvent(opponentScore);
        }
    }
 
    public void waitingForOpponent()
    {
        if (WaitingForOpponentEvent != null)
        {
            WaitingForOpponentEvent();
        }
    }
 
    #endregion
    #region GamePlayEvents
 
    public void didStartPlaying()
    {
        EazeGames.shared.gamePlayManager.didStartPlaying();
    }  
 
    //
    public void SendScore(long gameScore)
    {
        currentScore = gameScore;
        EazeGames.shared.gamePlayManager.sendScore(gameScore);
    }   
 
    public void FinishGame(long finalScore)
    {
        EazeGames.shared.gamePlayManager.finishPlayingWithFinalScore(finalScore);
    }
 
    public void LeaveGame()
    {
        EazeGames.shared.gamePlayManager.leaveGame();
    }
    #endregion
    #region PushManagament
#if FCM_PRESENT
 
    // private static void OnTokenReceived( object sender, TokenReceivedEventArgs token )
    // {
    //     EazeGames.shared.setFCMToken( token.Token );
    // }
#endif
    #endregion
 
//     private void OnApplicationFocus(bool focus)
//     {
//         if (focus && SceneManager.GetActiveScene().name != "{InitSceneName}")
//         {
// #if LOADING_SCENE
//             SceneManager.LoadScene("SampleScene");
// #else
//             SceneManager.LoadScene("{InitSceneName}");
// #endif
//             EndGame();
//         }
//    }
    public void RestartValorGame()
    {
        timeGame = defectTime;

        currentLevel = 0;
        currentScore = 0;
        oppScore = 0;
        playing = false;
        // endDate = 0;
        // gameDuration = 0;
    }
}
