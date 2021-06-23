<template>
  <div>
    <v-layout row wrap justify-space-around>
      <v-flex xs8>
        <v-text-field
          outlined
          color
          label="Id"
          :disabled="editMode"
          required
          :rules="[requiredRule]"
          v-model="currentItem.id"
        ></v-text-field>
        <v-text-field
          outlined
          color
          label="Nazwa"
          required
          :rules="[requiredRule]"
          v-model="currentItem.nazwa"
        ></v-text-field>
        <v-layout row wrap>
          <v-flex xs5>
            <v-layout row wrap>
              <v-flex xs8>
                <v-text-field
                  outlined
                  color
                  label="Cena zakupu"
                  required
                  type="number"
                  min="0"
                  :rules="[requiredRule]"
                  @input="calculateResultAndMargin()"
                  v-model="currentItem.cenaZakupu"
                ></v-text-field>
              </v-flex>
              <v-flex xs4>
                <v-autocomplete
                  color
                  :items="['PLN', 'EUR', 'USD', 'GBP']"
                  label="Waluta zakupu"
                  outlined
                  @input="calculateResultAndMargin()"
                  :rules="[requiredRule]"
                  v-model="currentItem.walutaZakupu"
                ></v-autocomplete>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  outlined
                  color
                  label="Marża"
                  readonly
                  v-model="marza"
                >
                </v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-autocomplete
                  color
                  :items="customers"
                  label="Klient"
                  outlined
                  item-text="showName"
                  item-value="id"
                  :rules="[requiredRule]"
                  v-model="currentItem.clientId"
                ></v-autocomplete>
              </v-flex>
            </v-layout>
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs5>
            <v-layout row wrap>
              <v-flex xs8>
                <v-text-field
                  outlined
                  color
                  label="Cena sprzedaży"
                  required
                  type="number"
                  min="0"
                  :rules="[requiredRule]"
                  @input="calculateResultAndMargin()"
                  v-model="currentItem.cenaSprzedazy"
                ></v-text-field>
              </v-flex>
              <v-flex xs4>
                <v-autocomplete
                  color
                  :items="['PLN', 'EUR', 'USD', 'GBP']"
                  @input="calculateResultAndMargin()"
                  label="Waluta"
                  outlined
                  :rules="[requiredRule]"
                  v-model="currentItem.walutaSprzedazy"
                ></v-autocomplete>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  outlined
                  color
                  label="Wynik"
                  readonly
                  v-model="wynik"
                >
                </v-text-field>
              </v-flex>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>

export default {
  name: 'NewCommodity',
  components: {
  },
  props: {
    currentItem: Object,
    contractors: Array,
    locations: Array,
    editMode: Boolean,
    customers: Array
  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadź wartość dodatnią'
        return true
      },
      wynik: 'wprowadź ceny',
      marza: 'wprowadź ceny'

    }
  },
  computed: {
  },
  watch: {
    currentItem (val) {
      this.$emit('editedProduct', this.currentItem)
    }
  },
  methods: {
    calculateResultAndMargin () {
      if (this.currentItem.walutaSprzedazy !== this.currentItem.walutaZakupu) {
        this.wynik = this.marza = 'Podane zostały ceny w różnych walutach!'
      } else {
        this.wynik = (this.currentItem.cenaSprzedazy && this.currentItem.cenaZakupu)
          ? (this.currentItem.cenaSprzedazy - this.currentItem.cenaZakupu)
          : 'wprowadż ceny'
        if (this.wynik && this.currentItem.cenaSprzedazy) {
          this.marza = ((this.wynik * 100) / this.currentItem.cenaSprzedazy).toFixed(2) + '%'
        }
      }
    }
  },
  mounted () {
    this.calculateResultAndMargin()
  },
  created () {
  },
  destroyed () {
  }
}
</script>

<style>
</style>
