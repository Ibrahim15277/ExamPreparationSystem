namespace ExamPreparationSystem.Domain;

public class Question
{
    public int Id { get; set; }
    public int SubjectId { get; set; } // К какому предмету относится
    public string Text { get; set; } = string.Empty; // Сам вопрос
    public List<string> Options { get; set; } = new(); // Варианты ответов
    public int CorrectAnswerIndex { get; set; } // Индекс правильного ответа (0, 1, 2...)
}