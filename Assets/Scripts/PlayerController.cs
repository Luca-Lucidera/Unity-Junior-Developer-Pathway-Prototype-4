using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public bool hasPowerup = false;
    public GameObject powerupIndicator;
 


    private Rigidbody playerRigidBody;
    private GameObject focalPoint;
    private readonly float powerUpBounceForce = 50f;

    void Start()
    {

        //Local
        playerRigidBody = GetComponent<Rigidbody>();

        //External
        focalPoint = GameObject.Find("FocalPoint");
    }


    void Update()
    {
        var forwardInput = Input.GetAxis("Vertical");
        //imposto che la direzione della forza deve arrivare dalla direzione dell'asse Z del FocalPoint
        playerRigidBody.AddForce(forwardInput * speed * focalPoint.transform.forward);

        //L'indicatore del buff seguirà la palla
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            //Crea un thread separato dall'update loop
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    //Crea una subroutine dove dice di aspettare 7 secondi, dopo disabilita il power up
    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                if (hasPowerup)
                {
                    Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
                    //direzione di rimbalzo
                    Vector3 directionToBounceAway = collision.gameObject.transform.position - transform.position;
                    enemyRB.AddForce(directionToBounceAway * powerUpBounceForce, ForceMode.Impulse);
                }
                break;
            default: break;
        }
    }
}
