using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    public Text countText;
    public Text winText;
    private int count;
    public GameObject Player, Door;
    public GameObject heart1, heart2, heart3, gameOver;
    public static int health;
    public Animator animator;

    public UnityEvent OnLandEvent;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        winText.text = "";
        SetCountText();

        health = 3;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
    }

    void Update()
    {

        
     
        if (Input.GetKey("escape"))
            Application.Quit();

        if (health > 3)
            health = 3;

        switch (health)
        {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                Time.timeScale = 0;
                gameOver.gameObject.SetActive(true);
                Player.gameObject.SetActive(false);
                
                break;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("IsJumping", false);

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if (moveHorizontal > 0f)
        {
            transform.localScale = new Vector2(.25f, .25f);
        }
        else if (moveHorizontal < 0f)
        {
            transform.localScale = new Vector2(-.25f, .25f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            animator.SetBool("IsJumping", false);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetBool("IsJumping", true);
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 4)
        {
            Door.gameObject.SetActive(false);
        }
        if (count >= 8)
        {
            winText.text = "You win!";
        }
    }
}
