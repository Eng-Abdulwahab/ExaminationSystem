namespace ExaminationSystem.Models;

public class Answer : ICloneable, IComparable
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public string Text { get; private set; }

    public Answer(string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        Id = _nextId++;
        Text = text;
    }

    public override string ToString()
    {
        return $"{Id}: {Text}";
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public int CompareTo(object? obj)
    {
        if (obj is not Answer other)
            throw new ArgumentException(
                "Object must be an Answer.",
                nameof(obj));

        return Id.CompareTo(other.Id);
    }
}