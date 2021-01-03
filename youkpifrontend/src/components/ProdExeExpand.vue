<template>
  <v-data-table
    headers="headers"
    items="items"
    hide-actions
    class="elevation-1"
    select-all
    pagination.sync="pagination"
    item-key="id"
    loading="true"
    search="search"
  >
  </v-data-table>
</template>

<script>
// eslint-disable-next-line no-unused-vars
import * as v from '../main.js'
export default {
  name: 'ProdExeExpand',
  components: {
  },
  props: {
    currentLine: Number,
    currentProcess: String
  },
  data () {
    return {
    }
  },
  computed: {
  },
  watch: {
  },
  methods: {
  },
  created () {
  },
  destroyed () {
  }
}
</script>

<style>
</style>
