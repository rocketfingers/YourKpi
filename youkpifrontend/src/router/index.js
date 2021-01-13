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
import StepsView from '../views/StepsMachines.vue.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
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
    path: '/login',
    name: 'Login',
    component: Login
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
