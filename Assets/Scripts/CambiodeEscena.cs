using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    public float tiempoParaCambiar = 60.0f; 
    public string nombreDeLaEscena = "Escenario2"; 

    void Start()
    {
        // Establece un temporizador para el cambio de escena
        StartCoroutine(CambiarEscenaDespuesDeTiempo(tiempoParaCambiar));
    }

    IEnumerator CambiarEscenaDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        
        // Cambia a la segunda escena
        SceneManager.LoadScene(nombreDeLaEscena);
    }
}
