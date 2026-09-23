namespace ExaminationSystem.Models;

public class PracticalExam : Exam
{
    public PracticalExam(int time, Question[] questions)
        : base(time, questions)
    {
    }

    public override void ShowExam()
    {
        Console.WriteLine("===== Practical Exam =====");
        Console.WriteLine($"Time: {Time} minutes");
        Console.WriteLine($"Number of Questions: {NumberOfQuestions}");

        int grade = ConductExam(out int totalGrade);

        Console.WriteLine();
        Console.WriteLine("===== Exam Finished =====");

        Console.WriteLine("Correct Answers:");

        foreach (Question question in Questions)
        {
            Console.WriteLine(
                $"{question.Header}: {question.RightAnswer.Text}");
        }

        Console.WriteLine();
        Console.WriteLine($"Grade: {grade} / {totalGrade}");
    }
}