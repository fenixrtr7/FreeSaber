using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
        menu, inGame, gameOver
}

public class GameManager : MonoBehaviour
{
    // Llamamos al estado
    public GameState currentGameState = GameState.menu;

    public static GameManager sharedInstance; // Uso de singleton

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(sharedInstance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void StarGame()
    {
        SetGameState(GameState.inGame);
    }


    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }


    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    public void RestartGame()
    {
        GamePreparationManager.instance.RestartValorGame();
        BackToMenu();

        SceneManager.LoadScene(0);
    }

    // Cambiar estado de juego
    private void SetGameState(GameState newGameState)
    {
        // Estado MENU
        if (newGameState == GameState.menu)
        {
            MenuManager.sharedInstance.ShowMainMenu();
            Debug.Log("Nuevo estado MENU");
        }

        // Estado en JUEGO !!
        else if (newGameState == GameState.inGame)
        {
            MenuManager.sharedInstance.ShowInGame();

            Debug.Log("Nuevo estado IN GAME");
        }

        // Estado GAME OVER.
        else if (newGameState == GameState.gameOver)
        {
            MenuManager.sharedInstance.ShowGameOver();

            GamePreparationManager.instance.FinishGame(GamePreparationManager.currentScore);

            Debug.Log("Nuevo estado GAME OVER. Score: " + GamePreparationManager.currentScore);
        }

        //Update current State
        this.currentGameState = newGameState;
    }
}
