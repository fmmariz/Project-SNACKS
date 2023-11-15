using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CharController : MonoBehaviour
{
    [SerializeField] 
    private float playerSpeed = 5.0f, jumpForce = 100.0f, resetCooldown = 5f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public float minX, maxX, minY, maxY;
    public GameObject death;

   
    private float _resetTimer;

    public Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        GameController.Instance.resetController.SetResetPosition(gameObject.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (GameController.Instance.GetCurrentGameState() == GameController.GameState.PLAYING)
        {
            MovementMethod();

            if (Input.GetKey(KeyCode.R) && _resetTimer <= 0)
            {
                Reset();
            }
            else if(_resetTimer > 0) 
            {
                _resetTimer -= Time.deltaTime;
                if (_resetTimer < 0.1f) _resetTimer = 0;
                GameController.Instance.uiController.UpdateResetTimer(_resetTimer / resetCooldown);
            }
        }
        else 
        {
            rb.velocity = Vector3.zero;
            animator.SetBool("isWalking", false);
            animator.SetBool("isJumping", false);
        }

        //float newX, newY;
        
        //newX = Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, minX, maxX);
        //newY = Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, minY, maxY);

        //GetComponent<Rigidbody2D>().position = new Vector2(newX, newY);

    }

    private void MovementMethod()
    {
        float moveDir = Input.GetAxisRaw("Horizontal");
        Movement(moveDir);

        if (moveDir != 0)
        {
            spriteRenderer.flipX = moveDir < 0;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetAxis("Vertical") > 0 && !animator.GetBool("isJumping"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isWalking", false);
            animator.SetBool("isJumping", true);
            GameController.Instance.soundControl.PlaySoundEffect("jump");
        }
    }

    void Movement(float horizontalMovement){
        Vector2 newVelocity = new Vector2(horizontalMovement * playerSpeed, 0);
        rb.velocity += newVelocity;
    }

    public void Reset()
    {
        gameObject.transform.position = GameController.Instance.resetController.GetResetPosition();
        _resetTimer = 5f;
    }


    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("portal")){
            if (GameController.Instance.GetCurrentGameState() != GameController.GameState.VICTORY)
            {
                GameController.Instance.soundControl.PlaySoundEffect("win");

                ShowWinScreen();
            }
            }
            if (other.gameObject.CompareTag("floor"))
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {
            GameController.Instance.soundControl.PlaySoundEffect("destroy");

            DestroyObstacle(other.gameObject);
        }else if(other.gameObject.CompareTag("snack"))
        {
            GameController.Instance.soundControl.PlaySoundEffect("powerup");
            GetSnack(other.gameObject);
        }
    }

    private void GetSnack(GameObject snackObject)
    {
        Destroy(snackObject);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
    }

    private void DestroyObstacle(GameObject obstacleObject)
    {
        if (gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 0f, 0f, 1f))
        {
            Instantiate(death, transform.position, transform.rotation);
            Destroy(obstacleObject);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            obstacleObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void ShowWinScreen()
    {
        GameController.Instance.GetUIController().ShowWinningMessage();
        GameController.Instance.SetCurrentGameState(GameController.GameState.VICTORY);
    }
}
