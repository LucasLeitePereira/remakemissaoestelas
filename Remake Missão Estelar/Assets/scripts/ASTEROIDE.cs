using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTEROIDE : MonoBehaviour
{
    public GameObject player;
    public GameObject respawn;

    private bool isRespawning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator BlinkPlayer()
    {
        Renderer playerRenderer = player.GetComponent<Renderer>();
        float blinkDuration = 3.0f; 
        float blinkInterval = 0.2f; 

        isRespawning = true;

        // Blink the player for the specified duration
        while (blinkDuration > 0)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            blinkDuration -= blinkInterval;
        }

        // Ensure the player is visible again after blinking
        playerRenderer.enabled = true;
        isRespawning = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isRespawning)
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector3.zero; 
                playerRigidbody.angularVelocity = Vector3.zero; 
            }

            collision.gameObject.transform.position = respawn.transform.position;

            
            GameObject solObject = GameObject.FindGameObjectWithTag("sol");
            if (solObject != null)
            {
                Transform solTransform = solObject.transform;
                collision.gameObject.transform.LookAt(solTransform);
            }

            
            StartCoroutine(BlinkPlayer());
        }
    }
}
