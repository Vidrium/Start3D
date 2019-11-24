using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Déclaration de la variable de vitesse
    public bool isGrounded;
    public float m_speed = 0.1f;
    public float j_speed = 10.0f;
    public Animator animator;
    private float canJump = 0f;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (col.gameObject.tag == "Death")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Création d'un nouveau vecteur de déplacement
        Vector3 move = new Vector3();

        if (Input.GetKey(KeyCode.Space) && Time.time > canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    rb.AddForce(new Vector3(0, j_speed, 0), ForceMode.Impulse);
                    isGrounded = false;
                    canJump = Time.time + 0.5f;
                }

            }
        }

        // Récupération des touches haut et bas
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("isWalking", true);
            move.z += m_speed;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        if (Input.GetKey(KeyCode.DownArrow))
            move.z -= m_speed;

        // Récupération des touches gauche et droite
        if (Input.GetKey(KeyCode.LeftArrow))
            move.x -= m_speed;
        if (Input.GetKey(KeyCode.RightArrow))
            move.x += m_speed;

        // On applique le mouvement à l'objet
        transform.position += move;
    }
}