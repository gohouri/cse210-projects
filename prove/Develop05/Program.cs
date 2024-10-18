using System;
using System.Threading;

public abstract class MindfulnessActivity
{
    protected string activityName;
    protected string description;
    protected int duration; // in seconds

    public void StartActivity()
    {
        Console.WriteLine($"\nStarting {activityName}...");
        Console.WriteLine(description);
        SetDuration();
        Console.WriteLine("Prepare to begin...");
        PauseWithSpinner(3); // Short pause to let the user prepare
    }

    public void EndActivity()
    {
        Console.WriteLine($"\nGood job! You have completed {activityName} for {duration} seconds.");
        PauseWithSpinner(3);
    }

    protected void SetDuration()
    {
        Console.Write("\nEnter the duration of the activity (in seconds): ");
        duration = int.Parse(Console.ReadLine());
    }

    protected void PauseWithSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(". ");
            Thread.Sleep(1000); // Pauses for 1 second
        }
        Console.WriteLine();
    }

    public abstract void RunActivity(); // Abstract method to run the specific activity
}

public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity()
    {
        activityName = "Breathing Exercise";
        description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    public override void RunActivity()
    {
        StartActivity();
        for (int i = 0; i < duration; i += 6) // 6 seconds cycle: 3 for in, 3 for out
        {
            Console.WriteLine("\nBreathe in...");
            PauseWithCountdown(3);
            Console.WriteLine("Breathe out...");
            PauseWithCountdown(3);
        }
        EndActivity();
    }

    private void PauseWithCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(1000); // Pauses for 1 second
        }
        Console.WriteLine();
    }
}

public class ReflectionActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience?",
        "What did you learn about yourself?"
    };

    public ReflectionActivity()
    {
        activityName = "Reflection Activity";
        description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    }

    public override void RunActivity()
    {
        StartActivity();
        Random rand = new Random();
        Console.WriteLine($"\n{prompts[rand.Next(prompts.Length)]}");

        int elapsed = 0;
        while (elapsed < duration)
        {
            Console.WriteLine($"\n{questions[rand.Next(questions.Length)]}");
            PauseWithSpinner(5); // Pauses for reflection on the question
            elapsed += 5;
        }
        EndActivity();
    }
}

public class ListingActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        activityName = "Listing Activity";
        description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    public override void RunActivity()
    {
        StartActivity();
        Random rand = new Random();
        Console.WriteLine($"\n{prompts[rand.Next(prompts.Length)]}");
        Console.WriteLine("Start listing items. Press Enter after each item.");

        int count = 0;
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.ReadLine(); // Each item is entered by the user
            count++;
        }
        Console.WriteLine($"\nYou listed {count} items.");
        EndActivity();
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nMindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an activity: ");
            string choice = Console.ReadLine();

            MindfulnessActivity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }

            activity.RunActivity();
        }
    }
}
