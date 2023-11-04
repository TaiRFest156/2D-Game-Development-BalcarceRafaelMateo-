using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroeMovement : MonoBehaviour
{
    public float JumpForce = 4.0f; // Ajusta la fuerza del salto
    private Rigidbody2D Rigidbody2D;
    public Vector2 speed = new Vector2(2, 2);
    public Animator anim;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        Vector3 movement = new Vector3(speed.x * inputX, Rigidbody2D.velocity.y, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);

        if (inputX < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (inputX > 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        anim.SetBool("running", Mathf.Abs(inputX) > 0.0f);
    }

    private void Jump()
    {
        if (Mathf.Abs(Rigidbody2D.velocity.y) < 0.01f)
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
}
