<template>
  <div class="relatorio-turma">
    <AccessibilityMenu />
    
    <header class="header">
      <button @click="voltar" class="btn-voltar">← Voltar</button>
      <h1>📊 Relatório da Turma</h1>
      <button @click="exportarCSV" class="btn-exportar">📥 Exportar CSV</button>
    </header>

    <main id="conteudo-principal" class="content" v-if="!loading && dados">
      <!-- INFORMAÇÕES DA TURMA -->
      <section class="turma-info">
        <h2>{{ dados.turma.nome }}</h2>
        <p class="codigo">Código: {{ dados.turma.codigo }}</p>
      </section>

      <!-- CARDS DE RESUMO -->
      <section class="resumo-cards">
        <div class="stat-card">
          <div class="stat-icon">👥</div>
          <div class="stat-info">
            <h3>Total de Alunos</h3>
            <p class="stat-value">{{ dados.resumo.totalAlunos }}</p>
          </div>
        </div>

        <div class="stat-card">
          <div class="stat-icon">✅</div>
          <div class="stat-info">
            <h3>Alunos Ativos</h3>
            <p class="stat-value">{{ dados.resumo.alunosAtivos }}</p>
          </div>
        </div>

        <div class="stat-card">
          <div class="stat-icon">🎯</div>
          <div class="stat-info">
            <h3>Média de Desafios</h3>
            <p class="stat-value">{{ dados.resumo.mediaDesafiosCompletos }}</p>
          </div>
        </div>

        <div class="stat-card">
          <div class="stat-icon">⭐</div>
          <div class="stat-info">
            <h3>Média de Pontos</h3>
            <p class="stat-value">{{ dados.resumo.mediaPontos }}</p>
          </div>
        </div>
      </section>

      <!-- GRÁFICO: DESEMPENHO DOS ALUNOS -->
      <section class="grafico-section">
        <h3>📈 Desempenho dos Alunos</h3>
        <div class="grafico-container">
          <canvas ref="graficoAlunos"></canvas>
        </div>
      </section>

      <!-- GRÁFICO: TAXA DE CONCLUSÃO POR DESAFIO -->
      <section class="grafico-section">
        <h3>📊 Taxa de Conclusão por Desafio</h3>
        <div class="grafico-container">
          <canvas ref="graficoDesafios"></canvas>
        </div>
      </section>

      <!-- TABELA DE ALUNOS -->
      <section class="tabela-section">
        <h3>🎓 Detalhamento por Aluno</h3>
        <table class="tabela-alunos">
          <thead>
            <tr>
              <th>Aluno</th>
              <th>Email</th>
              <th>Desafios Completos</th>
              <th>Total de Pontos</th>
              <th>Último Acesso</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="aluno in dados.alunos" :key="aluno.email">
              <td>
                <div class="aluno-info">
                  <span class="avatar-mini">{{ getAvatar(aluno.avatarId) }}</span>
                  <span>{{ aluno.nome }}</span>
                </div>
              </td>
              <td>{{ aluno.email }}</td>
              <td>
                <div class="progresso-bar">
                  <div class="progresso-fill" :style="{ width: (aluno.desafiosCompletos / dados.resumo.totalDesafios * 100) + '%' }"></div>
                  <span class="progresso-texto">{{ aluno.desafiosCompletos }}/{{ dados.resumo.totalDesafios }}</span>
                </div>
              </td>
              <td><strong>⭐ {{ aluno.totalPontos }}</strong></td>
              <td>{{ formatarData(aluno.ultimoDesafio) }}</td>
              <td>
                <button @click="verDetalhesAluno(aluno)" class="btn-detalhes">
                  Ver Detalhes
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </section>

      <!-- TABELA DE DESAFIOS -->
      <section class="tabela-section">
        <h3>🎮 Análise por Desafio</h3>
        <table class="tabela-desafios">
          <thead>
            <tr>
              <th>Desafio</th>
              <th>Nível</th>
              <th>Alunos Completos</th>
              <th>Taxa de Conclusão</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="desafio in dados.desafios" :key="desafio.desafioId">
              <td><strong>{{ desafio.titulo }}</strong></td>
              <td>
                <span class="nivel-badge" :class="'nivel-' + desafio.nivel">
                  {{ getNivelTexto(desafio.nivel) }}
                </span>
              </td>
              <td>{{ desafio.alunosCompletos }} / {{ desafio.totalAlunos }}</td>
              <td>
                <div class="progresso-bar">
                  <div class="progresso-fill" :style="{ width: desafio.taxaConclusao + '%' }"></div>
                  <span class="progresso-texto">{{ Math.round(desafio.taxaConclusao) }}%</span>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </section>
    </main>

    <div v-else-if="loading" class="loading">Carregando relatório...</div>
    <div v-else class="loading">Erro ao carregar relatório.</div>

    <!-- MODAL: DETALHES DO ALUNO -->
    <div v-if="alunoSelecionado" class="modal-overlay" @click="alunoSelecionado = null">
      <div class="modal" @click.stop>
        <h2>📊 Progresso Detalhado</h2>
        <p><strong>Aluno:</strong> {{ alunoSelecionado.nome }}</p>
        <p><strong>Email:</strong> {{ alunoSelecionado.email }}</p>
        <p><strong>Desafios Completos:</strong> {{ alunoSelecionado.desafiosCompletos }}/{{ dados.resumo.totalDesafios }}</p>
        <p><strong>Total de Pontos:</strong> ⭐ {{ alunoSelecionado.totalPontos }}</p>
        <button @click="alunoSelecionado = null" class="btn-fechar">Fechar</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, nextTick, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { relatorioService } from '../services/api'
