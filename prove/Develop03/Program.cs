using System;
using System.Collections.Generic;
using System.Linq;

// ========================= Reference.cs =========================
public class Reference
{
    public string Book { get; private set; }
    public int Chapter { get; private set; }
    public int VerseStart { get; private set; }
    public int? VerseEnd { get; private set; }

    // Constructor for a single verse
    public Reference(string book, int chapter, int verse)
    {
        Book = book;
        Chapter = chapter;
        VerseStart = verse;
        VerseEnd = null;
    }

    // Constructor for a range of verses
    public Reference(string book, int chapter, int verseStart, int verseEnd)
    {
        Book = book;
        Chapter = chapter;
        VerseStart = verseStart;
        VerseEnd = verseEnd;
    }

    // Method to get the formatted reference text
    public string GetDisplayText()
    {
        if (VerseEnd.HasValue)
        {
            return $"{Book} {Chapter}:{VerseStart}-{VerseEnd}";
        }
        else
        {
            return $"{Book} {Chapter}:{VerseStart}";
        }
    }
}

// ========================= Word.cs =========================
public class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    // Method to hide the word
    public void Hide()
    {
        IsHidden = true;
    }

    // Method to get the display text (word or underscores)
    public string GetDisplayText()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}

// ========================= Scripture.cs =========================
public class Scripture
{
    public Reference Reference { get; private set; }
    private List<Word> Words;
    private Random random;

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
        random = new Random();
    }

    // Method to hide a specific number of random words
    public void HideRandomWords(int numberOfWords)
    {
        int hiddenCount = 0;
        List<int> availableIndices = Words
            .Select((word, index) => new { word, index })
            .Where(w => !w.word.IsHidden)
            .Select(w => w.index)
            .ToList();

        if (availableIndices.Count == 0)
        {
            return; // All words are already hidden
        }

        for (int i = 0; i < numberOfWords; i++)
        {
            if (availableIndices.Count == 0)
                break;

            int randomIndex = random.Next(availableIndices.Count);
            int wordIndex = availableIndices[randomIndex];
            Words[wordIndex].Hide();
            availableIndices.RemoveAt(randomIndex);
            hiddenCount++;
        }
    }

    // Method to get the complete text with hidden words
    public string GetDisplayText()
    {
        return $"{Reference.GetDisplayText()}\n" + string.Join(" ", Words.Select(w => w.GetDisplayText()));
    }

    // Method to check if all words are hidden
    public bool IsAllHidden()
    {
        return Words.All(w => w.IsHidden);
    }
}

// ========================= Program.cs =========================
class Program
{
    static void Main(string[] args)
    {
        // Initialize the reference and scripture
        Reference reference = new Reference("John", 3, 16);
        Scripture scripture = new Scripture(reference, "For God so loved the world that He gave His one and only Son.");

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");

            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(2); // Hide 2 words at a time

            if (scripture.IsAllHidden())
            {
                Console.Clear();
                Console.WriteLine("All words are hidden. Great job memorizing!");
                break;
            }
        }
    }
}
