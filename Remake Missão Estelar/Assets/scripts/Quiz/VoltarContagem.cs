using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltarContagem : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject contagemPanel;
    public playerMovement playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SairQuiz()
    {
        menuPanel.SetActive(false); // Desativa o painel do menu
        contagemPanel.SetActive(true); // Ativa o painel de contagem
    }
  
}
