<template>
  <div class="login-container">
    <div class="login-card">
      <div class="logo">
        <h1>🎮 CodeSchool</h1>
        <p>Aprenda Pensamento Computacional Jogando!</p>
      </div>

      <div class="tabs">
        <button :class="{ active: !isCadastro }" @click="isCadastro = false" aria-label="Ir para aba de login">
          Entrar
        </button>
        <button :class="{ active: isCadastro }" @click="isCadastro = true" aria-label="Ir para aba de cadastro">
          Cadastrar
        </button>
      </div>

      <!-- FORMULÁRIO DE LOGIN -->
      <form v-if="!isCadastro" @submit.prevent="fazerLogin" class="form">
        <div class="form-group">
          <label for="login-email">Email</label>
          <input id="login-email" v-model="loginForm.email" type="email" placeholder="seu@email.com" required
            aria-required="true" />
        </div>

        <div class="form-group">
          <label for="login-senha">Senha</label>
          <input id="login-senha" v-model="loginForm.senha" type="password" placeholder="••••••••" required
            aria-required="true" />
        </div>

        <button type="submit" class="btn-primary" :disabled="loading">
          {{ loading ? 'Entrando...' : 'Entrar' }}
        </button>
      </form>

      <!-- FORMULÁRIO DE CADASTRO -->
      <form v-else @submit.prevent="fazerCadastro" class="form">
        <div class="form-group">
          <label for="cadastro-nome">Nome Completo</label>
          <input id="cadastro-nome" v-model="cadastroForm.nome" type="text" placeholder="João Silva" required
            aria-required="true" />
        </div>

        <div class="form-group">
          <label for="cadastro-email">Email</label>
          <input id="cadastro-email" v-model="cadastroForm.email" type="email" placeholder="seu@email.com" required
            aria-required="true" />
        </div>

        <div class="form-group">
          <label for="cadastro-senha">Senha</label>
          <input id="cadastro-senha" v-model="cadastroForm.senha" type="password" placeholder="••••••••" minlength="6"
            required aria-required="true" />
        </div>

        <div class="form-group">
          <label for="cadastro-tipo">Eu sou:</label>
          <select id="cadastro-tipo" v-model="cadastroForm.tipoUsuario">
            <option value="Aluno">Aluno</option>
            <option value="Professor">Professor</option>
          </select>
        </div>

        <button type="submit" class="btn-primary" :disabled="loading">
          {{ loading ? 'Cadastrando...' : 'Cadastrar' }}
        </button>
      </form>

      <!-- MENSAGEM DE ERRO/SUCESSO -->
      <div v-if="mensagem" :class="['mensagem', mensagemTipo]" role="alert">
        {{ mensagem }}
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '../stores/user'
import { authService } from '../services/api'

const router = useRouter()
const userStore = useUserStore()

const isCadastro = ref(false)
const loading = ref(false)
const mensagem = ref('')
const mensagemTipo = ref('') // 'sucesso' ou 'erro'

const loginForm = ref({
  email: '',
  senha: ''
})

const cadastroForm = ref({
  nome: '',
  email: '',
  senha: '',
  tipoUsuario: 'Aluno'
})

async function fazerLogin() {
  loading.value = true
  mensagem.value = ''

  try {
    const dados = await authService.login(loginForm.value.email, loginForm.value.senha)
    userStore.setUsuario(dados)

    mensagem.value = 'Login realizado com sucesso!'
    mensagemTipo.value = 'sucesso'

    setTimeout(() => {
      if (dados.tipo === 'Professor') {
        router.push('/professor')
      } else {
        router.push('/dashboard')
      }
    }, 500)
  } catch (error) {
    mensagem.value = error.response?.data?.mensagem || 'Erro ao fazer login'
    mensagemTipo.value = 'erro'
  } finally {
    loading.value = false
  }
}

async function fazerCadastro() {
  loading.value = true
  mensagem.value = ''

  try {
    const dados = await authService.registrar(cadastroForm.value)
    userStore.setUsuario(dados)

    mensagem.value = 'Cadastro realizado! Redirecionando...'
    mensagemTipo.value = 'sucesso'

    setTimeout(() => {
      if (dados.tipo === 'Professor') {
        router.push('/professor')
      } else {
        router.push('/dashboard')
      }
    }, 500)
  } catch (error) {
    mensagem.value = error.response?.data?.mensagem || 'Erro ao cadastrar'
    mensagemTipo.value = 'erro'
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.login-container {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  padding: 20px;
}

.login-card {
  background: white;
  border-radius: 20px;
  padding: 40px;
  box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
  max-width: 450px;
  width: 100%;
}

.logo {
  text-align: center;
  margin-bottom: 30px;
}

.logo h1 {
  font-size: 2.5rem;
  margin: 0;
  color: #667eea;
}

.logo p {
  color: #666;
  margin-top: 10px;
}

.tabs {
  display: flex;
  gap: 10px;
  margin-bottom: 30px;
}

.tabs button {
  flex: 1;
  padding: 12px;
  border: none;
  background: #f0f0f0;
  border-radius: 10px;
  font-size: 1rem;
  cursor: pointer;
  transition: all 0.3s;
}

.tabs button.active {
  background: #667eea;
  color: white;
  font-weight: bold;
}

.tabs button:hover:not(.active) {
  background: #e0e0e0;
}

.form {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.form-group label {
  font-weight: 600;
  color: #333;
}

.form-group input,
.form-group select {
  padding: 12px;
  border: 2px solid #e0e0e0;
  border-radius: 10px;
  font-size: 1rem;
  transition: border-color 0.3s;
}

.form-group input:focus,
.form-group select:focus {
  outline: none;
  border-color: #667eea;
}

.btn-primary {
  padding: 15px;
  background: #667eea;
  color: white;
  border: none;
  border-radius: 10px;
  font-size: 1.1rem;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-primary:hover:not(:disabled) {
  background: #5568d3;
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(102, 126, 234, 0.4);
}

.btn-primary:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}

.mensagem {
  margin-top: 20px;
  padding: 15px;
  border-radius: 10px;
  text-align: center;
  font-weight: 600;
}

.mensagem.sucesso {
  background: #d4edda;
  color: #155724;
  border: 1px solid #c3e6cb;
}

.mensagem.erro {
  background: #f8d7da;
  color: #721c24;
  border: 1px solid #f5c6cb;
}

/* Acessibilidade */
@media (prefers-reduced-motion: reduce) {
  * {
    transition: none !important;
  }
}
</style>