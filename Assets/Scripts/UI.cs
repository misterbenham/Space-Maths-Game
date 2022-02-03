using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{

    public Text problemText;
    public Text[] answersText;

    public Image remainingTimeDial;
    private float remainingTimeDialRate;

    public Text endText;



    public static UI instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //input.gameObject.SetActive(false);
        //goButton.gameObject.SetActive(false);
        remainingTimeDialRate = 1.0f / GameManager.instance.timePerProblem;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTimeDial.fillAmount = remainingTimeDialRate * GameManager.instance.remainingTime;
    }

    public void SetProblemText (Problem problem)
    {
        string operatorText = "";

        switch (problem.operation)
        {
            case MathsOperation.Addition: operatorText = " + "; break;
            case MathsOperation.Subtraction: operatorText = " - "; break;
            case MathsOperation.Multiplication: operatorText = " x "; break;
            case MathsOperation.Division: operatorText = " / "; break;
        }

        problemText.text = problem.firstNumber + operatorText + problem.secondNumber;

        for(int index = 0; index < answersText.Length; ++index)
        {
            answersText[index].text = problem.answers[index].ToString();
        }
    }

    public void SetEndText (bool win)
    {
        endText.gameObject.SetActive(true);
        //input.gameObject.SetActive(true);
        //goButton.gameObject.SetActive(true);

        if (win)
        {
            endText.text = "You Win!";
            endText.color = Color.green;
        }
        else
        {
            endText.text = "Game Over!";
            endText.color = Color.red;
        }
    }
}