import AccessibilityMenu from '../components/AccessibilityMenu.vue'
import { Chart, registerables } from 'chart.js'
import { useAccessibilityStore } from '../stores/accessibility'
import { useKeyboardShortcuts } from '../composables/useKeyboardShortcuts'
import { useNarracao } from '../composables/useNarracao'

Chart.register(...registerables)

const route = useRoute()
const router = useRouter()
const accessibilityStore = useAccessibilityStore()

// Ativar atalhos de teclado e narração
useKeyboardShortcuts()
useNarracao()

const turmaId = route.params.id
const dados = ref(null)
const loading = ref(true)
const error = ref(null)
const alunoSelecionado = ref(null)

const graficoAlunos = ref(null)
const graficoDesafios = ref(null)

const avatares = ['🦸', '🧙', '👑', '🤖', '👾', '🐉', '🦄', '⚡']

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

function formatarData(data) {
  if (!data) return 'Nunca'
  const date = new Date(data)
  return date.toLocaleDateString('pt-BR')
}

function voltar() {
  router.push('/professor')
}

function verDetalhesAluno(aluno) {
  alunoSelecionado.value = aluno
}

function exportarCSV() {
  if (!dados.value) return

  let csv = 'Aluno,Email,Desafios Completos,Total de Pontos,Último Acesso\n'

  dados.value.alunos.forEach(aluno => {
    csv += `"${aluno.nome}","${aluno.email}",${aluno.desafiosCompletos},${aluno.totalPontos},"${formatarData(aluno.ultimoDesafio)}"\n`
  })

  const blob = new Blob([csv], { type: 'text/csv' })
  const url = window.URL.createObjectURL(blob)
  const a = document.createElement('a')
  a.href = url
  a.download = `relatorio_${dados.value.turma.nome}_${new Date().toISOString().split('T')[0]}.csv`
  a.click()
}

function criarGraficos() {
  console.log('🎨 Iniciando criação de gráficos...')

  // Validar dados
  if (!dados.value) {
    console.error('❌ Dados não disponíveis')
    return
  }

  if (!dados.value.alunos || dados.value.alunos.length === 0) {
    console.warn('⚠️ Nenhum aluno encontrado')
    return
  }

  if (!dados.value.desafios || dados.value.desafios.length === 0) {
    console.warn('⚠️ Nenhum desafio encontrado')
    return
  }

  // Gráfico de Alunos
  console.log('📊 Criando gráfico de alunos...')
  if (graficoAlunos.value) {
    console.log('✅ Canvas de alunos encontrado:', graficoAlunos.value)
    try {
      new Chart(graficoAlunos.value, {
        type: 'bar',
        data: {
          labels: dados.value.alunos.map(a => a.nome),
          datasets: [
            {
              label: 'Desafios Completos',
              data: dados.value.alunos.map(a => a.desafiosCompletos),
              backgroundColor: '#667eea',
              borderRadius: 8
            },
            {
              label: 'Total de Pontos',
              data: dados.value.alunos.map(a => a.totalPontos),
              backgroundColor: '#764ba2',
              borderRadius: 8
            }
          ]
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          plugins: {
            legend: {
              position: 'top'
            }
          },
          scales: {
            y: {
              beginAtZero: true
            }
          }
        }
      })
      console.log('✅ Gráfico de alunos criado!')
    } catch (err) {
      console.error('❌ Erro ao criar gráfico de alunos:', err)
    }
  } else {
    console.error('❌ Canvas de alunos não encontrado no DOM')
  }

  // Gráfico de Desafios
  console.log('📊 Criando gráfico de desafios...')
  if (graficoDesafios.value) {
    console.log('✅ Canvas de desafios encontrado:', graficoDesafios.value)
    try {
      new Chart(graficoDesafios.value, {
        type: 'line',
        data: {
          labels: dados.value.desafios.map(d => d.titulo),
          datasets: [{
            label: 'Taxa de Conclusão (%)',
            data: dados.value.desafios.map(d => Math.round(d.taxaConclusao)),
            borderColor: '#667eea',
            backgroundColor: 'rgba(102, 126, 234, 0.1)',
            tension: 0.4,
            fill: true
          }]
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          plugins: {
            legend: {
              display: false
            }
          },
          scales: {
            y: {
              beginAtZero: true,
              max: 100,
              ticks: {
                callback: value => value + '%'
              }
            }
          }
        }
      })
      console.log('✅ Gráfico de desafios criado!')
    } catch (err) {
      console.error('❌ Erro ao criar gráfico de desafios:', err)
    }
  } else {
    console.error('❌ Canvas de desafios não encontrado no DOM')
  }
}

