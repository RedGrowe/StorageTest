import { createRouter, createWebHistory } from 'vue-router'
import StorageView from '../views/StorageView.vue'
import TransferView from '../views/TransferView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/transfer',
      name: 'transfer',
      component: TransferView
    },
    {
      path: '/storage',
      name: 'storage',
      component: StorageView
    },
  ]
})

export default router
