using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_saturno : MonoBehaviour
{
    public GameObject player;
    public GameObject respawn;
    public GameObject infoSaturno;
    public GameObject exclamação;

    // Start is called before the first frame update
    void Start()
    {
        infoSaturno.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0f, 0.3f * Time.deltaTime, 0f, Space.Self);

        bool sair = Input.GetKey(KeyCode.Escape);
        
        if (sair)
        {
            infoSaturno.SetActive(false);
            
            player.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector3.zero; // Pare o movimento atual do jogador
                playerRigidbody.angularVelocity = Vector3.zero; // Pare a rotação atual do jogador
            }

            
            collision.gameObject.transform.position = respawn.transform.position;

            // Olhar para o sol
            GameObject solObject = GameObject.FindGameObjectWithTag("sol");
            if (solObject != null)
            {
                Vector3 deslocamento = new Vector3(-200, 0, 0); 
                Transform solTransform = solObject.transform;
                collision.gameObject.transform.LookAt(solTransform);
                collision.gameObject.transform.position += deslocamento; // Move a nave para o lado planeta
            }

            infoSaturno.SetActive(true);
            player.SetActive(false);
            exclamação.SetActive(false);
        }
    }
}
