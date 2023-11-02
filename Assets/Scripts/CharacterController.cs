using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] 
    private float playerSpeed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.GetCurrentGameState() == GameController.GameState.PLAYING)
        {
            Movement(Input.GetAxis("Horizontal"));
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    void Movement(float horizontalMovement){
        Vector2 newVelocity = new Vector2(horizontalMovement, 0);
        GetComponent<Rigidbody2D>().velocity = newVelocity * playerSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("snack")){
            GetSnack(other.gameObject);
        }
        if(other.gameObject.CompareTag("obstacle")){
            DestroyObstacle(other.gameObject);
        }
        if(other.gameObject.CompareTag("portal")){
            ShowWinScreen();
        }
    }

    private void GetSnack(GameObject snackObject)
    {
        Destroy(snackObject);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
    }

    private void DestroyObstacle(GameObject obstacleObject)
    {
        Destroy(obstacleObject);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    private void ShowWinScreen()
    {
        GameController.Instance.GetUIController().ShowWinningMessage();
        GameController.Instance.SetCurrentGameState(GameController.GameState.VICTORY);
    }
}
