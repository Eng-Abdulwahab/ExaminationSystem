namespace ExaminationSystem.Models;

public class TrueOrFalseQuestion : Question
{
    public TrueOrFalseQuestion(
        string header,
        string body,
        int mark,
        Answer[] answerList,
        Answer rightAnswer)
        : base(header, body, mark, answerList, rightAnswer)
    {
    }

    public override void ShowQuestion()
    {
        Console.WriteLine($"{Header}: {Body}");
        Console.WriteLine($"Mark: {Mark}");

        for (int i = 0; i < AnswerList.Length; i++)
        {
            Console.WriteLine($"{i + 1}: {AnswerList[i].Text}");
        }
    }
}