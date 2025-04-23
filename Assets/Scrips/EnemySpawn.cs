using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Spawn Enemigos")]

    [Tooltip("Prefab de enemigos")]

    [SerializeField] private GameObject[] _enemiesPrefab;
    [Tooltip("Numeros de enemigos que van a spawnear")]
    
    [SerializeField] private int _enemiesToSpawn;
    [SerializeField] private Transform[] _spawnPoint;

    private BoxCollider2D _collider;

    [SerializeField] private int _enemyIndex;


    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if(_enemiesToSpawn <= 0)
        {
            CancelInvoke(); //cancela TODOS los Invokes de este script
        }
    }

    IEnumerator SpawnEnemy()
    {
        for(int i = 0; 1 < _enemiesToSpawn; i++)
        {
           foreach(Transform spawn in _spawnPoint)
           {
            _enemyIndex = Random.Range(0, _enemiesPrefab.Length);
            Instantiate(_enemiesPrefab[_enemyIndex], spawn.position, spawn.rotation);
           }

           yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            _collider.enabled = false;
            //InvokeRepeating("SpawnEnemy", 0, 2); //el segundo valor es para saber cada cuanto llamar la función
            //SpawnEnemy();
            //Invoke("SpawnEnemy", 5) 
            //↑ llamar la funcion después de 5 segundos (ejemplo)
            StartCoroutine(SpawnEnemy());
        }
    }
}
