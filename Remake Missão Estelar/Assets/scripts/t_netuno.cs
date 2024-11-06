using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_netuno : MonoBehaviour
{
    public GameObject player;
    public GameObject respawn;
    public GameObject infoNetuno;
    public GameObject exclamação;

    // Start is called before the first frame update
    void Start()
    {
        infoNetuno.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0f, 0.1f * Time.deltaTime, 0f, Space.Self);

        bool sair = Input.GetKey(KeyCode.Escape);

        if (sair)
        {
            infoNetuno.SetActive(false);
            
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
                collision.gameObject.transform.position = new Vector3(-2030, 1111, 999); // Faz o enquadramento da nave ao lado do planeta e olhando para o sol
            }

            infoNetuno.SetActive(true);
            player.SetActive(false);
            exclamação.SetActive(false);
        }
    }
}

