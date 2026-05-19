using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamPreparationSystem.Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialNestedStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamTests_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Options = table.Column<string>(type: "TEXT", nullable: false),
                    CorrectAnswerIndex = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_ExamTests_TestId",
                        column: x => x.TestId,
                        principalTable: "ExamTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Системы счисления и основы кодирования данных.", "💻 Информатика" },
                    { 2, "Орфография, пунктуация и культура речи.", "📚 Русский язык" },
                    { 3, "Арифметика, уравнения и основы геометрии.", "📐 Математика" }
                });

            migrationBuilder.InsertData(
                table: "ExamTests",
                columns: new[] { "Id", "Description", "SubjectId", "Title" },
                values: new object[,]
                {
                    { 1, "Тест на перевод чисел между 2-й и 10-й системами счисления.", 1, "Вариант №1: Двоичная система" },
                    { 2, "Тест на работу с большими позиционными системами счисления.", 1, "Вариант №2: Шестнадцатеричная система" },
                    { 3, "Проверка базовых орфографических правил начальной школы.", 2, "Вариант №1: Правила ЖИ-ШИ и Части речи" },
                    { 4, "Тест на расстановку знаков препинания и парные согласные.", 2, "Вариант №2: Пунктуация и Согласные" },
                    { 5, "Простые математические операции, приоритеты действий и поиск X.", 3, "Вариант №1: Арифметика и Уравнения" },
                    { 6, "Углы фигур, площади и перевод долей в десятичный формат.", 3, "Вариант №2: Геометрия и Дроби" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CorrectAnswerIndex", "Options", "TestId", "Text" },
                values: new object[,]
                {
                    { 1, 1, "11|13|15|9", 1, "Переведите двоичное число 1101 в десятичную систему:" },
                    { 2, 0, "11001|10111|11100|10011", 1, "Чему равно число 25 из десятичной системы в двоичной?" },
                    { 3, 1, "4|8|10|16", 1, "Сколько бит содержится в одном байте?" },
                    { 4, 2, "16|24|26|30", 2, "Переведите шестнадцатеричное число 1A в десятичную систему:" },
                    { 5, 2, "5|6|7|11", 2, "Какое число в восьмеричной системе соответствует двоичному числу 111?" },
                    { 6, 2, "9|A|G|F", 2, "Какая цифра отсутствует в шестнадцатеричной системе счисления?" },
                    { 7, 2, "Машина|Жираф|Жызнь|Шина", 3, "В каком слове допущена ошибка в правописании сочетаний ЖИ-ШИ?" },
                    { 8, 1, "Глагол|Имя существительное|Имя прилагательное|Наречие", 3, "Какая из этих частей речи отвечает на вопросы «Кто?» или «Что?»?" },
                    { 9, 2, "Признак предмета|Предмет|Действие предмета|Количество", 3, "Что обозначает глагол как часть речи?" },
                    { 10, 3, "Нос|Лес|Класс|Арбуз", 4, "Укажите слово, в котором на конце пишется парная согласная 'З', а слышится 'С':" },
                    { 11, 1, "Запятая|Точка|Вопросительный знак|Двоеточие", 4, "Какой знак препинания ставится в конце стандартного повествовательного предложения?" },
                    { 12, 0, "В именах собственных|В конце строки|Перед запятой|В длинных словах", 4, "Где всегда пишется большая (прописная) буква?" },
                    { 13, 1, "8|6|4|5", 5, "Вычислите значение выражения: 2 + 2 * 2" },
                    { 14, 2, "10|20|25|50", 5, "Чему равен квадрат числа 5 (5 во второй степени)?" },
                    { 15, 2, "2|3|4|9", 5, "Решите уравнение: 3x = 12. Чему равен x?" },
                    { 16, 1, "0.4|0.25|0.5|0.04", 6, "Какое число получится, если 1/4 перевести в десятичную дробь?" },
                    { 17, 1, "90 градусов|180 градусов|360 градусов|270 градусов", 6, "Чему равна сумма углов в абсолютно любом треугольнике?" },
                    { 18, 2, "Ромб|Треугольник|Квадрат|Трапеция", 6, "Как называется геометрическая фигура, у которой все четыре стороны равны, а углы прямые?" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamTests_SubjectId",
                table: "ExamTests",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestId",
                table: "Questions",
                column: "TestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "ExamTests");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
