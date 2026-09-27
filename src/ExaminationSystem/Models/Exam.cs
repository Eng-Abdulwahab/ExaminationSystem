using ExaminationSystem.UI;

namespace ExaminationSystem.Models;

public abstract class Exam
{
    public int Time { get; private set; }
    public int NumberOfQuestions { get; private set; }
    public Question[] Questions { get; private set; }

    protected Exam(int time, Question[] questions)
    {
        ArgumentNullException.ThrowIfNull(questions);

        if (time <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(time),
                "Exam time must be greater than zero.");

        if (questions.Length == 0)
            throw new ArgumentException(
                "Exam must contain at least one question.",
                nameof(questions));

        Time = time;
        Questions = questions;
        NumberOfQuestions = questions.Length;
    }

    public abstract void ShowExam();

    protected int ConductExam(out int totalGrade)
    {
        totalGrade = 0;
        int grade = 0;
        int questionNumber = 0;

        foreach (Question question in Questions)
        {
            questionNumber++;

            ConsoleUI.ShowHeader($"Question {questionNumber} of {Questions.Length}");
            question.ShowQuestion();

            totalGrade += question.Mark;

            int selectedAnswer = ReadAnswer(
                1,
                question.AnswerList.Length);

            Answer selected = question.AnswerList[selectedAnswer - 1];

            if (selected.Id == question.RightAnswer.Id)
            {
                grade += question.Mark;
                ConsoleUI.ShowSuccess("Correct!");
            }
            else
            {
                ConsoleUI.ShowError("Wrong!");
            }
        }

        return grade;
    }

    private static int ReadAnswer(int min, int max)
    {
        while (true)
        {
            string input = ConsoleUI.ReadText("Your answer");

            if (int.TryParse(input, out int value) &&
                value >= min &&
                value <= max)
            {
                return value;
            }

            ConsoleUI.ShowError($"Enter a whole number from {min} to {max}.");
        }
    }
}