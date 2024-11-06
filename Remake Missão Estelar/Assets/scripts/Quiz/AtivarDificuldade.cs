using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarDificuldade : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject difficultyPanel;

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
        menuPanel.SetActive(false); // Desativa o painel do menu
        difficultyPanel.SetActive(true); // Ativa o painel de dificuldade
    }
}
