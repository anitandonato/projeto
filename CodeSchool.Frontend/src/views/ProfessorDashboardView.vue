<template>
  <div class="professor-dashboard">
    <AccessibilityMenu />
     <a href="#conteudo-principal" @click.prevent="pularParaConteudo" class="skip-link"> Pular para conteúdo principal</a>
    <header class="header">
      <div class="logo">👨‍🏫 Painel do Professor</div>
      <div class="user-info">
        <span>Olá, {{ userStore.nomeUsuario }}!</span>
        <button @click="sair" class="btn-sair">Sair</button>
      </div>
    </header>

    <main id = "conteudo-principal" class="content">
      <section class="acoes">
        <button @click="mostrarModalCriar = true" class="btn-criar-turma">
          ➕ Criar Nova Turma
        </button>
      </section>

      <section class="turmas">
        <h2>📚 Minhas Turmas</h2>
        
        <div v-if="turmas.length === 0" class="vazio">
          <p>Você ainda não criou nenhuma turma.</p>
          <p>Clique em "Criar Nova Turma" para começar!</p>
        </div>

        <div v-else class="turmas-grid">
          <div 
            v-for="turma in turmas" 
            :key="turma.id" 
            class="turma-card"
            @click="verTurma(turma.id)"
          >
            <div class="turma-header">
              <h3>{{ turma.nome }}</h3>
              <span class="codigo">Código: {{ turma.codigo }}</span>
            </div>
            <div class="turma-info">
              <span class="alunos">👥 {{ turma.totalAlunos }} aluno(s)</span>
            </div>
            <button class="btn-ver">Ver Detalhes →</button>
          </div>
        </div>
      </section>
    </main>

    <div v-if="mostrarModalCriar" class="modal-overlay" @click="mostrarModalCriar = false">
      <div class="modal" @click.stop>
        <h2>Criar Nova Turma</h2>
        <form @submit.prevent="criarTurma">
          <div class="form-group">
            <label for="nome-turma">Nome da Turma</label>
            <input 
              id="nome-turma"
              v-model="nomeTurma" 
              type="text" 
              placeholder="Ex: 6º Ano A"
              required
            />
          </div>
          <div class="modal-acoes">
            <button type="button" @click="mostrarModalCriar = false" class="btn-cancelar">
              Cancelar
            </button>
            <button type="submit" class="btn-confirmar" :disabled="loading">
              {{ loading ? 'Criando...' : 'Criar Turma' }}
            </button>
          </div>
        </form>

        <div v-if="mensagem" :class="['mensagem', mensagemTipo]">
          {{ mensagem }}
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '../stores/user'
import { turmaService } from '../services/api'
import AccessibilityMenu from '../components/AccessibilityMenu.vue'
import { useAccessibilityStore } from '../stores/accessibility'
import { useKeyboardShortcuts } from '../composables/useKeyboardShortcuts'
const accessibilityStore = useAccessibilityStore()

const router = useRouter()
const userStore = useUserStore()

const turmas = ref([])
const mostrarModalCriar = ref(false)
const nomeTurma = ref('')
const loading = ref(false)
const mensagem = ref('')
const mensagemTipo = ref('')
useKeyboardShortcuts()
function pularParaConteudo() {
  const conteudo = document.getElementById('conteudo-principal')
  if (conteudo) {
    conteudo.setAttribute('tabindex', '-1')
    conteudo.focus()
    conteudo.scrollIntoView()
  }
}
function sair() {
  userStore.limparUsuario()
  router.push('/')
}

function verTurma(id) {
  router.push(`/professor/turma/${id}`)
}

async function carregarTurmas() {
  try {
    turmas.value = await turmaService.minhasTurmas()
  } catch (error) {
    console.error('Erro ao carregar turmas:', error)
  }
}

