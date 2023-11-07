using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroeScript : MonoBehaviour
{
    public float JumpForce = 4.0f; // Ajusta la fuerza del salto
    private Rigidbody2D Rigidbody2D;
    public Vector2 speed = new Vector2(6, 2);
    public Animator anim;
    public Transform meleeController;
    [SerializeField] private float health;
    

    void Start()
    {

        Rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bool isJumping = Mathf.Abs(Rigidbody2D.velocity.y) > 0.01f; 
        float inputX = Input.GetAxis("Horizontal");
        anim.SetFloat("yVelocity", Rigidbody2D.velocity.y);
        anim.SetBool("jumping",isJumping);

    

        

       if (Input.GetKeyDown(KeyCode.Space))
    {
        
        anim.SetBool("attacking", true);
        
    }
     else
    {
        anim.SetBool("attacking", false);
    }
        if (Input.GetKeyDown(KeyCode.Space) && isJumping)
    {
        
        anim.SetBool("airAttacking", true);
    }
    else
    {
        
        anim.SetBool("airAttacking", false);
    }
        
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


     public void TakeDamage(float damage)
    {
        health -= damage;
        anim.SetBool("hurting", true);
         StartCoroutine(DisableHurting());

        if (health <= 0)
        {
            Death();

        }

    
    }

    private void Death(){
        Destroy(gameObject);

    }

    private IEnumerator DisableHurting()
{
    // sespera 0.5 segundos para desactivar el booleano "hurting"
    yield return new WaitForSeconds(0.5f);

    anim.SetBool("hurting", false);
}

}