async function carregarDados() {
  loading.value = true
  error.value = null

  try {
    console.log('🔄 Carregando dados da turma:', turmaId)
    const resposta = await relatorioService.obterEstatisticasTurma(turmaId)

    console.log('✅ Dados recebidos:', resposta)
    dados.value = resposta

  } catch (err) {
    error.value = 'Erro ao carregar estatísticas.'
    console.error('❌ Erro ao carregar dados:', err)
  } finally {
    loading.value = false
  }
}

// Observar quando os dados estão prontos e o DOM foi renderizado
watch([dados, loading], async ([novoDados, novoLoading]) => {
  if (novoDados && !novoLoading) {
    // Aguardar o DOM ser atualizado
    await nextTick()

    // Aguardar mais um ciclo para garantir que o v-if renderizou
    await nextTick()

    console.log('📊 Dados prontos e DOM atualizado, criando gráficos...')
    console.log('Canvas Alunos:', graficoAlunos.value)
    console.log('Canvas Desafios:', graficoDesafios.value)

    // Criar gráficos
    criarGraficos()

    console.log('✅ Gráficos criados com sucesso!')
  }
})

onMounted(() => {
  carregarDados()
})
</script>

<style scoped>
.relatorio-turma {
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

.header h1 {
  margin: 0;
  color: #667eea;
}

.btn-voltar,
.btn-exportar {
  padding: 10px 20px;
  border: none;
  border-radius: 8px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-voltar {
  background: #f0f0f0;
  color: #333;
}

.btn-voltar:hover {
  background: #e0e0e0;
}

.btn-exportar {
  background: #28a745;
  color: white;
}

.btn-exportar:hover {
  background: #218838;
  transform: translateY(-2px);
}

.content {
  padding: 40px;
  max-width: 1400px;
  margin: 0 auto;
}

.turma-info {
  background: white;
  padding: 30px;
  border-radius: 15px;
  margin-bottom: 30px;
  text-align: center;
}

.turma-info h2 {
  margin: 0 0 10px 0;
  color: #667eea;
}

.codigo {
  font-size: 1.2rem;
  color: #666;
  font-weight: 600;
}

.resumo-cards {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
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

.grafico-section {
  background: white;
  padding: 30px;
  border-radius: 15px;
  margin-bottom: 30px;
}

.grafico-section h3 {
  margin: 0 0 20px 0;
  color: #667eea;
}

.grafico-container {
  height: 400px;
}

.tabela-section {
  background: white;
  padding: 30px;
  border-radius: 15px;
  margin-bottom: 30px;
  overflow-x: auto;
}

.tabela-section h3 {
  margin: 0 0 20px 0;
  color: #667eea;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th, td {
  padding: 15px;
  text-align: left;
  border-bottom: 1px solid #e0e0e0;
}

th {
  background: #f8f9fa;
  color: #667eea;
  font-weight: 600;
}

.aluno-info {
  display: flex;
  align-items: center;
  gap: 10px;
}

.avatar-mini {
  font-size: 2rem;
  display: inline-block;
  line-height: 1;
  vertical-align: middle;
  font-family: "Segoe UI Emoji", "Apple Color Emoji", "Noto Color Emoji", sans-serif;
  min-width: 2rem;
  text-align: center;
}

.progresso-bar {
  position: relative;
  width: 100%;
  height: 30px;
  background: #f0f0f0;
  border-radius: 15px;
  overflow: hidden;
}

.progresso-fill {
  height: 100%;
  background: linear-gradient(90deg, #667eea, #764ba2);
  transition: width 0.3s;
}

.progresso-texto {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-weight: 600;
  color: #333;
  font-size: 0.85rem;
}

.nivel-badge {
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

.btn-detalhes {
  padding: 8px 16px;
  background: #667eea;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  transition: all 0.3s;
}

.btn-detalhes:hover {
  background: #5568d3;
  transform: translateY(-2px);
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
}

.modal h2 {
  margin-top: 0;
  color: #667eea;
}

.btn-fechar {
  margin-top: 20px;
  padding: 10px 20px;
  background: #667eea;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
}
</style>