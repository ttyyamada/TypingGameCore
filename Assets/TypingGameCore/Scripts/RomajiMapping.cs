using System.Collections.Generic;
using System.Linq;

public class RomajiMapping
{
    public static readonly Dictionary<string, List<string>> HiraganaToRomaji = new Dictionary<string, List<string>>()
    {
        // あ行
        { "あ", new List<string> { "a" } },
        { "い", new List<string> { "i" } },
        { "う", new List<string> { "u", "whu", "wu" } },
        { "え", new List<string> { "e" } },
        { "お", new List<string> { "o" } },
        { "ぁ", new List<string> { "la", "xa" } },
        { "ぃ", new List<string> { "li", "xi" } },
        { "ぅ", new List<string> { "lu", "xu" } },
        { "ぇ", new List<string> { "le", "xe" } },
        { "ぉ", new List<string> { "lo", "xo" } },

        // や行
        { "や", new List<string> { "ya" } },
        { "ゆ", new List<string> { "yu" } },
        { "よ", new List<string> { "yo" } },
        { "ゃ", new List<string> { "xya", "lya" } },
        { "ゅ", new List<string> { "xyu", "lyu" } },
        { "ょ", new List<string> { "xyo", "lyo" } },
        { "っ", new List<string> { "ltu", "xtu" } },
        

        // か行
        { "か", new List<string> { "ka" } },
        { "き", new List<string> { "ki" } },
        { "く", new List<string> { "ku", "qu" } },
        { "け", new List<string> { "ke" } },
        { "こ", new List<string> { "ko" } },
        { "きゃ", new List<string> { "kya" } },
        { "きゅ", new List<string> { "kyu" } },
        { "きょ", new List<string> { "kyo" } },
        { "ぎ", new List<string> { "gi" } },
        { "ぎゃ", new List<string> { "gya" } },
        { "ぎゅ", new List<string> { "gyu" } },
        { "ぎょ", new List<string> { "gyo","gilyo" } },
        { "が", new List<string> { "ga" } },
        { "ぐ", new List<string> { "gu" } },
        { "げ", new List<string> { "ge" } },
        { "ご", new List<string> { "go" } },

        // さ行
        { "さ", new List<string> { "sa" } },
        { "し", new List<string> { "shi", "si" } },
        { "す", new List<string> { "su" } },
        { "せ", new List<string> { "se" } },
        { "そ", new List<string> { "so" } },
        { "しゃ", new List<string> { "sha", "sya" } },
        { "しゅ", new List<string> { "shu", "syu" } },
        { "しょ", new List<string> { "sho", "syo" } },
        { "ざ", new List<string> { "za" } },
        { "じ", new List<string> { "ji", "zi" } },
        { "ず", new List<string> { "zu" } },
        { "ぜ", new List<string> { "ze" } },
        { "ぞ", new List<string> { "zo" } },
        { "じぇ", new List<string> { "je", "zye"} },
        { "じゃ", new List<string> { "ja", "jya", "zya" } },
        { "じゅ", new List<string> { "ju", "jyu", "zyu" } },
        { "じょ", new List<string> { "jo", "jyo", "zyo" } },

        // た行
        { "た", new List<string> { "ta" } },
        { "ち", new List<string> { "chi", "ti" } },
        { "つ", new List<string> { "tsu", "tu" } },
        { "て", new List<string> { "te" } },
        { "と", new List<string> { "to" } },
        { "ちゃ", new List<string> { "cha", "cya", "tya" } },
        { "ちゅ", new List<string> { "chu", "cyu", "tyu" } },
        { "ちょ", new List<string> { "cho", "cyo", "tyo" } },
        { "だ", new List<string> { "da" } },
        { "ぢ", new List<string> { "di" } },
        { "づ", new List<string> { "du" } },
        { "で", new List<string> { "de" } },
        { "ど", new List<string> { "do" } },
        { "ぢゃ", new List<string> { "dya" } },
        { "ぢゅ", new List<string> { "dyu" } },
        { "ぢょ", new List<string> { "dyo" } },

        // な行
        { "な", new List<string> { "na" } },
        { "に", new List<string> { "ni" } },
        { "ぬ", new List<string> { "nu" } },
        { "ね", new List<string> { "ne" } },
        { "の", new List<string> { "no" } },
        { "にゃ", new List<string> { "nya" } },
        { "にゅ", new List<string> { "nyu" } },
        { "にょ", new List<string> { "nyo" } },

        // は行
        { "は", new List<string> { "ha" } },
        { "ひ", new List<string> { "hi" } },
        { "ふ", new List<string> { "fu", "hu" } },
        { "へ", new List<string> { "he" } },
        { "ほ", new List<string> { "ho" } },
        { "ひゃ", new List<string> { "hya" } },
        { "ひゅ", new List<string> { "hyu" } },
        { "ひょ", new List<string> { "hyo" } },
        { "ば", new List<string> { "ba" } },
        { "び", new List<string> { "bi" } },
        { "ぶ", new List<string> { "bu" } },
        { "べ", new List<string> { "be" } },
        { "ぼ", new List<string> { "bo" } },
        { "ぱ", new List<string> { "pa" } },
        { "ぴ", new List<string> { "pi" } },
        { "ぷ", new List<string> { "pu" } },
        { "ぺ", new List<string> { "pe" } },
        { "ぽ", new List<string> { "po" } },
        { "ぴゃ", new List<string> { "pya" } },
        { "ぴぃ", new List<string> { "pyi" } },
        { "ぴゅ", new List<string> { "pyu" } },
        { "ぴぇ", new List<string> { "pye" } },
        { "ぴょ", new List<string> { "pyo" } },

        // ま行
        { "ま", new List<string> { "ma" } },
        { "み", new List<string> { "mi" } },
        { "む", new List<string> { "mu" } },
        { "め", new List<string> { "me" } },
        { "も", new List<string> { "mo" } },
        { "みゃ", new List<string> { "mya" } },
        { "みゅ", new List<string> { "myu" } },
        { "みょ", new List<string> { "myo" } },

        // ら行
        { "ら", new List<string> { "ra" } },
        { "り", new List<string> { "ri" } },
        { "る", new List<string> { "ru" } },
        { "れ", new List<string> { "re" } },
        { "ろ", new List<string> { "ro" } },
        { "りゃ", new List<string> { "rya" } },
        { "りゅ", new List<string> { "ryu" } },
        { "りょ", new List<string> { "ryo" } },

        // わ行
        { "わ", new List<string> { "wa" } },
        { "を", new List<string> { "wo" } },
        { "ん", new List<string> { "nn", "xn" } },

        // 特殊音
        { "ヴ", new List<string> { "vu" } },
        { "ゔ", new List<string> { "vu" } },
        { "ー", new List<string> { "-" } },
        { "。", new List<string> { "." } },
        { "、", new List<string> { "," } },
        { "!", new List<string> { "!" } },
        { "！", new List<string> { "!" } },
        { "?", new List<string> { "?" } },
        { "？", new List<string> { "?" } },
        { "1", new List<string> { "1" } },
        { "2", new List<string> { "2" } },
        { "3", new List<string> { "3" } },
        { "4", new List<string> { "4" } },
        { "5", new List<string> { "5" } },
        { "6", new List<string> { "6" } },
        { "7", new List<string> { "7" } },
        { "8", new List<string> { "8" } },
        { "9", new List<string> { "9" } },
        { "%", new List<string> { "%" } },
        { "１", new List<string> { "1" } },
        { "２", new List<string> { "2" } },
        { "３", new List<string> { "3" } },
        { "４", new List<string> { "4" } },
        { "５", new List<string> { "5" } },
        { "６", new List<string> { "6" } },
        { "７", new List<string> { "7" } },
        { "８", new List<string> { "8" } },
        { "９", new List<string> { "9" } },
        { "％", new List<string> { "%" } },
        { ".", new List<string> { "." } },
        { ",", new List<string> { "," } },
        { "_", new List<string> { "_" } },
        { "てぃ", new List<string> { "thi" } },
        { "てゅ", new List<string> { "thu" } },
        { "てぇ", new List<string> { "the" } },
        { "てょ", new List<string> { "tho" } },
        
        { "りぃ", new List<string> { "ryi" } },
        { "りぇ", new List<string> { "rye" } },
        { "ゎ", new List<string> { "lwa", "xwa" } },
        { "ゐ", new List<string> { "wyi" } },
        { "ゑ", new List<string> { "wye" } },
        { "ゔぁ", new List<string> { "va" } },
        { "ゔぃ", new List<string> { "vi", "vyi" } },
        { "ゔぇ", new List<string> { "ve", "vye" } },
        { "ゔぉ", new List<string> { "vo" } },
        { "ゔゃ", new List<string> { "vya" } },
        { "ゔゅ", new List<string> { "vyu" } },
        { "ゔょ", new List<string> { "vyo" } },
        { "ヵ", new List<string> { "lka", "xka" } },
        { "ヶ", new List<string> { "lke", "xke" } },
        { "ヷ", new List<string> { "lva", "xva" } },
        { "ヸ", new List<string> { "lvi", "xvi" } },
        { "ヹ", new List<string> { "lve", "xve" } },
        { "ヺ", new List<string> { "lvo", "xvo" } },
        { "ヽ", new List<string> { "lhi", "lhe", "lhu", "lho" } },
        { "ヾ", new List<string> { "lhi", "lhe", "lhu", "lho" } },
        { "ヿ", new List<string> { "lka", "lke" } },
        { "ゝ", new List<string> { "lhi", "lhe", "lhu", "lho" } },
        { "ゞ", new List<string> { "lhi", "lhe", "lhu", "lho" } },
        { "ゟ", new List<string> { "lka", "lke" } },
        { "゠", new List<string> { "lka", "lke" } },
        { "ァ", new List<string> { "la", "xa" } },
        { "ア", new List<string> { "a" } },
        { "ィ", new List<string> { "li", "xi" } },
        { "イ", new List<string> { "i" } },
        { "ゥ", new List<string> { "lu", "xu" } },
        { "ウ", new List<string> { "u", "whu", "wu" } },
        { "ェ", new List<string> { "le", "xe" } },
        { "エ", new List<string> { "e" } },
        { "ォ", new List<string> { "lo", "xo" } },
        { "オ", new List<string> { "o" } },
        { "カ", new List<string> { "ka" } },
        { "ガ", new List<string> { "ga" } },
        { "キ", new List<string> { "ki" } },
        { "ギ", new List<string> { "gi" } },
        { "ク", new List<string> { "ku", "qu" } },
        { "グ", new List<string> { "gu" } },
        { "ケ", new List<string> { "ke" } },
        { "ゲ", new List<string> { "ge" } },
        { "コ", new List<string> { "ko" } },
        { "ゴ", new List<string> { "go" } },
        { "サ", new List<string> { "sa" } },
        { "ザ", new List<string> { "za" } },
        { "シ", new List<string> { "shi", "si" } },
        { "ジ", new List<string> { "ji", "zi" } },
        { "ス", new List<string> { "su" } },
        { "ズ", new List<string> { "zu" } },
        { "セ", new List<string> { "se" } },
        { "ゼ", new List<string> { "ze" } },
        { "ソ", new List<string> { "so" } },
        { "ゾ", new List<string> { "zo" } },
        { "タ", new List<string> { "ta" } },
        { "ダ", new List<string> { "da" } },
        { "チ", new List<string> { "chi", "ti" } },
        { "ヂ", new List<string> { "di" } },
        { "ッ", new List<string> { "xtu", "ltu", "ltsu" } },
        { "ツ", new List<string> { "tsu", "tu" } },
        { "ヅ", new List<string> { "du" } },
        { "テ", new List<string> { "te" } },
        { "デ", new List<string> { "de" } },
        { "ト", new List<string> { "to" } },
        { "ふぁ", new List<string> {"fa"} },
        { "ふぃ", new List<string> {"fi"} },
        { "ふぇ", new List<string> {"fe"} },
        { "ふぉ", new List<string> {"fo"} },
    };
    
    public static void AddMapping(string hiragana, string romaji)
    {
        if (HiraganaToRomaji.TryGetValue(hiragana, out List<string> romajiList))
        {
            if(romajiList.Contains(romaji)) return;
            romajiList.Add(romaji);
        }
        else
        {
            HiraganaToRomaji.Add(hiragana, new List<string> { romaji });
        }
    }
}

public static class RomajiConverter
{
    public static List<string> ConvertHiraganaToRomajiCandidates(string hiragana)
    {
        var result = new List<string> { "" };

        foreach (char character in hiragana)
        {
            string key = character.ToString();
            if (RomajiMapping.HiraganaToRomaji.TryGetValue(key, out List<string> romajiCandidates))
            {
                // 現在の結果リストに対して候補を展開
                result = result.SelectMany(r => romajiCandidates.Select(c => r + c)).ToList();
            }
            else
            {
                // マッピングが見つからない場合、そのまま追加
                result = result.Select(r => r + key).ToList();
            }
        }

        return result;
    }
}