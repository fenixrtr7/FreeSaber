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

    // Seed
    public int seedSelectedLevel = 1;

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Level Selected
        seedSelectedLevel = Random.Range(0,9999);

        // Seleciconar la seed
        Random.seed = seedSelectedLevel;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            Debug.Log("Nuevo estado GAME OVER");
        }

        //Update current State
        this.currentGameState = newGameState;
    }
}
