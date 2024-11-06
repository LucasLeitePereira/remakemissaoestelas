using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarPerguntas : MonoBehaviour
{
    public GameObject difficultyPanel;
    public GameObject questionPanel;
    public playerMovement playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarPerguntas()
    {
        difficultyPanel.SetActive(false);   // Desativa o painel de dificuldade
        questionPanel.SetActive(true);      // Ativa o painel de pergunta
    }
}
