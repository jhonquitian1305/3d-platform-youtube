using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;

    public float rotateSpeed = 5f;

    private Vector3 moveDirection;

    public CharacterController charController;
    
    //Acceder a la cámara
    public Camera playerCamera;
    
    //Para acceder al jugador
    public GameObject playerModel;

    public Animator animator;

    //Knockback(reacción cuando golpea un obstáculo)
    public bool isKnocking;
    public float knockbackLength = 0.5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;

    public GameObject[] playerPieces;
    
    public void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isKnocking)
        {
            float yStore = moveDirection.y;

            // Movimiento
            //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) +
                            (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection
                .Normalize(); //Normalizar el movimiento, cuando se mueva en diagonal no sume velocidad de los dos ejes
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }

            //Aplicando gravedad
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            //Para rotar al jugador cuando se gira la cámara y este se mueve
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, playerCamera.transform.rotation.eulerAngles.y, 0f);

                //Jugador rote de una forma suave hacia izquierda o derecha y adelante o atrás
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation,
                    rotateSpeed * Time.deltaTime);
            }

            //Animando al player
            animator.SetFloat("speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));

            //Animando el salto del player
            animator.SetBool("grounded", charController.isGrounded);
        }

        if (isKnocking)
        {
            knockbackCounter -= Time.deltaTime;
            
            float yStore = moveDirection.y;

            // Movimiento
            //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveDirection = (playerModel.transform.forward * knockbackPower.x);
            moveDirection.y = yStore;
            
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);
            
            if (knockbackCounter <= 0)
            {
                isKnocking = false;
            }
        }
    }

    public void Knockback()
    {
        isKnocking = true;
        knockbackCounter = knockbackLength;
        Debug.Log("knockback");
        moveDirection.y = knockbackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }
}
