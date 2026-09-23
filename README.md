# Examination System

A C# console-based Examination System designed to demonstrate core Object-Oriented Programming concepts through a simple examination domain.

## Overview

The system supports two types of examinations:

- Final Exam
- Practical Exam

It also supports different question types:

- Multiple Choice Question (MCQ)
- True / False

The system models the relationships between Subjects, Exams, Questions, and Answers while applying fundamental OOP principles in C#.

The project also uses enumerations to represent examination and question types during the creation process.

## OOP Concepts

- Encapsulation
- Abstraction
- Inheritance
- Polymorphism
- Composition
- Interfaces
- Method Overriding
- Constructor Chaining

Additional C# features demonstrated:

- `ICloneable`
- `IComparable`
- `ToString()` overriding
- Enumerations (`ExamType` and `QuestionType`)
- Exception handling and input validation

## Domain Model

```text
Subject
   │
   └── Exam
         │
         └── Questions[]
               │
               └── Answers[]
```

### Inheritance

```text
Exam
├── FinalExam
└── PracticalExam

Question
├── MCQQuestion
└── TrueOrFalseQuestion
```

`FinalExam` and `PracticalExam` inherit from the abstract `Exam` class.

`MCQQuestion` and `TrueOrFalseQuestion` inherit from the abstract `Question` class.

This allows common properties and behavior to be defined in the base classes while allowing the concrete question and exam types to be represented separately.

## Enumerations

The project uses two enumerations during the creation process:

### `ExamType`

```text
Final
Practical
```

`ExamType` is used to determine which concrete examination type should be created.

### `QuestionType`

```text
MCQ
TrueOrFalse
```

`QuestionType` is used when creating questions for a Final Exam to determine which concrete question type should be created.

The enums are used as selection values in the application and do not replace the corresponding classes. The concrete classes are still responsible for representing the actual exam and question objects.

## Design Decisions

### Composition vs. Aggregation

The relationship type was chosen according to the business rules of this system rather than simply because one class contains another.

### Exam → Question

In a general examination platform, `Exam` and `Question` could be modeled using **Aggregation** if questions were managed independently through a reusable question bank and could be shared between multiple exams.

For example:

```text
Question Bank

   ├── Q1
   ├── Q2
   └── Q3

Exam A ◇── Q1
Exam B ◇── Q1
```

In this project, the business case represents an exam as containing its own `Question[]`, without introducing an independent question bank or a shared question lifecycle.

Therefore, `Exam → Question` is modeled as **Composition** in this design.

```text
Exam ◆── Question
```

### Question → Answer

The business case defines a question as being associated with an array of answers:

```csharp
Answer[] AnswerList
```

These answers represent the choices belonging to that specific question, and the requirements do not introduce an independent answer bank or reusable answer lifecycle.

Therefore, `Question → Answer` is modeled as **Composition**.

```text
Question ◆── Answer[]
```

Each `Question` also maintains a `RightAnswer` that references the correct answer from its `AnswerList`.

### Subject → Exam

The `Subject` contains its associated exam and provides the functionality to create that exam.

Therefore, `Subject → Exam` is modeled as **Composition** in this design.

```text
Subject ◆── Exam
```

> Relationship types are domain-dependent design decisions. The same pair of classes can have different relationships in different business domains.

## Question Types

### MCQQuestion

An `MCQQuestion` allows the user to define multiple answer choices.

The number of choices is determined when the question is created, and one of the provided answers is selected as the correct answer.

Example:

```text
What is C#?

1. Programming Language
2. Database
3. Operating System
4. Browser
```

### TrueOrFalseQuestion

A `TrueOrFalseQuestion` provides two predefined answer choices:

```text
1. True
2. False
```

The user selects which of the two answers is the correct answer.

Both question types share the common structure defined by the abstract `Question` class, including the question body, mark, answer list, and right answer.

## UML Diagrams

### Class Diagram

![Class Diagram](Docs/ExaminationSystem-ClassDiagram.png)

### Sequence Diagram

![Sequence Diagram](Docs/ExaminationSystem-SequenceDiagram.png)

The editable draw.io source is also included:

`Docs/ExaminationSystem.drawio`

The source file contains both the Class Diagram and Sequence Diagram.

## Key Classes

### Subject

Represents a subject and maintains its associated examination.

### Exam

An abstract base class containing the common examination properties and behavior:

- Exam time
- Number of questions
- Questions array
- `ShowExam()` functionality

### FinalExam

Represents a final examination and supports:

- MCQ questions
- True / False questions

### PracticalExam

Represents a practical examination and supports:

- MCQ questions

### Question

An abstract base class containing the common properties shared by all question types:

- Header
- Body
- Mark
- Answer list
- Right answer

### Answer

Represents an individual answer choice and contains:

- Answer ID
- Answer text

The `Answer` class also implements `ICloneable` and `IComparable`.

## Interfaces

The project demonstrates:

### `ICloneable`

Used to provide cloning functionality for `Answer` objects.

The implementation uses `MemberwiseClone()` to create a shallow copy of the object.

### `IComparable`

Used to provide comparison behavior for `Answer` objects.

Answers are compared based on their `Id`.

## Validation and Exception Handling

The project includes input validation and exception handling to prevent invalid data from entering the domain model.

Examples include:

- Preventing empty or whitespace-only subject names
- Preventing empty question headers and bodies
- Ensuring marks are greater than zero
- Ensuring questions contain at least one answer
- Ensuring the correct answer exists in the answer list
- Validating numeric input and allowed ranges
- Validating question and exam type selections

Exceptions are thrown by the relevant classes when invalid domain data is provided and are handled by the application where appropriate.

## Project Structure

```text
ExaminationSystem/
│
├── Docs/
│   ├── ExaminationSystem.drawio
│   ├── ExaminationSystem-ClassDiagram.png
│   └── ExaminationSystem-SequenceDiagram.png
│
├── src/
│   └── ExaminationSystem/
│       ├── Models/
│       │   ├── Answer.cs
│       │   ├── Exam.cs
│       │   ├── ExamType.cs
│       │   ├── FinalExam.cs
│       │   ├── MCQQuestion.cs
│       │   ├── PracticalExam.cs
│       │   ├── Question.cs
│       │   ├── QuestionType.cs
│       │   ├── Subject.cs
│       │   └── TrueOrFalseQuestion.cs
│       │
│       ├── Program.cs
│       └── ExaminationSystem.csproj
│
├── .gitignore
├── ExaminationSystem.sln
└── README.md
```

## Purpose

This project was developed as an OOP exercise to practice object-oriented design, class relationships, abstraction, inheritance, polymorphism, composition, interface implementation, exception handling, and C# object behavior.

The goal is to model the given examination business case clearly while keeping the design simple, maintainable, and consistent with the implemented code and UML diagrams.
