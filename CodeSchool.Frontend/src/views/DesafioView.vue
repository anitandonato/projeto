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
        <button v-if="resultado.sucesso" @click="irParaProximoDesafio" class="btn-continuar">
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
import { ref, onMounted, onUnmounted, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { desafiosService, progressoService } from '../services/api'
import * as Blockly from 'blockly'
import { javascriptGenerator } from 'blockly/javascript'
import AccessibilityMenu from '../components/AccessibilityMenu.vue'
import { useAccessibilityStore } from '../stores/accessibility'
import { useKeyboardShortcuts } from '../composables/useKeyboardShortcuts'
import { useNarracao } from '../composables/useNarracao'

const accessibilityStore = useAccessibilityStore()

const route = useRoute()
const router = useRouter()

// Ativar narração
useNarracao()

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

  // Definir objetivo baseado no desafio (conforme ConfiguracaoGrid do banco)
  if (desafio.value) {
    const objetivos = {
      1: [3, 0], // Desafio 1: andar 3 passos para frente
      2: [2, 2], // Desafio 2: virar direita (0,0 → 0,2) e andar 2 passos (0,2 → 2,2)
      3: [4, 0], // Desafio 3: andar 5 passos com loop
      4: [1, 1], // Desafio 4: quadrado (volta ao ponto inicial)
      5: [4, 0], // Desafio 5: corredor em L
      6: [4, 0], // Desafio 6: escadaria diagonal
      7: [4, 4], // Desafio 7: zigue-zague
      8: [4, 4], // Desafio 8: explorador (grid menor 5x5)
      9: [4, 0], // Desafio 9: espiral (grid menor 5x5)
      10: [4, 0] // Desafio 10: desafio final (grid menor 5x5)
    }
    const [ox, oy] = objetivos[desafio.value.id] || [4, 4]
    grid.value[oy][ox] = 'objetivo'
  }

  resetarRobo()
}

