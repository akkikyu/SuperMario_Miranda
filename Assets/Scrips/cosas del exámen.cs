using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cosasdelexámen : MonoBehaviour
{
    public Transform playerTransform; //donde se almacena el objeto de personaje

    public Vector3 offset; //para que la cámara no esté justo en el centro, importante offset en la Z (-10)
    public Vector2 maxPosition; //posicion máxima cámara 
    public Vector2 minPosition; //posicion mínima cámara 


    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform; 
        
    }

    void FixedUpdate()
    {
        if(playerTransform == null) //si bariable jugador está vacía
        {
            return;
        } 

        Vector3 desiredPosition = playerTransform.position + offset; //posicion que queremos que tega la cam, posicion + el offset

        float.clapX = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x); //limitar movimiento eje X, acceder al valor de la x del desiredposition, 
        float.clapY = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y); //
        Vector3 clampedPosition = new Vector3(clampX, clampY, desiredPosition.z); //el eje z nos da igual porque no lo usamos así que usamos el desiredposition
    }
}
