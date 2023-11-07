using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonioSpawner : MonoBehaviour
{
    public GameObject demonioPrefab;
    public float tiempoEntreApariciones = 10.0f;
    private float tiempoParaSiguienteAparicion = 0.0f;
    private Vector3 spawnPoint; 
    public GameObject heroe;


    void Start()
    {
        tiempoParaSiguienteAparicion = tiempoEntreApariciones;
        // encuentra el spawn point aleatoriamente entre la izquierda y la derecha de la camara
        spawnPoint = Random.Range(0, 2) == 0 ? GetSpawnPointLeft() : GetSpawnPointRight();
        heroe = GameObject.FindWithTag("Player");
        DemonioScript demonioScript = demonioPrefab.GetComponent<DemonioScript>();
        demonioScript.heroe = heroe.transform;
        
    }

    void Update()
    {
        if (tiempoParaSiguienteAparicion <= 0.0f)
        {
            SpawnDemonio();
            tiempoParaSiguienteAparicion = tiempoEntreApariciones;
            // cambia el spawn point para el siguiente demonio
            spawnPoint = Random.Range(0, 2) == 0 ? GetSpawnPointLeft() : GetSpawnPointRight();
        }
        else
        {
            tiempoParaSiguienteAparicion -= Time.deltaTime;
        }
    }

    void SpawnDemonio()
    {
        // se instancia un nuevo demonio en la posicion de spawnPoint
        Instantiate(demonioPrefab, spawnPoint, Quaternion.identity);

    }

    Vector3 GetSpawnPointLeft() 
    {
        // se obtiene el punto de inicio desde la izquierda de la camara
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        Vector3 spawnPosition = new Vector3(-cameraWidth, Random.Range(-cameraHeight, cameraHeight), 0);
        return spawnPosition;
    }

    Vector3 GetSpawnPointRight() 
    {
        // se obtiene el punto de inicio desde la derecha de la camara
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        Vector3 spawnPosition = new Vector3(cameraWidth, Random.Range(-cameraHeight, cameraHeight), 0);
        return spawnPosition;
    }
}
