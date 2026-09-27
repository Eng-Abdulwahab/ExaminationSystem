using System;
using ExaminationSystem.Models;
using ExaminationSystem.UI;

namespace ExaminationSystem;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("An unexpected error occurred.");
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void Run()
    {
        ConsoleUI.ShowTitle();
        Console.WriteLine("Build an exam, then take it from the same console.");

        Subject subject = CreateSubject();

        ConsoleUI.ShowInfo("Subject ID", subject.Id.ToString("D3"));
        ConsoleUI.ShowInfo("Subject name", subject.Name);

        ConsoleUI.ShowHeader("Exam Setup");
        Console.WriteLine("1. Final exam     (MCQ + True/False)");
        Console.WriteLine("2. Practical exam (MCQ only)");

        ExamType examType = (ExamType)ConsoleUI.ReadInt("Exam type", 1, 2);

        int time = ConsoleUI.ReadInt("Duration in minutes", 1, 600);

        int numberOfQuestions = ConsoleUI.ReadInt("Number of questions", 1, 100);

        Question[] questions = new Question[numberOfQuestions];

        for (int i = 0; i < numberOfQuestions; i++)
        {
            questions[i] = CreateQuestion(examType, i + 1, numberOfQuestions);
        }

        Exam exam = CreateExam(examType, time, questions);
        subject.CreateExam(exam);

        Console.Clear();
        ConsoleUI.ShowInfo("Subject ID", subject.Id.ToString("D3"));
        ConsoleUI.ShowInfo("Subject name", subject.Name);
        Console.WriteLine();

        subject.Exam!.ShowExam();

        ConsoleUI.Pause();
    }

    static Subject CreateSubject()
    {
        while (true)
        {
            try
            {
                string name = ConsoleUI.ReadRequiredText("Subject name");
                return new Subject(name);
            }
            catch (ArgumentException ex)
            {
                ConsoleUI.ShowError($"Invalid Subject: {ex.Message}");
            }
        }
    }

    static Question CreateQuestion(ExamType examType, int questionNumber, int totalQuestions)
    {
        while (true)
        {
            try
            {
                ConsoleUI.ShowHeader($"Create Question {questionNumber} of {totalQuestions}");

                if (examType == ExamType.Final)
                {
                    Console.WriteLine("1. MCQ");
                    Console.WriteLine("2. True / False");

                    QuestionType questionType =
                        (QuestionType)ConsoleUI.ReadInt("Question type", 1, 2);

                    return questionType == QuestionType.MCQ
                        ? CreateMCQQuestion(questionNumber)
                        : CreateTrueOrFalseQuestion(questionNumber);
                }

                return CreateMCQQuestion(questionNumber);
            }
            catch (ArgumentException ex)
            {
                ConsoleUI.ShowError($"Invalid Question: {ex.Message}");
                Console.WriteLine("Please enter the question again.");
            }
        }
    }

    static MCQQuestion CreateMCQQuestion(int questionNumber)
    {
        string header = ReadQuestionHeader(questionNumber);

        string body = ConsoleUI.ReadRequiredText("Question");

        int mark = ConsoleUI.ReadInt("Mark", 1, 1000);

        int answerCount = ConsoleUI.ReadInt("Number of choices", 2, 10);

        Answer[] answers = new Answer[answerCount];

        for (int i = 0; i < answerCount; i++)
        {
            while (true)
            {
                try
                {
                    string text = ConsoleUI.ReadRequiredText($"Choice {i + 1}");

                    answers[i] = new Answer(text);
                    break;
                }
                catch (ArgumentException ex)
                {
                    ConsoleUI.ShowError($"Invalid Answer: {ex.Message}");
                }
            }
        }

        int rightAnswer = ConsoleUI.ReadInt("Right answer", 1, answerCount);

        return new MCQQuestion(header, body, mark, answers, answers[rightAnswer - 1]);
    }

    static TrueOrFalseQuestion CreateTrueOrFalseQuestion(int questionNumber)
    {
        string header = ReadQuestionHeader(questionNumber);

        string body = ConsoleUI.ReadRequiredText("Question");

        int mark = ConsoleUI.ReadInt("Mark", 1, 1000);

        Answer[] answers =
        {
            new Answer("True"),
            new Answer("False")
        };

        int rightAnswer = ConsoleUI.ReadInt("Right answer (1 = True, 2 = False)", 1, 2);

        return new TrueOrFalseQuestion(header, body, mark, answers, answers[rightAnswer - 1]);
    }

    static string ReadQuestionHeader(int questionNumber)
    {
        string defaultHeader = "Q" + questionNumber.ToString("D2");

        string input = ConsoleUI.ReadText($"Header (default: {defaultHeader})");

        return string.IsNullOrWhiteSpace(input)? defaultHeader : input.Trim();
    }

    static Exam CreateExam(ExamType examType, int time, Question[] questions)
    {
        return examType == ExamType.Final? new FinalExam(time, questions)
            : new PracticalExam(time, questions);
    }
}