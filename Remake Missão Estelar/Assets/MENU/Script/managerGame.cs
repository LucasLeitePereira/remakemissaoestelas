using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class managerGame : MonoBehaviour
{
    [SerializeField] private string nomeDoleveldeJogo;
    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoleveldeJogo);
    }

    public void SairJogo()
    {
        //Só vai funcionar depois de compilar o jogo
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }
}
