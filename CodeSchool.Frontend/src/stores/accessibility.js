import { defineStore } from 'pinia'
import { ref, watch } from 'vue'

export const useAccessibilityStore = defineStore('accessibility', () => {
  // Estados
  const altoContraste = ref(false)
  const tamanhFonte = ref('normal') // 'pequeno', 'normal', 'grande'
  const narracao = ref(false)
  const reducaoMovimento = ref(false)

  // Carregar preferências do localStorage
  function carregarPreferencias() {
    const salvo = localStorage.getItem('acessibilidade')
    if (salvo) {
      const dados = JSON.parse(salvo)
      altoContraste.value = dados.altoContraste || false
      tamanhFonte.value = dados.tamanhFonte || 'normal'
      narracao.value = dados.narracao || false
      reducaoMovimento.value = dados.reducaoMovimento || false
    }
    
    aplicarConfiguracoes()
  }

  // Salvar preferências
  function salvarPreferencias() {
    const dados = {
      altoContraste: altoContraste.value,
      tamanhFonte: tamanhFonte.value,
      narracao: narracao.value,
      reducaoMovimento: reducaoMovimento.value
    }
    localStorage.setItem('acessibilidade', JSON.stringify(dados))
  }

  // Aplicar configurações ao DOM
  function aplicarConfiguracoes() {
    const html = document.documentElement
    const body = document.body

    // Alto Contraste
    if (altoContraste.value) {
      html.classList.add('alto-contraste')
      body.classList.add('alto-contraste')
    } else {
      html.classList.remove('alto-contraste')
      body.classList.remove('alto-contraste')
    }

    // Tamanho da fonte
    html.classList.remove('fonte-pequena', 'fonte-normal', 'fonte-grande')
    html.classList.add(`fonte-${tamanhFonte.value === 'pequeno' ? 'pequena' : tamanhFonte.value}`)

    // Redução de movimento
    if (reducaoMovimento.value) {
      html.classList.add('reducao-movimento')
    } else {
      html.classList.remove('reducao-movimento')
    }
  }

  // Alternar alto contraste
  function toggleAltoContraste() {
    altoContraste.value = !altoContraste.value
    aplicarConfiguracoes()
    salvarPreferencias()
    
    // Log para debug
    console.log('Alto contraste:', altoContraste.value)
    console.log('Classes no HTML:', document.documentElement.classList.toString())
  }

  // Alterar tamanho da fonte
  function setTamanhoFonte(tamanho) {
    tamanhFonte.value = tamanho
    aplicarConfiguracoes()
    salvarPreferencias()
  }

// Alternar narração
function toggleNarracao() {
  narracao.value = !narracao.value
  salvarPreferencias()
  
  // Testar narração
  if (narracao.value) {
    if ('speechSynthesis' in window) {
      // Testar com uma frase
      setTimeout(() => {
        narrar('Narração de áudio ativada com sucesso!')
      }, 100)
    } else {
      alert('Seu navegador não suporta narração de áudio. Por favor, use Chrome, Edge ou Firefox.')
      narracao.value = false
    }
  } else {
    pararNarracao()
  }
}

  // Alternar redução de movimento
  function toggleReducaoMovimento() {
    reducaoMovimento.value = !reducaoMovimento.value
    aplicarConfiguracoes()
    salvarPreferencias()
  }

  // Narrar texto (se narração estiver ativa)
  function narrar(texto) {
    if (!narracao.value) return
    
    if ('speechSynthesis' in window) {
      // Cancelar narração anterior
      window.speechSynthesis.cancel()
      
      const utterance = new SpeechSynthesisUtterance(texto)
      utterance.lang = 'pt-BR'
      utterance.rate = 0.9
      utterance.volume = 1.0
      
      window.speechSynthesis.speak(utterance)
    } else {
      console.warn('Narração de áudio não suportada neste navegador')
    }
  }

  // Parar narração
  function pararNarracao() {
    if ('speechSynthesis' in window) {
      window.speechSynthesis.cancel()
    }
  }

  // Watch para aplicar mudanças
  watch([altoContraste, tamanhFonte, reducaoMovimento], () => {
    aplicarConfiguracoes()
  })

  // Inicializar
  carregarPreferencias()

  return {
    altoContraste,
    tamanhFonte,
    narracao,
    reducaoMovimento,
    toggleAltoContraste,
    setTamanhoFonte,
    toggleNarracao,
    toggleReducaoMovimento,
    narrar,
    pararNarracao,
    carregarPreferencias
  }
})