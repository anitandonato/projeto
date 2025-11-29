<template>
    <div class="turma-detalhes">
        <AccessibilityMenu />
        <a href="#conteudo-principal" @click.prevent="pularParaConteudo" class="skip-link"> Pular para conteúdo
            principal</a>
        <header class="header">
        <button @click="voltar" class="btn-voltar">← Voltar</button>
        <h1>👥 {{ turma?.nome || 'Carregando...'}}</h1>
        <button @click="verRelatorio" class="btn-relatorio">📊 Ver Relatório</button>
        </header>

        <main id="conteudo-principal" class="content" v-if="turma">
            <div class="info-turma">
                <div class="codigo-card">
                    <h3>📋 Código da Turma</h3>
                    <div class="codigo-destaque">{{ turma.codigo }}</div>
                    <p class="instrucao">Compartilhe este código com seus alunos para que eles possam entrar na turma!
                    </p>
                </div>

                <div class="stats-card">
                    <div class="stat">
                        <span class="stat-numero">{{ turma.alunos?.length || 0 }}</span>
                        <span class="stat-label">Alunos</span>
                    </div>
                    <div class="stat">
                        <span class="stat-numero">{{ mediaDesafios }}</span>
                        <span class="stat-label">Média de Desafios</span>
                    </div>
                    <div class="stat">
                        <span class="stat-numero">{{ mediaPontos }}</span>
                        <span class="stat-label">Média de Pontos</span>
                    </div>
                </div>
            </div>

            <section class="alunos-section">
                <h2>👥 Alunos da Turma</h2>

                <div v-if="!turma.alunos || turma.alunos.length === 0" class="vazio">
                    <p>Nenhum aluno entrou nesta turma ainda.</p>
                    <p>Compartilhe o código <strong>{{ turma.codigo }}</strong> com seus alunos!</p>
                </div>

                <div v-else class="alunos-tabela">
                    <table>
                        <thead>
                            <tr>
                                <th>Nome</th>
                                <th>Email</th>
                                <th>Desafios Completos</th>
                                <th>Total de Pontos</th>
                                <th>Progresso</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="aluno in turma.alunos" :key="aluno.id">
                                <td class="nome-aluno">{{ aluno.nome }}</td>
                                <td class="email-aluno">{{ aluno.email }}</td>
                                <td class="desafios">
                                    <span class="badge">{{ aluno.desafiosCompletos }}/3</span>
                                </td>
                                <td class="pontos">
                                    <span class="pontos-valor">⭐ {{ aluno.totalPontos }}</span>
                                </td>
                                <td class="progresso-cell">
                                    <div class="progress-bar">
                                        <div class="progress-fill"
                                            :style="{ width: calcularProgresso(aluno.desafiosCompletos) + '%' }"></div>
                                    </div>
                                    <span class="progress-text">{{ calcularProgresso(aluno.desafiosCompletos) }}%</span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </section>
        </main>

        <div v-else class="loading">Carregando turma...</div>
    </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { turmaService } from '../services/api'
import AccessibilityMenu from '../components/AccessibilityMenu.vue'
import { useAccessibilityStore } from '../stores/accessibility'
import { useKeyboardShortcuts } from '../composables/useKeyboardShortcuts'
import { useNarracao } from '../composables/useNarracao'

const accessibilityStore = useAccessibilityStore()

const route = useRoute()
const router = useRouter()

const turma = ref(null)

useKeyboardShortcuts()
useNarracao()

function verRelatorio() {
  router.push(`/professor/turma/${route.params.id}/relatorio`)
}
function pularParaConteudo() {
    const conteudo = document.getElementById('conteudo-principal')
    if (conteudo) {
        conteudo.setAttribute('tabindex', '-1')
        conteudo.focus()
        conteudo.scrollIntoView()
    }
}
const mediaDesafios = computed(() => {
    if (!turma.value?.alunos || turma.value.alunos.length === 0) return 0
    const total = turma.value.alunos.reduce((acc, aluno) => acc + aluno.desafiosCompletos, 0)
    return Math.round(total / turma.value.alunos.length)
})

const mediaPontos = computed(() => {
    if (!turma.value?.alunos || turma.value.alunos.length === 0) return 0
    const total = turma.value.alunos.reduce((acc, aluno) => acc + aluno.totalPontos, 0)
    return Math.round(total / turma.value.alunos.length)
})

