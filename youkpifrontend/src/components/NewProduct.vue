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
              :disabled="editMode"
              required
              :rules="[requiredRule]"
              v-model="currentProduct.id"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-autocomplete
              outlined
              color
              :rules="[requiredRule]"
              label="Typ wyrobu id"
              :items="productTypes"
              item-text="id"
              item-value="id"
              required
              v-model="currentProduct.typWyrobuId"
            ></v-autocomplete>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Numer Rys Norma"
              v-model="currentProduct.numerRysNorma"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              :rules="[requiredRule, numberRule]"
              color
              label="DN"
              type="number"
              min="0"
              v-model.number="currentProduct.dn"
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
              :rules="[requiredRule, numberRule]"
              label="PN"
              type="number"
              min="0"
              v-model.number="currentProduct.pn"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              :rules="[requiredRule]"
              color
              label="ANSI"
              v-model="currentProduct.ansi"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Wersja"
              v-model="currentProduct.wersja"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
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
    productTypes: Array,
    editMode: Boolean
  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadz dodatnia wartosc'
        return true
      }

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
