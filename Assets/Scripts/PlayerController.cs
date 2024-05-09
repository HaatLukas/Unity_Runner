using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool jumped; // Czy ktoœ skoczy³?
    public bool doublejumped; // Czy ktoœ skoczy³ dwukrotnie?

    public float jumpForce; // Si³a skoku
    public float liftingForce; // Si³a unoszenia

    public LayerMask whatIsGround; // Co uznajemy za ziemiê?

    private Rigidbody2D rb; // Nasze Rigidbody (Fizyka)
    private BoxCollider2D collision; // Nasze pude³ko z kolizj¹
    private float timestamp; // Piecz¹tka czasowa

    private InputMaster Input; // Nasze sterowanie


    private void Awake()
    {
        Input = new InputMaster();
        Input.Player.Jump.performed += context => Jump(); //Podpiêcie naszego
        // skoku do akcji Jump
    }
    private void OnEnable()
    {
        Input.Enable(); // W³¹cz sterowanie
    }
    private void OnDisable()
    {
        Input.Disable(); // Wy³¹cz sterowanie
    }

    private void Jump()
    {
        if (jumped == false)
        {
            SoundManager.instance.PlayOnceJumpSound();
            rb.velocity = (new Vector2(0f, jumpForce));
            jumped = true;

        }
        else if ( doublejumped == false )
        {
            SoundManager.instance.PlayOnceJumpSound();
            rb.velocity = (new Vector2(0f, jumpForce));
            doublejumped = true;
        }

        if (rb.velocity.y < 0)
        {
            rb.AddForce(new Vector2(0f, liftingForce * Time.deltaTime));
        }
    }


    void Start()
    {
        // Na starcie wgrywamy rb i kolizje od elementu do którego jesteœmy
        // przyczepieni
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
    }
    void Update()
    {

        if (GameManager.settings.inGame == false) { return; }

        if (rb.velocity.y < 0)
        {
            rb.AddForce(new Vector2(0f, liftingForce * Time.deltaTime));
        }

        if (IsGrounded() && Time.time >= timestamp)
        {
            if (jumped || doublejumped)
            {
                jumped = false;
                doublejumped = false;
            }
            timestamp = Time.time + 1f;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collision.bounds.center,
            collision.bounds.size, 0, Vector2.down, 0.1f, whatIsGround);
        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Je¿eli element, w który uderzyliœmy ma nalepkê przeszkoda I nie jesteœmy nieœmiertelni
        //GameManager.settings.immortality.isActive == false
        if (collision.CompareTag("Obstacle") && !GameManager.settings.immortality.isActive)
        {
            PlayerDeath();
        }
        // && - AND
        // || - LUB

        else if (collision.CompareTag("Coin"))
        {
            GameManager.settings.CollectCoins(1);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Immortality"))
        {
            GameManager.settings.ImmortalityCollected();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Magnet"))
        {

            Destroy (collision.gameObject);
        }

    
    }
    void PlayerDeath()
    { 
        // Nasza postaæ ma kompletnie siê zatrzymaæ
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.settings.GameOver();
    }



}
