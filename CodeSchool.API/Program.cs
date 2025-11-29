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

    // Atualizar descrições dos desafios (corrige enunciados)
    AtualizarDescricoesDesafios(context);

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

void AtualizarDescricoesDesafios(AppDbContext context)
{
    var desafios = context.Desafios.ToList();

    if (desafios.Count == 0) return;

    // Verificar se já foi atualizado (checando se a descrição do desafio 1 contém "MOVER")
    if (desafios[0].Descricao.Contains("MOVER"))
    {
        Console.WriteLine("✅ Descrições dos desafios já estão atualizadas!");
        return;
    }

    Console.WriteLine("🔄 Atualizando descrições dos desafios...");

    // Atualizar cada desafio
    foreach (var desafio in desafios)
    {
        switch (desafio.Id)
        {
            case 1:
                desafio.Descricao = "Mova o robô 3 passos para frente até alcançar o objetivo. Use apenas o bloco MOVER.";
                desafio.Objetivo = "Alcançar a posição [3,0]";
                break;
            case 2:
                desafio.Descricao = "Faça o robô andar 2 passos para frente, virar à direita e andar mais 2 passos até o objetivo.";
                desafio.Objetivo = "Alcançar a posição [2,2]";
                break;
            case 3:
                desafio.Descricao = "Use o bloco REPETIR para fazer o robô andar 5 passos sem repetir o bloco MOVER manualmente.";
                desafio.Objetivo = "Alcançar a posição [4,0] usando loops";
                break;
            case 4:
                desafio.Descricao = "Faça o robô andar em forma de quadrado (1 passo para cada lado) e voltar à posição inicial. Use LOOPS!";
                desafio.Objetivo = "Voltar para a posição inicial [1,1]";
                break;
            case 5:
                desafio.Descricao = "Navegue pelo corredor em formato de L. Ande 4 passos para frente, vire à direita e ande mais 2 passos para baixo.";
                desafio.Objetivo = "Alcançar a posição [4,0]";
                break;
            case 6:
                desafio.Descricao = "Suba a escada diagonal fazendo um movimento em zigue-zague. Padrão: mover, virar esquerda, mover, virar direita.";
                desafio.Objetivo = "Alcançar a posição [4,0]";
                break;
            case 7:
                desafio.Descricao = "Percorra o grid em zigue-zague da posição [0,0] até [4,4]. Planeje bem seus movimentos e viradas!";
                desafio.Objetivo = "Alcançar a posição [4,4]";
                break;
            case 8:
                desafio.Descricao = "Explore o mapa grande (6x6) indo da posição inicial [0,0] até o canto oposto [5,5]. Planeje a rota mais eficiente!";
                desafio.Objetivo = "Alcançar a posição [5,5]";
                break;
            case 9:
                desafio.Descricao = "Crie um movimento em espiral saindo do centro [3,3] até a borda do grid [6,0]. Desafio avançado com loops complexos!";
                desafio.Objetivo = "Alcançar a posição [6,0]";
                break;
            case 10:
                desafio.Descricao = "O GRANDE DESAFIO FINAL! Percorra o grid 7x7 do canto superior esquerdo [0,6] até o canto inferior direito [6,0]. Use TUDO que aprendeu: loops, viradas estratégicas e sequências complexas!";
                desafio.Objetivo = "Alcançar a posição [6,0]";
                break;
        }
    }

    context.SaveChanges();
    Console.WriteLine("✅ Descrições dos desafios atualizadas com sucesso!");
}