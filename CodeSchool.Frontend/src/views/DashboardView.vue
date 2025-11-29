<template>
  <div class="dashboard" :key="dashboardKey">
    <AccessibilityMenu />
    <a href="#conteudo-principal" @click.prevent="pularParaConteudo" class="skip-link">Pular para conteúdo principal</a>

    <header class="header">
      <div class="logo">🎮 CodeSchool</div>
      <div class="user-info">
        <span>Olá, {{ dashboardData?.nome || 'Aluno' }}!</span>
        <button @click="sair" class="btn-sair">Sair</button>
      </div>
    </header>

    <main id="conteudo-principal" class="content" v-if="!loading && dashboardData">
      <!-- MINHAS TURMAS -->
      <section class="minhas-turmas-section" v-if="minhasTurmas.length > 0">
        <h2>🎓 Minhas Turmas</h2>
        <div class="turmas-aluno-grid">
          <div v-for="turma in minhasTurmas" :key="turma.id" class="turma-aluno-card">
            <div class="turma-nome">{{ turma.nome }}</div>
            <div class="turma-prof">Professor(a): {{ turma.professor }}</div>
            <div class="turma-codigo-mini">Código: {{ turma.codigo }}</div>
          </div>
        </div>
      </section>

      <!-- BOTÃO ENTRAR EM NOVA TURMA -->
      <section class="turma-section">
        <button @click="mostrarModalTurma = true" class="btn-entrar-turma">
          {{ minhasTurmas.length > 0 ? '➕ Entrar em Outra Turma' : '🎓 Entrar em uma Turma' }}
        </button>
      </section>

      <section class="stats">
        <div class="stat-card">
          <div class="stat-icon">👤</div>
          <div class="stat-info">
            <h3>Avatar</h3>
            <p class="avatar-emoji">{{ getAvatar(dashboardData.avatarId) }}</p>
          </div>
        </div>

        <div class="stat-card">
          <div class="stat-icon">⭐</div>
          <div class="stat-info">
            <h3>Pontos</h3>
            <p class="stat-value">{{ dashboardData.pontos }}</p>
          </div>
        </div>

        <div class="stat-card">
          <div class="stat-icon">🎯</div>
          <div class="stat-info">
            <h3>Nível</h3>
            <p class="stat-value">{{ dashboardData.nivel }}</p>
          </div>
        </div>

        <div class="stat-card">
          <div class="stat-icon">✅</div>
          <div class="stat-info">
            <h3>Desafios Completos</h3>
            <p class="stat-value">{{ dashboardData.desafiosCompletos }}/10</p>
          </div>
        </div>
      </section>

      <section class="badges" v-if="dashboardData.badges.length > 0">
        <h2>🏆 Suas Conquistas</h2>
        <div class="badges-grid">
          <div v-for="badge in dashboardData.badges" :key="badge.nome" class="badge-card">
            <div class="badge-icon">{{ badge.icone }}</div>
            <h4>{{ badge.nome }}</h4>
            <p>{{ badge.descricao }}</p>
          </div>
        </div>
      </section>

      <section class="desafios">
        <h2>🎮 Desafios Disponíveis</h2>

        <div class="filtros-desafios">
          <button
            @click="filtroAtivo = 'todos'"
            :class="{ 'ativo': filtroAtivo === 'todos' }"
            class="btn-filtro">
            📚 Todos
          </button>
          <button
            @click="filtroAtivo = 'completos'"
            :class="{ 'ativo': filtroAtivo === 'completos' }"
            class="btn-filtro">
            ✅ Completos
          </button>
          <button
            @click="filtroAtivo = 'pendentes'"
            :class="{ 'ativo': filtroAtivo === 'pendentes' }"
            class="btn-filtro">
            ⏳ Pendentes
          </button>
        </div>

        <div class="desafios-grid" v-if="desafiosFiltrados.length > 0">
          <div v-for="desafio in desafiosFiltrados" :key="desafio.id" class="desafio-card"
            :class="{ concluido: desafio.concluido }">
            <div class="desafio-header">
              <h3>{{ desafio.titulo }}</h3>
              <span class="nivel" :class="'nivel-' + desafio.nivel">
                {{ getNivelTexto(desafio.nivel) }}
              </span>
            </div>
            <p>{{ desafio.descricao }}</p>
            <div class="desafio-footer">
              <span class="pontos">⭐ {{ desafio.pontos }} pontos</span>
              <button @click="irParaDesafio(desafio.id)" class="btn-jogar" :class="{ 'completo': desafio.concluido }">
                {{ desafio.concluido ? '🔁 Jogar Novamente' : '▶️ Jogar' }}
              </button>
            </div>
          </div>
        </div>
        <p v-else class="loading">Carregando desafios...</p>
      </section>
    </main>

    <div v-else-if="loading" class="loading">Carregando dashboard...</div>
    <div v-else class="loading">
      Erro ao carregar.
      <button @click="carregarDados"
        style="margin-top: 20px; padding: 10px 20px; background: white; color: #667eea; border: none; border-radius: 8px; cursor: pointer;">
        Tentar Novamente
      </button>
    </div>

    <!-- MODAL: ENTRAR NA TURMA -->
    <div v-if="mostrarModalTurma" class="modal-overlay" @click="mostrarModalTurma = false">
      <div class="modal" @click.stop>
        <h2>Entrar em uma Turma</h2>
        <p>Digite o código fornecido pelo seu professor:</p>
        <form @submit.prevent="entrarNaTurma">
          <div class="form-group">
            <label for="codigo-turma">Código da Turma</label>
            <input id="codigo-turma" v-model="codigoTurma" type="text" placeholder="Ex: ABC123" maxlength="6" required
              style="text-transform: uppercase" />
          </div>
          <div class="modal-acoes">
            <button type="button" @click="mostrarModalTurma = false" class="btn-cancelar">
              Cancelar
            </button>
            <button type="submit" class="btn-confirmar" :disabled="loadingTurma">
              {{ loadingTurma ? 'Entrando...' : 'Entrar' }}
            </button>
          </div>
        </form>

        <div v-if="mensagemTurma" :class="['mensagem', mensagemTurmaTipo]">
          {{ mensagemTurma }}
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, watch, computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { useUserStore } from '../stores/user'
import { progressoService, desafiosService, turmaService } from '../services/api'
import AccessibilityMenu from '../components/AccessibilityMenu.vue'
import { useAccessibilityStore } from '../stores/accessibility'
import { useKeyboardShortcuts } from '../composables/useKeyboardShortcuts'
import { useNarracao } from '../composables/useNarracao'

