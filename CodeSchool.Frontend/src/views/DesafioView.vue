<template>
  <div class="desafio">
    <AccessibilityMenu />
    <a href="#conteudo-principal" @click.prevent="pularParaConteudo" class="skip-link"> Pular para conteúdo
      principal</a>
    <header class="header">
      <button @click="voltar" class="btn-voltar">← Voltar</button>
      <h1 v-if="desafio">{{ desafio.titulo }}</h1>
    </header>

    <main id="conteudo-principal" class="content" v-if="desafio">
      <div class="instrucoes">
        <h2>📝 Objetivo:</h2>
        <p>{{ desafio.descricao }}</p>
        <div class="pontos-info">⭐ Vale {{ desafio.pontos }} pontos!</div>
      </div>

      <div class="trabalho-area">
        <!-- VISUALIZAÇÃO DO GRID -->
        <div class="visualizacao">
          <h3>🎮 Visualização</h3>
          <div class="grid-container">
            <div v-for="(linha, y) in grid" :key="y" class="grid-linha">
              <div v-for="(celula, x) in linha" :key="x" class="grid-celula" :class="{
                'com-robo': robotX === x && robotY === y,
                'objetivo': celula === 'objetivo'
              }">
                <div v-if="robotX === x && robotY === y" class="robo">
                  {{ getRoboEmoji() }}
                </div>
                <div v-if="celula === 'objetivo'" class="flag">🚩</div>
              </div>
            </div>
          </div>
          <div class="controles">
            <button @click="resetarGrid" class="btn-reset">🔄 Resetar</button>
          </div>
        </div>

        <!-- EDITOR BLOCKLY -->
        <div class="editor-area">
          <h3>🧩 Monte sua solução:</h3>
          <div id="blocklyDiv" style="height: 350px; width: 100%;"></div>

          <div class="acoes">
            <button @click="executar" class="btn-executar">▶️ Executar</button>
            <button @click="submeter" class="btn-submeter">✅ Enviar Solução</button>
          </div>
        </div>
      </div>

      <div v-if="resultado" :class="['resultado', resultado.sucesso ? 'sucesso' : 'erro']">
        <h3>{{ resultado.mensagem }}</h3>
        <p v-if="resultado.pontosGanhos">Você ganhou {{ resultado.pontosGanhos }} pontos!</p>
        <div v-if="resultado.badgesNovas && resultado.badgesNovas.length > 0">
          <h4>🏆 Novas conquistas:</h4>
          <ul>
            <li v-for="badge in resultado.badgesNovas" :key="badge">{{ badge }}</li>
          </ul>
        </div>
        <button v-if="resultado.sucesso" @click="voltarComReload" class="btn-continuar">
          Continuar
        </button>
      </div>

      <div v-if="mensagemExecucao" class="mensagem-execucao">
        {{ mensagemExecucao }}
      </div>
    </main>

    <div v-else class="loading">Carregando desafio...</div>
  </div>
</template>

