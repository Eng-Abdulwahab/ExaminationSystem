namespace ExaminationSystem.Models;

public class Subject
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public string Name { get; private set; }
    public Exam? Exam { get; private set; }

    public Subject(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Id = _nextId++;
        Name = name;
    }

    public void CreateExam(Exam exam)
    {
        ArgumentNullException.ThrowIfNull(exam);

        if (Exam is not null)
            throw new InvalidOperationException(
                "This subject already has an exam.");

        Exam = exam;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}";
    }
}