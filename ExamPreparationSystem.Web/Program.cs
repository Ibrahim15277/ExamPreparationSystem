using Microsoft.EntityFrameworkCore;
using ExamPreparationSystem.Application;
using ExamPreparationSystem.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// 1. Добавляем поддержку компонентов Razor и интерактивного Server-режима
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 2. РЕГИСТРАЦИЯ БАЗЫ ДАННЫХ SQLite
// Файл базы данных exam.db создастся в корне папки запуска приложения
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=exam.db"));

// 3. РЕГИСТРАЦИЯ СЕРВИСА ЭКЗАМЕНОВ 
builder.Services.AddScoped<ExamService>();

var app = builder.Build();

// Настройка конвейера обработки HTTP-запросов (Middleware)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // Значение HSTS по умолчанию составляет 30 дней.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();

// Маппинг главного компонента Blazor и включение интерактивного режима рендеринга
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();