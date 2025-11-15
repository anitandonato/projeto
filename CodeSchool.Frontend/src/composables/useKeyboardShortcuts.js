import { onMounted, onBeforeUnmount } from 'vue'
import { useAccessibilityStore } from '../stores/accessibility'

export function useKeyboardShortcuts() {
  const accessibilityStore = useAccessibilityStore()
  let handleKeyPress = null

  function setupKeyboardShortcuts() {
    handleKeyPress = (event) => {
      // Alt + 1: Pular para conteúdo principal
      if (event.altKey && event.key === '1') {
        event.preventDefault()
        const conteudo = document.getElementById('conteudo-principal')
        if (conteudo) {
          conteudo.setAttribute('tabindex', '-1')
          conteudo.focus()
          conteudo.scrollIntoView({ behavior: 'smooth' })
          accessibilityStore.narrar('Navegando para conteúdo principal')
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