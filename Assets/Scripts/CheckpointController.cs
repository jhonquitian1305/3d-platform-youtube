using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider playerCollider)
    {
        if (playerCollider.tag == "Player")
        {
            GameManager.instance.SetSpawnPoint(transform.position);
            Debug.Log("Spawn Set");
        }
    }
}
