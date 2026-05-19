using ExamPreparationSystem.Domain;

namespace ExamPreparationSystem.Application;

public class ExamService
{
    private readonly List<Subject> _subjects = new();
    private readonly List<Question> _questions = new();

    public Subject? CurrentSubject { get; private set; }
    public List<Question> CurrentQuestions { get; private set; } = new();
    public int CurrentQuestionIndex { get; private set; }
    public int CorrectAnswersCount { get; private set; }
    public bool IsExamFinished { get; private set; }

    public ExamService()
    {
        SeedData();
    }

    public List<Subject> GetSubjects() => _subjects;

    public void StartExam(int subjectId)
    {
        CurrentSubject = _subjects.FirstOrDefault(s => s.Id == subjectId);
        CurrentQuestions = _questions.Where(q => q.SubjectId == subjectId).ToList();
        CurrentQuestionIndex = 0;
        CorrectAnswersCount = 0;
        IsExamFinished = false;
    }

    public void SubmitAnswer(int selectedOptionIndex)
    {
        if (IsExamFinished || CurrentQuestions.Count == 0) return;

        if (selectedOptionIndex == CurrentQuestions[CurrentQuestionIndex].CorrectAnswerIndex)
        {
            CorrectAnswersCount++;
        }

        if (CurrentQuestionIndex < CurrentQuestions.Count - 1)
        {
            CurrentQuestionIndex++;
        }
        else
        {
            IsExamFinished = true;
        }
    }

    public void Reset()
    {
        CurrentSubject = null;
        CurrentQuestions.Clear();
        IsExamFinished = false;
    }

    private void SeedData()
    {
        
        // ПРЕДМЕТ 1: ИНФОРМАТИКА (5 вопросов)
        
        _subjects.Add(new Subject
        {
            Id = 1,
            Name = "💻 Информатика: Системы счисления",
            Description = "Перевод чисел между двоичной, восьмеричной, десятичной и шестнадцатеричной системами."
        });

        _questions.Add(new Question
        {
            Id = 1,
            SubjectId = 1,
            Text = "Переведите двоичное число 1101 в десятичную систему счисления:",
            Options = new List<string> { "11", "13", "15", "9" },
            CorrectAnswerIndex = 1 // 13
        });
        _questions.Add(new Question
        {
            Id = 2,
            SubjectId = 1,
            Text = "Чему равно число 25 из десятичной системы в двоичной?",
            Options = new List<string> { "11001", "10111", "11100", "10011" },
            CorrectAnswerIndex = 0 // 11001
        });
        _questions.Add(new Question
        {
            Id = 3,
            SubjectId = 1,
            Text = "Переведите шестнадцатеричное число 1A в десятичную систему:",
            Options = new List<string> { "16", "24", "26", "30" },
            CorrectAnswerIndex = 2 // 26
        });
        _questions.Add(new Question
        {
            Id = 4,
            SubjectId = 1,
            Text = "Какое число в восьмеричной системе соответствует двоичному числу 111?",
            Options = new List<string> { "5", "6", "7", "11" },
            CorrectAnswerIndex = 2 // 7
        });
        _questions.Add(new Question
        {
            Id = 5,
            SubjectId = 1,
            Text = "Сколько бит содержится в одном байте?",
            Options = new List<string> { "4", "8", "10", "16" },
            CorrectAnswerIndex = 1 // 8
        });

        
        // ПРЕДМЕТ 2: РУССКИЙ ЯЗЫК (5 вопросов)
        
        _subjects.Add(new Subject
        {
            Id = 2,
            Name = "📚 Русский язык: Базовая орфография",
            Description = "Простейшие правила правописания, части речи и знаки препинания."
        });

        _questions.Add(new Question
        {
            Id = 6,
            SubjectId = 2,
            Text = "В каком слове допущена ошибка в правописании сочетаний ЖИ-ШИ?",
            Options = new List<string> { "Машина", "Жираф", "Жызнь", "Шина" },
            CorrectAnswerIndex = 2 // Жызнь
        });
        _questions.Add(new Question
        {
            Id = 7,
            SubjectId = 2,
            Text = "Какая из этих частей речи отвечает на вопросы «Кто?» или «Что?»?",
            Options = new List<string> { "Глагол", "Имя существительное", "Имя прилагательное", "Наречие" },
            CorrectAnswerIndex = 1 // Имя существительное
        });
        _questions.Add(new Question
        {
            Id = 8,
            SubjectId = 2,
            Text = "Укажите слово, в котором на конце пишется парная согласная 'З', а слышится 'С':",
            Options = new List<string> { "Нос", "Лес", "Класс", "Арбуз" },
            CorrectAnswerIndex = 3 // Арбуз
        });
        _questions.Add(new Question
        {
            Id = 9,
            SubjectId = 2,
            Text = "Какое слово всегда пишется с большой (прописной) буквы в любом месте предложения?",
            Options = new List<string> { "Город", "Москва", "Река", "Книга" },
            CorrectAnswerIndex = 1 // Москва
        });
        _questions.Add(new Question
        {
            Id = 10,
            SubjectId = 2,
            Text = "Какой знак препинания ставится в конце стандартного повествовательного предложения?",
            Options = new List<string> { "Запятая", "Точка", "Вопросительный знак", "Двоеточие" },
            CorrectAnswerIndex = 1 // Точка
        });

        
        // ПРЕДМЕТ 3: МАТЕМАТИКА (5 вопросов)
        
        _subjects.Add(new Subject
        {
            Id = 3,
            Name = "📐 Математика: Базовый счет",
            Description = "Простые арифметические действия, дроби и основы геометрии."
        });

        _questions.Add(new Question
        {
            Id = 11,
            SubjectId = 3,
            Text = "Вычислите значение выражения: 2 + 2 * 2",
            Options = new List<string> { "8", "6", "4", "5" },
            CorrectAnswerIndex = 1 // 6 (первым идет умножение)
        });
        _questions.Add(new Question
        {
            Id = 12,
            SubjectId = 3,
            Text = "Чему равен квадрат числа 5 (5 во второй степени)?",
            Options = new List<string> { "10", "20", "25", "50" },
            CorrectAnswerIndex = 2 // 25
        });
        _questions.Add(new Question
        {
            Id = 13,
            SubjectId = 3,
            Text = "Какое число получится, если 1/4 (одну четвертую) перевести в десятичную дробь?",
            Options = new List<string> { "0.4", "0.25", "0.5", "0.04" },
            CorrectAnswerIndex = 1 // 0.25
        });
        _questions.Add(new Question
        {
            Id = 14,
            SubjectId = 3,
            Text = "Чему равна сумма углов в абсолютно любом треугольнике?",
            Options = new List<string> { "90 градусов", "180 градусов", "360 градусов", "270 градусов" },
            CorrectAnswerIndex = 1 // 180 градусов
        });
        _questions.Add(new Question
        {
            Id = 15,
            SubjectId = 3,
            Text = "Решите простейшее уравнение: 3x = 12. Чему равен x?",
            Options = new List<string> { "2", "3", "4", "9" },
            CorrectAnswerIndex = 2 // 4
        });
    }
}