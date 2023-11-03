using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.XR;

public class CharController : MonoBehaviour
{
    [SerializeField] 
    private float playerSpeed = 5.0f, jumpForce = 15.0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public float minX, maxX, minY, maxY;

    public Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (GameController.Instance.GetCurrentGameState() == GameController.GameState.PLAYING)
        {
            Movement(Input.GetAxis("Horizontal"));

            if ((Input.GetKey(KeyCode.RightArrow)))
            {
                spriteRenderer.flipX = false;
                animator.SetBool("isWalking", true);

            }
            else if ((Input.GetKey(KeyCode.D)))
            {
                spriteRenderer.flipX = false;
                animator.SetBool("isWalking", true);

            }
            else if ((Input.GetKey(KeyCode.LeftArrow)))
            {
                spriteRenderer.flipX = true;
                animator.SetBool("isWalking", true);

            }
            else if ((Input.GetKey(KeyCode.A)))
            {
                spriteRenderer.flipX = true;
                animator.SetBool("isWalking", true);

            }
            else if ((Input.GetKey(KeyCode.Space)))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", true);

            }
            else if ((Input.GetKey(KeyCode.W)))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", true);

            }
            else
            {
                animator.SetBool("isWalking", false);

            }


        }

        else 
        {
            
            rb.velocity = Vector3.zero;
            animator.SetBool("isWalking", false);
            animator.SetBool("isJumping", false);

        }
        float newX, newY;

        newX = Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, minX, maxX);
        newY = Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, minY, maxY);

        GetComponent<Rigidbody2D>().position = new Vector2(newX, newY);

    }

    void Movement(float horizontalMovement){
        Vector2 newVelocity = new Vector2(horizontalMovement, 0);
        rb.velocity = newVelocity * playerSpeed;
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
        if (other.gameObject.CompareTag("floor"))
        {
            animator.SetBool("isJumping", false);
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
