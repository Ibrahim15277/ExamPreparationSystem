using Microsoft.EntityFrameworkCore;
using ExamPreparationSystem.Domain;

namespace ExamPreparationSystem.Application;

public class AppDbContext : DbContext
{
	public DbSet<Subject> Subjects { get; set; } = null!;
	public DbSet<Question> Questions { get; set; } = null!;

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Настройка связи 1:N
		modelBuilder.Entity<Question>()
			.HasOne<Subject>()
			.WithMany()
			.HasForeignKey(q => q.SubjectId)
			.OnDelete(DeleteBehavior.Cascade);

		// Конвертация списка строк в одну строку для SQLite
		modelBuilder.Entity<Question>()
			.Property(q => q.Options)
			.HasConversion(
				v => string.Join('|', v),
				v => v.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList()
			);

		// ==========================================
		// НАПОЛНЕНИЕ БАЗЫ ДАННЫХ (SEED DATA)
		// ==========================================
		modelBuilder.Entity<Subject>().HasData(
			new Subject { Id = 1, Name = "💻 Информатика: Системы счисления", Description = "Перевод чисел между двоичной, восьмеричной, десятичной и шестнадцатеричной системами." },
			new Subject { Id = 2, Name = "📚 Русский язык: Базовая орфография", Description = "Простейшие правила правописания, части речи и знаки препинания." },
			new Subject { Id = 3, Name = "📐 Математика: Базовый счет", Description = "Простые арифметические действия, дроби и основы геометрии." }
		);

		modelBuilder.Entity<Question>().HasData(
			// Информатика (5 вопросов)
			new Question { Id = 1, SubjectId = 1, Text = "Переведите двоичное число 1101 в десятичную систему счисления:", Options = new List<string> { "11", "13", "15", "9" }, CorrectAnswerIndex = 1 },
			new Question { Id = 2, SubjectId = 1, Text = "Чему равно число 25 из десятичной системы в двоичной?", Options = new List<string> { "11001", "10111", "11100", "10011" }, CorrectAnswerIndex = 0 },
			new Question { Id = 3, SubjectId = 1, Text = "Переведите шестнадцатеричное число 1A в десятичную систему:", Options = new List<string> { "16", "24", "26", "30" }, CorrectAnswerIndex = 2 },
			new Question { Id = 4, SubjectId = 1, Text = "Какое число в восьмеричной системе соответствует двоичному числу 111?", Options = new List<string> { "5", "6", "7", "11" }, CorrectAnswerIndex = 2 },
			new Question { Id = 5, SubjectId = 1, Text = "Сколько бит содержится в одном байте?", Options = new List<string> { "4", "8", "10", "16" }, CorrectAnswerIndex = 1 },

			// Русский язык (5 вопросов)
			new Question { Id = 6, SubjectId = 2, Text = "В каком слове допущена ошибка в правописании сочетаний ЖИ-ШИ?", Options = new List<string> { "Машина", "Жираф", "Жызнь", "Шина" }, CorrectAnswerIndex = 2 },
			new Question { Id = 7, SubjectId = 2, Text = "Какая из этих частей речи отвечает на вопросы «Кто?» или «Что?»?", Options = new List<string> { "Глагол", "Имя существительное", "Имя прилагательное", "Наречие" }, CorrectAnswerIndex = 1 },
			new Question { Id = 8, SubjectId = 2, Text = "Укажите слово, в котором на конце пишется парная согласная 'З', а слышится 'С':", Options = new List<string> { "Нос", "Лес", "Класс", "Арбуз" }, CorrectAnswerIndex = 3 },
			new Question { Id = 9, SubjectId = 2, Text = "Какое слово всегда пишется с большой (прописной) буквы в любом месте предложения?", Options = new List<string> { "Город", "Москва", "Река", "Книга" }, CorrectAnswerIndex = 1 },
			new Question { Id = 10, SubjectId = 2, Text = "Какой знак препинания ставится в конце стандартного повествовательного предложения?", Options = new List<string> { "Запятая", "Точка", "Вопросительный знак", "Двоеточие" }, CorrectAnswerIndex = 1 },

			// Математика (5 вопросов)
			new Question { Id = 11, SubjectId = 3, Text = "Вычислите значение выражения: 2 + 2 * 2", Options = new List<string> { "8", "6", "4", "5" }, CorrectAnswerIndex = 1 },
			new Question { Id = 12, SubjectId = 3, Text = "Чему равен квадрат числа 5 (5 во второй степени)?", Options = new List<string> { "10", "20", "25", "50" }, CorrectAnswerIndex = 2 },
			new Question { Id = 13, SubjectId = 3, Text = "Какое число получится, если 1/4 (одну четвертую) перевести в десятичную дробь?", Options = new List<string> { "0.4", "0.25", "0.5", "0.04" }, CorrectAnswerIndex = 1 },
			new Question { Id = 14, SubjectId = 3, Text = "Чему равна сумма углов в абсолютно любом треугольнике?", Options = new List<string> { "90 градусов", "180 градусов", "360 градусов", "270 градусов" }, CorrectAnswerIndex = 1 },
			new Question { Id = 15, SubjectId = 3, Text = "Решите простейшее уравнение: 3x = 12. Чему равен x?", Options = new List<string> { "2", "3", "4", "9" }, CorrectAnswerIndex = 2 }
		);
	}
}