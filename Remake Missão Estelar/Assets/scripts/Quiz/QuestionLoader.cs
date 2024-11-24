using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System;
using TMPro;
using Random = UnityEngine.Random;

public class QuestionLoader : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons = new Button[4];

    private List<Question> easyQuestions = new List<Question>();
    private List<Question> intermediateQuestions = new List<Question>();
    private List<Question> hardQuestions = new List<Question>();
    private List<Question> selectedQuestions = new List<Question>();
    private string resposta;
    public GameObject acertos;
    private int qtdCorretas = 0;

    private int questionIndex = 0;

    public GameObject resultPanel;
    public GameObject questionPanel;
    public GameObject playerController;
    public GameObject buttonAdvanceQuestion;
    public Sprite spriteRespostaIncorreta; // Sprite vermelho para resposta incorreta
    public Sprite spriteOriginal; // Armazena o sprite original do bot�o


    private bool fim = false;
    private bool escolheu = false;

    private Color corOriginalBotao = Color.white;
    private Color corRespostaCorreta = Color.green;
    private Color corRespostaIncorreta = Color.red;
    TextAsset textAsset;


    private void LoadQuestionsFromFile()
    {
        try
        {
            textAsset = Resources.Load<TextAsset>("Perguntas");
            MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
            StreamReader sr = new StreamReader(memoryStream);

            string line;
            Question currentQuestion = null;
            Debug.Log(sr.ReadLine());
            while ((line = sr.ReadLine()) != null)
            {

                if (line.StartsWith("Facil") || line.StartsWith("Intermediario") || line.StartsWith("Dificil"))
                {
                    if (currentQuestion != null)
                    {
                        switch (currentQuestion.difficulty)
                        {
                            case "Facil":
                                easyQuestions.Add(currentQuestion);
                                break;
                            case "Intermediario":
                                intermediateQuestions.Add(currentQuestion);
                                break;
                            case "Dificil":
                                hardQuestions.Add(currentQuestion);
                                break;
                        }
                    }
                    currentQuestion = new Question();
                    currentQuestion.difficulty = line.Trim();
                }
                else if (currentQuestion != null)
                {
                    if (currentQuestion.questionText == null)
                    {
                        currentQuestion.questionText = line.Trim();
                    }
                    else if (currentQuestion.choices == null)
                    {
                        currentQuestion.choices = new List<string>(line.Split('/'));
                    }
                    else if (currentQuestion.correctAnswer == null)
                    {
                        currentQuestion.correctAnswer = line.Trim();
                    }
                }
            }

            if (currentQuestion != null)
            {
                switch (currentQuestion.difficulty)
                {
                    case "Facil":
                        easyQuestions.Add(currentQuestion);
                        break;
                    case "Intermediario":
                        intermediateQuestions.Add(currentQuestion);
                        break;
                    case "Dificil":
                        hardQuestions.Add(currentQuestion);
                        break;
                }
            }

            sr.Close();
            memoryStream.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
    }

    public void OnEasyButtonClick()
    {
        questionIndex = 0;
        LoadQuestionsFromFile();
        SelectRandomQuestions(easyQuestions, 10);
        DisplayQuestion(0); // Mostrar a primeira pergunta selecionada.
    }

    public void OnIntermediateButtonClick()
    {
        questionIndex = 0;
        LoadQuestionsFromFile();
        SelectRandomQuestions(intermediateQuestions, 10);
        DisplayQuestion(0); // Mostrar a primeira pergunta selecionada
    }

    public void OnHardButtonClick()
    {
        questionIndex = 0;
        LoadQuestionsFromFile();
        SelectRandomQuestions(hardQuestions, 10);
        DisplayQuestion(0); // Mostrar a primeira pergunta selecionada
    }

    private void SelectRandomQuestions(List<Question> questionList, int numberOfQuestions)
    {
        if (questionList.Count < numberOfQuestions)
        {
            Debug.LogWarning("N�o h� perguntas suficientes no n�vel de dificuldade escolhido.");
            return;
        }

        selectedQuestions.Clear();
        List<Question> tempQuestions = new List<Question>(questionList);

        for (int i = 0; i < numberOfQuestions; i++)
        {
            int randomIndex = Random.Range(0, tempQuestions.Count);
            selectedQuestions.Add(tempQuestions[randomIndex]);
            tempQuestions.RemoveAt(randomIndex);
        }
    }

    public void DisplayQuestion(int index)
    {
        if (answerButtons.Length > 0 && answerButtons[0].image.sprite != null)
        {
            spriteOriginal = answerButtons[0].image.sprite; // Salva o sprite original apenas uma vez
        }

        if (index < 0 || index >= selectedQuestions.Count)
        {
            Debug.LogWarning("�ndice de pergunta fora do alcance.");
            return;
        }

        Question question = selectedQuestions[index];
        resposta = question.correctAnswer;

        // Atualizar o texto da pergunta na interface
        questionText.text = question.questionText;

        // Atualizar os bot�es de resposta na interface
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < question.choices.Count)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = question.choices[i];
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // M�todo chamado quando um bot�o de resposta � clicado.
    public void OnButtonClick(int optionIndex)
    {

        if (answerButtons[optionIndex].GetComponentInChildren<TMPro.TextMeshProUGUI>().text == resposta)
        {
            if (escolheu == false)
            {
                Debug.Log("Resposta correta!");
                qtdCorretas++;
                answerButtons[optionIndex].image.color = corRespostaCorreta;
                escolheu = true;
            }
        }
        else
        {
            if (escolheu == false)
            {
                Debug.Log("Resposta incorreta!");
                answerButtons[optionIndex].image.sprite = spriteRespostaIncorreta; // Troca o sprite do bot�o
                escolheu = true;
            }
        }

        if (escolheu == true)
        {
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (answerButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text == resposta)
                {
                    answerButtons[i].image.color = corRespostaCorreta;
                }
            }
        }


        if (answerButtons[optionIndex].GetComponentInChildren<TMPro.TextMeshProUGUI>().text == resposta)
        {
            if (escolheu == false)
            {
                Debug.Log("Resposta correta!");
                qtdCorretas++;
                answerButtons[optionIndex].image.color = corRespostaCorreta;
                escolheu = true;
                ColorBlock colors = answerButtons[optionIndex].GetComponent<Button>().colors;
                colors.selectedColor = Color.white;
            }

        }
        else
        {
            if (escolheu == false)
            {
                Debug.Log("Resposta incorreta!");
                answerButtons[optionIndex].image.color = corRespostaIncorreta;
                escolheu = true;
                ColorBlock colors = answerButtons[optionIndex].GetComponent<Button>().colors;
                colors.selectedColor = Color.white;
            }

        }

        if (escolheu == true)
        {
            for (int i = 0; i < answerButtons.Length; i++)
            {
                ColorBlock colors = answerButtons[i].GetComponent<Button>().colors;
                colors.selectedColor = Color.white;

                if (answerButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text == resposta)
                {
                    answerButtons[i].image.color = corRespostaCorreta;

                }

            }
        }

    }

    //M�todo chamado quando o bot�o de avan�ar pergunta � clicado
    public void AdvanceQuestion()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].image.color = corOriginalBotao; // Restaura a cor original
            answerButtons[i].image.sprite = spriteOriginal; // Restaura o sprite original
        }

        if (escolheu == true)
        {
            escolheu = false;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].image.color = corOriginalBotao;
            }

            // Avan�ar para a pr�xima pergunta
            questionIndex++;


            if (questionIndex < selectedQuestions.Count)
            {
                DisplayQuestion(questionIndex);
            }
            else
            {

                if (acertos != null)
                {
                    TextMeshProUGUI textoAcerto = acertos.GetComponent<TextMeshProUGUI>();
                    textoAcerto.text = qtdCorretas.ToString();
                }
                //A��es realizadas quando todas as perguntas foram respondidas
                questionPanel.SetActive(false); //Desativa o painel de perguntas
                resultPanel.SetActive(true); //Ativa o painel de resultado
                Debug.Log("Fim das perguntas.");
                qtdCorretas = 0;
            }
        }

    }



}



[System.Serializable]
public class Question
{
    public string difficulty;
    public string questionText;
    public List<string> choices; // Adicione esta linha para as op��es de resposta.
    public string correctAnswer; // Adicione esta linha para a resposta correta.
}

