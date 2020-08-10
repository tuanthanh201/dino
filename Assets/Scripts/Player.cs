using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text currentScore;
    public Text highScore;
    int score = 0;

    public float jumpPower = 1000f;
    public float antiJumpPower = 8000f;
    Rigidbody2D myRigidbody;
    bool isGround = false;
    bool isGameOver = false;
    public float xPos = -7.6f;
    public Animator anim;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        highScore.text = PlayerPrefs.GetInt("highScore", 0).ToString();
    }

    void FixedUpdate()
    {
        if (!isGameOver)
        {
            score++;
            currentScore.text = score.ToString();
        }

        if (score > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", score);
            highScore.text = PlayerPrefs.GetInt("highScore").ToString();
        }

        if (isGameOver)
        {
            return;
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && isGround)
        {
            myRigidbody.AddForce(Vector3.up * jumpPower * Time.deltaTime * myRigidbody.mass * myRigidbody.gravityScale);
            anim.SetBool("Jump", true);

            if (Input.GetKey(KeyCode.DownArrow))
            {
                myRigidbody.AddForce(Vector3.down * jumpPower * Time.deltaTime * myRigidbody.mass * myRigidbody.gravityScale);
            }
        }

        else
        {
            anim.SetBool("Jump", false);
        }

        if (Input.GetKey(KeyCode.DownArrow) && isGround)
        {
            anim.SetBool("Duck", true);
        }

        else
        {
            anim.SetBool("Duck", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
        }

        if (collision.collider.tag == "Challenge")
        {
            GameOver();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void GameOver()
    {
        myRigidbody.gravityScale = 0f;
        isGameOver = true;
        anim.SetBool("GameOver", true);
        FindObjectOfType<ChallengeScroller>().GameOver();
        FindObjectOfType<ScrollGround>().xVel = 0f;
        FindObjectOfType<ScrollClouds>().xVel = 0f;
        FindObjectOfType<Score>().GameOver();
        myRigidbody.AddForce(Vector3.down * antiJumpPower * Time.deltaTime * myRigidbody.mass);
    }
}
