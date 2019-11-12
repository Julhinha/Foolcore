using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoCurupira : MonoBehaviour
{
    private Animator ControlAnimJogador;
    private Rigidbody2D corpoRigidoJogador;
    public GameObject CurupiraGrafico;

    private float velocidademodulo = 2.0F;
    private float forcaModulo = 4.0f;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;

    private bool estanoChao = false;
    private bool pular = false;




    void Start()
    {
        ControlAnimJogador = CurupiraGrafico.GetComponent<Animator>();
        corpoRigidoJogador = GetComponent<Rigidbody2D>();

        //Idle

        ControlAnimJogador.SetBool("bAndar", false);
        ControlAnimJogador.SetBool("bPular", false);

    }


    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (horizontal > 0.0f && estanoChao == true)
        {
            corpoRigidoJogador.velocity = new Vector2(10.0f, 0.0f) * velocidademodulo;
            CurupiraGrafico.GetComponent<SpriteRenderer>().flipX = false;

            //Andando
            ControlAnimJogador.SetBool("bAndar", true);
            ControlAnimJogador.SetBool("bPular", false);

        }
        else if (horizontal < 0.0f && estanoChao == true)
        {
            corpoRigidoJogador.velocity = new Vector2(-10.0f, 0.0f) * velocidademodulo;
            CurupiraGrafico.GetComponent<SpriteRenderer>().flipX = true;

            //Andando
            ControlAnimJogador.SetBool("bAndar", true);
            ControlAnimJogador.SetBool("bPular", false);
        }
        else if (estanoChao == true)
        {
            corpoRigidoJogador.velocity = new Vector2(0.0f, 0.0f);

            //Idle
            ControlAnimJogador.SetBool("bAndar", false);
            ControlAnimJogador.SetBool("bPular", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && estanoChao == true)
        {
            estanoChao = false;
            pular = true;

            //Pulando

            ControlAnimJogador.SetBool("bAndar", false);
            ControlAnimJogador.SetBool("bPular", true);
        }


    }

    void FixedUpdate()
    {
        if (pular == true)
        {
            corpoRigidoJogador.AddForce(new Vector2(0.0f, 3.0f) * forcaModulo, ForceMode2D.Impulse);
            pular = false;
        }
    }

    void OnCollisionEnter2D(Collision2D infoColisao)
    {
        if (infoColisao.gameObject.name == "Chao")
        {
            estanoChao = true;
        }
    }
}
