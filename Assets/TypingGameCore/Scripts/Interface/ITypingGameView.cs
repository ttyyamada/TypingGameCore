namespace YmdTypingGame
{
    public interface ITypingGameView
    {
        void Reset();
        void OnSetQuestion(string question);
        void OnSetHiragana(string hiragana);
        void OnInputCollect(string input);
        void OnComplete(string completeText);
        void OnCollect(string colletedText);
        void OnMiss();
        
        void OnCompleteQuestion();
    }
}