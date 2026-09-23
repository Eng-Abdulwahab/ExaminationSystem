namespace ExaminationSystem.Models;

public class FinalExam : Exam
{
    public FinalExam(int time, Question[] questions)
        : base(time, questions)
    {
    }

    public override void ShowExam()
    {
        Console.WriteLine("===== Final Exam =====");
        Console.WriteLine($"Time: {Time} minutes");
        Console.WriteLine($"Number of Questions: {NumberOfQuestions}");

        int grade = ConductExam(out int totalGrade);

        Console.WriteLine();
        Console.WriteLine("===== Final Result =====");
        Console.WriteLine($"Grade: {grade} / {totalGrade}");
    }
}