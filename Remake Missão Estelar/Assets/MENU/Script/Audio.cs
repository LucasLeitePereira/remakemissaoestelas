using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class usabilidade : MonoBehaviour
{

    private bool estadoSom = true;
    [SerializeField] private AudioSource fundomusical;

    [SerializeField] private Sprite somligadoSprite;
    [SerializeField] private Sprite somdesligadoSprite;

    [SerializeField] private Image muteImage;
    public void LigarDesligarsom()
    {
        estadoSom = !estadoSom;
        fundomusical.enabled = estadoSom;

        if (estadoSom)
        {
            muteImage.sprite = somligadoSprite;
        }
        else
        {
            muteImage.sprite = somdesligadoSprite;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
