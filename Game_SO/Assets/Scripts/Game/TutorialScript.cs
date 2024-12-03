using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    //Variables
    [Header("Components")] 
    [SerializeField] private Animator anim;
    [SerializeField] private Animator staticCanvasAnim;
    [SerializeField] private Animator cardsAnim;
    [SerializeField] private GameObject cardsFatherObject;
    [SerializeField] private GameObject staticCanvas;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private CameraMovement camMovement;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;

    //Generic
    private int transition;


    private void Awake()
    {
        camMovement.enabled = false;
        staticCanvas.SetActive(false);
        cardsFatherObject.SetActive(false);
        transition = 0;

        nextButton.onClick.AddListener(() => NextButton());
        backButton.onClick.AddListener(() => BackButton());
    }

    private void Start()
    {
        transition = 0;
        tutorialCanvas.SetActive(true);
    }
    
    private void Update()
    {
        /*//Checking if its touching screen
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            transition++;
            camMovement.enabled = false;
            anim.SetInteger("transition", transition); //Going to next tutorial text
        }

        if (transition >= 7)
        {
            cardsFatherObject.SetActive(true);
            cardsAnim.SetBool("transition", true);
            staticCanvas.SetActive(true);
            staticCanvasAnim.SetBool("transition", true);
            transition = 0;
            camMovement.enabled = true;
            tutorialCanvas.SetActive(false);
        }*/
    }

    private void NextButton()
    {
        transition++;
        camMovement.enabled = false;
        anim.SetInteger("transition", transition); //Going to next tutorial text

        if (transition >= 7)
        {
            cardsFatherObject.SetActive(true);
            cardsAnim.SetBool("transition", true);
            staticCanvas.SetActive(true);
            staticCanvasAnim.SetBool("transition", true);
            transition = 0;
            camMovement.enabled = true;
            tutorialCanvas.SetActive(false);
        }
    }

    private void BackButton()
    {
        transition--;
        camMovement.enabled = false;
        anim.SetInteger("transition", transition); //Going to next tutorial text

        if (transition <= 0)
        {
            transition = 0;
        }
    }
}
