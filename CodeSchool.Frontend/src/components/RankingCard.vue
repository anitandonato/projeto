<template>
  <div class="ranking-card">
    <div class="ranking-header">
      <h2>🏆 Ranking da Turma</h2>
      <select v-if="minhasTurmas.length > 0" v-model="turmaSelecionada" @change="carregarRanking" class="select-turma">
        <option value="">Selecione uma turma</option>
        <option v-for="turma in minhasTurmas" :key="turma.id" :value="turma.id">
          {{ turma.nome }}
        </option>
      </select>
    </div>

    <div v-if="loading" class="loading">Carregando ranking...</div>

    <div v-else-if="ranking.length === 0" class="vazio">
      <p>Selecione uma turma para ver o ranking!</p>
    </div>

    <div v-else class="ranking-content">
      <!-- MINHA POSIÇÃO -->
      <div v-if="minhaPosicao" class="minha-posicao">
        <div class="posicao-badge">{{ minhaPosicao.posicao }}º</div>
        <div class="posicao-info">
          <span class="meu-nome">Você está em {{ minhaPosicao.posicao }}º lugar</span>
          <span class="meus-pontos">{{ minhaPosicao.pontos }} pontos</span>
        </div>
      </div>

      <!-- TABELA DE RANKING -->
      <div class="ranking-table">
        <div v-for="(aluno, index) in ranking" :key="aluno.alunoId"
             :class="['ranking-row', { 'destaque': index < 3, 'eu': aluno.alunoId === meuId }]">

          <!-- Posição -->
          <div class="posicao">
            <span v-if="index === 0" class="medalha">🥇</span>
            <span v-else-if="index === 1" class="medalha">🥈</span>
            <span v-else-if="index === 2" class="medalha">🥉</span>
            <span v-else class="numero-posicao">{{ aluno.posicao }}º</span>
          </div>

          <!-- Avatar e Nome -->
          <div class="aluno-info">
            <span class="avatar">{{ getAvatar(aluno.avatarId) }}</span>
            <div class="nome-nivel">
              <span class="nome">{{ aluno.nome }}</span>
              <span class="nivel">Nível {{ aluno.nivel }}</span>
            </div>
          </div>

          <!-- Estatísticas -->
          <div class="stats">
            <div class="stat">
              <span class="stat-label">Pontos</span>
              <span class="stat-value">{{ aluno.pontos }}</span>
            </div>
            <div class="stat">
              <span class="stat-label">Desafios</span>
              <span class="stat-value">{{ aluno.desafiosCompletos }}/10</span>
            </div>
            <div class="stat">
              <span class="stat-label">Badges</span>
              <span class="stat-value">🏆 {{ aluno.badges }}</span>
            </div>
          </div>
        </div>
      </div>

      <!-- MENSAGEM DE INCENTIVO -->
      <div class="mensagem-incentivo">
        <p v-if="minhaPosicao && minhaPosicao.posicao === 1">
          🎉 Parabéns! Você é o líder da turma! Continue assim!
        </p>
        <p v-else-if="minhaPosicao && minhaPosicao.posicao <= 3">
          🌟 Você está no pódio! Continue se esforçando!
        </p>
        <p v-else>
          💪 Continue praticando para subir no ranking!
        </p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { rankingService, turmaService } from '../services/api'

const props = defineProps({
  minhasTurmas: {
    type: Array,
    default: () => []
  }
})

const ranking = ref([])
const minhaPosicao = ref(null)
const turmaSelecionada = ref('')
const loading = ref(false)
const minhasTurmas = ref(props.minhasTurmas || [])
const meuId = ref(null)

const avatares = ['🦸', '🧙', '👑', '🤖', '👾', '🐉', '🦄', '⚡']

function getAvatar(avatarId) {
  if (!avatarId || avatarId < 1 || avatarId > 8) {
    return avatares[0]
  }
  return avatares[avatarId - 1]
}

async function carregarRanking() {
  if (!turmaSelecionada.value) return

  loading.value = true
  try {
    // Buscar ranking da turma
    ranking.value = await rankingService.obterRankingTurma(turmaSelecionada.value)

    // Buscar minha posição
    minhaPosicao.value = await rankingService.obterMinhaPosicao(turmaSelecionada.value)
  } catch (error) {
    console.error('Erro ao carregar ranking:', error)
  } finally {
    loading.value = false
  }
}

