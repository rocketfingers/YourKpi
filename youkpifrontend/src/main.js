import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import axios from 'axios'
import dtMixin from './components/dtMixin.js'
import VuetifyDialog from 'vuetify-dialog'
import auth from './auth'
Vue.mixin(dtMixin)
const backendAdress = process.env.VUE_APP_BACKEND

Vue.config.productionTip = false
const axiosInstance = axios.create({
  baseURL: backendAdress,
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

axiosInstance.interceptors.request.use(
  config => {
    var token = window.localStorage.getItem('token')
    console.log('interceptor')
    config.headers = {
      Authorization: `Bearer ${token}`
    }
    return config
  },
  error => Promise.reject(error)
)
axiosInstance.interceptors.response.use(function (response) {
  return response
}, function (err) {
  if (err.response === undefined) {
    store.commit('SETERRORSNACK', err.message)
  } else if (err.response.status === 401) {
    if (store.getters.getCurrentPath !== '/login') {
      auth.logout()
    }
  } else if (err.response.status === 403) {
    if (store.getters.getCurrentPath !== '/main') {
      store.commit('SETERRORSNACK', 'You are not authrized to this resource')
    }
  } else {
    store.commit('SETERRORSNACK', err.response.data)
  }
  return Promise.reject(err)
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
