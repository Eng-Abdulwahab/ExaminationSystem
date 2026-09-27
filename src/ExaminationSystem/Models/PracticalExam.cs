using ExaminationSystem.UI;

namespace ExaminationSystem.Models;

public class PracticalExam : Exam
{
    public PracticalExam(int time, Question[] questions) : base(time, questions)
    {
    }

    public override void ShowExam()
    {
        ConsoleUI.ShowHeader("Practical Exam");
        ConsoleUI.ShowInfo("Time", Time + " minutes");
        ConsoleUI.ShowInfo("Number of Questions", NumberOfQuestions.ToString());

        int grade = ConductExam(out int totalGrade);

        ConsoleUI.ShowHeader("Exam Finished");

        Console.WriteLine("Correct Answers:");
        foreach (Question question in Questions)
        {
            Console.WriteLine($"{question.Header}: {question.RightAnswer.Text}");
        }

        Console.WriteLine();
        ConsoleUI.ShowInfo("Grade", grade + " / " + totalGrade);
    }
}