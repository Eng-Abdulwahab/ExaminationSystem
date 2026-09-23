using ExaminationSystem.Models;

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
        Subject subject = CreateSubject();

        Console.WriteLine();
        Console.WriteLine("Choose Exam Type:");
        Console.WriteLine("1. Final Exam");
        Console.WriteLine("2. Practical Exam");

        int examTypeInput = ReadInt(
            "Enter choice: ",
            1,
            2);

        ExamType examType = (ExamType)examTypeInput;

        int time = ReadInt(
            "Enter Exam Time (minutes): ",
            1,
            int.MaxValue);

        int numberOfQuestions = ReadInt(
            "Enter Number of Questions: ",
            1,
            int.MaxValue);

        Question[] questions = new Question[numberOfQuestions];

        for (int i = 0; i < numberOfQuestions; i++)
        {
            questions[i] = CreateQuestion(
                examType,
                i + 1);
        }

        Exam exam = CreateExam(
            examType,
            time,
            questions);

        subject.CreateExam(exam);

        Console.Clear();

        Console.WriteLine(subject);
        Console.WriteLine();

        subject.Exam!.ShowExam();
    }

    static Subject CreateSubject()
    {
        while (true)
        {
            try
            {
                Console.Write("Enter Subject Name: ");

                string name = ReadRequiredText();

                return new Subject(name);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"Invalid Subject: {ex.Message}");
            }
        }
    }

    static Question CreateQuestion(
        ExamType examType,
        int questionNumber)
    {
        while (true)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine(
                    $"===== Question {questionNumber} =====");

                if (examType == ExamType.Final)
                {
                    Console.WriteLine("1. MCQ");
                    Console.WriteLine("2. True / False");

                    int questionTypeInput = ReadInt(
                        "Choose Question Type: ",
                        1,
                        2);

                    QuestionType questionType =
                        (QuestionType)questionTypeInput;

                    return questionType == QuestionType.MCQ
                        ? CreateMCQQuestion(questionNumber)
                        : CreateTrueOrFalseQuestion(questionNumber);
                }

                return CreateMCQQuestion(questionNumber);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"Invalid Question: {ex.Message}");
                Console.WriteLine(
                    "Please enter the question again.");
            }
        }
    }

    static MCQQuestion CreateMCQQuestion(
        int questionNumber)
    {
        string header =
            ReadQuestionHeader(questionNumber);

        Console.Write("Enter Question Body: ");
        string body = ReadRequiredText();

        int mark = ReadInt(
            "Enter Mark: ",
            1,
            int.MaxValue);

        int answerCount = ReadInt(
            "Enter Number of Answers: ",
            2,
            int.MaxValue);

        Answer[] answers = new Answer[answerCount];

        for (int i = 0; i < answerCount; i++)
        {
            while (true)
            {
                try
                {
                    Console.Write(
                        $"Enter Answer {i + 1}: ");

                    string text = ReadRequiredText();

                    answers[i] = new Answer(text);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(
                        $"Invalid Answer: {ex.Message}");
                }
            }
        }

        int rightAnswer = ReadInt(
            "Enter Right Answer Number: ",
            1,
            answerCount);

        return new MCQQuestion(
            header,
            body,
            mark,
            answers,
            answers[rightAnswer - 1]);
    }

    static TrueOrFalseQuestion CreateTrueOrFalseQuestion(
        int questionNumber)
    {
        string header =
            ReadQuestionHeader(questionNumber);

        Console.Write("Enter Question Body: ");
        string body = ReadRequiredText();

        int mark = ReadInt(
            "Enter Mark: ",
            1,
            int.MaxValue);

        Answer trueAnswer = new Answer("True");
        Answer falseAnswer = new Answer("False");

        Answer[] answers =
        {
            trueAnswer,
            falseAnswer
        };

        int rightAnswer = ReadInt(
            "Enter Right Answer (1 = True, 2 = False): ",
            1,
            2);

        return new TrueOrFalseQuestion(
            header,
            body,
            mark,
            answers,
            answers[rightAnswer - 1]);
    }

    static Exam CreateExam(
        ExamType examType,
        int time,
        Question[] questions)
    {
        return examType == ExamType.Final
            ? new FinalExam(time, questions)
            : new PracticalExam(time, questions);
    }

    static string ReadQuestionHeader(
        int questionNumber)
    {
        Console.Write(
            $"Enter Question Header " +
            $"(default: Q{questionNumber}): ");

        string input =
            Console.ReadLine() ?? string.Empty;

        return string.IsNullOrWhiteSpace(input)
            ? $"Q{questionNumber}"
            : input;
    }

    static string ReadRequiredText()
    {
        while (true)
        {
            string input =
                Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.Write(
                "Input cannot be empty. Try again: ");
        }
    }

    static int ReadInt(
        string message,
        int min,
        int max)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(
                    Console.ReadLine(),
                    out int value) &&
                value >= min &&
                value <= max)
            {
                return value;
            }

            Console.WriteLine(
                $"Invalid input. Enter a number between {min} and {max}.");
        }
    }
}