async function criarTurma() {
  if (!nomeTurma.value.trim()) return

  loading.value = true
  mensagem.value = ''

  try {
    const novaTurma = await turmaService.criarTurma(nomeTurma.value)
    
    mensagem.value = `Turma criada! Código: ${novaTurma.codigo}`
    mensagemTipo.value = 'sucesso'
    
    setTimeout(() => {
      mostrarModalCriar.value = false
      nomeTurma.value = ''
      mensagem.value = ''
      carregarTurmas()
    }, 2000)
  } catch (error) {
    mensagem.value = error.response?.data?.mensagem || 'Erro ao criar turma'
    mensagemTipo.value = 'erro'
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  carregarTurmas()
})
</script>

<style scoped>
.professor-dashboard {
  min-height: 100vh;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

.header {
  background: white;
  padding: 20px 40px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.logo {
  font-size: 1.5rem;
  font-weight: bold;
  color: #667eea;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 20px;
}

.btn-sair {
  padding: 8px 20px;
  background: #ff6b6b;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  transition: all 0.3s;
}

.btn-sair:hover {
  background: #ff5252;
}

.content {
  padding: 40px;
  max-width: 1200px;
  margin: 0 auto;
}

.acoes {
  margin-bottom: 30px;
}

.btn-criar-turma {
  padding: 15px 30px;
  background: white;
  color: #667eea;
  border: 3px solid #667eea;
  border-radius: 12px;
  font-size: 1.1rem;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-criar-turma:hover {
  background: #667eea;
  color: white;
  transform: translateY(-2px);
  box-shadow: 0 5px 20px rgba(102, 126, 234, 0.4);
}

.turmas h2 {
  color: white;
  margin-bottom: 20px;
}

.vazio {
  background: white;
  border-radius: 15px;
  padding: 60px;
  text-align: center;
  color: #666;
}

.vazio p {
  font-size: 1.1rem;
  margin: 10px 0;
}

.turmas-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 20px;
}

.turma-card {
  background: white;
  border-radius: 15px;
  padding: 25px;
  cursor: pointer;
  transition: all 0.3s;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.turma-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
}

.turma-header h3 {
  margin: 0 0 10px 0;
  color: #333;
}

.codigo {
  display: inline-block;
  padding: 5px 12px;
  background: #e3f2fd;
  color: #1976d2;
  border-radius: 20px;
  font-size: 0.9rem;
  font-weight: bold;
}

.turma-info {
  margin: 15px 0;
  color: #666;
}

.alunos {
  font-size: 1.1rem;
}

.btn-ver {
  width: 100%;
  padding: 10px;
  background: #667eea;
  color: white;
  border: none;
  border-radius: 8px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-ver:hover {
  background: #5568d3;
}

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.modal {
  background: white;
  border-radius: 20px;
  padding: 40px;
  max-width: 500px;
  width: 90%;
  box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
}

.modal h2 {
  margin-top: 0;
  color: #667eea;
}

.form-group {
  margin-bottom: 20px;
}

.form-group label {
  display: block;
  margin-bottom: 8px;
  font-weight: 600;
  color: #333;
}

.form-group input {
  width: 100%;
  padding: 12px;
  border: 2px solid #e0e0e0;
  border-radius: 10px;
  font-size: 1rem;
  transition: border-color 0.3s;
}

.form-group input:focus {
  outline: none;
  border-color: #667eea;
}

.modal-acoes {
  display: flex;
  gap: 15px;
  margin-top: 30px;
}

.btn-cancelar,
.btn-confirmar {
  flex: 1;
  padding: 12px;
  border: none;
  border-radius: 10px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-cancelar {
  background: #f0f0f0;
  color: #666;
}

.btn-cancelar:hover {
  background: #e0e0e0;
}

.btn-confirmar {
  background: #667eea;
  color: white;
}

.btn-confirmar:hover:not(:disabled) {
  background: #5568d3;
}

.btn-confirmar:disabled {
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
}

.mensagem.erro {
  background: #f8d7da;
  color: #721c24;
}
</style>