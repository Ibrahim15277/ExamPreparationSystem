namespace ExamPreparationSystem.Domain;

public class ExamTest
{
    public int Id { get; set; }
    public int SubjectId { get; set; } // К какому предмету относится тест
    public string Title { get; set; } = string.Empty; // Например: "Вариант 1 (Базовый)", "Вариант 2 (Углубленный)"
    public string Description { get; set; } = string.Empty;
}