<script setup>
import { registrarBlocosCustomizados } from '../services/blocosCustomizados'
import { ref, onMounted, onUnmounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { desafiosService, progressoService } from '../services/api'
import * as Blockly from 'blockly'
import { javascriptGenerator } from 'blockly/javascript'
import AccessibilityMenu from '../components/AccessibilityMenu.vue'
import { useAccessibilityStore } from '../stores/accessibility'
import { useKeyboardShortcuts } from '../composables/useKeyboardShortcuts'

const accessibilityStore = useAccessibilityStore()

const route = useRoute()
const router = useRouter()

const desafio = ref(null)
const workspace = ref(null)
const resultado = ref(null)
const mensagemExecucao = ref('')

// Grid 5x5
const GRID_SIZE = 5
const grid = ref([])
const robotX = ref(0)
const robotY = ref(0)
const robotDirecao = ref(0) // 0=direita, 1=baixo, 2=esquerda, 3=cima
useKeyboardShortcuts()
// Inicializar grid
function pularParaConteudo() {
  const conteudo = document.getElementById('conteudo-principal')
  if (conteudo) {
    conteudo.setAttribute('tabindex', '-1')
    conteudo.focus()
    conteudo.scrollIntoView()
  }
}
function inicializarGrid() {
  grid.value = []
  for (let y = 0; y < GRID_SIZE; y++) {
    const linha = []
    for (let x = 0; x < GRID_SIZE; x++) {
      linha.push('vazio')
    }
    grid.value.push(linha)
  }

  // Definir objetivo baseado no desafio
  if (desafio.value) {
    const objetivos = {
      1: [2, 0], // Desafio 1: andar 3 casas (posição [3,0])
      2: [2, 1], // Desafio 2: virar direita e andar 2
      3: [4, 0]  // Desafio 3: andar 5 casas
    }
    const [ox, oy] = objetivos[desafio.value.id] || [4, 4]
    grid.value[oy][ox] = 'objetivo'
  }

  resetarRobo()
}

function resetarRobo() {
  robotX.value = 0
  robotY.value = 0
  robotDirecao.value = 0
}

function resetarGrid() {
  resetarRobo()
  mensagemExecucao.value = ''
  resultado.value = null
}

function getRoboEmoji() {
  const direcoes = ['🤖➡️', '🤖⬇️', '🤖⬅️', '🤖⬆️']
  return direcoes[robotDirecao.value]
}

function voltar() {
  router.push('/dashboard')
}

function voltarComReload() {
  router.push('/dashboard')
  window.location.reload()
}

async function carregarDesafio() {
  try {
    const id = route.params.id
    desafio.value = await desafiosService.obterDesafio(id)

    inicializarGrid()

    setTimeout(() => {
      inicializarBlockly()
    }, 100)
  } catch (error) {
    console.error('Erro ao carregar desafio:', error)
    alert('Erro ao carregar desafio')
    voltar()
  }
}

function inicializarBlockly() {
  // Registrar blocos customizados
  registrarBlocosCustomizados()

  // Toolbox com blocos customizados
  const toolbox = {
    kind: 'categoryToolbox',
    contents: [
      {
        kind: 'category',
        name: 'Movimento',
        colour: 160,
        contents: [
          {
            kind: 'block',
            type: 'mover_frente'
          },
          {
            kind: 'block',
            type: 'virar_direita'
          },
          {
            kind: 'block',
            type: 'virar_esquerda'
          }
        ]
      },
      {
        kind: 'category',
        name: 'Controle',
        colour: 120,
        contents: [
          {
            kind: 'block',
            type: 'controls_repeat_ext',
            inputs: {
              TIMES: {
                shadow: {
                  type: 'math_number',
                  fields: { NUM: 3 }
                }
              }
            }
          }
        ]
      },
      {
        kind: 'category',
        name: 'Números',
        colour: 230,
        contents: [
          {
            kind: 'block',
            type: 'math_number',
            fields: { NUM: 1 }
          }
        ]
      }
    ]
  }

  workspace.value = Blockly.inject('blocklyDiv', {
    toolbox: toolbox,
    grid: {
      spacing: 20,
      length: 3,
      colour: '#ccc',
      snap: true
    },
    zoom: {
      controls: true,
      wheel: true,
      startScale: 1.0,
      maxScale: 3,
      minScale: 0.3,
      scaleSpeed: 1.2
    }
  })
}

// Executar código
async function executar() {
  if (!workspace.value) return

  resetarRobo()
  mensagemExecucao.value = 'Executando...'
  resultado.value = null

  // Gerar código JavaScript
  const code = javascriptGenerator.workspaceToCode(workspace.value)

  if (!code || code.trim() === '') {
    mensagemExecucao.value = '⚠️ Adicione blocos para executar!'
    return
  }

  // Criar funções para interpretar
  const comandos = []

  // Parsear código gerado
  const linhas = code.split('\n').filter(l => l.trim())

  for (const linha of linhas) {
    if (linha.includes('mover()')) {
      comandos.push('mover')
    } else if (linha.includes('virarDireita()')) {
      comandos.push('virar_direita')
    } else if (linha.includes('virarEsquerda()')) {
      comandos.push('virar_esquerda')
    }
  }

  // Se não tiver comandos, interpretar blocos de repetição
  if (comandos.length === 0) {
    const topBlocks = workspace.value.getTopBlocks(true)

    for (const block of topBlocks) {
      await interpretarBloco(block, comandos)
    }
  }

  if (comandos.length === 0) {
    mensagemExecucao.value = '⚠️ Configure os blocos corretamente!'
    return
  }

  // Executar comandos com animação
  for (const comando of comandos) {
    await sleep(500)

    if (comando === 'mover') {
      moverRobo()
    } else if (comando === 'virar_direita') {
      virarDireita()
    } else if (comando === 'virar_esquerda') {
      virarEsquerda()
    }
  }

  // Verificar se chegou no objetivo
  const chegouNoObjetivo = grid.value[robotY.value][robotX.value] === 'objetivo'

  if (chegouNoObjetivo) {
    mensagemExecucao.value = '🎉 Parabéns! Você completou o desafio! Agora clique em "Enviar Solução".'
  } else {
    mensagemExecucao.value = '❌ Quase lá! O robô não chegou na bandeira. Tente ajustar sua solução.'
  }
}

// Interpretar blocos recursivamente
async function interpretarBloco(block, comandos) {
  if (!block) return

  if (block.type === 'mover_frente') {
    comandos.push('mover')
  } else if (block.type === 'virar_direita') {
    comandos.push('virar_direita')
  } else if (block.type === 'virar_esquerda') {
    comandos.push('virar_esquerda')
  } else if (block.type === 'controls_repeat_ext') {
    const inputBlock = block.getInputTargetBlock('TIMES')
    const vezes = inputBlock ? parseInt(inputBlock.getFieldValue('NUM') || 1) : 1

    const doBlock = block.getInputTargetBlock('DO')

    for (let i = 0; i < vezes; i++) {
      if (doBlock) {
        await interpretarBlocoEFilhos(doBlock, comandos)
      }
    }
  }

  // Processar próximo bloco
  const nextBlock = block.getNextBlock()
  if (nextBlock) {
    await interpretarBloco(nextBlock, comandos)
  }
}

// Interpretar bloco e seus filhos
async function interpretarBlocoEFilhos(block, comandos) {
  if (!block) return

  await interpretarBloco(block, comandos)

  const nextBlock = block.getNextBlock()
  if (nextBlock) {
    await interpretarBlocoEFilhos(nextBlock, comandos)
  }
}

function virarDireita() {
  robotDirecao.value = (robotDirecao.value + 1) % 4
}

function virarEsquerda() {
  robotDirecao.value = (robotDirecao.value - 1 + 4) % 4
}
function moverRobo() {
  // Mover na direção atual
  switch (robotDirecao.value) {
    case 0: // direita
      if (robotX.value < GRID_SIZE - 1) robotX.value++
      break
    case 1: // baixo
      if (robotY.value < GRID_SIZE - 1) robotY.value++
      break
    case 2: // esquerda
      if (robotX.value > 0) robotX.value--
      break
    case 3: // cima
      if (robotY.value > 0) robotY.value--
      break
  }
}

function sleep(ms) {
  return new Promise(resolve => setTimeout(resolve, ms))
}

async function submeter() {
  if (!workspace.value) return

  const xml = Blockly.Xml.workspaceToDom(workspace.value)
  const xmlText = Blockly.Xml.domToText(xml)

  try {
    const resposta = await progressoService.submeterDesafio(
      desafio.value.id,
      xmlText
    )

    resultado.value = resposta
  } catch (error) {
    console.error('Erro ao submeter:', error)
    resultado.value = {
      sucesso: false,
      mensagem: 'Erro ao enviar solução. Tente novamente!'
    }
  }
}

onMounted(() => {
  carregarDesafio()
})

onUnmounted(() => {
  if (workspace.value) {
    workspace.value.dispose()
  }
})
</script>

<style scoped>
.desafio {
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

.instrucoes {
  background: white;
  border-radius: 15px;
  padding: 25px;
  margin-bottom: 30px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.instrucoes h2 {
  color: #667eea;
  margin-top: 0;
}

.pontos-info {
  background: #fff3cd;
  padding: 10px;
  border-radius: 8px;
  font-weight: bold;
  color: #856404;
  margin-top: 15px;
}

.trabalho-area {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 30px;
}

@media (max-width: 1024px) {
  .trabalho-area {
    grid-template-columns: 1fr;
  }
}

.visualizacao {
  background: white;
  border-radius: 15px;
  padding: 25px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.visualizacao h3 {
  color: #667eea;
  margin-top: 0;
}

.grid-container {
  background: #f0f0f0;
  padding: 20px;
  border-radius: 10px;
  display: inline-block;
}

.grid-linha {
  display: flex;
}

.grid-celula {
  width: 60px;
  height: 60px;
  border: 2px solid #ccc;
  display: flex;
  align-items: center;
  justify-content: center;
  background: white;
  position: relative;
  font-size: 2rem;
}

.grid-celula.com-robo {
  background: #e3f2fd;
}

.grid-celula.objetivo {
  background: #fff3cd;
}

.robo {
  font-size: 2.5rem;
  animation: pulse 1s infinite;
}

@keyframes pulse {

  0%,
  100% {
    transform: scale(1);
  }

  50% {
    transform: scale(1.1);
  }
}

.flag {
  font-size: 2rem;
}

.controles {
  margin-top: 20px;
  text-align: center;
}

.btn-reset {
  padding: 10px 20px;
  background: #ff9800;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
}

.btn-reset:hover {
  background: #f57c00;
}

.editor-area {
  background: white;
  border-radius: 15px;
  padding: 25px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.editor-area h3 {
  color: #667eea;
  margin-top: 0;
}

.acoes {
  display: flex;
  gap: 15px;
  margin-top: 20px;
}

.btn-executar,
.btn-submeter {
  flex: 1;
  padding: 15px;
  border: none;
  border-radius: 10px;
  font-size: 1.1rem;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-executar {
  background: #ffc107;
  color: #333;
}

.btn-executar:hover {
  background: #ffb300;
  transform: translateY(-2px);
}

.btn-submeter {
  background: #28a745;
  color: white;
}

.btn-submeter:hover {
  background: #218838;
  transform: translateY(-2px);
}

.resultado {
  margin-top: 30px;
  padding: 25px;
  border-radius: 15px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.resultado.sucesso {
  background: #d4edda;
  color: #155724;
  border: 2px solid #c3e6cb;
}

.resultado.erro {
  background: #f8d7da;
  color: #721c24;
  border: 2px solid #f5c6cb;
}

.resultado h3 {
  margin-top: 0;
}

.btn-continuar {
  margin-top: 20px;
  padding: 12px 30px;
  background: #667eea;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  font-size: 1rem;
}

.btn-continuar:hover {
  background: #5568d3;
}

.mensagem-execucao {
  margin-top: 20px;
  padding: 20px;
  background: #e3f2fd;
  border-radius: 10px;
  text-align: center;
  font-weight: 600;
  color: #1976d2;
  border: 2px solid #90caf9;
}

.loading {
  text-align: center;
  color: white;
  font-size: 1.2rem;
  padding: 40px;
}
</style>