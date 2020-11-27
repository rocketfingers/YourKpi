import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import axios from 'axios'

Vue.config.productionTip = false
const axiosInstance = axios.create({
  baseURL: 'http://localhost:5000',
  timeout: 150000000
})
Vue.prototype.$http = axiosInstance

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')

export {
  axiosInstance,
  router,
  store
}
