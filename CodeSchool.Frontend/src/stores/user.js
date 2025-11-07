import { defineStore } from 'pinia'
import { ref, computed } from 'vue'

export const useUserStore = defineStore('user', () => {
  const usuario = ref(null)
  const token = ref(null)

  // Carregar dados do localStorage ao iniciar
  function carregarDados() {
    const usuarioSalvo = localStorage.getItem('usuario')
    const tokenSalvo = localStorage.getItem('token')
    
    if (usuarioSalvo) {
      usuario.value = JSON.parse(usuarioSalvo)
    }
    if (tokenSalvo) {
      token.value = tokenSalvo
    }
  }

  // Salvar usuário após login
  function setUsuario(dados) {
    usuario.value = dados
    token.value = dados.token
    
    localStorage.setItem('usuario', JSON.stringify(dados))
    localStorage.setItem('token', dados.token)
  }

  // Logout
  function limparUsuario() {
    usuario.value = null
    token.value = null
    localStorage.removeItem('usuario')
    localStorage.removeItem('token')
  }

  // Computed
  const estaLogado = computed(() => !!token.value)
  const nomeUsuario = computed(() => usuario.value?.nome || '')

  // Inicializar
  carregarDados()

  return {
    usuario,
    token,
    estaLogado,
    nomeUsuario,
    setUsuario,
    limparUsuario,
    carregarDados
  }
})