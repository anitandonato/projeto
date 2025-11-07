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
    }
  ]
})

// Guard de autenticação
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token')
  
  if (to.meta.requiresAuth && !token) {
    next('/')
  } else if (to.name === 'login' && token) {
    next('/dashboard')
  } else {
    next()
  }
})

export default router