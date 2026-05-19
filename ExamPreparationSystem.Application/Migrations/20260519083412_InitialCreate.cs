using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamPreparationSystem.Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Options = table.Column<string>(type: "TEXT", nullable: false),
                    CorrectAnswerIndex = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Перевод чисел между двоичной, восьмеричной, десятичной и шестнадцатеричной системами.", "💻 Информатика: Системы счисления" },
                    { 2, "Простейшие правила правописания, части речи и знаки препинания.", "📚 Русский язык: Базовая орфография" },
                    { 3, "Простые арифметические действия, дроби и основы геометрии.", "📐 Математика: Базовый счет" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CorrectAnswerIndex", "Options", "SubjectId", "Text" },
                values: new object[,]
                {
                    { 1, 1, "11|13|15|9", 1, "Переведите двоичное число 1101 в десятичную систему счисления:" },
                    { 2, 0, "11001|10111|11100|10011", 1, "Чему равно число 25 из десятичной системы в двоичной?" },
                    { 3, 2, "16|24|26|30", 1, "Переведите шестнадцатеричное число 1A в десятичную систему:" },
                    { 4, 2, "5|6|7|11", 1, "Какое число в восьмеричной системе соответствует двоичному числу 111?" },
                    { 5, 1, "4|8|10|16", 1, "Сколько бит содержится в одном байте?" },
                    { 6, 2, "Машина|Жираф|Жызнь|Шина", 2, "В каком слове допущена ошибка в правописании сочетаний ЖИ-ШИ?" },
                    { 7, 1, "Глагол|Имя существительное|Имя прилагательное|Наречие", 2, "Какая из этих частей речи отвечает на вопросы «Кто?» или «Что?»?" },
                    { 8, 3, "Нос|Лес|Класс|Арбуз", 2, "Укажите слово, в котором на конце пишется парная согласная 'З', а слышится 'С':" },
                    { 9, 1, "Город|Москва|Река|Книга", 2, "Какое слово всегда пишется с большой (прописной) буквы в любом месте предложения?" },
                    { 10, 1, "Запятая|Точка|Вопросительный знак|Двоеточие", 2, "Какой знак препинания ставится в конце стандартного повествовательного предложения?" },
                    { 11, 1, "8|6|4|5", 3, "Вычислите значение выражения: 2 + 2 * 2" },
                    { 12, 2, "10|20|25|50", 3, "Чему равен квадрат числа 5 (5 во второй степени)?" },
                    { 13, 1, "0.4|0.25|0.5|0.04", 3, "Какое число получится, если 1/4 (одну четвертую) перевести в десятичную дробь?" },
                    { 14, 1, "90 градусов|180 градусов|360 градусов|270 градусов", 3, "Чему равна сумма углов в абсолютно любом треугольнике?" },
                    { 15, 2, "2|3|4|9", 3, "Решите простейшее уравнение: 3x = 12. Чему равен x?" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SubjectId",
                table: "Questions",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
