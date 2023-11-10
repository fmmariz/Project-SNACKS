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
            child.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

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
