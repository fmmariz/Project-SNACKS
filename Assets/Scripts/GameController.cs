using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState{
        PLAYING,
        VICTORY,
        GAMEOVER,
        PAUSED
    }

    public static GameController Instance { get; private set; }
    public UIController uiController;
    public SoundController soundControl;
    public ResetController resetController;

    [SerializeField]
    public GameObject stage;

    private GameState _currentState; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();
        soundControl = GetComponent<SoundController>();
        resetController = GetComponent<ResetController>();
        _currentState = GameState.PLAYING;

        foreach (Transform child in stage.transform)
        {
            Rigidbody2D rigidBody;
            if (child.TryGetComponent<Rigidbody2D>(out rigidBody))
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentState  == GameState.PLAYING)
            {
                Time.timeScale = 0f;
                uiController.ShowPauseMessage(true);
                _currentState = GameState.PAUSED;
            }
            else if(_currentState == GameState.PAUSED)
            {
                _currentState = GameState.PLAYING;

                Time.timeScale = 1f;
                uiController.ShowPauseMessage(false);
            }
        }
    }

    public UIController GetUIController()
    {
        return uiController;
    }

    public GameState GetCurrentGameState()
    {
        return _currentState;
    }

    public void SetCurrentGameState(GameState currentState)
    {
        _currentState = currentState;
    }

    public void CloseGame()
    {
        Debug.Log("Aie!");
        Application.Quit();
    }

}
