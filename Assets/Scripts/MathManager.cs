using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MathManager : MonoBehaviour
{
    public GameObject correctText;
    public GameObject wrongText;

    public AudioSource audio;

    public Text equationText;
    public Text answer1Text;
    public Text answer2Text;
    public Text answer3Text;
    public Text scoreText;
    public Text missText;

    public int answer1;
    public int answer2;
    public int answer3;

    public int leftNumber;
    public int rightNumber;
    public int result;

    public int[] multiDiff;
    public int[] diffRange;
    public int rangeIndex;

    public int score;
    public int miss;

    public int answerLocation;

    // Start is called before the first frame update
    void Start()
    {
        ChooseGame();

        //reset all game values
        rangeIndex = 0;
        score = 0;
        miss = 0;

        //update UI on start
        scoreText.text = "Score:" + score;
        missText.text = "Wrong:" + miss;
    }

    public void ChooseGame()
    {
        switch (GameManager.instance.gameMode)
        {
            case 1:
                AddGame();
                break;
            case 2:
                SubGame();
                break;
            case 3:
                MultiGame();
                break;
            case 4:
                ComboGame();
                break;
        }
    }

    public void AddGame()
    {
        //generate the equation
        leftNumber = Random.Range(1, diffRange[rangeIndex]);
        rightNumber = Random.Range(1, diffRange[rangeIndex]);
        result = leftNumber + rightNumber;

        //display the equation
        equationText.text = leftNumber + " + " + rightNumber;

        //display the possible answers
        PlaceAnswers();
    }

    public void SubGame()
    {
        //generate the equation
        leftNumber = Random.Range(1, diffRange[rangeIndex]);
        rightNumber = Random.Range(1, diffRange[rangeIndex]);
        result = leftNumber - rightNumber;

        //display the equation
        equationText.text = leftNumber + " - " + rightNumber;

        //display the possible answers
        PlaceAnswers();
    }

    public void MultiGame()
    {
        //generate the equation
        leftNumber = Random.Range(0, multiDiff[rangeIndex]);
        rightNumber = Random.Range(0, multiDiff[rangeIndex]);
        result = leftNumber * rightNumber;

        //display the equation
        equationText.text = leftNumber + " x " + rightNumber;

        //display the possible answers
        PlaceAnswers();
    }

    public void ComboGame()
    {
        int choice = Random.Range(1, 101);
        if (choice <= 33)
        {
            AddGame();
        }
        else if (choice <= 66)
        {
            SubGame();
        }
        else
        {
            MultiGame();
        }
    }

    public void PlaceAnswers()
    {
        answerLocation = Random.Range(1, 4);
        switch (answerLocation)
        {
            case 1:
                answer1 = result;
                answer1Text.text = answer1.ToString();
                 
                answer2 = GenerateWrongAnswer(result);
                answer2Text.text = answer2.ToString();

                answer3 = GenerateWrongAnswer(result);
                if (answer3 == answer2)
                {
                    answer3 = ChangeNumber(answer3);
                }
                answer3Text.text = answer3.ToString();
                break;
            case 2:
                answer1 = GenerateWrongAnswer(result);
                answer1Text.text = answer1.ToString();

                answer2 = result;
                answer2Text.text = answer2.ToString();

                answer3 = GenerateWrongAnswer(result);
                if (answer3 == answer1)
                {
                    answer3 = ChangeNumber(answer3);
                }
                answer3Text.text = answer3.ToString();

                break;
            case 3:

                answer1 = GenerateWrongAnswer(result);
                answer1Text.text = answer1.ToString();

                answer2 = GenerateWrongAnswer(result);
                if (answer2 == answer1)
                {
                    answer2 = ChangeNumber(answer2);
                }
                answer2Text.text = answer2.ToString();

                answer3 = result;
                answer3Text.text = answer3.ToString();
                break;
        }
    }

    public int ChangeNumber(int number)
    {
        int newNumber = number;

        if (Random.Range(1, 101) <= 50)
        {
            newNumber += Random.Range(1, 3);
            if (newNumber == result)
                newNumber += 1;
        }
        else
        {
            newNumber -= Random.Range(1, 3);
            if (newNumber == result)
                newNumber -= 1;
        }

        return newNumber;
    }

    public int GenerateWrongAnswer(int answer)
    {
        //cahnge the wrong answers so they are either lower or higher than the answer
        int number = 0;
        if (Random.Range(1, 101) <= 50)
        {
            number = answer + Random.Range(1, 5);
            return number;
        }
        else
        {
            number = answer - Random.Range(1, 5);
            return number;
        }
    }

    //FOR TESTING CHANGE TO TEXT
    public void CheckAnswer(Text answerText)
    {
        string test = answerText.text;
        int answer = int.Parse(test);

        if (answer == result)
        {
            score++;

            audio.Play();

            if (score % 5 == 0 && (rangeIndex < diffRange.Length && rangeIndex < multiDiff.Length))
            {
                rangeIndex++;
            }
            scoreText.text = "Score:" + score;
            StartCoroutine(DisplayScore(true));
        }
        else
        {
            miss++;
            missText.text = "Wrong:" + miss;
            StartCoroutine(DisplayScore(false));
        }


        if (miss >= 3)
        {
            //end the game
            SceneManager.LoadScene("MenuScene");
        }

    }

    IEnumerator DisplayScore(bool isCorrect)
    {
        GameObject tempObj;

        if (isCorrect)
            tempObj = correctText;
        else
            tempObj = wrongText;

        tempObj.SetActive(true);

        yield return new WaitForSeconds(1);

        tempObj.SetActive(false);

        if(isCorrect)
            ChooseGame();
    }
}
