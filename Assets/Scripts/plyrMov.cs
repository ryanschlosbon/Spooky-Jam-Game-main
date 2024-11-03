using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class plyrMov : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Rigidbody")]
    public Rigidbody2D theRB;
    public Rigidbody2D theRB2;

    [Header("Editable-Player-Stuff")]
    public float plyrMovSpd;
    public float accelRate;
    public float plyrAccel;
    public float accelMax;
    public float airJumpAssist;
    public float jumpForce;
    public float keyReleaseOffset;
    public float jumpTimeCounter;
    public float maxJumpTime;
    public float fallForce;
    public float distanceToWall;
    float horizontalInput;
    float verticalInput;
    public int atBase;

    [Header("Grounded-Stuff")]
    public float fastFallGravityScale;
    public float normalGravityScale;
    public bool isGrounded;
    public bool canFastFall;
    public bool canJump;
    public bool isJumping;
    public Transform groundCheck;
    public Transform playerSprite;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    [Header("Line Stuff")]
    public bool canSlide;
    public LayerMask iceLayer;
    public LineRenderer lineRenderer;
    public int currentPointIndex = 0;
    public int iceSpeed;
    public float iceCheckRadius;
    public float iceAccelRate, iceAccel;
    public bool canRegenMeter;

    [Header("Game over stuff")]
    public GameObject gameOverUI;
    public bool gameOver;
    public bool gameWon;
    public GameObject winUI;

    void Start()
    {
        theRB.freezeRotation = true;
        iceAccel = 0;
        plyrAccel = 1f;
        atBase = 0;
        normalGravityScale = theRB.gravityScale;

        if (winUI != null)
        {
            winUI.SetActive(false);
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Setup Stuff.
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        theRB.velocity = new Vector2(horizontalInput * (plyrMovSpd * plyrAccel), theRB.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        canSlide = Physics2D.OverlapCircle(playerSprite.position, iceCheckRadius, iceLayer);
    
        if (canSlide)
        {
            iceAccel = iceAccelRate;
        }
        if (!canSlide)
        {
            iceAccel = 0;
        }

        if (gameOver || gameWon)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TriggerGameRestart();
            }
        }

        if (horizontalInput > 0 && plyrAccel < accelMax)
        {
            plyrAccel += (accelRate + iceAccel) * Time.deltaTime;
        }
        if (horizontalInput <=0 && plyrAccel >= 1f)
        {
            plyrAccel -= (accelRate + iceAccel) * Time.deltaTime;
        }

        if ((isGrounded || canSlide))
        {
            canFastFall = false;
            canJump = true;
        }
        else
        {
            canFastFall = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            isJumping = true;
            Debug.Log("This is working");
            Jump();
            canJump = false;
            isJumping = false;
        }
        else
        {
            theRB.gravityScale = normalGravityScale;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopJumpMomentum();
        }

        if (verticalInput < 0 && canFastFall && !isJumping)
        {
            FastFall();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("death"))
        {
            TriggerGameOver();
        }

        if (other.CompareTag("win"))
        {
            TriggerGameWin();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("crystal"))
        {
            Debug.Log("They're in da zone!");
            canRegenMeter = true;
        }
        else
        {
            canRegenMeter = false;
        }
    }

    public void FastFall()
    {
        Debug.Log("Fall!");
        theRB.gravityScale = fastFallGravityScale;
    }

    public void StopJumpMomentum()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y - keyReleaseOffset);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y + jumpForce);
        }
        else if (!isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y + (jumpForce+airJumpAssist));
        }

        canJump = false;
    }

    public void IceJump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y + (jumpForce * plyrAccel));
    }

    public int GetClosestIndex()
    {
        int closestIndex = -1;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector2 pointPosition = lineRenderer.GetPosition(i);
            float distance = Vector2.Distance(playerSprite.position, pointPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        Debug.Log("Closest index is " + closestIndex);

        return closestIndex;
    }

    public void TriggerGameWin()
    {
        if (!gameWon)
        {
            gameWon = true;
            Time.timeScale = 0f;
            winUI.SetActive(true);
        }
    }

    public void TriggerGameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);

        }
    }

    public void TriggerGameRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
