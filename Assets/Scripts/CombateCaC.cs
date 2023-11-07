using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    
    public Transform meleeController;
    
    public float radioMelee;

    public float meleeDamage;
    [SerializeField] private float timeNextAttack;
    [SerializeField] private float timeBetweenAttack;


    private void Update(){

     if(timeNextAttack > 0){
            timeNextAttack -= Time.deltaTime;
        }
        

    if (Input.GetKeyDown(KeyCode.Space) && timeNextAttack <= 0)
     {
        Melee();
        timeNextAttack = timeBetweenAttack;
     }
     }
    
    private void Melee()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(meleeController.position,radioMelee);

        foreach (Collider2D collider in objetos)
        {
            if (collider.CompareTag("Enemigo"))
            {
                collider.transform.GetComponent<DemonioScript>().TakeDamage(meleeDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeController.position,radioMelee);
    }
}
