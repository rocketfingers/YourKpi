import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Produkty from '../views/Produkty.vue'
import Czesci from '../views/Czesci.vue'
import TypWyrobu from '../views/TypWyrobu.vue'
import Login from '../views/Login.vue'
import Pracownicy from '../views/Pracownicy.vue'
import Oferta from '../views/Oferta.vue'
import Maszyny from '../views/Maszyny.vue'
import ReasonCodes from '../views/ReasonCodes.vue'
import Komponenty from '../views/Komponenty.vue'
import Procesy from '../views/Procesy.vue'
import StepsView from '../views/StepsMachines.vue'
import Customers from '../views/Customers.vue'
import Store from '../views/Magazyn.vue'
import ActivityHistory from '../views/ActivityHistory.vue'
import Towary from '../views/Towary.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/activityhistory',
    name: 'ActivityHistory',
    component: ActivityHistory
  },
  {
    path: '/produkty',
    name: 'Produkty',
    component: Produkty
  },
  {
    path: '/czesci',
    name: 'Czesci',
    component: Czesci
  },
  {
    path: '/typwyrobu',
    name: 'TypWyrobu',
    component: TypWyrobu
  },
  {
    path: '/pracownicy',
    name: 'Pracownicy',
    component: Pracownicy
  },
  {
    path: '/typwyrobu',
    name: 'TypWyrobu',
    component: TypWyrobu
  },
  {
    path: '/oferta',
    name: 'Oferta',
    component: Oferta
  },
  {
    path: '/maszyny',
    name: 'Maszyny',
    component: Maszyny
  },
  {
    path: '/reasonCodes',
    name: 'ReasonCodes',
    component: ReasonCodes
  },
  {
    path: '/components',
    name: 'Components',
    component: Komponenty
  },
  {
    path: '/processes',
    name: 'Księga procesów',
    component: Procesy
  },
  {
    path: '/stepsmachines',
    name: 'Steps machines',
    component: StepsView
  },
  {
    path: '/customers',
    name: 'Klienci',
    component: Customers
  },
  {
    path: '/store',
    name: 'Magazyn',
    component: Store
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/commodity',
    name: 'Towary',
    component: Towary
  }
  // {
  // path: '/about',
  // name: 'About',
  // route level code-splitting
  // this generates a separate chunk (about.[hash].js) for this route
  // which is lazy-loaded when the route is visited.
  // component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  // }
]

const router = new VueRouter({
  routes
})

export default router
