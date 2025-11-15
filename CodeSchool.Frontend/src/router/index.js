import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      component: () => import('../views/LoginView.vue')
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: () => import('../views/DashboardView.vue'),
      meta: { requiresAuth: true }
    },
    {
      path: '/desafio/:id',
      name: 'desafio',
      component: () => import('../views/DesafioView.vue'),
      meta: { requiresAuth: true }
    },
    {
      path: '/professor',
      name: 'professor-dashboard',
      component: () => import('../views/ProfessorDashboardView.vue'),
      meta: { requiresAuth: true }
    },
    {
      path: '/professor/turma/:id',
      name: 'turma-detalhes',
      component: () => import('../views/TurmaDetalhesView.vue'),
      meta: { requiresAuth: true }
    }
  ]
})

// Guard de autenticação MELHORADO
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token')
  const isAuthenticated = !!token
  
  // Se a rota requer autenticação
  if (to.meta.requiresAuth) {
    if (!isAuthenticated) {
      // Não autenticado, redirecionar para login
      console.log('Não autenticado, redirecionando para login')
      next('/')
    } else {
      // Autenticado, permitir acesso
      next()
    }
  } 
  // Se está tentando acessar login mas já está autenticado
  else if (to.name === 'login' && isAuthenticated) {
    // Redirecionar para dashboard apropriado
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}')
    if (usuario.tipo === 'Professor') {
      next('/professor')
    } else {
      next('/dashboard')
    }
  } 
  // Rota pública, permitir acesso
  else {
    next()
  }
})

export default router