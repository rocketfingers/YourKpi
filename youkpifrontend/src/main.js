import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import axios from 'axios'
import dtMixin from './components/dtMixin.js'
import VuetifyDialog from 'vuetify-dialog'
Vue.mixin(dtMixin)

Vue.config.productionTip = false
const axiosInstance = axios.create({
  baseURL: 'http://localhost:5000',
  timeout: 150000000
})
Vue.prototype.$http = axiosInstance
Vue.use(VuetifyDialog, {
  context: {
    vuetify
  },
  confirm: {
    actions: {
      false: {
        text: 'Nie',
        color: 'error'
      },
      true: {
        text: 'Tak',
        color: 'success'
      }
    },
    width: 500
  },
  warning: {},
  error: {},
  prompt: {
    actions: {
      false: {
        text: 'Anuluj',
        color: 'error'
      },
      true: {
        text: 'Potwierdź',
        color: 'success'
      }
    },
    icon: 'report_problem', // to disable icon just put false
    width: 500
  }

})
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
