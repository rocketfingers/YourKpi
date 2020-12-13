import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Produkty from '../views/Produkty.vue'
import Czesci from '../views/Czesci.vue'
import TypWyrobu from '../views/TypWyrobu.vue'
import Login from '../views/Login.vue'
import Pracownicy from '../views/Pracownicy.vue'
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
