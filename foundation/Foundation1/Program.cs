using System;
using System.Collections.Generic;

namespace YouTubeVideoTracker
{
    // Comment class to represent a single comment on a video
    public class Comment
    {
        // Properties to store the commenter's name and the comment text
        public string CommenterName { get; set; }
        public string CommentText { get; set; }

        // Constructor to initialize the properties
        public Comment(string commenterName, string commentText)
        {
            CommenterName = commenterName;
            CommentText = commentText;
        }

        // Override ToString() for easy display
        public override string ToString()
        {
            return $"{CommenterName}: {CommentText}";
        }
    }

    // Video class to represent a YouTube video
    public class Video
    {
        // Properties to store video details
        public string Title { get; set; }
        public string Author { get; set; }
        public int LengthInSeconds { get; set; }

        // List to store comments associated with the video
        private List<Comment> comments;

        // Constructor to initialize video details and the comments list
        public Video(string title, string author, int lengthInSeconds)
        {
            Title = title;
            Author = author;
            LengthInSeconds = lengthInSeconds;
            comments = new List<Comment>();
        }

        // Method to add a comment to the video
        public void AddComment(Comment comment)
        {
            comments.Add(comment);
        }

        // Method to get the number of comments
        public int GetNumberOfComments()
        {
            return comments.Count;
        }

        // Method to get all comments
        public List<Comment> GetComments()
        {
            return comments;
        }
    }

    // Program class containing the Main method
    class Program
    {
        static void Main(string[] args)
        {
            // Create a list to store videos
            List<Video> videos = new List<Video>();

            // Create Video 1
            Video video1 = new Video("Understanding Abstraction in C#", "JaneDoe", 600);
            video1.AddComment(new Comment("User1", "Great explanation!"));
            video1.AddComment(new Comment("User2", "Very helpful, thanks!"));
            video1.AddComment(new Comment("User3", "Could you provide more examples?"));
            videos.Add(video1);

            // Create Video 2
            Video video2 = new Video("Intro to Object-Oriented Programming", "JohnSmith", 750);
            video2.AddComment(new Comment("Alice", "Loved this video!"));
            video2.AddComment(new Comment("Bob", "Clear and concise."));
            video2.AddComment(new Comment("Charlie", "Waiting for the next part."));
            videos.Add(video2);

            // Create Video 3
            Video video3 = new Video("Advanced C# Concepts", "CodeMaster", 1200);
            video3.AddComment(new Comment("Dave", "This was a bit too fast."));
            video3.AddComment(new Comment("Eve", "Excellent content!"));
            video3.AddComment(new Comment("Frank", "Can you cover LINQ next?"));
            videos.Add(video3);

            // Optional: Create Video 4
            Video video4 = new Video("C# Design Patterns", "DevGuru", 900);
            video4.AddComment(new Comment("Grace", "Design patterns are so useful."));
            video4.AddComment(new Comment("Heidi", "Great examples provided."));
            video4.AddComment(new Comment("Ivan", "Looking forward to more videos like this."));
            videos.Add(video4);

            // Iterate through each video and display its details and comments
            foreach (Video video in videos)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
                Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
                Console.WriteLine("Comments:");

                foreach (Comment comment in video.GetComments())
                {
                    Console.WriteLine($" - {comment}");
                }
                Console.WriteLine("--------------------------------------------------\n");
            }

            // Wait for user input before closing (optional)
            // Console.ReadLine();
        }
    }
}