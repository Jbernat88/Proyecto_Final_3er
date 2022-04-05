using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 20f;
    public float turnspeed = 40f;
    private float horizontalInput;
    private float verticalInput;
    public float jumpForce = 400f;

    public bool isOnGround;

    private Rigidbody playerRigidbody;

    private bool IsCoolDown = true;
    public float jumpSpeed = 0.5f;//Lo que tarda en poder volver a saltar


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

    }


    void Update()
    {
        // Usamos los inputs del Input Manager
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Mueve el player hacia delante y atras. 
        //transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);


        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);

        if (isOnGround == true)
        {
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidbody.AddForce(Vector3.up * jumpForce);
                isOnGround = false;

                speed = 10f;

                StartCoroutine(CoolDown());
            }
        }
         
        //float horizontalInput = Input.GetAxis("Horizontal");
        //playerRigidbody.AddTorque(Vector3.up * turnspeed * horizontalInput);

        //Debug.Log(speed * verticalInput);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        

        if (otherCollider.gameObject.CompareTag("Money")) //Moneda
        {
            Destroy(otherCollider.gameObject);
        }
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("ground"))
        {
            isOnGround = true;

            speed = 20f;
        }
    }

    private IEnumerator CoolDown() //Cool Down del salto
    {
        IsCoolDown = false;
        yield return new WaitForSeconds(jumpSpeed);
        IsCoolDown = true;
        
    }

}


