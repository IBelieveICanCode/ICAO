using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Adminka;
using UnityEngine;
using UtilityScripts;

public class ICAO {
    private static readonly Dictionary<string, string> Alphabet = new Dictionary<string, string>
        {
        {"A", "Alpha" },
        {"B", "Bravo" },
        {"C", "Charlie" },
        {"D", "Delta" },
        {"E", "Echo" },
        {"F", "Foxtrot" },
        {"G", "Golf" },
        {"H", "Hotel" },
        {"I", "India" },
        {"J", "Juliet" },
        {"K", "Kilo" },
        {"L", "Lima" },
        {"M", "Mike" },
        {"N", "November" },
        {"O", "Oscar" },
        {"P", "Papa" },
        {"Q", "Quebec" },
        {"R", "Romeo" },
        {"S", "Sierra" },
        {"T", "Tango" },
        {"U", "Uniform" },
        {"V", "Victor" },
        {"W", "Whiskey" },
        {"X", "Xray" },
        {"Y", "Yankee" },
        {"Z", "Zulu" }
    };

    public string this[string letter] => Alphabet[letter.ToUpper()] != null ? Alphabet[letter] : null;

    //foreach (KeyValuePair<string, string> pair in Alphabet)
    //{
    //    if (letter.ToUpper().Equals(pair.Key))
    //        return pair.Value;
    //}
    //return null;
    public static Dictionary<string, string> ReturnRandomWordsFromAlphabet(int amountOfWords)
    {
        Dictionary<string, string> _returningWords = new Dictionary<string,string>();
        int _seed = Random.Range(0, int.MaxValue);
        Queue<string> _shuffledLetters = new Queue<string>(Utility.ShuffleArray(Alphabet.Keys.ToArray(), _seed));
        for (int i = 0; i < amountOfWords; i++)
        {
            string _randomLetter = _shuffledLetters.Dequeue();
            _shuffledLetters.Enqueue(_randomLetter);
            _returningWords.Add(_randomLetter, Alphabet[_randomLetter]);
           
        }
        return _returningWords;
    }
    

}