function calcularProgresso(desafiosCompletos) {
    return Math.round((desafiosCompletos / 3) * 100)
}

function voltar() {
    router.push('/professor')
}

async function carregarTurma() {
    try {
        const id = route.params.id
        turma.value = await turmaService.obterAlunosDaTurma(id)
    } catch (error) {
        console.error('Erro ao carregar turma:', error)
        alert('Erro ao carregar turma')
        voltar()
    }
}

onMounted(() => {
    carregarTurma()
})
</script>

<style scoped>
.btn-relatorio {
  padding: 10px 20px;
  background: #28a745;
  color: white;
  border: none;
  border-radius: 8px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-relatorio:hover {
  background: #218838;
  transform: translateY(-2px);
}
.turma-detalhes {
    min-height: 100vh;
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

.header {
    background: white;
    padding: 20px 40px;
    display: flex;
    align-items: center;
    gap: 20px;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.btn-voltar {
    padding: 10px 20px;
    background: #667eea;
    color: white;
    border: none;
    border-radius: 8px;
    cursor: pointer;
    font-weight: 600;
    transition: all 0.3s;
}

.btn-voltar:hover {
    background: #5568d3;
}

.header h1 {
    margin: 0;
    color: #333;
}

.content {
    padding: 40px;
    max-width: 1400px;
    margin: 0 auto;
}

.info-turma {
    display: grid;
    grid-template-columns: 1fr 2fr;
    gap: 30px;
    margin-bottom: 40px;
}

@media (max-width: 1024px) {
    .info-turma {
        grid-template-columns: 1fr;
    }
}

.codigo-card,
.stats-card {
    background: white;
    border-radius: 15px;
    padding: 30px;
    box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.codigo-card h3 {
    margin-top: 0;
    color: #667eea;
}

.codigo-destaque {
    font-size: 3rem;
    font-weight: bold;
    color: #667eea;
    text-align: center;
    padding: 30px;
    background: #e3f2fd;
    border-radius: 15px;
    letter-spacing: 8px;
    margin: 20px 0;
}

.instrucao {
    color: #666;
    text-align: center;
    margin: 0;
}

.stats-card {
    display: flex;
    justify-content: space-around;
    align-items: center;
}

.stat {
    text-align: center;
}

.stat-numero {
    display: block;
    font-size: 3rem;
    font-weight: bold;
    color: #667eea;
}

.stat-label {
    display: block;
    color: #666;
    margin-top: 10px;
}

.alunos-section h2 {
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

.vazio strong {
    color: #667eea;
    font-size: 1.2rem;
}

.alunos-tabela {
    background: white;
    border-radius: 15px;
    padding: 30px;
    box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
    overflow-x: auto;
}

table {
    width: 100%;
    border-collapse: collapse;
}

thead {
    background: #f8f9fa;
}

th {
    padding: 15px;
    text-align: left;
    font-weight: 600;
    color: #667eea;
    border-bottom: 2px solid #e0e0e0;
}

td {
    padding: 15px;
    border-bottom: 1px solid #f0f0f0;
}

.nome-aluno {
    font-weight: 600;
    color: #333;
}

.email-aluno {
    color: #666;
}

.badge {
    display: inline-block;
    padding: 5px 12px;
    background: #e3f2fd;
    color: #1976d2;
    border-radius: 20px;
    font-size: 0.9rem;
    font-weight: bold;
}

.pontos-valor {
    font-weight: bold;
    color: #ffa726;
}

.progresso-cell {
    display: flex;
    align-items: center;
    gap: 10px;
}

.progress-bar {
    flex: 1;
    height: 10px;
    background: #e0e0e0;
    border-radius: 10px;
    overflow: hidden;
}

.progress-fill {
    height: 100%;
    background: linear-gradient(90deg, #667eea, #764ba2);
    transition: width 0.3s;
}

.progress-text {
    font-size: 0.9rem;
    font-weight: 600;
    color: #667eea;
    min-width: 40px;
}

.loading {
    text-align: center;
    color: white;
    font-size: 1.2rem;
    padding: 40px;
}

tbody tr:hover {
    background: #f8f9fa;
}
</style>