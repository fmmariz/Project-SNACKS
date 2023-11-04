using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState{
        PLAYING,
        VICTORY
    }
    public static GameController Instance { get; private set; }
    public static UIController uiController;

    [SerializeField]
    public GameObject stage;

    private GameState _currentState; 

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

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
        _currentState = GameState.PLAYING;

        stage.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;


    }

    // Update is called once per frame
    void Update()
    {
        
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
}
