using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private QuizManager quizManager;

    public string name;
    public int score;
    public bool nameInputBoolean;
    public bool gameComplete;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        nameInputBoolean = true;
    }

    private void Update()
    {
        quizManager = FindObjectOfType<QuizManager>();
        score = quizManager.correctCards;
    }
}
    