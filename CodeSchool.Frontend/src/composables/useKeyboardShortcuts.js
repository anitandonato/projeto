import { onMounted, onBeforeUnmount } from 'vue'
import { useAccessibilityStore } from '../stores/accessibility'

export function useKeyboardShortcuts() {
  const accessibilityStore = useAccessibilityStore()
  let handleKeyPress = null

  function setupKeyboardShortcuts() {
    handleKeyPress = (event) => {
      // Alt + 1: Pular para conteúdo principal (voltar ao topo)
      if (event.altKey && event.key === '1') {
        event.preventDefault()

        // Tentar encontrar o conteúdo principal
        const conteudo = document.getElementById('conteudo-principal')

        if (conteudo) {
          // Se encontrou, focar nele
          conteudo.setAttribute('tabindex', '-1')
          conteudo.focus()
          conteudo.scrollIntoView({ behavior: 'smooth', block: 'start' })
          accessibilityStore.narrar('Navegando para conteúdo principal')
        } else {
          // Se não encontrou (loading), fazer scroll para o topo da página
          window.scrollTo({ top: 0, behavior: 'smooth' })
          // Focar no primeiro elemento focável ou no body
          const primeiroFocavel = document.querySelector('button, a, input, [tabindex]:not([tabindex="-1"])')
          if (primeiroFocavel) {
            primeiroFocavel.focus()
          }
          accessibilityStore.narrar('Navegando para o topo da página')
        }
      }

      // Alt + C: Alternar alto contraste
      if (event.altKey && event.key === 'c') {
        event.preventDefault()
        accessibilityStore.toggleAltoContraste()
        accessibilityStore.narrar('Alto contraste ' + (accessibilityStore.altoContraste ? 'ativado' : 'desativado'))
      }

      // Alt + +: Aumentar fonte
      if (event.altKey && (event.key === '+' || event.key === '=')) {
        event.preventDefault()
        if (accessibilityStore.tamanhFonte === 'pequeno') {
          accessibilityStore.setTamanhoFonte('normal')
        } else if (accessibilityStore.tamanhFonte === 'normal') {
          accessibilityStore.setTamanhoFonte('grande')
        }
        accessibilityStore.narrar('Fonte aumentada')
      }

      // Alt + -: Diminuir fonte
      if (event.altKey && event.key === '-') {
        event.preventDefault()
        if (accessibilityStore.tamanhFonte === 'grande') {
          accessibilityStore.setTamanhoFonte('normal')
        } else if (accessibilityStore.tamanhFonte === 'normal') {
          accessibilityStore.setTamanhoFonte('pequeno')
        }
        accessibilityStore.narrar('Fonte diminuída')
      }

      // Alt + N: Alternar narração
      if (event.altKey && event.key === 'n') {
        event.preventDefault()
        accessibilityStore.toggleNarracao()
        setTimeout(() => {
          accessibilityStore.narrar('Narração ' + (accessibilityStore.narracao ? 'ativada' : 'desativada'))
        }, 100)
      }
    }

    window.addEventListener('keydown', handleKeyPress)
  }

  onMounted(() => {
    setupKeyboardShortcuts()
  })

  onBeforeUnmount(() => {
    if (handleKeyPress) {
      window.removeEventListener('keydown', handleKeyPress)
      handleKeyPress = null
    }
  })
}