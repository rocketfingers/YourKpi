<template>
  <div>
    <v-card-text>
      <v-container grid-list-md>
        <v-layout row wrap justify-space-around>
          <v-flex xs5>
            <v-layout row wrap>
              <v-flex xs12>
                <v-text-field
                  outlined
                  color
                  label="Id"
                  required
                  :readonly="!validateId && editMode"
                  :rules="[requiredRule, isItemAlreadyExists]"
                  v-model="currentItem.komponentId"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  outlined
                  color
                  :rules="[requiredRule]"
                  label="Nazwa"
                  v-model="currentItem.nazwa"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  outlined
                  :rules="[requiredRule]"
                  color
                  label="Wymiar"
                  v-model="currentItem.wymiar"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  outlined
                  :rules="[requiredRule]"
                  color
                  label="Gatunek podstaw."
                  v-model="currentItem.gatunekPodst"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  outlined
                  :rules="[requiredRule, ltidRule]"
                  color
                  label="LTID"
                  :min="1"
                  type="number"
                  hint="Wartości 1-999"
                  :max="999"
                  v-model="currentItem.ltid"
                ></v-text-field>
              </v-flex>
            </v-layout>
          </v-flex>
          <v-flex xs1> </v-flex>
          <v-flex xs5>
            <v-layout row wrap>
              <v-flex xs12 md6>
                <v-text-field
                  outlined
                  :rules="[requiredRule]"
                  color
                  label="Ilość"
                  type="number"
                  min="0"
                  v-model="currentItem.ilosc"
                ></v-text-field>
              </v-flex>
              <v-flex xs12 md6>
                <v-text-field
                  outlined
                  color
                  :rules="[requiredRule]"
                  label="Jednostka"
                  v-model.number="currentItem.jednostka"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  outlined
                  color
                  :rules="[requiredRule]"
                  label="Cena jednostkowa"
                  type="number"
                  v-model.number="currentItem.cenaJednostkowa"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-autocomplete
                  color
                  :items="processes"
                  label="Proces id"
                  outlined
                  item-text="showName"
                  item-value="id"
                  :rules="[requiredRule]"
                  v-model="currentItem.processId"
                ></v-autocomplete>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  outlined
                  disabled
                  color
                  label="Część"
                  v-model="currentItem.czesc"
                ></v-text-field>
              </v-flex>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card-text>
  </div>
</template>

<script>

export default {
  name: 'NewComponent',
  components: {
  },
  props: {
    currentItem: Object,
    editMode: Boolean,
    processes: Array,
    notValidate: Boolean,
    validateId: Boolean,
    components: Array
  },
  data () {
    return {
      requiredRule: (v) => (!!v || this.notValidate) || 'To pole jest wymagane',
      ltidRule: (v) => ((v > 0 && v < 1000) || this.notValidate) || 'Wprowadź poprawną wartość!!',
      isItemAlreadyExists: (v) => (!(this.components.some(p => p.komponentId.toUpperCase().trim() === v?.toUpperCase().trim() && p.isNewFlag !== true) && !this.editMode && this.validateId === true)) || 'Komponent o tym id już istnieje!!'
    }
  },
  computed: {
  },
  watch: {
    currentItem (val) {
      // // eslint-disable-next-line no-debugger
      // debugger
      // alert(this.validateId)
      // alert(this.editMode)
      // alert(this.components[3].komponentId)
      // alert((this.components.some(p => p.komponentId?.toUpperCase().trim() === this.currentItem.komponentId.toUpperCase().trim())))
      // alert((this.components.some(p => p.komponentId.toUpperCase().trim() === val.komponentId.toUpperCase().trim())))
      this.$emit('editedProduct', this.currentItem)
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
