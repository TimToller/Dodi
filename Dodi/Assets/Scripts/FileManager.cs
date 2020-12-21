
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class FileManager : MonoBehaviour
{
    
    
    
    //get the text fields: q for question
    [SerializeField] public Text question_field;
    public TextAsset TxtFile;

    //Creates Question Dictionary
    public Dictionary<string, float> questionsListNum = new Dictionary<string, float>();
    public Dictionary<string, string> questionsListStr = new Dictionary<string, string>();
    public Dictionary<string, float> questionsListPro = new Dictionary<string, float>();

    //creates an int for every Dictionary to keep track of the questions
    public int dictCounterNum = 0;
    public int dictCounterStr = 0;
    public int dictCounterPro = 0;


    void Start()
    {
        PlayerPrefs.SetString("1", "Tim");
        PlayerPrefs.SetString("2", "Nadine");
        PlayerPrefs.SetString("3", "Linda");
        PlayerPrefs.SetString("4", "Kiki");


        //starts the ReadFromFile function right away
        ReadFromFile();
        PlayerPrefs.SetString("activeGame", "water");
        //shuffle every Dictionary:

        //shuffle(questionsListNum);
        //shuffle(questionsListStr);
        //shuffle(questionsListPro);
        if (question_field != null)
        {
            print(getQuestion().Key);
            question_field.text = getQuestion().Key;
        }
        
        Save();

    }

    public void ReadFromFile()
    {


        /*if(SaveSystem.LoadData()!=null)
        {
            DodiData data = SaveSystem.LoadData();

            questionsListNum = data.currentQuestionsListNum;
            questionsListStr = data.currentQuestionsListStr;
            questionsListPro = data.currentQuestionsListPro;

            dictCounterNum = data.questionIndexNum;
            dictCounterStr = data.questionIndexStr;
            dictCounterPro = data.questionIndexPro;

            print(data.questionIndexNum);
            print(questionsListNum.ElementAt(dictCounterNum+1));
            
        }*/
        //else
        //{
        //Read every Line in one by one and store it in an Array list
        string questionsUnsplit = TxtFile.ToString();
            string[] questionLines = Regex.Split(questionsUnsplit, Environment.NewLine);



            for (int i = 0; i < questionLines.Length; i++)
            {
                //checks if the Syntax of the Text file is correct (every line contains a ;
                if (!questionLines[i].Contains(';'))
                {
                    //send error if not correct and break
                    Debug.LogError("ERROR, THE TEXT FILE IS FORMATTED WRONG! (Missing ;)");
                    break;
                }

                //split the Line up into two parts
                string[] questionSplit = questionLines[i].Split(';');


                //check if Answer is Word
                if (questionSplit[1].StartsWith("\"") && questionSplit[1].EndsWith("\""))
                {
                    //is word and add to dictionary
                    questionSplit[1] = questionSplit[1].Trim('"');
                    questionsListStr.Add(questionSplit[0], questionSplit[1]);
                }

                //check if only Digit or uses ./,
                else if (questionSplit[1].All(char.IsDigit) || questionSplit[1].Contains('.') || questionSplit[1].Contains(",") || questionSplit[1].Contains("%"))
                {

                    //checks if the answer contains ,
                    if (questionSplit[1].Contains(","))
                    {
                        //replace , with . and send warning message
                        questionSplit[1] = questionSplit[1].Replace(',', '.');
                        Debug.LogWarning("Number Used , instead of . in question line: " + (i + 1) + ", \"" + questionSplit[0] + "\", please replace!");
                    }

                    //check if Answer is Procent
                    if (questionSplit[1].StartsWith("%") && questionSplit[1].EndsWith("%"))
                    {
                        //is Procent and add to dictionary
                        questionSplit[1] = questionSplit[1].Trim('%');
                        questionsListPro.Add(questionSplit[0], float.Parse(questionSplit[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
                    }
                    //is Number answer, add to dictionary
                    else
                    {
                        questionsListNum.Add(questionSplit[0], float.Parse(questionSplit[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
                    }

                }


                else
                {
                    //send error message if not word or number
                    Debug.LogError("ERROR, THE TEXT FILE ANSWER ISN'T FORMATTED CORRECTLY! (Missing \" or mixing words and numbers)");
                    break;

                }
                
            //}
            shuffle();

        }
        Save();
    }
    private void OnApplicationQuit()
    {
        File.Delete(Application.persistentDataPath + "/data.dodi");
    }

    public dynamic getQuestion()
    {
        switch (PlayerPrefs.GetString("activeGame")) {
            case "water":
                return getRandQuestionPro();
            case "basketball":
                return getRandQuestionNum();
            case "chicken":
                return getRandQuestionStr();
            default:
                Debug.LogError("Unknown activeGame! (Spelling?)");
                return null; 
            //etc...
        }
    }
    //shuffels a specific Dictionary(must be a string, float dictionary thogh), this function is probably not necessary beacause we will shuffle everything at once with shuffle(), but good to have 
    public void shuffle(Dictionary<string, float> dict)
    {
        //generates random Number
        System.Random rand = new System.Random();
        //shuffels the dictionary
        dict = dict.OrderBy(x => rand.Next())
            .ToDictionary(item => item.Key, item => item.Value);
        Save();
    }
    //shuffels a specific Dictionary(must be a string, string dictionary thogh), this function is probably not necessary beacause we will shuffle everything at once with shuffle(), but good to have 
    public void shuffle(Dictionary<string, string> dict)
    {
        //generates random Number
        System.Random rand = new System.Random();
        //shuffels the dictionary
        dict = dict.OrderBy(x => rand.Next())
            .ToDictionary(item => item.Key, item => item.Value);
        Save();
    }
    //if no specific dictionary is definned, every dictionary gets shuffled
    public void shuffle()
    {
        System.Random rand = new System.Random();
        questionsListNum = questionsListNum.OrderBy(x => rand.Next())
            .ToDictionary(item => item.Key, item => item.Value);

        questionsListStr = questionsListStr.OrderBy(x => rand.Next())
            .ToDictionary(item => item.Key, item => item.Value);

        questionsListPro = questionsListPro.OrderBy(x => rand.Next())
            .ToDictionary(item => item.Key, item => item.Value);

        //just to be sure reset the counter
        dictCounterNum = 0;
        dictCounterStr = 0;
        dictCounterPro = 0;

        Save();
    }

    //takes a dictionary and int as input to manage the counter
    public int getNextInt(Dictionary<string, string> dict, int counter)
    {
        //counter+1 and make sure it isn't over the Dictionary limit
        counter = counter + 1;
        if (counter >= dict.Count())
        {
            //reset counter and shuffle new if over
            counter = 0;
            shuffle(dict);
        }
        Save();
        return counter;
    }
    //same thing as the function on top, hust for float Dictionarys aswell
    public int getNextInt(Dictionary<string, float> dict, int counter)
    {
        counter = counter + 1;
        if (counter >= dict.Count())
        {
            counter = 0;
            shuffle(dict);
        }
        Save();
        return counter;
    }


    public KeyValuePair<string, string> getRandQuestionStr()
    {
        //create random Number for the QuestionStr Dictionary 
        int index = getNextInt(questionsListStr, dictCounterStr);
        dictCounterStr = index;

        //log the Output Answer for Debugging 
        Debug.Log("Question: " + questionsListStr.ElementAt(index).Key + "==> Answer Output: " + questionsListStr.ElementAt(index).Value);

        KeyValuePair<string, string> keyValuePair = questionsListStr.ElementAt(index);
        Save();
        return keyValuePair;
    }
    public void setRandQuestionStr()
    {
        question_field.text = getRandQuestionStr().Key;
    }

    public KeyValuePair<string, float> getRandQuestionNum()
    {
        //create random Number for the QuestionNum Dictionary 
        int index = getNextInt(questionsListNum, dictCounterNum);

        dictCounterNum = index;

        //log the Output Answer for Debugging 
        Debug.Log("Question: " + questionsListNum.ElementAt(index).Key + "==> Answer Output: " + questionsListNum.ElementAt(index).Value);

        KeyValuePair<string, float> keyValuePair = questionsListNum.ElementAt(index);
        Save();
        return keyValuePair;
    }

    public void setRandQuestionNum()
    {
        question_field.text = getRandQuestionNum().Key;
    }

    public KeyValuePair<string, float> getRandQuestionPro()
    {
        //create random Number for the QuestionPro Dictionary 
        int index = getNextInt(questionsListPro, dictCounterPro);

        dictCounterPro = index;

        //log the Output Answer for Debugging 
        Debug.Log("Question: " + questionsListPro.ElementAt(index).Key + "==> Answer Output: " + questionsListPro.ElementAt(index).Value);

        KeyValuePair<string, float> keyValuePair = questionsListPro.ElementAt(index);
        Save();
        return keyValuePair;
    }
    public void setRandQuestionPro()
    {
        question_field.text = getRandQuestionPro().Key;
    }
    public void Save()
    {
        SaveSystem.saveDodiStats(this);
    }

}
