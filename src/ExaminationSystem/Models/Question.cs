namespace ExaminationSystem.Models;

public abstract class Question
{
    public string Header { get; private set; }
    public string Body { get; private set; }
    public int Mark { get; private set; }

    public Answer[] AnswerList { get; private set; }
    public Answer RightAnswer { get; private set; }

    protected Question(
        string header,
        string body,
        int mark,
        Answer[] answerList,
        Answer rightAnswer)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(header);
        ArgumentException.ThrowIfNullOrWhiteSpace(body);
        ArgumentNullException.ThrowIfNull(answerList);
        ArgumentNullException.ThrowIfNull(rightAnswer);

        if (mark <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(mark),
                "Mark must be greater than zero.");

        if (answerList.Length == 0)
            throw new ArgumentException(
                "Question must have at least one answer.",
                nameof(answerList));

        if (!answerList.Contains(rightAnswer))
            throw new ArgumentException(
                "Right answer must exist in AnswerList.");

        Header = header;
        Body = body;
        Mark = mark;
        AnswerList = answerList;
        RightAnswer = rightAnswer;
    }

    public abstract void ShowQuestion();

    public override string ToString()
    {
        return $"{Header}: {Body} - Mark: {Mark}";
    }
}