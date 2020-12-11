import router from './router/index'
import store from './store/index'
export default {

  loginWithToken (token) {
    window.localStorage.removeItem('token')
    window.localStorage.setItem('token', token)
    var user = this.parseJwt(token)
    user.validdate = this.getDateFromTicks(user.exp)
    store.commit('LOGIN', user)
    return user
  },

  logout () {
    window.localStorage.removeItem('token')
    store.commit('LOGOUT')
    router.push({ name: 'Login' })
  },

  getAuthHeader () {
    const token = window.localStorage.getItem('token')
    if (token) {
      return {
        Authorization: 'Bearer ' + token
      }
    } else {
      return {}
    }
  },
  CheckIsLogged () {
    var token = window.localStorage.getItem('token')
    if (token) {
      var user = this.parseJwt(token)

      store.commit('LOGIN', user)
    }
  },
  b64DecodeUnicode (str) {
    // Going backwards: from bytestream, to percent-encoding, to original string.
    return decodeURIComponent(window.atob(str).split('').map(function (c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
    }).join(''))
  },
  parseJwt (token) {
    var base64Url = token.split('.')[1]
    var base64 = base64Url.replace('-', '+').replace('_', '/')
    var parsed = JSON.parse(this.b64DecodeUnicode(base64))
    return parsed
  },
  getDateFromTicks (ticks) {
    return new Date(ticks * 1000)
  }
}