const dashboardKey = ref(0)
const accessibilityStore = useAccessibilityStore()
const router = useRouter()
const route = useRoute()
const userStore = useUserStore()

const dashboardData = ref(null)
const desafios = ref([])
const minhasTurmas = ref([])
const mostrarModalTurma = ref(false)
const codigoTurma = ref('')
const loadingTurma = ref(false)
const mensagemTurma = ref('')
const mensagemTurmaTipo = ref('')
const loading = ref(true)
const filtroAtivo = ref('todos')

const avatares = ['🦸', '🧙', '👑', '🤖', '👾', '🐉', '🦄', '⚡']

useKeyboardShortcuts()
useNarracao()

// Filtrar desafios baseado no filtro ativo
const desafiosFiltrados = computed(() => {
  if (filtroAtivo.value === 'completos') {
    return desafios.value.filter(d => d.concluido)
  } else if (filtroAtivo.value === 'pendentes') {
    return desafios.value.filter(d => !d.concluido)
  }
  return desafios.value // 'todos'
})

function pularParaConteudo() {
  const conteudo = document.getElementById('conteudo-principal')
  if (conteudo) {
    conteudo.setAttribute('tabindex', '-1')
    conteudo.focus()
    conteudo.scrollIntoView()
  }
}

function getAvatar(avatarId) {
  // Garantir que avatarId seja um número válido entre 1 e 8
  if (!avatarId || avatarId < 1 || avatarId > 8) {
    return avatares[0] // Retorna o primeiro avatar como padrão
  }
  return avatares[avatarId - 1]
}

function getNivelTexto(nivel) {
  const niveis = { 1: 'Fácil', 2: 'Médio', 3: 'Difícil' }
  return niveis[nivel] || 'Fácil'
}

function irParaDesafio(id) {
  router.push(`/desafio/${id}`)
}

function sair() {
  userStore.limparUsuario()
  router.push('/')
}

async function entrarNaTurma() {
  if (!codigoTurma.value.trim()) return

  loadingTurma.value = true
  mensagemTurma.value = ''

  try {
    const resposta = await turmaService.entrarNaTurma(codigoTurma.value.toUpperCase())

    mensagemTurma.value = resposta.mensagem
    mensagemTurmaTipo.value = 'sucesso'

    minhasTurmas.value = await turmaService.minhasTurmasAluno()

    setTimeout(() => {
      mostrarModalTurma.value = false
      codigoTurma.value = ''
      mensagemTurma.value = ''
    }, 2000)
  } catch (error) {
    mensagemTurma.value = error.response?.data?.mensagem || 'Erro ao entrar na turma'
    mensagemTurmaTipo.value = 'erro'
  } finally {
    loadingTurma.value = false
  }
}

async function carregarDados() {
  console.log('🔄 Iniciando carregamento do dashboard...')
  loading.value = true

  try {
    console.log('📡 Fazendo requisições para API...')

    const [dashboard, listaDesafios, turmasDoAluno] = await Promise.all([
      progressoService.obterDashboard(),
      desafiosService.listarDesafios(),
      turmaService.minhasTurmasAluno()
    ])

    console.log('✅ Dados carregados com sucesso!')
    console.log('Dashboard:', dashboard)
    console.log('Desafios:', listaDesafios)
    console.log('Turmas:', turmasDoAluno)

    dashboardData.value = dashboard
    desafios.value = listaDesafios
    minhasTurmas.value = turmasDoAluno

    // Forçar re-render
    dashboardKey.value++

  } catch (error) {
    console.error('❌ ERRO GERAL ao carregar dados:', error)
    console.error('Status:', error.response?.status)
    console.error('Mensagem:', error.response?.data)

    if (error.response?.status === 401) {
      console.log('🔒 Token inválido, fazendo logout...')
      userStore.limparUsuario()
      router.push('/')
    }
  } finally {
    console.log('✅ Finalizando carregamento, loading = false')
    loading.value = false
  }
}

