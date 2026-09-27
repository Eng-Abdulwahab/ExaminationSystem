using ExaminationSystem.UI;

namespace ExaminationSystem.Models;

public class FinalExam : Exam
{
    public FinalExam(int time, Question[] questions)
        : base(time, questions)
    {
    }

    public override void ShowExam()
    {
        ConsoleUI.ShowHeader("Final Exam");
        ConsoleUI.ShowInfo("Time", Time + " minutes");
        ConsoleUI.ShowInfo("Number of Questions", NumberOfQuestions.ToString());

        int grade = ConductExam(out int totalGrade);

        ConsoleUI.ShowHeader("Final Result");
        ConsoleUI.ShowInfo("Grade", grade + " / " + totalGrade);
    }
}