using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float playerSpeed = 4.5f;

    public int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        //esto teletransporta al personaje

        //transform.position = new Vector3(-92.13f,4.9f,0);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x + direction * playerSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        //transform.Translate(new Vector3(direction * playerSpeed * Time.deltaTime, 0, 0));

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), playerSpeed * Time.deltaTime);
    }
}
