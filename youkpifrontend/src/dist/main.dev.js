"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
Object.defineProperty(exports, "router", {
  enumerable: true,
  get: function get() {
    return _router["default"];
  }
});
Object.defineProperty(exports, "store", {
  enumerable: true,
  get: function get() {
    return _store["default"];
  }
});
exports.axiosInstance = void 0;

var _vue = _interopRequireDefault(require("vue"));

var _App = _interopRequireDefault(require("./App.vue"));

require("./registerServiceWorker");

var _router = _interopRequireDefault(require("./router"));

var _store = _interopRequireDefault(require("./store"));

var _vuetify = _interopRequireDefault(require("./plugins/vuetify"));

var _axios = _interopRequireDefault(require("axios"));

var _dtMixin = _interopRequireDefault(require("./components/dtMixin.js"));

var _vuetifyDialog = _interopRequireDefault(require("vuetify-dialog"));

var _auth = _interopRequireDefault(require("./auth"));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

_vue["default"].mixin(_dtMixin["default"]);

_vue["default"].config.productionTip = false;

var axiosInstance = _axios["default"].create({
  // baseURL: 'https://youkpibackend.azurewebsites.net',
  baseURL: 'http://localhost:5000',
  timeout: 150000000
});

exports.axiosInstance = axiosInstance;
_vue["default"].prototype.$http = axiosInstance;

_vue["default"].use(_vuetifyDialog["default"], {
  context: {
    vuetify: _vuetify["default"]
  },
  confirm: {
    actions: {
      "false": {
        text: 'Nie',
        color: 'error'
      },
      "true": {
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
      "false": {
        text: 'Anuluj',
        color: 'error'
      },
      "true": {
        text: 'Potwierdź',
        color: 'success'
      }
    },
    icon: 'report_problem',
    // to disable icon just put false
    width: 500
  }
});

axiosInstance.interceptors.request.use(function (config) {
  var token = window.localStorage.getItem('token');
  console.log('interceptor');
  config.headers = {
    Authorization: "Bearer ".concat(token)
  };
  return config;
}, function (error) {
  return Promise.reject(error);
});
axiosInstance.interceptors.response.use(function (response) {
  return response;
}, function (err) {
  if (err.response === undefined) {
    _store["default"].commit('SETERRORSNACK', err.message);
  } else if (err.response.status === 401) {
    if (_store["default"].getters.getCurrentPath !== '/login') {
      _auth["default"].logout();
    }
  } else if (err.response.status === 403) {
    if (_store["default"].getters.getCurrentPath !== '/main') {
      _store["default"].commit('SETERRORSNACK', 'You are not authrized to this resource');
    }
  } else {
    _store["default"].commit('SETERRORSNACK', err.response.data);
  }

  return Promise.reject(err);
});
new _vue["default"]({
  router: _router["default"],
  store: _store["default"],
  vuetify: _vuetify["default"],
  render: function render(h) {
    return h(_App["default"]);
  }
}).$mount('#app');