onMounted(async () => {
  // Buscar turmas se não foram passadas por props
  if (minhasTurmas.value.length === 0) {
    try {
      minhasTurmas.value = await turmaService.minhasTurmasAluno()
    } catch (error) {
      console.error('Erro ao carregar turmas:', error)
    }
  }

  // Selecionar primeira turma automaticamente
  if (minhasTurmas.value.length > 0) {
    turmaSelecionada.value = minhasTurmas.value[0].id
    await carregarRanking()
  }

  // Pegar meu ID do localStorage
  const usuario = JSON.parse(localStorage.getItem('usuario') || '{}')
  meuId.value = usuario.id
})
</script>

<style scoped>
.ranking-card {
  background: white;
  border-radius: 15px;
  padding: 30px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
  margin-top: 30px;
}

.ranking-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 25px;
  flex-wrap: wrap;
  gap: 15px;
}

.ranking-header h2 {
  margin: 0;
  color: #667eea;
  font-size: 1.8rem;
}

.select-turma {
  padding: 10px 15px;
  border: 2px solid #667eea;
  border-radius: 8px;
  font-size: 1rem;
  background: white;
  cursor: pointer;
  transition: all 0.3s;
}

.select-turma:hover {
  background: #f0f4ff;
}

.loading {
  text-align: center;
  padding: 40px;
  color: #667eea;
  font-size: 1.2rem;
}

.vazio {
  text-align: center;
  padding: 40px;
  color: #999;
}

.minha-posicao {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 20px;
  border-radius: 12px;
  display: flex;
  align-items: center;
  gap: 20px;
  margin-bottom: 25px;
}

.posicao-badge {
  font-size: 3rem;
  font-weight: bold;
  background: rgba(255, 255, 255, 0.2);
  width: 80px;
  height: 80px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.posicao-info {
  display: flex;
  flex-direction: column;
  gap: 5px;
}

.meu-nome {
  font-size: 1.3rem;
  font-weight: bold;
}

.meus-pontos {
  font-size: 1.1rem;
  opacity: 0.9;
}

.ranking-table {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.ranking-row {
  display: grid;
  grid-template-columns: 60px 1fr auto;
  align-items: center;
  gap: 15px;
  padding: 15px;
  border-radius: 10px;
  background: #f8f9fa;
  transition: all 0.3s;
}

.ranking-row:hover {
  transform: translateX(5px);
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.ranking-row.destaque {
  background: linear-gradient(135deg, #fff9e6 0%, #fff3cc 100%);
  border: 2px solid #ffd700;
}

.ranking-row.eu {
  background: linear-gradient(135deg, #e3f2fd 0%, #bbdefb 100%);
  border: 2px solid #667eea;
  font-weight: bold;
}

.posicao {
  text-align: center;
  font-size: 1.5rem;
}

.medalha {
  font-size: 2.5rem;
}

.numero-posicao {
  font-weight: bold;
  color: #667eea;
}

.aluno-info {
  display: flex;
  align-items: center;
  gap: 12px;
}

.avatar {
  font-size: 2.5rem;
  display: inline-block;
  min-width: 50px;
  text-align: center;
}

.nome-nivel {
  display: flex;
  flex-direction: column;
}

.nome {
  font-size: 1.1rem;
  font-weight: 600;
  color: #333;
}

.nivel {
  font-size: 0.9rem;
  color: #667eea;
}

.stats {
  display: flex;
  gap: 20px;
}

.stat {
  display: flex;
  flex-direction: column;
  align-items: center;
  min-width: 80px;
}

.stat-label {
  font-size: 0.8rem;
  color: #999;
  text-transform: uppercase;
}

.stat-value {
  font-size: 1.1rem;
  font-weight: bold;
  color: #333;
}

.mensagem-incentivo {
  margin-top: 25px;
  padding: 15px;
  background: #f0f4ff;
  border-left: 4px solid #667eea;
  border-radius: 8px;
  text-align: center;
}

.mensagem-incentivo p {
  margin: 0;
  font-size: 1.1rem;
  color: #667eea;
  font-weight: 600;
}

@media (max-width: 768px) {
  .ranking-row {
    grid-template-columns: 1fr;
    gap: 10px;
  }

  .stats {
    justify-content: space-around;
    width: 100%;
  }

  .ranking-header {
    flex-direction: column;
    align-items: stretch;
  }

  .select-turma {
    width: 100%;
  }
}
</style>
