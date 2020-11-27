<template>
  <div>
    <v-layout row wrap justify-space-around>
      <v-flex xs5>
        <v-layout row wrap>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              label="Id"
              v-model="currentProduct.id"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-select
              outlined
              color
              label="Typ wyrobu id"
              :items="productTypes"
              item-text="id"
              item-value="id"
              v-model="currentProduct.typWyrobuId"
              autocomplete
            ></v-select>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              label="Numer Rys Norma"
              v-model="currentProduct.numerRysNorma"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              label="DN"
              type="number"
              v-model="currentProduct.dn"
            ></v-text-field>
          </v-flex>
        </v-layout>
      </v-flex>
      <v-flex xs1> </v-flex>
      <v-flex xs5>
        <v-layout row wrap>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              label="PN"
              type="number"
              v-model="currentProduct.pn"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              label="ANSI"
              v-model="currentProduct.ansi"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              label="Wersja"
              v-model="currentProduct.wersja"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              label="Uszczelnienie"
              v-model="currentProduct.uszczelnienie"
            ></v-text-field>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
export default {
  name: 'NewProduct',
  components: {
  },
  props: {
    currentProduct: Object,
    productTypes: Array
  },
  data () {
    return {
    }
  },
  computed: {
  },
  watch: {
    currentProduct (val) {
      this.$emit('editedProduct', this.currentProduct)
    }
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
