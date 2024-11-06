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
    public TextMeshProUGUI acertos;
    public TextMeshProUGUI motivacao;
    private int qtdCorretas = 0;

    private int questionIndex = 0;

    public GameObject resultPanel;
    public GameObject questionPanel;
    public GameObject playerController;
    public GameObject buttonAdvanceQuestion;

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
            Debug.LogWarning("Não há perguntas suficientes no nível de dificuldade escolhido.");
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
        if (index < 0 || index >= selectedQuestions.Count)
        {
            Debug.LogWarning("Índice de pergunta fora do alcance.");
            return;
        }

        Question question = selectedQuestions[index];
        resposta = question.correctAnswer;

        // Atualizar o texto da pergunta na interface
        questionText.text = question.questionText;

        // Atualizar os botões de resposta na interface
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

    // Método chamado quando um botão de resposta é clicado.
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

    //Método chamado quando o botão de avançar pergunta é clicado
    public void AdvanceQuestion()
    {
        if (escolheu == true)
        {
            escolheu = false;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].image.color = corOriginalBotao;
            }

            // Avançar para a próxima pergunta
            questionIndex++;


            if (questionIndex < selectedQuestions.Count)
            {
                DisplayQuestion(questionIndex);
            }
            else
            {

                if (qtdCorretas < 5)
                {
                    motivacao.text = "Continue jogando para aprender ainda mais.";
                }
                else if (qtdCorretas >= 5 && qtdCorretas <= 7)
                {
                    motivacao.text = "Você foi bem mas ainda pode melhorar! Continue sua exploração";
                }
                else if (qtdCorretas >= 8 && qtdCorretas <= 9)
                {
                    motivacao.text = "Você está se tornando um explorador nato! Continue explorando o universo.";
                }
                else if (qtdCorretas > 9)
                {
                    motivacao.text = "Parabéns! Você é um verdadeiro explorador das galáxias";
                }


                //Ações realizadas quando todas as perguntas foram respondidas
                acertos.text = "Você acertou " + qtdCorretas.ToString() + " de 10 questões possíveis";
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
    public List<string> choices; // Adicione esta linha para as opções de resposta.
    public string correctAnswer; // Adicione esta linha para a resposta correta.
}


