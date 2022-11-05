using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    public int healthAmount;
    public bool isFullHealth;

    public GameObject healthEffect;

    private void OnTriggerEnter(Collider playerCollider)
    {
        if (playerCollider.tag == "Player")
        {
            Destroy(gameObject);
            
            Instantiate(healthEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

            if (isFullHealth)
            {
                HealthManager.instance.ResetHealth();
            }
            else
            {
                HealthManager.instance.AddHealth(healthAmount);
            }
        }
    }
}
