using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public string scenePlay;
    public string sceneCredito;
    public AudioSource audio;
    private bool audioOn = true;
    public GameObject botaoVolume;
    public GameObject botaoMute;


    public void Jogar()
    {
        Debug.Log("Mudando pra cena de Jogo");
        SceneManager.LoadScene(scenePlay);
    }

    public void Sair()
    {
        Debug.Log("Saindo do Jogo");
        Application.Quit();
    }

    public void Credito()
    {
        Debug.Log("Mudando pra cena de Crédito");
        SceneManager.LoadScene(sceneCredito);
    }

    public void ControleAudio()
    {
        if (audioOn)
        {
            Debug.Log("Audio Desligado");
            audio.volume = 0f;
            botaoVolume.SetActive(false);
            botaoMute.SetActive(true);
        }
        else
        {
            Debug.Log("Audio Ligado!");
            audio.volume = 1f;
            botaoVolume.SetActive(true);
            botaoMute.SetActive(false);
        }
        audioOn = !audioOn;
    }
}
