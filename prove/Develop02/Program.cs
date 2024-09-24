using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    // Private fields
    private string _prompt;
    private string _response;
    private string _date;

    // Public properties to access private fields
    public string Prompt => _prompt;
    public string Response => _response;
    public string Date => _date;

    // Constructor to initialize the entry
    public Entry(string prompt, string response, string date)
    {
        _prompt = prompt;
        _response = response;
        _date = date;
    }

    // Method to display the entry
    public void DisplayEntry()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}\n");
    }
}

public class Journal
{
    // List to hold entries
    private List<Entry> _entries = new List<Entry>();

    // Method to add an entry to the journal
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    // Method to display all entries in the journal
    public void DisplayJournal()
    {
        foreach (Entry entry in _entries)
        {
            entry.DisplayEntry();
        }
    }

    // Method to save the journal to a file
    public void SaveJournal(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                // Use the public properties to access the fields
                outputFile.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
    }

    // Method to load the journal from a file
    public void LoadJournal(string filename)
    {
        string[] lines = System.IO.File.ReadAllLines(filename);
        _entries.Clear();

        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            // Create a new entry with parts and add to the journal
            Entry newEntry = new Entry(parts[1], parts[2], parts[0]);
            _entries.Add(newEntry);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal myJournal = new Journal();
        bool running = true;

        // Main loop for interacting with the user
        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = GetRandomPrompt();
                    Console.WriteLine(prompt);
                    string response = Console.ReadLine();
                    string date = DateTime.Now.ToShortDateString();
                    Entry newEntry = new Entry(prompt, response, date);
                    myJournal.AddEntry(newEntry);
                    break;
                case "2":
                    myJournal.DisplayJournal();
                    break;
                case "3":
                    Console.Write("Enter the filename to save: ");
                    string saveFilename = Console.ReadLine();
                    myJournal.SaveJournal(saveFilename);
                    break;
                case "4":
                    Console.Write("Enter the filename to load: ");
                    string loadFilename = Console.ReadLine();
                    myJournal.LoadJournal(loadFilename);
                    break;
                case "5":
                    running = false;
                    break;
            }
        }
    }

    // Method to display the menu
    static void DisplayMenu()
    {
        Console.WriteLine("1. Add a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal");
        Console.WriteLine("4. Load the journal");
        Console.WriteLine("5. Quit");
        Console.Write("Choose an option: ");
    }

    // Method to get a random prompt for the journal
    static string GetRandomPrompt()
    {
        List<string> prompts = new List<string>()
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };
        Random rand = new Random();
        return prompts[rand.Next(prompts.Count)];
    }
}
