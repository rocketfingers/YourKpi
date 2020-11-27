<template>
  <v-container>
    <v-layout row wrap elevation-3>
      <v-flex xs12>
        <v-toolbar flat color="white">
          <v-toolbar-title>Produkty</v-toolbar-title>
          <v-divider class="mx-2" inset vertical></v-divider>
          <v-spacer></v-spacer>
          <v-text-field
            append-icon="search"
            label="Wyszukaj"
            single-line
            hide-details
            class="elevation-1"
            v-model="search"
          ></v-text-field>
          <v-btn color="primary" dark class="mb-2" @click="addProduct()"
            >Dodaj</v-btn
          >
        </v-toolbar>
      </v-flex>
      <v-flex xs12>
        <v-data-table
          :headers="headers"
          :items="products"
          class="elevation-1"
          item-key="id"
          loading="true"
          search="search"
        >
        </v-data-table>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
// import * as v from '../main.js'

export default {
  name: 'Produkty',
  components: {},
  props: {},
  data () {
    return {
      // api
      getAllProducts: 'api/Products/GetAllProducts',

      headers: [
        { text: 'Id', value: 'id' },
        { text: 'Typ wyrobu id', value: 'typWyrobuId' },
        { text: 'NumerRysNorma', value: 'numerRysNorma' },
        { text: 'DN', value: 'dn' },
        { text: 'PN', value: 'pn' },
        { text: 'ANSI', value: 'ansi' },
        { text: 'Wersja', value: 'wersja' },
        { text: 'Uszczelnienie', value: 'uszczelnienie' },
        { text: 'Akcje', value: 'actions' }
      ],
      products: [],
      search: ''
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.getProducts()
    },
    getProducts () {
      this.$http.get(this.getAllProducts).then(Response => {
        this.products = Response.data
      }).catch((e) => {
        var a = 'aaa'
        this.products = a
      })
    },

    addProduct () {

    }
  },
  created () {
    this.initialise()
  },
  destroyed () {}
}
</script>

<style>
</style>
