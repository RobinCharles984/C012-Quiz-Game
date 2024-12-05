using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    //Variables
    //General
    private Board board;
    [SerializeField] public List<Color> backgroundColor;
    [SerializeField] public Color correctColor, wrongColor, normalColor;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private BoardUI boardUI;
    [SerializeField] private Image image;
    private bool answered;

    //Cards
    [SerializeField] private List<Sprite> cardImages;
    [SerializeField] private TMP_Text cardQuestion;
    [SerializeField] private List<Button> options;
    [SerializeField] private Image cardImage;
    [SerializeField] private Image cardColor;
    [SerializeField] private TMP_Text correctText;
    [SerializeField] private Button nextCard;

    private void Awake()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        Button nextButton = nextCard;
        nextButton.onClick.AddListener(() => NextButton(nextButton, quizManager.boards));

        nextCard.gameObject.SetActive(false);
    }

    private void Update()
    {
        //Card Background Images
        if (quizManager.answeredCards >= 0 && quizManager.answeredCards < 5)
        {
            image.sprite = cardImages[0];
            cardColor.color = backgroundColor[0];
        }
        else if (quizManager.answeredCards >= 5 && quizManager.answeredCards < 10)
        {
            image.sprite = cardImages[1];
            cardColor.color = backgroundColor[1];
        }
        else if (quizManager.answeredCards >= 10 && quizManager.answeredCards < 15)
        {
            image.sprite = cardImages[2];
            cardColor.color = backgroundColor[2];
        }
        else if (quizManager.answeredCards >= 15 && quizManager.answeredCards < 20)
        {
            image.sprite = cardImages[3];
            cardColor.color = backgroundColor[3];
        }
        else if (quizManager.answeredCards >= 20 && quizManager.answeredCards < 25)
        {
            image.sprite = cardImages[4];
            cardColor.color = backgroundColor[4];
        }
        else if (quizManager.answeredCards >= 25 && quizManager.answeredCards < 30)
        {
            image.sprite = cardImages[5];
            cardColor.color = backgroundColor[5];
        }
        else if (quizManager.answeredCards >= 30 && quizManager.answeredCards < 35)
        {
            image.sprite = cardImages[6];
            cardColor.color = backgroundColor[6];
        }
        else if (quizManager.answeredCards >= 35 && quizManager.answeredCards < 40)
        {
            image.sprite = cardImages[7];
            cardColor.color = backgroundColor[7];
        }
        else if (quizManager.answeredCards >= 40 && quizManager.answeredCards < 45)
        {
            image.sprite = cardImages[8];
            cardColor.color = backgroundColor[8];
        }
    }

    //Sends QuizManager values to UI
    public void SetQuestion(Board board, int nCard)
    {
        this.board = board;

        nCard = boardUI.cardNumber;

        //If card was already correct answered
        if (board.phaseCards[nCard].cardAnswered)
        {
            cardQuestion.text = board.phaseCards[nCard].cardQuestion;

            for (int i = 0; i < board.phaseCards[nCard].options.Count; i++)
            {
                options[i].gameObject.name = board.phaseCards[nCard].options[i];
                options[i].GetComponentInChildren<TMP_Text>().text = board.phaseCards[nCard].options[i];
                options[i].image.color = wrongColor;
                options[i].interactable = false;
                if (options[i].gameObject.name == board.phaseCards[nCard].correctAnswer) options[i].image.color = correctColor;
                if (board.phaseCards[nCard].correctAnswered)
                    correctText.text = "Resposta Certa!";

                else
                    correctText.text = "Resposta Errada!";


            }

            nextCard.gameObject.SetActive(true);

            correctText.gameObject.SetActive(true);

            answered = true;

            cardImage.sprite = board.phaseCards[nCard].cardImage;
        }
        else //If not
        {
            cardQuestion.text = board.phaseCards[nCard].cardQuestion;

            for (int i = 0; i < board.phaseCards[nCard].options.Count; i++)
            {
                options[i].gameObject.name = board.phaseCards[nCard].options[i];
                options[i].GetComponentInChildren<TMP_Text>().text = board.phaseCards[nCard].options[i];
                options[i].image.color = normalColor;
                options[i].interactable = true;
            }

            nextCard.gameObject.SetActive(false);

            correctText.gameObject.SetActive(true);
            correctText.text = "";

            answered = false;

            image.sprite = board.phaseCards[nCard].cardImage;
        }
    }

    //Clicking the next button
    public void NextButton(Button btn, Board board)
    {
        try
        {
            quizManager.CloseCard();

            int nCard;

            boardUI.cardNumber += 1;

            nCard = boardUI.cardNumber;

            this.board = board;

            quizManager.SelectCard(nCard);

            cardQuestion.text = board.phaseCards[nCard].cardQuestion;

            for (int i = 0; i < board.phaseCards[nCard].options.Count; i++)
            {
                options[i].gameObject.name = board.phaseCards[nCard].options[i];
                options[i].GetComponentInChildren<TMP_Text>().text = board.phaseCards[nCard].options[i];
                options[i].image.color = normalColor;
                options[i].interactable = true;
            }

            nextCard.gameObject.SetActive(false);

            correctText.gameObject.SetActive(true);
            correctText.text = "";

            answered = false;

            image.sprite = board.phaseCards[nCard].cardImage;
        }
        catch (Exception e)
        {
            quizManager.CloseCard();
            print("Cant go to next card");
        }
    }

    //Clicking the option button
    void OnClick(Button btn)
    {
        if (!answered)
        {
            answered = true;
            bool val = quizManager.Answer(btn.name);

            if (val)
            {
                btn.image.color = correctColor;
                AudioManager.instance.PlaySFX("Correct");
                correctText.gameObject.SetActive(true);
                correctText.text = "Resposta Certa!";
                SetQuestion(quizManager.boards, boardUI.cardNumber);
            }
            else
            {
                btn.image.color = wrongColor;
                AudioManager.instance.PlaySFX("Wrong");
                correctText.gameObject.SetActive(true);
                correctText.text = "Resposta Errada!";
                SetQuestion(quizManager.boards, boardUI.cardNumber);
            }

            nextCard.gameObject.SetActive(true);
        }
        else
        {
            nextCard.gameObject.SetActive(true);
        }
    }
}