using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DodiData
{
    public Dictionary<string, float> currentQuestionsListNum;
    public Dictionary<string, string> currentQuestionsListStr;
    public Dictionary<string, float> currentQuestionsListPro;




    public int questionIndexNum;
    public int questionIndexStr;
    public int questionIndexPro;

    public DodiData (FileManager manager)
    {
        currentQuestionsListNum = manager.questionsListNum;
        currentQuestionsListStr = manager.questionsListStr;
        currentQuestionsListPro = manager.questionsListPro;

        questionIndexNum = manager.dictCounterNum;
        questionIndexStr = manager.dictCounterStr;
        questionIndexPro = manager.dictCounterPro;


    }

    

}
