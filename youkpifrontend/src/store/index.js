import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    user: {},
    error: ''
  },
  getters: {
    UserName: state => { return state.user.sub },
    GetError: state => { return state.error }
  },
  mutations: {
    LOGIN (state, user) {
      state.user = user
    },
    LOGOUT (state) {
      state.user = {}
    },
    SETERRORSNACK (state, error) {
      state.error = error
    }
  },
  actions: {
  },
  modules: {
  }
})