function resetarRobo() {
  // Posição inicial baseada no desafio (conforme ConfiguracaoGrid)
  const posicoesIniciais = {
    1: [0, 0], // Desafio 1
    2: [0, 0], // Desafio 2
    3: [0, 0], // Desafio 3
    4: [1, 1], // Desafio 4: quadrado começa no centro
    5: [0, 2], // Desafio 5: corredor em L
    6: [0, 4], // Desafio 6: escadaria (começa embaixo)
    7: [0, 0], // Desafio 7
    8: [0, 0], // Desafio 8
    9: [3, 3], // Desafio 9: espiral (começa no centro)
    10: [0, 4]  // Desafio 10: desafio final (começa embaixo)
  }

  const [px, py] = posicoesIniciais[desafio.value?.id] || [0, 0]
  robotX.value = px
  robotY.value = py
  robotDirecao.value = 0 // Sempre começa olhando para a direita
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

function irParaProximoDesafio() {
  const desafioAtualId = parseInt(route.params.id)
  const proximoDesafioId = desafioAtualId + 1

  // Se tem próximo desafio (até 10), ir para ele
  if (proximoDesafioId <= 10) {
    router.push(`/desafio/${proximoDesafioId}`)
  } else {
    // Se completou todos, voltar ao dashboard
    router.push('/dashboard')
  }
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
        name: 'Condicionais',
        colour: 210,
        contents: [
          {
            kind: 'block',
            type: 'se'
          },
          {
            kind: 'block',
            type: 'se_senao'
          },
          {
            kind: 'block',
            type: 'comparacao'
          }
        ]
      },
      {
        kind: 'category',
        name: 'Sensores',
        colour: 290,
        contents: [
          {
            kind: 'block',
            type: 'tem_parede_frente'
          },
          {
            kind: 'block',
            type: 'esta_no_objetivo'
          }
        ]
      },
      {
        kind: 'category',
        name: 'Variáveis',
        colour: 330,
        contents: [
          {
            kind: 'block',
            type: 'definir_variavel'
          },
          {
            kind: 'block',
            type: 'obter_variavel'
          },
          {
            kind: 'block',
            type: 'incrementar_variavel'
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
          },
          {
            kind: 'block',
            type: 'math_arithmetic'
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

  // Criar funções para interpretar
  const comandos = []

  // Interpretar blocos diretamente (não usar JavaScript gerado)
  const topBlocks = workspace.value.getTopBlocks(true)

  if (topBlocks.length === 0) {
    mensagemExecucao.value = '⚠️ Adicione blocos para executar!'
    return
  }

  // Processar todos os blocos
  for (const block of topBlocks) {
    await interpretarBloco(block, comandos)
  }

  if (comandos.length === 0) {
    mensagemExecucao.value = '⚠️ Configure os blocos corretamente!'
    return
  }

  // Rastrear o caminho percorrido
  const caminhoPercorrido = []
  caminhoPercorrido.push({ x: robotX.value, y: robotY.value })

  // Executar comandos com animação
  for (const comando of comandos) {
    await sleep(500)

    if (comando === 'mover') {
      moverRobo()
      caminhoPercorrido.push({ x: robotX.value, y: robotY.value })
    } else if (comando === 'virar_direita') {
      virarDireita()
    } else if (comando === 'virar_esquerda') {
      virarEsquerda()
    }
  }

  // Verificar se chegou no objetivo
  const chegouNoObjetivo = grid.value[robotY.value][robotX.value] === 'objetivo'

  if (chegouNoObjetivo) {
    // Validar se a solução está correta
    const validacao = validarSolucao(desafio.value.id, comandos, caminhoPercorrido)

    if (validacao.valido) {
      mensagemExecucao.value = '🎉 Parabéns! Você completou o desafio! Agora clique em "Enviar Solução".'
    } else {
      mensagemExecucao.value = `⚠️ Você chegou na bandeira, mas ${validacao.mensagem} Tente usar a solução pedida no enunciado.`
    }
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
    console.log('🔄 Detectado bloco REPEAT')

    const inputBlock = block.getInputTargetBlock('TIMES')
    console.log('Input Block:', inputBlock)

    const vezes = inputBlock ? parseInt(inputBlock.getFieldValue('NUM') || 1) : 1
    console.log('🔢 Vai repetir', vezes, 'vezes')

    const doBlock = block.getInputTargetBlock('DO')
    console.log('DO Block:', doBlock)

    // Repetir os blocos dentro do DO
    for (let i = 0; i < vezes; i++) {
      console.log(`  📍 Iteração ${i + 1}/${vezes}`)

      // Processar todos os blocos dentro do DO
      let currentBlock = doBlock
      while (currentBlock) {
        console.log('    🧩 Processando bloco:', currentBlock.type)

        if (currentBlock.type === 'mover_frente') {
          comandos.push('mover')
          console.log('    ✅ Adicionado: mover')
        } else if (currentBlock.type === 'virar_direita') {
          comandos.push('virar_direita')
          console.log('    ✅ Adicionado: virar_direita')
        } else if (currentBlock.type === 'virar_esquerda') {
          comandos.push('virar_esquerda')
          console.log('    ✅ Adicionado: virar_esquerda')
        }
        currentBlock = currentBlock.getNextBlock()
      }
    }

    console.log('✅ Fim do REPEAT, total de comandos:', comandos.length)
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

// Validar se a solução está correta
function validarSolucao(desafioId, comandos, caminhoPercorrido) {
  // Definir validações para cada desafio
  const validacoes = {
    1: {
      // Desafio 1: "Faça o robô andar 3 passos para frente"
      minMovimentos: 3,
      maxComandos: 6,
      mensagemPoucos: 'você não andou 3 passos. O desafio pede para andar 3 passos para frente.',
      mensagemMuitos: 'você usou muitos blocos. Tente usar apenas 3 blocos "mover" ou use o bloco "repetir".'
    },
    2: {
      // Desafio 2: "Faça o robô virar à direita e andar 2 passos"
      // Solução: mover 2x, virar direita, mover 2x = chega em (2,2)
      minMovimentos: 4,
      minViradas: 1,
      maxComandos: 8,
      mensagemPoucos: 'faltam movimentos. Você precisa andar e virar para chegar na bandeira.',
      mensagemMuitos: 'você usou muitos blocos. Tente encontrar o caminho mais curto.'
    },
    3: {
      // Desafio 3: "Use um loop para fazer o robô andar 5 passos"
      minMovimentos: 5,
      maxComandos: 8,
      mensagemPoucos: 'você não andou 5 passos. Use o bloco "repetir" para andar 5 passos.',
      mensagemMuitos: 'você usou muitos blocos. Use o bloco "repetir" para fazer o robô andar 5 vezes.'
    },
    4: {
      // Desafio 4: "Quadrado perfeito" - volta ao ponto inicial
      minMovimentos: 4,
      minViradas: 4,
      maxComandos: 20,
      mensagemPoucos: 'faltam movimentos ou viradas. Você precisa fazer um quadrado completo.',
      mensagemMuitos: 'você usou muitos blocos. Use o bloco "repetir" com mover e virar dentro.'
    },
    5: {
      // Desafio 5: "Corredor em L"
      maxComandos: 15,
      mensagemMuitos: 'você usou muitos blocos. Tente encontrar um caminho mais direto.'
    },
    6: {
      // Desafio 6: "Escadaria diagonal"
      maxComandos: 20,
      mensagemMuitos: 'você usou muitos blocos. Tente usar loops para repetir o padrão.'
    },
    7: {
      // Desafio 7: "Zigue-zague"
      maxComandos: 25,
      mensagemMuitos: 'você usou muitos blocos. Tente otimizar usando loops.'
    },
    8: {
      // Desafio 8: "Explorador"
      maxComandos: 30,
      mensagemMuitos: 'você usou muitos blocos. Planeje uma rota mais eficiente.'
    },
    9: {
      // Desafio 9: "Espiral"
      maxComandos: 35,
      mensagemMuitos: 'você usou muitos blocos. Use loops aninhados para criar a espiral.'
    },
    10: {
      // Desafio 10: "Desafio Final"
      maxComandos: 40,
      mensagemMuitos: 'você usou muitos blocos. Combine loops e padrões que você aprendeu.'
    }
  }

  const validacao = validacoes[desafioId]

  if (!validacao) {
    // Se não tiver validação específica, aceita qualquer solução
    return { valido: true }
  }

  // Contar movimentos e viradas
  const movimentos = comandos.filter(c => c === 'mover').length
  const viradas = comandos.filter(c => c === 'virar_direita' || c === 'virar_esquerda').length

  // Verificar quantidade mínima de movimentos
  if (validacao.minMovimentos && movimentos < validacao.minMovimentos) {
    return {
      valido: false,
      mensagem: validacao.mensagemPoucos || 'faltam movimentos.'
    }
  }

  // Verificar quantidade mínima de viradas
  if (validacao.minViradas && viradas < validacao.minViradas) {
    return {
      valido: false,
      mensagem: validacao.mensagemPoucos || 'você precisa virar para completar o desafio.'
    }
  }

  // Verificar se usou muitos comandos (solução ineficiente)
  if (validacao.maxComandos && comandos.length > validacao.maxComandos) {
    return {
      valido: false,
      mensagem: validacao.mensagemMuitos || 'você usou muitos blocos. Tente otimizar sua solução.'
    }
  }

  return { valido: true }
}

async function submeter() {
  if (!workspace.value) return

  // Limpar mensagem de execução ao submeter
  mensagemExecucao.value = ''

  // VALIDAR SOLUÇÃO ANTES DE SUBMETER
  // Resetar e simular execução para validar
  const robotXBackup = robotX.value
  const robotYBackup = robotY.value
  const robotDirecaoBackup = robotDirecao.value

  resetarRobo()

  // Interpretar blocos para obter comandos
  const comandos = []
  const topBlocks = workspace.value.getTopBlocks(true)

  if (topBlocks.length === 0) {
    resultado.value = {
      sucesso: false,
      mensagem: '⚠️ Adicione blocos antes de enviar!'
    }
    return
  }

  // Processar todos os blocos
  for (const block of topBlocks) {
    await interpretarBloco(block, comandos)
  }

  if (comandos.length === 0) {
    resultado.value = {
      sucesso: false,
      mensagem: '⚠️ Configure os blocos corretamente antes de enviar!'
    }
    return
  }

  // Rastrear o caminho percorrido durante simulação
  const caminhoPercorrido = []
  caminhoPercorrido.push({ x: robotX.value, y: robotY.value })

  // Simular execução (sem animação)
  for (const comando of comandos) {
    if (comando === 'mover') {
      moverRobo()
      caminhoPercorrido.push({ x: robotX.value, y: robotY.value })
    } else if (comando === 'virar_direita') {
      virarDireita()
    } else if (comando === 'virar_esquerda') {
      virarEsquerda()
    }
  }

  // Verificar se chegou no objetivo
  const chegouNoObjetivo = grid.value[robotY.value][robotX.value] === 'objetivo'

  if (!chegouNoObjetivo) {
    // Restaurar posição do robô
    robotX.value = robotXBackup
    robotY.value = robotYBackup
    robotDirecao.value = robotDirecaoBackup

    resultado.value = {
      sucesso: false,
      mensagem: '❌ O robô não chegou na bandeira! Execute primeiro para ver o que acontece.'
    }
    return
  }

  // Validar se a solução está correta
  const validacao = validarSolucao(desafio.value.id, comandos, caminhoPercorrido)

  if (!validacao.valido) {
    // Restaurar posição do robô
    robotX.value = robotXBackup
    robotY.value = robotYBackup
    robotDirecao.value = robotDirecaoBackup

    resultado.value = {
      sucesso: false,
      mensagem: `⚠️ Solução inválida! ${validacao.mensagem}`
    }
    return
  }

  // Restaurar posição do robô
  robotX.value = robotXBackup
  robotY.value = robotYBackup
  robotDirecao.value = robotDirecaoBackup

  // SOLUÇÃO VÁLIDA - Enviar para o backend
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

// Observar mudanças na rota para recarregar desafio
watch(() => route.params.id, (novoId, antigoId) => {
  if (novoId !== antigoId) {
    // Limpar workspace antigo antes de carregar novo desafio
    if (workspace.value) {
      try {
        workspace.value.dispose()
        workspace.value = null
      } catch (error) {
        console.log('Erro ao limpar workspace:', error.message)
      }
    }

    // Limpar estado
    resultado.value = null
    mensagemExecucao.value = ''

    // Carregar novo desafio
    carregarDesafio()
  }
})

onUnmounted(() => {
  try {
    if (workspace.value) {
      workspace.value.dispose()
      workspace.value = null
    }
  } catch (error) {
    console.log('Workspace já foi limpo:', error.message)
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