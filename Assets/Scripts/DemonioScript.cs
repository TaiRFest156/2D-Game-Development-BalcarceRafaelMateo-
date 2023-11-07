using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonioScript : MonoBehaviour
{
    public Transform Heroe; // se asigna el objeto del jugador al que el demonio perseguira
    public float velocidad = 1.0f; // velocidad de seguimiento del enemigo al jugador
    public float distanciaMinima = 2.0f; // distancia minima a mantener entre el Demonio y el jugador
    public float distanciaAtaque = 2.0f; // el rango de ataque del Demonio
    public Animator anim;
    [SerializeField] private float health;
    public Transform fireController;
    public float radioFire;
    public float fireDamage;
    [SerializeField] private float timeNextAttack;
    [SerializeField] private float timeBetweenAttack;


      void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if(timeNextAttack > 0){
            timeNextAttack -= Time.deltaTime;
        }
        
            Vector2 targetPosition = new Vector2(Heroe.position.x, Heroe.position.y);
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            
            float distanciaAlJugador = Vector2.Distance(currentPosition, targetPosition);
             if (distanciaAlJugador > distanciaMinima)
             {
            // mueve al Demonio hacia el jugador
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, velocidad * Time.deltaTime);
             }
             if (distanciaAlJugador <= distanciaAtaque && timeNextAttack <= 0)
            {
            anim.SetBool("firing", true);
            Fire();
    
            timeNextAttack = timeBetweenAttack;
            }
            else
            {
            anim.SetBool("firing", false);

            }
       
        
    

        Vector3 direction = Heroe.transform.position - transform.position;
        if (direction.x > 0.0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
           
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            
         }
        
    }
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();

        }

    
    }


     private void Fire()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(fireController.position,radioFire);

        foreach (Collider2D collider in objetos)
        {
            if (collider.CompareTag("Player"))
            {
                collider.transform.GetComponent<HeroeScript>().TakeDamage(fireDamage);
            }
        }
    }

      private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(fireController.position,radioFire);
    }


    private void Death(){
        Destroy(gameObject);

    }

}


