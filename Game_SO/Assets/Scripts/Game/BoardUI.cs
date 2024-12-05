using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BoardUI : MonoBehaviour
{
    //Variables
    //Generic
    [SerializeField] private QuizManager quizManager;
    private Board board;
    private Animator anim;
    [HideInInspector] public bool selected;
    [HideInInspector] public int cardNumber;

    //Boards
    [SerializeField] private TMP_Text phaseName;
    [SerializeField] public TMP_Text phasePoints;
    [SerializeField] private Color phaseColor;
    [SerializeField] private Image phaseImage;
    [SerializeField] private SpriteRenderer phaseBg;
    [SerializeField] public List<Button> phaseCards;
    [SerializeField] public List<Image> phaseCardsBacks;
    [SerializeField] public Color normalCardColor, lockedCardColor;

    private void Awake()
    {
        for (int i = 0; i < phaseCards.Count; i++)
        {
            Button localBtn = phaseCards[i];
            localBtn.onClick.AddListener((() => OnClick(localBtn)));
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (quizManager.answeredCards)
        {
            case 0:
                phaseName.text = "Fase 1: Memória Principal";
                anim.SetInteger("board", 0);
                break;
            case 5:
                phaseName.text = "Fase 2: Páginas";
                anim.SetInteger("board", 1);
                break;
            case 10:
                phaseName.text = "Fase 3: Scheduling e Threads";
                anim.SetInteger("board", 2);
                break;
            case 15:
                phaseName.text = "Fase 4: SO em Geral";
                anim.SetInteger("board", 3);
                break;
        }
    }

    //Setting board through quiz manager
    public void SetBoard(Board board)
    {
        this.board = board;

        phaseName.text = board.phaseName;
        phaseBg.color = board.phaseColor;
        phaseImage.sprite = board.phaseImage;
        phasePoints.text = "Pontos: " + quizManager.totalPoints;

        for (int i = 0; i < 45; i++)
        {
            phaseCards[i].GetComponent<Image>().color = lockedCardColor;
            phaseCards[i].interactable = false;
            phaseCards[i].gameObject.SetActive(false);
        }

        //Locking all cards
        for (int i = 0; i < 45; i++)
        {
            phaseCards[i].gameObject.SetActive(false);
            board.phaseCards[i].cardUnlocked = false;
            board.phaseCards[i].cardAnswered = false;

            phaseCards[i].GetComponent<Image>().color = lockedCardColor;
            phaseCards[i].interactable = false;
        }

        //Unlocking the first card
        board.phaseCards[0].cardUnlocked = false;
        phaseCards[0].image.color = normalCardColor;
        phaseCards[0].interactable = false;

        selected = false;
    }

    void OnClick(Button btn)
    {
        if (!selected)
        {
            selected = true;
            cardNumber = int.Parse(btn.name);
            quizManager.SelectCard(cardNumber);
        }
    }
}