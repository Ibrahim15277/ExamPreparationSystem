using Microsoft.EntityFrameworkCore;
using ExamPreparationSystem.Domain;

namespace ExamPreparationSystem.Application;


/// Сервис для управления логикой проведения экзаменов и взаимодействия с БД.

public class ExamService
{
    private readonly AppDbContext _context;

    // Свойства для управления состоянием текущей сессии теста
    public Subject? CurrentSubject { get; private set; }
    public ExamTest? CurrentTest { get; private set; }
    public List<Question> CurrentQuestions { get; private set; } = new();
    public int CurrentQuestionIndex { get; private set; }
    public int CorrectAnswersCount { get; private set; }
    public bool IsExamFinished { get; private set; }

    public ExamService(AppDbContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    
    /// Получает список всех доступных экзаменационных предметов.
    
    public List<Subject> GetSubjects()
    {
        return _context.Subjects.ToList();
    }

    
    /// Получает предмет по его идентификатору.
    
    public Subject? GetSubjectById(int subjectId)
    {
        return _context.Subjects.FirstOrDefault(s => s.Id == subjectId);
    }

    
    /// Получает список тестов (вариантов), привязанных к конкретному предмету.
    
    public List<ExamTest> GetTestsBySubject(int subjectId)
    {
        return _context.ExamTests.Where(t => t.SubjectId == subjectId).ToList();
    }

    
    /// Инициализирует и запускает конкретный выбранный тест (вариант).
    
    public void StartTest(int testId)
    {
        CurrentTest = _context.ExamTests.FirstOrDefault(t => t.Id == testId);
        if (CurrentTest == null) return;

        CurrentSubject = _context.Subjects.FirstOrDefault(s => s.Id == CurrentTest.SubjectId);
        CurrentQuestions = _context.Questions.Where(q => q.TestId == testId).ToList();

        CurrentQuestionIndex = 0;
        CorrectAnswersCount = 0;
        IsExamFinished = false;
    }

    
    /// Обрабатывает ответ пользователя на текущий вопрос.
    
    public void SubmitAnswer(int selectedOptionIndex)
    {
        if (IsExamFinished || CurrentQuestions.Count == 0) return;

        if (CurrentQuestions[CurrentQuestionIndex].CorrectAnswerIndex == selectedOptionIndex)
        {
            CorrectAnswersCount++;
        }

        if (CurrentQuestionIndex + 1 < CurrentQuestions.Count)
        {
            CurrentQuestionIndex++;
        }
        else
        {
            IsExamFinished = true;
        }
    }

    
    /// Сбрасывает состояние текущей сессии тестирования.
    
    public void Reset()
    {
        CurrentSubject = null;
        CurrentTest = null;
        CurrentQuestions.Clear();
        CurrentQuestionIndex = 0;
        CorrectAnswersCount = 0;
        IsExamFinished = false;
    }
}