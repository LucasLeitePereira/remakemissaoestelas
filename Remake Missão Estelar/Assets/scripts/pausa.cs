using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class script : MonoBehaviour
{

    public GameObject pausa;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausa.activeSelf)
            {
                Despausar();
            }
            else
            {
                Pausar();
            }
        }

        if (pausa.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            Sair();
        }
    }

    public void Pausar()
    {
        pausa.SetActive(true);

        Time.timeScale = 0;
    }

    public void Despausar()
    {
        pausa.SetActive(false);

        Time.timeScale = 1;
    }

    public void Sair()
    {
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }
}