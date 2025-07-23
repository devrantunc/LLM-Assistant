using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
Env.Load(); // .env dosyasını yükler

//OpenRoutera istek atar
builder.Services.AddHttpClient();

//Controllerları etkinleştir
builder.Services.AddControllers();

//CORS ayarları
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

//CORSu etkinleştir
app.UseCors();

//API çağrılarını yönlendir
app.MapControllers();


app.Run();
