using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;

    private Rigidbody ballRigidBody;
    private GameObject player;

    void Start()
    {
        //Ball component
        ballRigidBody = GetComponent<Rigidbody>();

        //External
        player = GameObject.Find("Player");
    }

    void Update()
    {
        //Per calcolare la direzione della forza da applicare alla palla per inseguire il giocaore
        //bisogna prendere la posizione del giocatore e sottrarci la posizione attuale della palla
        Vector3 directionToChase = (player.transform.position - transform.position).normalized;
        ballRigidBody.AddForce(directionToChase * speed);

        if (transform.position.y < -1)
        {
            Destroy(gameObject);
        }
    }
}
