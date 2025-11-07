<template>
  <div class="dashboard">
    <header class="header">
      <div class="logo">🎮 CodeSchool</div>
      <div class="user-info">
        <span>Olá, {{ dashboardData?.nome || 'Aluno' }}!</span>
        <button @click="sair" class="btn-sair">Sair</button>
      </div>
    </header>

    <main class="content" v-if="dashboardData">
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
            <h3>Desafios</h3>
            <p class="stat-value">{{ dashboardData.desafiosCompletos }}</p>
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
        <div class="desafios-grid" v-if="desafios.length > 0">
          <div 
            v-for="desafio in desafios" 
            :key="desafio.id" 
            class="desafio-card"
            :class="{ concluido: desafio.concluido }"
          >
            <div class="desafio-header">
              <h3>{{ desafio.titulo }}</h3>
              <span class="nivel" :class="'nivel-' + desafio.nivel">
                {{ getNivelTexto(desafio.nivel) }}
              </span>
            </div>
            <p>{{ desafio.descricao }}</p>
            <div class="desafio-footer">
              <span class="pontos">⭐ {{ desafio.pontos }} pontos</span>
              <button 
                v-if="!desafio.concluido"
                @click="irParaDesafio(desafio.id)"
                class="btn-jogar"
              >
                Jogar
              </button>
              <span v-else class="concluido-badge">✅ Concluído</span>
            </div>
          </div>
        </div>
        <p v-else class="loading">Carregando desafios...</p>
      </section>
    </main>

    <div v-else class="loading">Carregando dashboard...</div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '../stores/user'
import { progressoService, desafiosService } from '../services/api'

const router = useRouter()
const userStore = useUserStore()

const dashboardData = ref(null)
const desafios = ref([])

const avatares = ['🦸', '🧙', '🥷', '🤖', '👾', '🐉', '🦄', '⚡']

function getAvatar(avatarId) {
  return avatares[avatarId - 1] || avatares[0]
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

async function carregarDados() {
  try {
    const [dashboard, listaDesafios] = await Promise.all([
      progressoService.obterDashboard(),
      desafiosService.listarDesafios()
    ])
    
    dashboardData.value = dashboard
    desafios.value = listaDesafios
  } catch (error) {
    console.error('Erro ao carregar dados:', error)
    if (error.response?.status === 401) {
      userStore.limparUsuario()
      router.push('/')
    }
  }
}

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
</style>