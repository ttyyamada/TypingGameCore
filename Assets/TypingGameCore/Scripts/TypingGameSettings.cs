namespace YmdTypingGame
{
    [System.Serializable]
    public class TypingGameSettings
    {
        public float completeWaitTime = 0.3f;
        public float missWaitTime = 0.7f;
        public int[] nextStateCount { get; set; }  = { 2, 4, 7, 9, 12 };
        public float gameTime = 60f;
    }
}