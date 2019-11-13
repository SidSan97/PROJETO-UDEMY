using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Animator MyAnimator;
    SpriteRenderer MySprite;
    Rigidbody2D body;
    public float maxSpeed = 5f;
    public float maxJump = 10f;
    public bool ground;
    private int direcao;


    // Use this for initialization
    void Start ()
    {
        body = this.GetComponent<Rigidbody2D>(); //traz o componente de rigidBody2D
        MySprite = this.GetComponent<SpriteRenderer>();// traz o spriteRenderer
        MyAnimator = this.GetComponent<Animator>();
	}


    // Update is called once per frame
    void Update ()
    {

        float move = Input.GetAxis("Horizontal"); //recebe o input
        if (move < 0)
        {
            direcao = -1;
        }
        else if (move > 0)
        {
            direcao = 1;
        }

       if (Input.GetButtonDown("Jump") && ground == true){ // detecta pulo
            body.AddForce(Vector2.up * maxJump, ForceMode2D.Impulse);// adiciona força vertical no rigidBody
                ground = false;
                MyAnimator.SetBool("grounded", ground);
        }

        body.velocity = new Vector2(move * maxSpeed, body.velocity.y); // adiciona velocidade ao rigidbody2D

        if (direcao == -1 && MySprite != null)
        {
            MySprite.flipX = true;
        }
        else if (direcao == 1)
        {
            MySprite.flipX = false;
        }

        MyAnimator.SetFloat("velocityX", body.velocity.x);

	}

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Ground")
        {
            ground = true;
            MyAnimator.SetBool("grounded", ground);
        }
        else
        {
            ground = false;
        }
    }
}
