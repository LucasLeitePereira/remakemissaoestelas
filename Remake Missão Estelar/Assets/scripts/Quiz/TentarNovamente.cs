using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentarNovamente : MonoBehaviour
{
    public GameObject resultPanel;
    public GameObject menuPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void VoltarAoMenu()
    {
        resultPanel.SetActive(false); // Desativa o painel de resultado
        menuPanel.SetActive(true); // Ativa o painel de menu
    }
}
