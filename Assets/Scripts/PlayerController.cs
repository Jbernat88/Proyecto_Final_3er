using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    //Movimiento
    public float speed = 20f;
    public float turnspeed = 40f;
    private float horizontalInput;
    private float verticalInput;
    public float jumpForce = 200f;
    public float downForce = 10000f;

    //Doble Salto
    public bool isOnGround;
    public bool lookRight = true;
    public bool lookLeft;
    public bool doubleJump = true;   
    private bool IsCoolDown = true;
    public float jumpSpeed = 0.5f;//Lo que tarda en poder volver a saltar

    //Disparo
    public GameObject proyectil;
    private bool IsCoolDownShot = true;
    public float shootSpeed = 4f;

    //Vida
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;


    //Municio
    public int rounds;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        //Max Health
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }


    void Update()
    {
        Debug.Log(currentHealth);
        Debug.Log(rounds);
        Debug.Log("Salto" +doubleJump);
        Debug.Log("Ground"+isOnGround);
        // Usamos los inputs del Input Manager
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Mueve el player hacia delante y atras. 
        //transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);


        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput, Space.World);

        
        


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isOnGround == true && doubleJump == true)
            {               
                playerRigidbody.AddForce(Vector3.up * jumpForce);
                isOnGround = false;
                doubleJump = true;
                speed = 5f;
                StartCoroutine(CoolDown());
            }
            else if (doubleJump == true)
            {
                playerRigidbody.AddForce(Vector3.up * jumpForce);
                doubleJump = false;
            }            

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isOnGround == false)
            {
                playerRigidbody.AddForce(Vector3.down * downForce);                                             
            }

        }

        if ( horizontalInput < 0 && lookRight==true)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            lookRight = false;
            lookLeft = true;
        }

        if (horizontalInput > 0 && lookLeft == true)
        {

            transform.Rotate(new Vector3(0, 180, 0));
            lookLeft = false;
            lookRight = true;
        }


        //float horizontalInput = Input.GetAxis("Horizontal");
        //playerRigidbody.AddTorque(Vector3.up * turnspeed * horizontalInput);

        //Debug.Log(speed * verticalInput);


        //Disparo

        if (Input.GetKey(KeyCode.Mouse0) && IsCoolDownShot && rounds>0/*&& !GameOver*/)
        {
            Instantiate(proyectil, transform.position, transform.rotation);

            StartCoroutine(CoolDownShot());

            rounds--;

            TakeDamage(10);

            //soundManager.SelecionAudio(0, 0.2f);
        }

        //Health
        if(currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        

        if (otherCollider.gameObject.CompareTag("Money")) //Moneda
        {
            Destroy(otherCollider.gameObject);
            rounds++;
            HealDamage(10);

        }
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
            doubleJump = true;

            speed = 7f;
        }
    }

    private IEnumerator CoolDown() //Cool Down del salto
    {
        IsCoolDown = false;
        yield return new WaitForSeconds(jumpSpeed);
        IsCoolDown = true;
        
    }

    private IEnumerator CoolDownShot() //Cool Down del disparo
    {
        IsCoolDownShot = false;
        yield return new WaitForSeconds(shootSpeed);
        IsCoolDownShot = true;
    }

    //TakeDamage
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void HealDamage(int damage)
    {
        currentHealth += damage;
        healthBar.SetHealth(currentHealth);
    }

}


