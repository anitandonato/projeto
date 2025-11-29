# COMO CORRIGIR OS DESAFIOS

## 🐛 Problemas Resolvidos:

1. ✅ **Blockly ficando "troncho"** ao clicar em "Continuar" → CORRIGIDO no código
2. ⏳ **Descrições dos desafios erradas** → Precisa atualizar o banco de dados

---

## 🔧 PASSO A PASSO PARA CORRIGIR AS DESCRIÇÕES:

### OPÇÃO 1: Usando DB Browser for SQLite (MAIS FÁCIL)

1. **Baixe o DB Browser for SQLite:**
   - Link: https://sqlitebrowser.org/dl/
   - Ou use: https://sqliteonline.com/ (online, não precisa instalar)

2. **Abra o banco de dados:**
   - Abrir arquivo: `C:\Users\anita\Documents\CodeSchool\CodeSchool.API\codeschool.db`
   - OU arraste o arquivo para o DB Browser

3. **Execute o script:**
   - Clique na aba **"Execute SQL"**
   - Abra o arquivo `CORRIGIR-DESAFIOS.sql` no Bloco de Notas
   - **Copie TODO o conteúdo** (Ctrl+A, Ctrl+C)
   - **Cole na janela SQL** do DB Browser
   - Clique em **"Execute"** (▶️ ou F5)

4. **Salvar mudanças:**
   - Clique em **"Write Changes"** (💾)
   - Pronto! Banco atualizado ✅

5. **Reinicie o backend:**
   ```bash
   # Pare o backend (Ctrl+C)
   # Rode novamente:
   cd CodeSchool.API
   dotnet run
   ```

---

### OPÇÃO 2: Deletar banco e recriar (MAIS RÁPIDO mas perde dados)

**⚠️ ATENÇÃO: Isso apaga TODOS os dados (usuários, turmas, progressos)**

1. **Pare o backend** (Ctrl+C no terminal)

2. **Delete o banco:**
   ```bash
   cd C:\Users\anita\Documents\CodeSchool\CodeSchool.API
   del codeschool.db
   del codeschool.db-shm
   del codeschool.db-wal
   ```

3. **Atualize as descrições no código:**
   - Abra: `C:\Users\anita\Documents\CodeSchool\CodeSchool.API\Data\Data\AppDbContext.cs`
   - Procure por `new Desafio` e atualize as descrições conforme o arquivo `CORRIGIR-DESAFIOS.sql`

4. **Recrie o banco:**
   ```bash
   dotnet ef database update
   ```

5. **Rode o backend:**
   ```bash
   dotnet run
   ```

6. **Crie novos usuários de teste** (faça cadastro manual no frontend)

---

### OPÇÃO 3: Atualizar via código C# (PROFISSIONAL)

Se você quiser atualizar programaticamente, adicione este método em `Program.cs`:

```csharp
// Adicione ANTES de app.Run();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Atualizar descrições
    var desafios = context.Desafios.ToList();

    desafios[0].Descricao = "Mova o robô 3 passos para frente até alcançar o objetivo. Use apenas o bloco MOVER.";
    desafios[1].Descricao = "Faça o robô andar 2 passos para frente, virar à direita e andar mais 2 passos até o objetivo.";
    // ... continue para os outros

    context.SaveChanges();
    Console.WriteLine("✅ Descrições atualizadas!");
}
```

---

## 🧪 TESTAR AS CORREÇÕES:

1. **Reinicie TUDO:**
   ```bash
   # Terminal 1: Backend
   cd CodeSchool.API
   dotnet run

   # Terminal 2: Frontend
   cd CodeSchool.Frontend
   npm run dev
   ```

2. **Teste o Blockly:**
   - Faça login como aluno
   - Resolva um desafio
   - Clique em **"Continuar"**
   - ✅ O Blockly deve recarregar limpo (não "troncho")

3. **Teste as descrições:**
   - Abra cada desafio (1 a 10)
   - Verifique se a descrição está correta
   - Verifique se o objetivo está claro

---

## 📋 DESCRIÇÕES CORRETAS (para referência):

1. **Primeiros Passos:** Mova o robô 3 passos para frente até alcançar o objetivo. Use apenas o bloco MOVER.

2. **Virando à Direita:** Faça o robô andar 2 passos para frente, virar à direita e andar mais 2 passos até o objetivo.

3. **Usando Loops:** Use o bloco REPETIR para fazer o robô andar 5 passos sem repetir o bloco MOVER manualmente.

4. **Quadrado Perfeito:** Faça o robô andar em forma de quadrado (1 passo para cada lado) e voltar à posição inicial. Use LOOPS!

5. **Corredor em L:** Navegue pelo corredor em formato de L. Ande 4 passos para frente, vire à direita e ande mais 2 passos para baixo.

6. **Escadaria:** Suba a escada diagonal fazendo um movimento em zigue-zague. Padrão: mover, virar esquerda, mover, virar direita.

7. **Zigue-Zague:** Percorra o grid em zigue-zague da posição [0,0] até [4,4]. Planeje bem seus movimentos e viradas!

8. **Explorador:** Explore o mapa grande (6x6) indo da posição inicial [0,0] até o canto oposto [5,5]. Planeje a rota mais eficiente!

9. **Espiral:** Crie um movimento em espiral saindo do centro [3,3] até a borda do grid [6,0]. Desafio avançado com loops complexos!

10. **Desafio Final:** O GRANDE DESAFIO FINAL! Percorra o grid 7x7 do canto superior esquerdo [0,6] até o canto inferior direito [6,0]. Use TUDO que aprendeu!

---

## ✅ PRONTO!

Após aplicar as correções:
- ✅ Blockly não vai mais ficar quebrado ao clicar "Continuar"
- ✅ Descrições dos desafios estarão claras e objetivas
- ✅ Sistema pronto para gravar vídeo e tirar prints!

---

**Se tiver dúvidas, me chama!** 🚀
