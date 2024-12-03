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
    [HideInInspector]public bool selected;
    [HideInInspector] public int cardNumber;
    
    //Boards
    [SerializeField]private TMP_Text phaseName;
    [SerializeField] public TMP_Text phasePoints;
    [SerializeField]private Color phaseColor;
    [SerializeField]private Image phaseImage;
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
                phaseName.text = "Fase 1: Defini��o de les�o por press�o";
                anim.SetInteger("board", 0);
                break;
            case 5:
                phaseName.text = "Fase 2: Fatores de risco para o paciente adquirir les�o por press�o.";
                anim.SetInteger("board", 1);
                break;
            case 10:
                phaseName.text = "Fase 3: Classifica��o do est�gio da les�o por press�o";
                anim.SetInteger("board", 2);
                break;
            case 15:
                phaseName.text = "Fase 4: �Avalia��o da les�o por press�o�:";
                anim.SetInteger("board", 3);
                break;
            case 20:
                phaseName.text = "Fase 5: Medidas preventivas para les�o por press�o";
                anim.SetInteger("board", 4);
                break;
            case 25:
                phaseName.text = "Fase 6: T�cnica e tipo de limpeza da les�o por press�o";
                anim.SetInteger("board", 5);
                break;
            case 30:
                phaseName.text = "Fase 7: Defini��o e tipo de desbridamento de les�o por press�o�:";
                anim.SetInteger("board", 6);
                break;
            case 35:
                phaseName.text = "Fase 8: Tipos de coberturas e dispositivos para tratar les�o por press�o�:";
                anim.SetInteger("board", 7);
                break;
            case 40:
                phaseName.text = "Fase 9: Esta �ltima etapa, t�m por objetivo fornecer aos Profissionais enfermeiros, as condutas terap�uticas de acordo do est�gio da les�o por press�o.";
                anim.SetInteger("board", 8);
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
