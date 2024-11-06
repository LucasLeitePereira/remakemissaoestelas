using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_lua : MonoBehaviour
{
    public GameObject player;
    public GameObject respawn;
    public GameObject infoLua;
    public GameObject exclamação;

    // Start is called before the first frame update
    void Start()
    {
        infoLua.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0f, 0.6f * Time.deltaTime, 0f, Space.Self);

        bool sair = Input.GetKey(KeyCode.Escape);

        if (sair)
        {
            infoLua.SetActive(false);
            
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
                Transform solTransform = solObject.transform;
                collision.gameObject.transform.LookAt(solTransform);
            }


            infoLua.SetActive(true);
            exclamação.SetActive(false);
            player.SetActive(false);

        }
        
    }



}
