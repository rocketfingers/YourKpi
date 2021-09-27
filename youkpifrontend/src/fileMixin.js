import * as v from '../src/main'
export default {
  data () {
    return {}
  },
  computed: {},
  watch: {},
  methods: {
    openPdf (url, winName, params) {
      if (url) {
        if (!params) {
          winName =
            'toolbar=yes,scrollbars=yes,resizable=yes,top=0,left=0,width=960,height=500'
        }
        if (!winName) {
          winName = ''
        }
        v.axiosInstance({
          url,
          method: 'GET',
          responseType: 'blob'
        })
          .then(response => {
            if (response.data) {
              if (response.data.size > 0) {
                var fileURL = window.URL.createObjectURL(
                  new Blob([response.data], { type: 'application/pdf' })
                )
                var win = window.open(fileURL, winName, params)

                if (window.focus) {
                  win.focus()
                }
              } else {
                // plik nie istieje
              }
            } else {
              // plik nie istieje
            }
          })
          .catch(e => {
            // plik nie istieje
          })
      }
    }
  },
  created () {}
}
