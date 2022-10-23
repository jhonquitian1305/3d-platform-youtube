using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public void OnTriggerEnter(Collider playerCollider)
    {
        if (playerCollider.tag == "Player")
        {
            //Reapareciendo el personaje cuando muere
            GameManager.instance.Respawn();
        }
    }
}
