using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private float health;
    public float speed = 5.0f;  // Velocidad de movimiento del boss
    private bool movingRight = true;  // Variable para controlar la direccion del movimiento
    private SpriteRenderer spriteRenderer;  
    public float damage = 2.0f; // Dano infligido por el jefe
    public float cooldown = 10.0f; // Tiempo de enfriamiento en segundos
    private float lastAttackTime; // Tiempo del ultimo ataque
    private bool canAttack = true; // Variable para controlar el ataque

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // Obtiene el componente SpriteRenderer
        StartCoroutine(ChangeDirectionRoutine());
        lastAttackTime = Time.time;
    }

    private void Update()
    {
        // Calcula la direccion en la que debe moverse
        Vector2 moveDirection = movingRight ? Vector2.right : Vector2.left;

        // Aplica el movimiento con el delta time
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Voltea el sprite del boss en la direccion del movimiento
        if (moveDirection.x > 0.0f)
        {
            spriteRenderer.flipX = true;  // No voltear
        }
        else if (moveDirection.x < 0.0f)
        {
            spriteRenderer.flipX = false;  // Voltear
        }

        if (canAttack && Time.time - lastAttackTime >= cooldown)
        {
            canAttack = false;
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canAttack = false;
        }
    }

    private void Attack()
    {
        HeroeScript hero = FindObjectOfType<HeroeScript>(); 
        if (hero != null)
        {
            hero.TakeDamage(damage);
        }
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            // Espera 6 segundos
            yield return new WaitForSeconds(6.0f);

            // Cambia de direccion
            movingRight = !movingRight;
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

    private void Death()
    {
        Destroy(gameObject);
    }
}
