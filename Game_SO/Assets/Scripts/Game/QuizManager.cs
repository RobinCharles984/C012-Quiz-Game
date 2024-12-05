using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    //Variables
    [SerializeField] public Board boards;
    [SerializeField] private GameObject cardCanvas;
    [SerializeField] private GameObject winningCanvas;
    [SerializeField] private GameObject looserCanvas;
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private BoardUI boardUI;
    [SerializeField] private Animator boardAnim;
    [SerializeField] private CameraMovement camMove;
    
    //Generic
    private bool correct;
    [HideInInspector] public int nCard;
    [HideInInspector] public int answeredCards;
    [HideInInspector] private int correctBoards;
    [HideInInspector] public int correctCards;
    [HideInInspector] public int totalPoints;

    private void Start()
    {
        cardCanvas.SetActive(false);
        camMove.enabled = true;
        answeredCards = 0;
        correctBoards = 0;
        correctCards = 0;
        totalPoints = 0;
        
        //Setting board on start
        boardUI.SetBoard(boards);
    }
    
    //Setting Question when selecting card
    public void SelectCard(int cardNumber)
    {
        cardCanvas.SetActive(true);
        camMove.enabled = false;
        quizUI.SetQuestion(boards, cardNumber);
    }
    
    public void CloseCard()
    {
        cardCanvas.SetActive(false);
        camMove.enabled = true;
        boardUI.selected = false;
    }

    private void Update()
    {
        //Win or Loose
        if (answeredCards >= 20)
        {
            if (totalPoints >= 16)
            {
                winningCanvas.SetActive(true);
                GameManager.instance.gameComplete = true;
                GameManager.instance.nameInputBoolean = false;
                GameManager.instance.score = totalPoints;
            }
            else
            {
                looserCanvas.SetActive(true);
                GameManager.instance.gameComplete = false;
                GameManager.instance.nameInputBoolean = false;
                GameManager.instance.score = totalPoints;
            }
        }

    }

    public bool Answer(string answer)
    {
        correct = false;

        //System if the answer is correct
        if (answer == boards.phaseCards[boardUI.cardNumber].correctAnswer)
        {
            //Try unlocking next card, else unlock this one ang proceed
            try
            {
                //Chaging internal values for card
                boards.phaseCards[boardUI.cardNumber].cardAnswered = true;
                boards.phaseCards[boardUI.cardNumber].correctAnswered = true;
                boards.phaseCards[boardUI.cardNumber + 1].cardUnlocked = true;
            
                //Chaging UI
                boardUI.phaseCards[boardUI.cardNumber + 1].interactable = true;
                boardUI.phaseCards[boardUI.cardNumber + 1].GetComponent<Image>().color = boardUI.normalCardColor;
            }
            catch (Exception e)
            {
                //Chaging internal values for card
                boards.phaseCards[boardUI.cardNumber].cardAnswered = true;
                boards.phaseCards[boardUI.cardNumber].cardUnlocked = true;
            
                //Chaging UI
                boardUI.phaseCards[boardUI.cardNumber].interactable = true;
                boardUI.phaseCards[boardUI.cardNumber].GetComponent<Image>().color = boardUI.normalCardColor;
            }
            
            correct = true;
            answeredCards++;
            correctCards++;
            totalPoints = correctCards;

            //Animation for actual card
            boardAnim.SetInteger("card", answeredCards);

            boardUI.phasePoints.text = "Pontos: " + totalPoints;
            print("Correct Answer");
        }
        else //If the answer is incorret
        {
            //Try unlocking next card, else unlock this one ang proceed
            try
            {
                //Chaging internal values for card
                boards.phaseCards[boardUI.cardNumber].cardAnswered = true;
                boards.phaseCards[boardUI.cardNumber].correctAnswered = false;
                boards.phaseCards[boardUI.cardNumber + 1].cardUnlocked = true;

                //Chaging UI
                boardUI.phaseCards[boardUI.cardNumber + 1].interactable = true;
                boardUI.phaseCards[boardUI.cardNumber + 1].GetComponent<Image>().color = boardUI.normalCardColor;
            }
            catch (Exception e)
            {
                //Chaging internal values for card
                boards.phaseCards[boardUI.cardNumber].cardAnswered = true;
                boards.phaseCards[boardUI.cardNumber].cardUnlocked = true;

                //Chaging UI
                boardUI.phaseCards[boardUI.cardNumber].interactable = true;
                boardUI.phaseCards[boardUI.cardNumber].GetComponent<Image>().color = boardUI.normalCardColor;
            }

            answeredCards++;

            //Animation for actual card
            //boardAnim.SetInteger("card", answeredCards);

            correct = false;
            print("Wrong Answer! The correct was: " + boards.phaseCards[boardUI.cardNumber].correctAnswer);
        }

        return correct;
    }

    //Scene Manager
    public void SceneControl(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

//Classes
[System.Serializable]
public class Card
{
    public string cardQuestion;
    public List<string> options;
    public string correctAnswer;
    public Sprite cardImage;
    public Sprite cardBorder;
    public bool cardAnswered;
    public bool cardUnlocked;
    public bool correctAnswered;
}

[System.Serializable]
public class Board
{
    public string phaseName;
    public Color phaseColor;
    public Sprite phaseImage;
    public List<Card> phaseCards;
    public List<Image> phaseCardsBacks;
    public bool boardFinished;
}