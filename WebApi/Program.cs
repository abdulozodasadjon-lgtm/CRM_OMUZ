using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<GroupRepository>();
builder.Services.AddScoped<MentorRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<StudentGroupRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
