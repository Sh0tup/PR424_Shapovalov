using System;
using System.Collections.Generic;
using System.Linq;

public class TextStats
{
    private string text;
    private int wordCount;
    private int sentenceCount;
    private int vowelsCount;
    private int consonantsCount;
    private string shortestWord;
    private string longestWord;
    private Dictionary<char, int> letter;

    public string Text
    {
        get { return text; }
        set
        {
            if (value.Length >= 100)
                text = value;
        }
    }

    public int WordCount
    {
        get { return wordCount; }
    }

    public int SentenceCount
    {
        get { return sentenceCount; }
    }

    public int VowelsCount
    {
        get { return vowelsCount; }
    }

    public int ConsonantsCount
    {
        get { return consonantsCount; }
    }

    public string ShortestWord
    {
        get { return shortestWord; }
    }

    public string LongestWord
    {
        get { return longestWord; }
    }

    public TextStats(string text)
    {
        Text = text;
        letter = new Dictionary<char, int>();
        CalculateStats();
    }

    private void CalculateStats()
    {
        string vowels = "аеёиоуыэюяaeiouy";
        sentenceCount = 0;
        vowelsCount = 0;
        consonantsCount = 0;
        shortestWord = "";
        longestWord = "";

        string[] words = text.Split(' ');

        foreach (string w in words)
        {
            string clean = "";
            foreach (char c in w)
            {
                if (char.IsLetter(c))
                    clean += c;
            }

            if (clean.Length == 0)
                continue;

            wordCount++;

            if (shortestWord == "" || clean.Length < shortestWord.Length)
                shortestWord = clean;

            if (clean.Length > longestWord.Length)
                longestWord = clean;

            foreach (char c in clean)
            {
                char lower = char.ToLower(c);

                if (vowels.Contains(lower))
                {
                    vowelsCount++;
                }
                else
                {
                    consonantsCount++;
                }


                if (letter.ContainsKey(lower))
                {
                    letter[lower]++;
                }
                else
                {
                    letter.Add(lower, 1); 
                }
            }
        }

        foreach (char c in text)
        {
            if (c == '.' || c == '!' || c == '?')
                sentenceCount++;
        }
    }

    public void Info()
    {
        Console.WriteLine($"Количество слов: {wordCount}");
        Console.WriteLine($"Самое короткое слово: {shortestWord}");
        Console.WriteLine($"Самое длинное слово: {longestWord}");
        Console.WriteLine($"Количество предложений: {sentenceCount}");
        Console.WriteLine($"Гласных букв: {vowelsCount}");
        Console.WriteLine($"Согласных букв: {consonantsCount}");
        Console.WriteLine("Частота встречаемости букв:");

        List<char> letters = new List<char>();
        foreach (KeyValuePair<char, int> pair in letter)
            letters.Add(pair.Key);

        for (int i = 0; i < letters.Count - 1; i++)
        {
            for (int j = 0; j < letters.Count - 1 - i; j++)
            {
                if (letter[letters[j]] < letter[letters[j + 1]])
                {
                    char temp = letters[j];
                    letters[j] = letters[j + 1];
                    letters[j + 1] = temp;
                }
            }
        }

        foreach (char c in letters)
            Console.WriteLine($"  {c} - {letter[c]}");
    }
}

class Program
{
    static void Main()
    {
        List<TextStats> statsHistory = new List<TextStats>();

        while (true)
        {
            Console.WriteLine("1. Ввести новый текст");
            Console.WriteLine("2. Статистика по прошлым текстам");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите текст (min 100):");
                    string input = Console.ReadLine();

                    if (input.Length < 100)
                    {
                        Console.WriteLine("Ошибка: текст слишком короткий (min 100)");
                        continue;
                    }

                    TextStats stats = new TextStats(input);
                    statsHistory.Add(stats);
                    stats.Info();
                    Console.WriteLine();
                    break;

                case "2":
                    if (statsHistory.Count == 0)
                    {
                        Console.WriteLine("Пока нет проанализированного текста.");
                        break;
                    }

                    for (int i = 0; i < statsHistory.Count; i++)
                    {
                        Console.WriteLine($"Текст {i + 1}");
                        statsHistory[i].Info();
                        Console.WriteLine();
                    }
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Выберите цифру из предложенных");
                    break;
            }
        }
    }
}