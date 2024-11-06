using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltarAoMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject difficultyPanel;
    public playerMovement playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VoltarParaOMenu()
    {
        difficultyPanel.SetActive(false); // Desativa o painel do menu
        menuPanel.SetActive(true); // ativa o painel do menu
    }
}
