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
              disabled
              required
              v-model="currentProduct.id"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              label="Nazwa"
              required
              :rules="[requiredRule]"
              v-model="currentProduct.productName"
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
          <v-flex 12>
            <v-layout row wrap>
              <v-flex xs5>
                <v-select
                  outlined
                  :rules="[requiredRule]"
                  color
                  label="DN"
                  min="0"
                  v-model.number="currentProduct.dn"
                  :items="[
                    6, 10, 15, 20, 25, 32, 40, 50, 65, 80, 100, 125, 150, 200,
                    250, 300, 350, 400, 450, 500,
                  ]"
                ></v-select>
              </v-flex>
              <v-spacer></v-spacer>
              <v-flex xs5>
                <v-select
                  outlined
                  color
                  :rules="[requiredRule]"
                  label="PN"
                  v-model.number="currentProduct.pn"
                  :items="[16, 40, 63, 100, 110, 160, 260]"
                ></v-select>
              </v-flex>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-flex>
      <v-flex xs12 md2 xl align-self-center>
        <v-layout row wrap justify-space-around>
          <v-btn
            v-show="currentProduct.id && editMode"
            color="primary"
            dark
            class="mb-2"
            @click="duplicateProduct(currentProduct)"
            v-if="!readonly"
          >
            <v-flex xs12> Duplikuj </v-flex>
            <v-flex xs12>
              <v-icon>fa-copy </v-icon>
            </v-flex>
          </v-btn>
        </v-layout>
      </v-flex>
      <v-flex xs5>
        <v-layout row wrap>
          <v-flex xs12>
            <v-autocomplete
              outlined
              :rules="[requiredRule]"
              color
              label="ANSI"
              v-model="currentProduct.ansi"
              :items="['100', '150', '300', '600', '900', '1500', '2500']"
            ></v-autocomplete>
          </v-flex>
          <v-flex xs12>
            <v-select
              outlined
              color
              :rules="[requiredRule]"
              label="Wersja"
              v-model="currentProduct.wersja"
              :items="['I', 'II']"
            ></v-select>
          </v-flex>
          <v-flex xs12>
            <v-autocomplete
              outlined
              color
              :rules="[requiredRule]"
              label="Uszczelnienie"
              v-model="currentProduct.uszczelnienie"
              :items="[
                'PTFE',
                'PTFE+C',
                'PTFE+C+G',
                'DERLIN',
                'PEEK',
                'Metal-Metal',
              ]"
            ></v-autocomplete>
          </v-flex>
          <v-flex xs12>
            <v-layout row wrap>
              <v-flex xs9>
                <v-text-field
                  v-model="currentProduct.cena"
                  outlined
                  :disabled="readonly"
                  :rules="[requiredRule, priceRule]"
                  type="number"
                  min="0"
                  @change="currentProduct.waluta = 'PLN'"
                  label="Cena"
                ></v-text-field>
              </v-flex>
              <v-flex xs3>
                <v-autocomplete
                  label="Waluta"
                  outlined
                  :items="['PLN', 'EUR', 'USD', 'GBP']"
                  v-model="currentProduct.waluta"
                  autocomplete
                ></v-autocomplete>
              </v-flex>
              <v-flex xs12>
                <v-autocomplete
                  outlined
                  color
                  :rules="[minRule]"
                  label="Temp min"
                  v-model="currentProduct.tempMin"
                  :items="[-196, -30, -20]"
                ></v-autocomplete>
              </v-flex>
              <v-flex xs12>
                <v-autocomplete
                  outlined
                  color
                  :rules="[maxRule]"
                  label="Temp max"
                  v-model="currentProduct.TempMax"
                  :items="[120, 180, 200, 250]"
                ></v-autocomplete>
              </v-flex>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex xs12>
        <ProductParts
          :currentProduct="currentProduct"
          :parts="parts"
        ></ProductParts>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import ProductParts from '../components/ProductParts'

export default {
  name: 'NewProduct',
  components: {
    ProductParts: ProductParts
  },
  props: {
    currentProduct: Object,
    productTypes: Array,
    editMode: Boolean,
    parts: Array,
    readonly: Boolean
  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadź wartość dodatnią'
        return true
      },
      minRule: val => {
        if (val < -196 || val > -20) return 'Wprowadź wartość pomiędzy -196, a -20'
        return true
      },
      maxRule: val => {
        if (val < 120 || val > 500) return 'Wprowadź wartość pomiędzy 120 a 500'
        return true
      },
      intRule: val => {
        if (val.includes(',') || val.includes('.')) return 'Wprowadź wartość całkowitą'
        return true
      },
      priceRule: val => {
        if (val < 0) return 'Wprowadz dodatnia wartosc'
        if (val > 500000) return 'Maksymalna wartość: 500 000'
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
    async duplicateProduct (product) {
      var res = await this.$dialog.confirm({
        text: 'Czy na pewno chcesz utworzyć duplikat dla:  ' + product.id + '? (aktualny produkt zostanie zapisany, zostaniesz przeniesiony do duplikatu)',
        title: 'Uwaga'
      })
      if (res) {
        this.$emit('duplicateProduct', product)
      }
    }
  },
  created () {
  },
  destroyed () {
  }
}
</script>

<style>
</style>
