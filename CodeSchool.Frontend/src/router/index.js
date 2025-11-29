import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      component: () => import('../views/LoginView.vue')
    },
    // Rotas do Aluno
    {
      path: '/dashboard',
      name: 'dashboard',
      component: () => import('../views/DashboardView.vue'),
      meta: { requiresAuth: true, role: 'Aluno' }
    },
    {
      path: '/aluno',
      redirect: '/dashboard'
    },
    {
      path: '/desafio/:id',
      name: 'desafio',
      component: () => import('../views/DesafioView.vue'),
      meta: { requiresAuth: true, role: 'Aluno' }
    },
    // Rotas do Professor
    {
      path: '/professor',
      name: 'professor-dashboard',
      component: () => import('../views/ProfessorDashboardView.vue'),
      meta: { requiresAuth: true, role: 'Professor' }
    },
    {
      path: '/professor/turma/:id',
      name: 'turma-detalhes',
      component: () => import('../views/TurmaDetalhesView.vue'),
      meta: { requiresAuth: true, role: 'Professor' }
    },
    {
      path: '/professor/turma/:id/relatorio',
      name: 'relatorio-turma',
      component: () => import('../views/RelatorioTurmaView.vue'),
      meta: { requiresAuth: true, role: 'Professor' }
    }
  ]
})

// Guard de autenticação e autorização
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token')
  const isAuthenticated = !!token
  const usuario = JSON.parse(localStorage.getItem('usuario') || '{}')
  const userRole = usuario.tipo // 'Professor' ou 'Aluno'

  // Se a rota requer autenticação
  if (to.meta.requiresAuth) {
    if (!isAuthenticated) {
      console.log('❌ Não autenticado, redirecionando para login')
      next('/')
      return
    }

    // Verificar se o role do usuário corresponde ao role da rota
    if (to.meta.role && to.meta.role !== userRole) {
      console.log(`❌ Acesso negado: usuário é ${userRole}, mas a rota requer ${to.meta.role}`)

      // Redirecionar para o dashboard correto do usuário
      if (userRole === 'Professor') {
        next('/professor')
      } else {
        next('/dashboard')
      }
      return
    }

    // Usuário autenticado e com role correto
    console.log(`✅ Acesso permitido para ${userRole}`)
    next()
  }
  // Se está tentando acessar login mas já está autenticado
  else if (to.name === 'login' && isAuthenticated) {
    console.log(`Já autenticado como ${userRole}, redirecionando para dashboard`)
    if (userRole === 'Professor') {
      next('/professor')
    } else {
      next('/dashboard')
    }
  }
  // Rota pública
  else {
    next()
  }
})

export default router