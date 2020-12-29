"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports["default"] = void 0;

var _vue = _interopRequireDefault(require("vue"));

var _vueRouter = _interopRequireDefault(require("vue-router"));

var _Home = _interopRequireDefault(require("../views/Home.vue"));

var _Produkty = _interopRequireDefault(require("../views/Produkty.vue"));

var _Czesci = _interopRequireDefault(require("../views/Czesci.vue"));

var _TypWyrobu = _interopRequireDefault(require("../views/TypWyrobu.vue"));

var _Login = _interopRequireDefault(require("../views/Login.vue"));

var _Pracownicy = _interopRequireDefault(require("../views/Pracownicy.vue"));

var _Oferta = _interopRequireDefault(require("../views/Oferta.vue"));

var _Maszyny = _interopRequireDefault(require("../views/Maszyny.vue"));

var _ReasonCodes = _interopRequireDefault(require("../views/ReasonCodes.vue"));

var _Komponenty = _interopRequireDefault(require("../views/Komponenty.vue"));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

_vue["default"].use(_vueRouter["default"]);

var routes = [{
  path: '/',
  name: 'Home',
  component: _Home["default"]
}, {
  path: '/produkty',
  name: 'Produkty',
  component: _Produkty["default"]
}, {
  path: '/czesci',
  name: 'Czesci',
  component: _Czesci["default"]
}, {
  path: '/typwyrobu',
  name: 'TypWyrobu',
  component: _TypWyrobu["default"]
}, {
  path: '/pracownicy',
  name: 'Pracownicy',
  component: _Pracownicy["default"]
}, {
  path: '/typwyrobu',
  name: 'TypWyrobu',
  component: _TypWyrobu["default"]
}, {
  path: '/oferta',
  name: 'Oferta',
  component: _Oferta["default"]
}, {
  path: '/maszyny',
  name: 'Maszyny',
  component: _Maszyny["default"]
}, {
  path: '/reasonCodes',
  name: 'ReasonCodes',
  component: _ReasonCodes["default"]
}, {
  path: '/components',
  name: 'Components',
  component: _Komponenty["default"]
}, {
  path: '/login',
  name: 'Login',
  component: _Login["default"]
} // {
// path: '/about',
// name: 'About',
// route level code-splitting
// this generates a separate chunk (about.[hash].js) for this route
// which is lazy-loaded when the route is visited.
// component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
// }
];
var router = new _vueRouter["default"]({
  routes: routes
});
var _default = router;
exports["default"] = _default;