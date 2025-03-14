using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimerScript : MonoBehaviour
{
    //Variables
    public int numeroEntero = 5;

    private float numeroDecimal = 7.5f;

    bool boleana = true; //variable verdadero o falso;

    string cadenaTexto = "Hola Mundo";

    string Text = "klk papi";


    // Start is called before the first frame update
    void Start()
    {
        Calculos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Calculos()
    {
         Debug.Log(numeroEntero);
        numeroEntero = 17;
        Debug.Log(numeroEntero);

        numeroEntero = 7 + 5; //puedes hacer tmb restas "-" divisiones "/" y multiplicaciones "·"

        numeroEntero++; //te suma solo 1

        numeroEntero += 2; //con este puedes poner más de uno

        Debug.Log(Text);

    }
}
