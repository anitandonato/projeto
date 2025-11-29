import { onMounted, onBeforeUnmount, watch } from 'vue'
import { useAccessibilityStore } from '../stores/accessibility'

export function useNarracao() {
  const accessibilityStore = useAccessibilityStore()
  let handleMouseOver = null

  function setupNarracao() {
    handleMouseOver = (event) => {
      // Só narrar se a narração estiver ativada
      if (!accessibilityStore.narracao) return

      const target = event.target

      // Pegar texto do elemento
      let texto = ''

      // 1. Verificar se tem aria-label
      if (target.getAttribute('aria-label')) {
        texto = target.getAttribute('aria-label')
      }
      // 2. Verificar se tem title
      else if (target.getAttribute('title')) {
        texto = target.getAttribute('title')
      }
      // 3. Verificar se é um botão ou link
      else if (target.tagName === 'BUTTON' || target.tagName === 'A') {
        texto = target.textContent.trim()
      }
      // 4. Verificar se é um input
      else if (target.tagName === 'INPUT') {
        const label = document.querySelector(`label[for="${target.id}"]`)
        if (label) {
          texto = label.textContent.trim()
        } else if (target.placeholder) {
          texto = target.placeholder
        }
      }
      // 5. Verificar se é um heading
      else if (['H1', 'H2', 'H3', 'H4', 'H5', 'H6'].includes(target.tagName)) {
        texto = target.textContent.trim()
      }
      // 6. Verificar se é um parágrafo ou span com texto curto
      else if (target.tagName === 'P' || target.tagName === 'SPAN') {
        const conteudo = target.textContent.trim()
        // Só narrar se for texto curto (até 200 caracteres)
        if (conteudo.length > 0 && conteudo.length <= 200) {
          texto = conteudo
        }
      }
      // 7. Verificar se é um card ou div com classe específica
      else if (target.classList.contains('stat-card') ||
               target.classList.contains('card') ||
               target.classList.contains('btn')) {
        texto = target.textContent.trim()
        // Limitar tamanho
        if (texto.length > 200) {
          texto = texto.substring(0, 200)
        }
      }

      // Narrar se tiver texto
      if (texto && texto.length > 0) {
        accessibilityStore.narrar(texto)
      }
    }

    // Adicionar event listener
    document.addEventListener('mouseover', handleMouseOver, true)
  }

  function removerNarracao() {
    if (handleMouseOver) {
      document.removeEventListener('mouseover', handleMouseOver, true)
      handleMouseOver = null
    }
  }

  onMounted(() => {
    setupNarracao()
  })

  onBeforeUnmount(() => {
    removerNarracao()
  })

  // Watch para ativar/desativar
  watch(() => accessibilityStore.narracao, (novoValor) => {
    if (novoValor) {
      setupNarracao()
    } else {
      accessibilityStore.pararNarracao()
    }
  })
}
