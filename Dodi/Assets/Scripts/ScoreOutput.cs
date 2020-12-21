using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ScoreOutput : MonoBehaviour
{
    public Text player1;
    public Text player2;
    public Text player3;
    public Text player4;

    public Text question;
    public Text answer;
    private float correctScore;
    private Dictionary<string, float> playerList;
    private Dictionary<string, float> differenceList;

    void Start()
    {

        DodiData data = SaveSystem.LoadData();
        if (PlayerPrefs.GetString("activeGame").Equals("water"))
        {
            correctScore = data.currentQuestionsListPro.ElementAt(data.questionIndexPro).Value;
        }

        question.text = "Frage: "+data.currentQuestionsListPro.ElementAt(data.questionIndexPro).Key;
        answer.text = "Antwort: "+correctScore.ToString()+"%";
        playerList = new Dictionary<string, float>();
        differenceList = new Dictionary<string, float>();
        for (int i=1; i<5; i++)
        {
            playerList.Add(PlayerPrefs.GetString(i.ToString()), PlayerPrefs.GetFloat(PlayerPrefs.GetString(i.ToString())));

            differenceList.Add(playerList.ElementAt(i-1).Key, Math.Abs(correctScore - playerList.ElementAt(i-1).Value));
        }

        var sortedList = from entry in differenceList orderby correctScore - entry.Value descending select entry;

        player1.text = "Platz 1: " + sortedList.ElementAt(0).Key + ": " + Math.Round(sortedList.ElementAt(0).Value, 2).ToString().Trim('[', ']')  + "% unterschied";
        player2.text = "Platz 2: " + sortedList.ElementAt(1).Key + ": " + Math.Round(sortedList.ElementAt(1).Value, 2).ToString().Trim('[', ']') + "% unterschied";
        player3.text = "Platz 3: " + sortedList.ElementAt(2).Key + ": " + Math.Round(sortedList.ElementAt(2).Value, 2).ToString().Trim('[', ']') + "% unterschied";
        player4.text = "Platz 4: " + sortedList.ElementAt(3).Key + ": " + Math.Round(sortedList.ElementAt(3).Value, 2).ToString().Trim('[', ']') + "% unterschied";
    }

    
}
