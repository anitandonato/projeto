using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CodeSchool.API.Data;
using CodeSchool.API.Models;
using CodeSchool.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Digite 'Bearer' seguido do seu token JWT. Exemplo: Bearer eyJhbGci..."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Banco de dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=codeschool.db"));
    builder.Services.AddScoped<AuthService>();
    builder.Services.AddScoped<GameService>();
// Configurar CORS (permitir frontend acessar API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configurar autenticação JWT
var jwtKey = "ChaveSecretaSuperSegura123!@#CodeSchool2024";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "CodeSchoolAPI",
            ValidAudience = "CodeSchoolApp",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

var app = builder.Build();

// ========== CRIAR BANCO AUTOMATICAMENTE ==========
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    // Criar apenas usuários (desafios já vêm do seed)
    CriarUsuariosIniciais(context);

    // Popular dados de teste
    await CodeSchool.API.SeedData.PopularDadosTeste(context);
}

// Configurar pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
// ========== FUNÇÕES DE SEED ==========

void CriarUsuariosIniciais(AppDbContext context)
{
    if (context.Usuarios.Any()) return;

    var usuarios = new List<Usuario>
    {
        new Usuario
        {
            Nome = "Maria Santos",
            Email = "maria@aluno.com",
            SenhaHash = BCrypt.Net.BCrypt.HashPassword("senha123"),
            Tipo = TipoUsuario.Aluno,
            AvatarId = 1
        },
        new Usuario
        {
            Nome = "Prof. Ana Silva",
            Email = "ana@professor.com",
            SenhaHash = BCrypt.Net.BCrypt.HashPassword("senha123"),
            Tipo = TipoUsuario.Professor,
            AvatarId = 2
        }
    };

    context.Usuarios.AddRange(usuarios);
    context.SaveChanges();
    Console.WriteLine("✅ Usuários criados com sucesso!");
}