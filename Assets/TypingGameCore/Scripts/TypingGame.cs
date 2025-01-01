using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YmdTypingGame
{
    public class TypingGame : MonoBehaviour
    {
        [SerializeField] public TypingGameSettings settings;
        [SerializeField] private TypingQuestionsData typingQuestionsData;
        [SerializeField] private float gameTime = 60f;
        public TypingGameView view;
        public List<string> targetHiragana; // 現在のひらがな単語
        public int currentHiraganaIndex = 0; // 現在のひらがなの位置
        public string currentInput = ""; // 現在のローマ字入力
        private bool isNPending = false; // 「ん」の入力が保留中か
        private bool isValidContinue = false; // 「ん」の入力の確定後に続けて入力処理をするか
        public bool IsTest = false; // テストモードかどうか
        public string currentInputText = ""; // 現在の入力文字列
        public string beforeInput = ""; // 一つ前の入力文字列

        public string questionHiragana = "";
        int colorIndex = 0;

        private float transitionTime = 0f;
        private bool cannotInput = false;
        private bool isTransition = false;

        private TypingQuestions[] questions = null;

        private bool gameStarted = false;
        private bool isGameOver = false;

        private void Start()
        {
            // テストモードの場合は設定をせずにゲーム開始
            if (IsTest) return;
        }

        private void GameStart()
        {
            if (typingQuestionsData != null)
            {
                questions = typingQuestionsData.questions.ToArray();
            }
            else
            {
                questions = Resources.Load<TypingQuestionsData>("TypingQuestions").questions.ToArray();
            }

            view.Reset();

            Next();
        }

        public void SetQuestion(string question)
        {
            view.OnSetQuestion(question);
        }

        public void SetTargetHiragana(string hiragana)
        {
            targetHiragana = SplitHiragana(hiragana);
            currentHiraganaIndex = 0;
            currentInput = "";
            beforeInput = "";
            isNPending = false;
            currentInputText = "";
            questionHiragana = hiragana;
            view.OnSetHiragana(hiragana);
            AddDynamicMappings();
        }

        private void AddDynamicMappings()
        {
            for (var i = 0; i < targetHiragana.Count - 1; i++)
            {
                var current = targetHiragana[i];
                var next = targetHiragana[i + 1];
                // 次の文字が小文字でも「っ」の場合はマッピングをしない
                if (next == "っ") continue;

                // 次の文字が小文字の場合
                if (!IsSmallCharacter(next[0])) continue;
                string combined = current + next;

                // 既存のマッピングに存在しない場合、新しいマッピングを追加
                if (RomajiMapping.HiraganaToRomaji.ContainsKey(combined)) continue;
                // 推測されるローマ字を作成
                if (!RomajiMapping.HiraganaToRomaji.TryGetValue(current, out var currentRomajiList) ||
                    !RomajiMapping.HiraganaToRomaji.TryGetValue(next, out var nextRomajiList)) continue;
                foreach (string romaji1 in currentRomajiList)
                {
                    foreach (var newRomaji in nextRomajiList.Select(romaji2 => romaji1 + romaji2))
                    {
                        RomajiMapping.AddMapping(combined, newRomaji);
                        Debug.Log($"自動マッピング追加: {combined} -> {newRomaji}");
                    }
                }
            }
        }

        private List<string> SplitHiragana(string hiragana)
        {
            var result = new List<string>();
            for (var i = 0; i < hiragana.Length; i++)
            {
                var current = hiragana[i].ToString();

                // 次の文字が小さい文字の場合（拗音や促音）
                if (i + 1 < hiragana.Length && IsSmallCharacter(hiragana[i + 1]))
                {
                    if (RomajiMapping.HiraganaToRomaji.TryGetValue(current + hiragana[i + 1], out _))
                    {
                        // 拗音（「ぎょ」など）を1文字として扱う
                        current += hiragana[i + 1];

                        // 二つの文字が元々対応している文字を連結させたものを新たなマッピングとして追加登録
                        foreach (var s in RomajiMapping.HiraganaToRomaji[hiragana[i].ToString()])
                        {
                            foreach (string s2 in RomajiMapping.HiraganaToRomaji[hiragana[i + 1].ToString()]
                                         .Where(s2 => !RomajiMapping.HiraganaToRomaji[current].Contains(s)))
                            {
                                RomajiMapping.HiraganaToRomaji[current].Add(s + s2);
                            }
                        }

                        i++;
                    }
                    else
                    {
                        // 分けて処理
                        result.Add(current);
                        current = hiragana[i + 1].ToString();
                        i++;
                    }
                }

                result.Add(current);
            }

            return result;
        }

        private static bool IsSmallCharacter(char c)
        {
            return "ぁぃぅぇぉゃゅょっ".Contains(c);
        }

        private IEnumerator WaitAndGameStart()
        {
            yield return new WaitForSeconds(0.1f);
            GameStart();
        }

        void Update()
        {
            if (!gameStarted)
            {
                if (!Input.GetKeyDown(KeyCode.Space)) return;
                gameStarted = true;
                StartCoroutine(WaitAndGameStart());

                return;
            }
            if(isGameOver) return;
            if(gameTime > 0)
            {
                gameTime -= Time.deltaTime;
                view.SetTime(gameTime);
            }
            else
            {
                view.SetTime(0f);
                isGameOver = true;
                return;
            }
            if(cannotInput) return;

            if (isTransition && (transitionTime -= Time.deltaTime) > 0) return;
            {
                if (transitionTime < 0f)
                {
                    isTransition = false;
                    transitionTime = 0;
                    Next(); // 次の単語に進む
                }
            }
            foreach (char c in Input.inputString)
            {
                HandleInput(c);
                break;
            }
        }

        public void HandleInput(char inputChar)
        {
            // 空白は無視
            if (inputChar == ' ')
            {
                return;
            }

            beforeInput = currentInput; // 入力履歴を保存

            // 「っ」の処理
            if (currentHiraganaIndex < targetHiragana.Count &&
                targetHiragana[currentHiraganaIndex] == "っ")
            {
                if (HandleSmallTsu(inputChar))
                {
                    return;
                }
            }

            // 特殊処理: 「ん」に対する処理
            if (currentHiraganaIndex < targetHiragana.Count &&
                targetHiragana[currentHiraganaIndex] == "ん")
            {
                HandleNCharacter(inputChar);

                if (!isValidContinue)
                {
                    return;
                }
                isValidContinue = false;
            }

            // 入力を追加
            currentInput += inputChar;

            // 入力が有効か確認
            if (IsCurrentInputValid())
            {
                // 入力が有効な場合、completeTextを更新
                currentInputText += inputChar;
                // 現在の文字が完成した場合
                if (IsCurrentCharacterComplete())
                {
                    // 確定処理を実行
                    OnComplete();
                    // 次の文字の処理を開始する
                    if (!string.IsNullOrEmpty(currentInput))
                    {
                        char nextChar = currentInput[0];
                        currentInput = currentInput.Substring(1); // 最初の文字を切り捨て
                        HandleInput(nextChar);
                    }
                }

                view.OnInputCollect(currentInputText);
            }
            else
            {
                OnMiss();
            }
        }

        /// <summary>
        /// ミス時の処理
        /// </summary>
        private void OnMiss()
        {
            Debug.Log($"不正解: {currentInput}");
            // 入力を元に戻す
            currentInput = beforeInput;
            view.OnMiss();
            StartCoroutine(WaitMissDuration());
        }

        /// <summary>
        /// ミス時の入力待ち処理
        /// </summary>
        private IEnumerator WaitMissDuration()
        {
            cannotInput = true;
            yield return new WaitForSeconds(settings.missWaitTime);
            cannotInput = false;
        }

        private void OnComplete()
        {
            Debug.Log($"正しい入力: {currentInput}");
            colorIndex += targetHiragana[currentHiraganaIndex].Length;
            currentInput = ""; // 入力をリセット
            currentHiraganaIndex++; // 次の文字へ
            // 今のひらがなの確定したところまでを赤文字に変更する
            var tmpHiragana = questionHiragana;
            tmpHiragana = tmpHiragana.Insert(colorIndex, "</color>");
            tmpHiragana = tmpHiragana.Insert(0, "<color=red>");
            view.OnComplete(tmpHiragana);

            // 最後の文字の場合
            if (currentHiraganaIndex >= targetHiragana.Count)
            {
                Debug.Log("単語完成！");
                view.OnCompleteQuestion();
                if (!IsTest)
                {
                    isTransition = true;
                    transitionTime = settings.completeWaitTime;
                }
                else
                {
                    Next();
                }
            }
            else if (targetHiragana[currentHiraganaIndex] == " ")
            {
                currentHiraganaIndex++; // スペースをスキップ
            }
        }

        public void Next()
        {
            colorIndex = 0;
            if (IsTest) return;
            var question = questions[Random.Range(0, questions.Length)];
            SetQuestion(question.question);
            SetTargetHiragana(question.hiragana);
        }

        private bool HandleSmallTsu(char inputChar)
        {
            if (currentHiraganaIndex + 1 >= targetHiragana.Count)
            {
                Debug.LogError("「っ」の次の文字が範囲外です");
                return false;
            }

            var nextHiragana = targetHiragana[currentHiraganaIndex + 1];

            // 「っ」に対応する特殊ローマ字を取得（ltu, xtu など）
            if (RomajiMapping.HiraganaToRomaji.TryGetValue("っ", out var smallTsuRomaji))
            {
                var currentInputWithChar = currentInput + inputChar;

                // 完全一致の場合、「っ」として確定
                if (smallTsuRomaji.Contains(currentInputWithChar))
                {
                    Debug.Log($"「っ」として確定: {currentInputWithChar}");
                    currentInputText += currentInputWithChar;
                    view.OnInputCollect(currentInputText);
                    currentInput = ""; // 入力をリセット
                    currentHiraganaIndex++; // 「っ」を消費
                    colorIndex++;
                    return true;
                }

                // 部分一致の場合、「っ」の入力途中
                if (smallTsuRomaji.Any(romaji => romaji.StartsWith(currentInputWithChar)))
                {
                    Debug.Log($"「っ」の特殊ローマ字入力中: {currentInputWithChar}");
                    currentInput += inputChar; // 入力を進める
                    view.OnInputCollect(currentInputText + currentInput);
                    return true;
                }
            }

            // 次のひらがな文字に対応するローマ字候補を取得（繰り返し文字）
            if (RomajiMapping.HiraganaToRomaji.TryGetValue(nextHiragana, out var nextRomaji))
            {
                var repeatedChar = inputChar.ToString();

                if (nextRomaji.Any(romaji => romaji.StartsWith(repeatedChar)))
                {
                    Debug.Log($"「っ」の繰り返し文字: {inputChar}");
                    currentInputText += inputChar; // 完了した入力を反映
                    view.OnInputCollect(currentInputText);
                    currentInput = ""; // 入力をリセット
                    currentHiraganaIndex++; // 「っ」を消費
                    colorIndex++;
                    return true;
                }
            }

            Debug.Log($"「っ」の次の文字が無効: {inputChar}");
            currentInput = ""; // 入力をリセット
            return false; // 無効な入力
        }

        /// <summary>
        /// 「ん」に対する特殊処理
        /// </summary>
        private bool HandleNCharacter(char inputChar)
        {
            switch (isNPending)
            {
                // nn が入力された場合、「ん」を確定
                case true when inputChar == 'n':
                    currentInputText += inputChar;
                    Debug.Log($"「ん」として確定:" + currentInputText);
                    view.OnInputCollect(currentInputText);
                    // 入力をリセット
                    currentInput = "";
                    // 保留解除
                    isNPending = false;
                    beforeInput = "";
                    OnComplete();
                    return true;
                // nが保留中で次の文字が母音の場合は失敗とする
                case true when "aiueo".Contains(inputChar):
                    return false;
                // nが保留中で次の文字が母音でない場合は保留解除して処理を続行
                case true:
                    Debug.Log($"保留中の「ん」を確定: n");
                    view.OnInputCollect(currentInputText + inputChar);
                    // 保留解除
                    isNPending = false;
                    OnComplete();
                    beforeInput = "";
                    isValidContinue = true;
                    return true;
            }

            // `n` を1回入力した場合は保留
            if (inputChar == 'n')
            {
                Debug.Log($"n保留: {inputChar}");
                currentInputText += inputChar;
                view.OnInputCollect(currentInputText);
                isNPending = true;
                return true; // 次の入力を待つ
            }

            // それ以外の場合は無効
            return false;
        }
        
        /// <summary>
        /// 今の入力が有効か
        /// </summary>
        private bool IsCurrentInputValid()
        {
            if (currentHiraganaIndex >= targetHiragana.Count)
            {
                Debug.Log($"無効: currentHiraganaIndexが範囲外 ({currentHiraganaIndex}/{targetHiragana.Count})");
                return false;
            }

            var currentHiragana = targetHiragana[currentHiraganaIndex];

            // 「っ」の特殊処理。現在の文字が「っ」の場合で、次の文字がある場合繰り返しで有効にする
            if (currentHiragana == "っ" && currentHiraganaIndex + 1 < targetHiragana.Count)
            {
                var nextHiragana = targetHiragana[currentHiraganaIndex + 1];
                // 次の文字がない場合は通常の部分一致チェック
                if (!RomajiMapping.HiraganaToRomaji.TryGetValue(nextHiragana, out var nextRomaji))
                {
                    return NormalValid(currentHiragana);
                }
                  
                // 次の文字が繰り返し文字の部分一致かどうかを確認
                var repeatedChar = currentInput.Length > 0 ? currentInput[^1].ToString() : "";
                // 次の文字の最初と今の入力の最後が一致するかどうか
                if (nextRomaji.Any(romaji => romaji.StartsWith(repeatedChar)))
                {
                    Debug.Log($"「っ」の繰り返し候補: {currentInput}");
                    // 次の文字が繰り返し文字の部分一致の場合は有効
                    return true;
                }
            }

            // 通常の部分一致チェック
            return NormalValid(currentHiragana);
        }

        /// <summary>
        /// 通常の部分一致チェック
        /// </summary>
        private bool NormalValid(string currentHiragana)
        {
            // 通常の部分一致チェック
            if (!RomajiMapping.HiraganaToRomaji.TryGetValue(currentHiragana, out var validRomaji)) return false;
            foreach (var romaji in validRomaji)
            {
                if (!romaji.StartsWith(currentInput)) continue;
                Debug.Log($"有効な部分入力: {currentInput} -> {currentHiragana}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 今の文字が完全に入力されたかどうかを確認
        /// </summary>
        private bool IsCurrentCharacterComplete()
        {
            if (currentHiraganaIndex >= targetHiragana.Count) return false;

            var currentHiragana = targetHiragana[currentHiraganaIndex];
            // 今の文字がマッピングに含まれているものがあるか見る
            if (RomajiMapping.HiraganaToRomaji.TryGetValue(currentHiragana, out var validRomaji))
            {
                // 完全一致で確定
                return validRomaji.Contains(currentInput);
            }

            return false;
        }
    }
}