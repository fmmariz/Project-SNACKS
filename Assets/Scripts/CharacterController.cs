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

    private List<ResetListeners> _resetListeners;

    [SerializeField]
    public LayerMask floorLayer;

    private enum ActiveSnack
    {
        POWER,
        FLIGHT,
        SHOOT
    }
    private List<ActiveSnack> _activeSnacks;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        GameController.Instance.resetController.SetResetPosition(gameObject.transform.position);
        _resetListeners = new List<ResetListeners>();
        _activeSnacks = new List<ActiveSnack>();
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
            else if (_resetTimer > 0)
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

    void Movement(float horizontalMovement)
    {
        Vector2 newVelocity = new Vector2(horizontalMovement * playerSpeed, 0);
        rb.velocity += newVelocity;
    }

    public void Reset()
    {
        rb.velocity = Vector3.zero;
        gameObject.transform.position = GameController.Instance.resetController.GetResetPosition();
        _resetTimer = 5f;
        foreach (ResetListeners rl in _resetListeners)
        {
            rl.OnReset();
        }
    }

    public void AddResetListeners(ResetListeners resetListeners)
    {
        _resetListeners.Add(resetListeners);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("portal"))
        {
            if (GameController.Instance.GetCurrentGameState() != GameController.GameState.VICTORY)
            {
                GameController.Instance.soundControl.PlaySoundEffect("win");

                ShowWinScreen();
            }
        }else if (other.gameObject.CompareTag("floor"))
        {
            GroundRaycastJumpCheck();
        }else if (other.gameObject.CompareTag("obstacle"))
        {
            DestroyObstacle(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("snack"))
        {
            GameController.Instance.soundControl.PlaySoundEffect("powerup");
            GameController.Instance.uiController.ShowMessage("Power UP !!", "Break obstaclesw with ease!", 3f);
            GetSnack(other.gameObject);
        }
    }

    void GroundRaycastJumpCheck()
    {
        RaycastHit hit;
        SpriteRenderer _sr = spriteRenderer;
        Vector3 bottomPosition = transform.position;
        bottomPosition.y = _sr.bounds.min.y;
        float distance = (_sr.bounds.max.y - _sr.bounds.min.y)/ 5f;
        Debug.Log($"{ distance}");
        Debug.DrawRay( transform.position, Vector3.down * distance, Color.magenta, 30f,false);

        if (Physics2D.Raycast(bottomPosition, Vector3.down, distance, floorLayer))
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void GetSnack(GameObject snackObject)
    {
        Destroy(snackObject);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
        _activeSnacks.Add(ActiveSnack.POWER);
    }

    private void DestroyObstacle(GameObject obstacleObject)
    {
        if (_activeSnacks.Contains(ActiveSnack.POWER))
        {
            GameController.Instance.soundControl.PlaySoundEffect("destroy");
            Instantiate(death, transform.position, transform.rotation);
            Destroy(obstacleObject);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void ShowWinScreen()
    {
        GameController.Instance.GetUIController().ShowWinningMessage();
        GameController.Instance.SetCurrentGameState(GameController.GameState.VICTORY);
    }

    public void SetDead(bool dead)
    {
        GetComponent<BoxCollider2D>().enabled = !dead;
    }
}
