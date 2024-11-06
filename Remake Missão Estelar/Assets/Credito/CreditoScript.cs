using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CreditoScript : MonoBehaviour
{
    public VideoPlayer video;
    public string nomeDaCena;

    private void Start()
    {
        video.loopPointReached += fimVideo;
    }

    private void Update()
    {
        sairDoCredito();
    }

    void fimVideo(VideoPlayer vp)
    {
        SceneManager.LoadScene(nomeDaCena);
    }

    void sairDoCredito()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Mudando de cena");
            SceneManager.LoadScene(nomeDaCena);
        }
    }
}
