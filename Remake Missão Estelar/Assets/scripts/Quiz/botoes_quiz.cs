using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botoes_quiz : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject contagemPanel;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IniciarQuiz()
    {
        contagemPanel.SetActive(false); // Desativa o painel de contagem
        menuPanel.SetActive(true); // Ativa o painel do menu
    }

    public void VoltarParaOJogo()
    {
        contagemPanel.SetActive(false); // Desativa o painel de contagem
        player.SetActive(true);
    }
}
