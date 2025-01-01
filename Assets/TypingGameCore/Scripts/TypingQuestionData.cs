using System.Collections.Generic;
using UnityEngine; 

[CreateAssetMenu(fileName = "TypingQuestionsData", menuName = "TypingGame/QuestionsData", order = 1)]
public class TypingQuestionsData : ScriptableObject
{
    public List<TypingQuestions> questions = new List<TypingQuestions>();
}