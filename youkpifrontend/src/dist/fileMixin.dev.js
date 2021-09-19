"use strict";

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports["default"] = void 0;

var v = _interopRequireWildcard(require("../src/main"));

function _getRequireWildcardCache() { if (typeof WeakMap !== "function") return null; var cache = new WeakMap(); _getRequireWildcardCache = function _getRequireWildcardCache() { return cache; }; return cache; }

function _interopRequireWildcard(obj) { if (obj && obj.__esModule) { return obj; } if (obj === null || _typeof(obj) !== "object" && typeof obj !== "function") { return { "default": obj }; } var cache = _getRequireWildcardCache(); if (cache && cache.has(obj)) { return cache.get(obj); } var newObj = {}; var hasPropertyDescriptor = Object.defineProperty && Object.getOwnPropertyDescriptor; for (var key in obj) { if (Object.prototype.hasOwnProperty.call(obj, key)) { var desc = hasPropertyDescriptor ? Object.getOwnPropertyDescriptor(obj, key) : null; if (desc && (desc.get || desc.set)) { Object.defineProperty(newObj, key, desc); } else { newObj[key] = obj[key]; } } } newObj["default"] = obj; if (cache) { cache.set(obj, newObj); } return newObj; }

var _default = {
  data: function data() {
    return {};
  },
  computed: {},
  watch: {},
  methods: {
    openPdf: function openPdf(url, winName, params) {
      if (url) {
        if (!params) {
          winName = 'toolbar=yes,scrollbars=yes,resizable=yes,top=0,left=0,width=960,height=500';
        }

        if (!winName) {
          winName = '';
        }

        v.axiosInstance({
          url: url,
          method: 'GET',
          responseType: 'blob'
        }).then(function (response) {
          if (response.data) {
            if (response.data.size > 0) {
              var fileURL = window.URL.createObjectURL(new Blob([response.data], {
                type: 'application/pdf'
              }));
              var win = window.open(fileURL, winName, params);

              if (window.focus) {
                win.focus();
              }
            } else {// plik nie istieje
            }
          } else {// plik nie istieje
            }
        })["catch"](function (e) {// plik nie istieje
        });
      }
    }
  },
  created: function created() {}
};
exports["default"] = _default;