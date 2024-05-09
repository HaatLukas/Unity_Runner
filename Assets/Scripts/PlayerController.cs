using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool jumped; // Czy kto� skoczy�?
    public bool doublejumped; // Czy kto� skoczy� dwukrotnie?

    public float jumpForce; // Si�a skoku
    public float liftingForce; // Si�a unoszenia

    public LayerMask whatIsGround; // Co uznajemy za ziemi�?

    private Rigidbody2D rb; // Nasze Rigidbody (Fizyka)
    private BoxCollider2D collision; // Nasze pude�ko z kolizj�
    private float timestamp; // Piecz�tka czasowa

    private InputMaster Input; // Nasze sterowanie


    private void Awake()
    {
        Input = new InputMaster();
        Input.Player.Jump.performed += context => Jump(); //Podpi�cie naszego
        // skoku do akcji Jump
    }
    private void OnEnable()
    {
        Input.Enable(); // W��cz sterowanie
    }
    private void OnDisable()
    {
        Input.Disable(); // Wy��cz sterowanie
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
        // Na starcie wgrywamy rb i kolizje od elementu do kt�rego jeste�my
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
        // Je�eli element, w kt�ry uderzyli�my ma nalepk� przeszkoda I nie jeste�my nie�miertelni
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
        // Nasza posta� ma kompletnie si� zatrzyma�
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.settings.GameOver();
    }



}
