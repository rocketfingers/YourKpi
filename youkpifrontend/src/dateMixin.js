export default {
  data () {
    return {

    }
  },
  computed: {
  },
  watch: {
  },
  methods: {
    formatDateTimeYYYYMMDD (date) {
      if (date) {
        if (date instanceof Date) {
          return (
            date.getFullYear() +
                  '-' +
                  (date.getMonth() + 1) +
                  '-' +
                  date.getDate()
          )
        }
        if (date.toString().includes('T')) {
          return date.toString().split('T')[0]
        }

        return date
      }
    }
  },
  created () { }
}
