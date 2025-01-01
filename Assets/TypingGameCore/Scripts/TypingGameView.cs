using UnityEngine;
using UnityEngine.UI;

namespace YmdTypingGame
{
    public class TypingGameView : MonoBehaviour, ITypingGameView
    {
        [SerializeField] private Text questionText;
        [SerializeField] private Text completeText;
        [SerializeField] private Text inputText;
        
        [SerializeField] private Text chainCountText;
        [SerializeField] private Text maxChainCountText;
        [SerializeField] private Text collectedCountText;
        [SerializeField] private Text missCountText;
        [SerializeField] private Text timeText;
        
        private int chainCount = 0;
        private int maxChainCount = 0;
        private int collectedCount = 0;
        private int missCount = 0;
        
        public double GetScore => maxChainCount * 100 + collectedCount * 10;
        
        public void SetTime(float time)
        {
            timeText.text = time.ToString("F2");
        }


        public void Reset()
        {
            chainCount = 0;
            maxChainCount = 0;
            collectedCount = 0;
            missCount = 0;
            chainCountText.text = "0";
            maxChainCountText.text = "0";
            collectedCountText.text = "0";
            missCountText.text = "0";
        }

        public void OnSetQuestion(string question)
        {
            questionText.text = question;
        }

        public void OnSetHiragana(string hiragana)
        {
            inputText.text = "";
            completeText.text = hiragana;
        }

        public void OnInputCollect(string input)
        {
            chainCount++;
            if (chainCount > maxChainCount)
            {
                maxChainCount = chainCount;
                maxChainCountText.text = maxChainCount.ToString();
            }
            chainCountText.text = chainCount.ToString();
            inputText.text = input;
        }

        public void OnComplete(string complete)
        {
            completeText.text = complete;
        }

        public void OnCollect(string colletedText)
        {
            inputText.text = colletedText;
        }

        public void OnMiss()
        {
            missCount++;
            missCountText.text = missCount.ToString();
            chainCount = 0;
        }

        public void OnCompleteQuestion()
        {
            collectedCount++;
            collectedCountText.text = collectedCount.ToString();
        }
    }
}