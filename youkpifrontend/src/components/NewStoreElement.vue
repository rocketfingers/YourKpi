<template>
  <div>
    <v-layout row wrap justify-space-around>
      <v-flex xs12 md5>
        <v-layout row wrap>
          <v-flex xs12>
            <v-flex xs12>
              <v-autocomplete
                label="Magazyn"
                outlined
                :items="stores"
                :rules="[requiredRule]"
                return-object
                item-text="name"
                v-model="currentItem.magazyn"
                @change="setElements(currentItem.magazyn)"
                autocomplete
              ></v-autocomplete>
            </v-flex>
            <v-flex xs12>
              <v-autocomplete
                label="Id"
                outlined
                :items="elements"
                :rules="[requiredRule]"
                item-value="id"
                item-text="showName"
                v-model="currentItem.elementId"
                autocomplete
              ></v-autocomplete>
            </v-flex>
          </v-flex>
          <!-- <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Nazwa"
              v-model="currentItem.nazwa"
            ></v-text-field>
          </v-flex> -->
          <v-flex xs12>
            <v-layout row wrap>
              <v-flex xs8>
                <v-text-field
                  outlined
                  color
                  :rules="[requiredRule]"
                  label="Ilość"
                  type="number"
                  min="0"
                  v-model="currentItem.ilosc"
                ></v-text-field>
              </v-flex>
              <v-spacer></v-spacer>
              <v-flex xs3>
                <v-text-field
                  outlined
                  color
                  :rules="[requiredRule]"
                  label="Jednostka"
                  v-model="currentItem.jednostka"
                ></v-text-field>
              </v-flex>
            </v-layout>
          </v-flex>

          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Cena jednostkowa netto"
              v-model="currentItem.cenaJdnNetto"
            ></v-text-field>
          </v-flex>
        </v-layout>
      </v-flex>

      <v-flex xs12 md5>
        <v-layout row wrap>
          <v-flex xs12>
            <v-autocomplete
              label="Kontrahent"
              outlined
              :items="contractors"
              item-text="showName"
              item-value="id"
              :rules="[requiredRule]"
              v-model="currentItem.kontrahentId"
              autocomplete
            ></v-autocomplete>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              type="date"
              label="Data przyjęcia"
              v-model="currentItem.dataPrzyjecia"
            ></v-text-field>
          </v-flex>

          <v-flex xs12>
            <v-text-field
              outlined
              color
              label="Numer faktury"
              :rules="[requiredRule]"
              v-model="currentItem.nrFakturyId"
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
    currentItem: Object,
    editMode: Boolean,
    stores: Array,
    components: Array,
    products: Array,
    parts: Array,
    contractors: Array
  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadź wartość dodatnią'
        return true
      },
      elements: []

    }
  },
  computed: {
  },
  watch: {
    currentItem (val) {
      this.setElements(this.currentItem.magazyn)

      this.$emit('editedItem', this.currentItem)
    }
  },
  methods: {
    setElements (magazyn) {
      if (magazyn === null) {
        this.elements = []
        return
      }
      switch (magazyn.id) {
        case 1:
          this.elements = this.parts
          break
        case 2:
          this.elements = this.products
          break
        case 3:
          this.elements = this.components
          break
      }
    }
  },
  created () {
  },
  destroyed () {
  },
  mounted () {
    this.setElements(this.currentItem.magazyn)
  }
}
</script>

<style>
</style>
