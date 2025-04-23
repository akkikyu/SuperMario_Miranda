using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimerScript : MonoBehaviour
{
    private int[] numeros = {75, 1, 3};

    public int[] numeros2;

    private int[ , ] numeros3 = {{7, 8, 98}, {9, 22, 45}, {69, 6, 99}};

    List<int> listaDeNumeros = new List<int> {3, 5, 8, 9, 69, 12};



    void Start()
    {
        /*foreach(int numero in listaDeNumeros)
        {
            Debug,Log(numero);
        }

        listaDeNumeros.Add(22); //añadir un nuevo número a la lista al final

        listaDeNumeros.Remove(5); //le dices que numero quieres eliminar, en caso de tener numero repetido te elimina el primero

        listaDeNumeros.RemoveAt(0); //quitas el numero en la posicion de lista, empiza la liasta desde el 0 que es el primer numero

        listaDeNumeros.RemoveAt(listaDeNumeros) //

        listaDeNumeros.RemoveAt(listaDeNumeros.count - 1) //para eliminar el último número de la lista

        listaDeNumeros.Clear(); //borrar todos los números de la lista

        listaDeNumeros.Sort(); //ordenar la lista de + pequeño a + grande

        listaDeNumeros.Reverse(); //te lo ordena del revés

        foreach(inh numero in listaDeNumeros)
        {
            Debug.Log(numero);
        }*/

        
        //Debug.Log(numeros[0]);
        //Debug.Log(numeros3[1, 2]);

        /*foreach(int numero in numeros) //de se ejecuta una vez por cada item del array
        {
            Debug.Log(numeros);
        }
        //por cada, por si eres retrasado y no sabes inglés
        //solo se usan con arrays o listas*/


        /*for(int i = 0; i > 15; i++) // variable de control/condicion cumplir para que se ejecute el bucle /sumarle 1 lo del principio para volvea a empezar hasta que termine de cumplirse
        {
            Debug.Log(i);
        }
        //se repite el numero de veces que le digamos


        /*for(int i = 0; i > numeros.Length; i++) // es como un foreach
        {
            Debug.Log(numeros[i]);
        }
        */

        /*while(true) //mientras sea verdadero se ejecuta
        {
            krtk hj     
        }*/

        /*nt i = 0; //funciona como el bucle for
        while(i < numeros.Lenght) 
        {
            Debug.Log(numeros[i]);
            i++;
        }
        */

        /*while(boleana == true) //infinito y te cargas el pc
        {

        }
        */

        /*int i = 75;
        do
        {
            Debug.Log("tetas");
        }
        while (i < numeros.Lenght);*/
    }
}
