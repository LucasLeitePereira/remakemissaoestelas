using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sairdoquiz : MonoBehaviour
{

    public GameObject resultPanel;
    public GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SairDoQuiz()
    {
        resultPanel.SetActive(false); // Desativa o painel de resultado
        player.SetActive(true);
    }
}
