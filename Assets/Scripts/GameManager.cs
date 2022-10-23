using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 respawnPosition;

    public void Awake()
    {
        instance = this;
    }
        
    void Start()
    {
        //Ocultando el cursor cuando inicia el juego
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerController.instance.transform.position;
    }
    void Update()
    {
        
    }

    //Reaparecer cuando muere
    public void Respawn()
    {
        StartCoroutine(RespawnWaiter());
    }
    
    //Corrutinas (Espera un tiempo para volver a reaparecer el personaje)
    public IEnumerator RespawnWaiter()
    {
        PlayerController.instance.gameObject.SetActive(false);

        CameraController.instance.cmBrain.enabled = false;

        yield return new WaitForSeconds(2f);

        PlayerController.instance.transform.position = respawnPosition;
        CameraController.instance.cmBrain.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);
    }
}
