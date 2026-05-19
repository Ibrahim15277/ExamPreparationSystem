using Microsoft.EntityFrameworkCore;
using ExamPreparationSystem.Domain;

namespace ExamPreparationSystem.Application;

public class AppDbContext : DbContext
{
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<ExamTest> ExamTests { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Связь 1:N — У одного Предмета много Тестов
        modelBuilder.Entity<ExamTest>()
            .HasOne<Subject>()
            .WithMany()
            .HasForeignKey(t => t.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // Связь 1:N — У одного Теста много Вопросов
        modelBuilder.Entity<Question>()
            .HasOne<ExamTest>()
            .WithMany()
            .HasForeignKey(q => q.TestId)
            .OnDelete(DeleteBehavior.Cascade);

        // Конвертация списка строк в одну строку для SQLite
        modelBuilder.Entity<Question>()
            .Property(q => q.Options)
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        
        // НАПОЛНЕНИЕ БАЗЫ ДАННЫХ (SEED DATA)
        

        // 1. ПРЕДМЕТЫ (Категории)
        modelBuilder.Entity<Subject>().HasData(
            new Subject { Id = 1, Name = "💻 Информатика", Description = "Системы счисления и основы кодирования данных." },
            new Subject { Id = 2, Name = "📚 Русский язык", Description = "Орфография, пунктуация и культура речи." },
            new Subject { Id = 3, Name = "📐 Математика", Description = "Арифметика, уравнения и основы геометрии." }
        );

        // 2. ТЕСТЫ (Подкаталоги вариантов для каждого предмета)
        modelBuilder.Entity<ExamTest>().HasData(
            // Тесты по Информатике
            new ExamTest { Id = 1, SubjectId = 1, Title = "Вариант №1: Двоичная система", Description = "Тест на перевод чисел между 2-й и 10-й системами счисления." },
            new ExamTest { Id = 2, SubjectId = 1, Title = "Вариант №2: Шестнадцатеричная система", Description = "Тест на работу с большими позиционными системами счисления." },

            // Тесты по Русскому языку
            new ExamTest { Id = 3, SubjectId = 2, Title = "Вариант №1: Правила ЖИ-ШИ и Части речи", Description = "Проверка базовых орфографических правил начальной школы." },
            new ExamTest { Id = 4, SubjectId = 2, Title = "Вариант №2: Пунктуация и Согласные", Description = "Тест на расстановку знаков препинания и парные согласные." },

            // Тесты по Математике
            new ExamTest { Id = 5, SubjectId = 3, Title = "Вариант №1: Арифметика и Уравнения", Description = "Простые математические операции, приоритеты действий и поиск X." },
            new ExamTest { Id = 6, SubjectId = 3, Title = "Вариант №2: Геометрия и Дроби", Description = "Углы фигур, площади и перевод долей в десятичный формат." }
        );

        // 3. ВОПРОСЫ (Привязанные к Тестам через TestId)
        modelBuilder.Entity<Question>().HasData(
            // --- ИНФОРМАТИКА: ТЕСТ 1 (Двоичная система) ---
            new Question { Id = 1, TestId = 1, Text = "Переведите двоичное число 1101 в десятичную систему:", Options = new List<string> { "11", "13", "15", "9" }, CorrectAnswerIndex = 1 },
            new Question { Id = 2, TestId = 1, Text = "Чему равно число 25 из десятичной системы в двоичной?", Options = new List<string> { "11001", "10111", "11100", "10011" }, CorrectAnswerIndex = 0 },
            new Question { Id = 3, TestId = 1, Text = "Сколько бит содержится в одном байте?", Options = new List<string> { "4", "8", "10", "16" }, CorrectAnswerIndex = 1 },

            // --- ИНФОРМАТИКА: ТЕСТ 2 (Шестнадцатеричная система) ---
            new Question { Id = 4, TestId = 2, Text = "Переведите шестнадцатеричное число 1A в десятичную систему:", Options = new List<string> { "16", "24", "26", "30" }, CorrectAnswerIndex = 2 },
            new Question { Id = 5, TestId = 2, Text = "Какое число в восьмеричной системе соответствует двоичному числу 111?", Options = new List<string> { "5", "6", "7", "11" }, CorrectAnswerIndex = 2 },
            new Question { Id = 6, TestId = 2, Text = "Какая цифра отсутствует в шестнадцатеричной системе счисления?", Options = new List<string> { "9", "A", "G", "F" }, CorrectAnswerIndex = 2 },

            // --- РУССКИЙ ЯЗЫК: ТЕСТ 1 (Орфография и части речи) ---
            new Question { Id = 7, TestId = 3, Text = "В каком слове допущена ошибка в правописании сочетаний ЖИ-ШИ?", Options = new List<string> { "Машина", "Жираф", "Жызнь", "Шина" }, CorrectAnswerIndex = 2 },
            new Question { Id = 8, TestId = 3, Text = "Какая из этих частей речи отвечает на вопросы «Кто?» или «Что?»?", Options = new List<string> { "Глагол", "Имя существительное", "Имя прилагательное", "Наречие" }, CorrectAnswerIndex = 1 },
            new Question { Id = 9, TestId = 3, Text = "Что обозначает глагол как часть речи?", Options = new List<string> { "Признак предмета", "Предмет", "Действие предмета", "Количество" }, CorrectAnswerIndex = 2 },

            // --- РУССКИЙ ЯЗЫК: ТЕСТ 2 (Пунктуация и согласные) ---
            new Question { Id = 10, TestId = 4, Text = "Укажите слово, в котором на конце пишется парная согласная 'З', а слышится 'С':", Options = new List<string> { "Нос", "Лес", "Класс", "Арбуз" }, CorrectAnswerIndex = 3 },
            new Question { Id = 11, TestId = 4, Text = "Какой знак препинания ставится в конце стандартного повествовательного предложения?", Options = new List<string> { "Запятая", "Точка", "Вопросительный знак", "Двоеточие" }, CorrectAnswerIndex = 1 },
            new Question { Id = 12, TestId = 4, Text = "Где всегда пишется большая (прописная) буква?", Options = new List<string> { "В именах собственных", "В конце строки", "Перед запятой", "В длинных словах" }, CorrectAnswerIndex = 0 },

            // --- МАТЕМАТИКА: ТЕСТ 1 (Арифметика) ---
            new Question { Id = 13, TestId = 5, Text = "Вычислите значение выражения: 2 + 2 * 2", Options = new List<string> { "8", "6", "4", "5" }, CorrectAnswerIndex = 1 },
            new Question { Id = 14, TestId = 5, Text = "Чему равен квадрат числа 5 (5 во второй степени)?", Options = new List<string> { "10", "20", "25", "50" }, CorrectAnswerIndex = 2 },
            new Question { Id = 15, TestId = 5, Text = "Решите уравнение: 3x = 12. Чему равен x?", Options = new List<string> { "2", "3", "4", "9" }, CorrectAnswerIndex = 2 },

            // --- МАТЕМАТИКА: ТЕСТ 2 (Геометрия и дроби) ---
            new Question { Id = 16, TestId = 6, Text = "Какое число получится, если 1/4 перевести в десятичную дробь?", Options = new List<string> { "0.4", "0.25", "0.5", "0.04" }, CorrectAnswerIndex = 1 },
            new Question { Id = 17, TestId = 6, Text = "Чему равна сумма углов в абсолютно любом треугольнике?", Options = new List<string> { "90 градусов", "180 градусов", "360 градусов", "270 градусов" }, CorrectAnswerIndex = 1 },
            new Question { Id = 18, TestId = 6, Text = "Как называется геометрическая фигура, у которой все четыре стороны равны, а углы прямые?", Options = new List<string> { "Ромб", "Треугольник", "Квадрат", "Трапеция" }, CorrectAnswerIndex = 2 }
        );
    }
}