// Detectar quando volta para o dashboard e recarregar dados
watch(() => route.path, (newPath) => {
  if (newPath === '/dashboard') {
    console.log('🔄 Voltou para o dashboard, recarregando dados...')
    carregarDados()
  }
})

onMounted(() => {
  carregarDados()
})
</script>

<style scoped>
.dashboard {
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
  transform: translateY(-2px);
}

.content {
  padding: 40px;
  max-width: 1200px;
  margin: 0 auto;
}

.minhas-turmas-section {
  margin-bottom: 30px;
}

.minhas-turmas-section h2 {
  color: white;
  margin-bottom: 20px;
}

.turmas-aluno-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
  gap: 15px;
  margin-bottom: 20px;
}

.turma-aluno-card {
  background: white;
  border-radius: 12px;
  padding: 20px;
  box-shadow: 0 3px 15px rgba(0, 0, 0, 0.1);
  border-left: 4px solid #667eea;
}

.turma-nome {
  font-size: 1.2rem;
  font-weight: bold;
  color: #667eea;
  margin-bottom: 8px;
}

.turma-prof {
  color: #666;
  margin-bottom: 5px;
  font-size: 0.9rem;
}

.turma-codigo-mini {
  color: #999;
  font-size: 0.85rem;
  font-weight: 600;
  margin-top: 8px;
  padding-top: 8px;
  border-top: 1px solid #e0e0e0;
}

.turma-section {
  margin-bottom: 30px;
  text-align: center;
}

.btn-entrar-turma {
  padding: 15px 40px;
  background: white;
  color: #667eea;
  border: 3px solid #667eea;
  border-radius: 12px;
  font-size: 1.1rem;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-entrar-turma:hover {
  background: #667eea;
  color: white;
  transform: translateY(-2px);
  box-shadow: 0 5px 20px rgba(102, 126, 234, 0.4);
}

.stats {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 20px;
  margin-bottom: 40px;
}

.stat-card {
  background: white;
  border-radius: 15px;
  padding: 25px;
  display: flex;
  align-items: center;
  gap: 15px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.stat-icon {
  font-size: 3rem;
}

.stat-info h3 {
  margin: 0;
  color: #666;
  font-size: 0.9rem;
}

.stat-value {
  font-size: 2rem;
  font-weight: bold;
  color: #667eea;
  margin: 5px 0 0 0;
}

.avatar-emoji {
  font-size: 3rem;
  margin: 0;
}

.badges {
  margin-bottom: 40px;
}

.badges h2 {
  color: white;
  margin-bottom: 20px;
}

.badges-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  gap: 20px;
}

.badge-card {
  background: white;
  border-radius: 15px;
  padding: 20px;
  text-align: center;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.badge-icon {
  font-size: 3rem;
  margin-bottom: 10px;
}

.badge-card h4 {
  color: #667eea;
  margin: 10px 0;
}

.desafios h2 {
  color: white;
  margin-bottom: 20px;
}

.filtros-desafios {
  display: flex;
  gap: 10px;
  margin-bottom: 20px;
  justify-content: center;
}

.btn-filtro {
  padding: 10px 20px;
  background: white;
  color: #667eea;
  border: 2px solid #667eea;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  transition: all 0.3s;
}

.btn-filtro:hover {
  background: #f0f0ff;
}

.btn-filtro.ativo {
  background: #667eea;
  color: white;
}

.desafios-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 20px;
}

.desafio-card {
  background: white;
  border-radius: 15px;
  padding: 25px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s;
}

.desafio-card:hover {
  transform: translateY(-5px);
}

.desafio-card.concluido {
  opacity: 0.7;
  background: #f0f0f0;
}

.desafio-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 15px;
}

.desafio-header h3 {
  margin: 0;
  color: #333;
}

.nivel {
  padding: 5px 12px;
  border-radius: 20px;
  font-size: 0.8rem;
  font-weight: bold;
}

.nivel-1 {
  background: #d4edda;
  color: #155724;
}

.nivel-2 {
  background: #fff3cd;
  color: #856404;
}

.nivel-3 {
  background: #f8d7da;
  color: #721c24;
}

.desafio-card p {
  color: #666;
  margin: 0 0 15px 0;
}

.desafio-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.pontos {
  font-weight: bold;
  color: #667eea;
}

.btn-jogar {
  padding: 8px 20px;
  background: #667eea;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  transition: all 0.3s;
}

.btn-jogar:hover {
  background: #5568d3;
  transform: translateY(-2px);
}

.concluido-badge {
  color: #28a745;
  font-weight: bold;
}

.loading {
  text-align: center;
  color: white;
  font-size: 1.2rem;
  padding: 40px;
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

.modal p {
  color: #666;
  margin-bottom: 20px;
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