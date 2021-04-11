<template>
  <div>
    <v-layout row wrap justify-space-around>
      <v-flex xs12 md5>
        <v-text-field
          outlined
          color
          label="Nr"
          :disabled="editMode"
          required
          type="number"
          :rules="[requiredRule, numberRule]"
          v-model="currentItem.nr"
        ></v-text-field>
        <v-text-field
          outlined
          color
          label="Id"
          :disabled="editMode"
          required
          :rules="[requiredRule]"
          v-model="currentItem.towarId"
        ></v-text-field>
        <v-text-field
          outlined
          color
          label="Nazwa"
          required
          :rules="[requiredRule]"
          v-model="currentItem.nazwa"
        ></v-text-field>
        <v-text-field
          outlined
          color
          label="Ilość"
          required
          type="number"
          :rules="[requiredRule, numberRule]"
          v-model="currentItem.ilosc"
        ></v-text-field>
        <v-text-field
          outlined
          color
          label="Cena jednostkowa netto"
          required
          type="number"
          :rules="[requiredRule, numberRule]"
          v-model="currentItem.cenaJendNet"
        ></v-text-field>
      </v-flex>
      <v-flex xs12 md5>
        <v-text-field
          outlined
          color
          label="Magazyn"
          required
          :rules="[requiredRule]"
          v-model="currentItem.magazyn"
        ></v-text-field>
        <v-text-field
          outlined
          color
          label="Data przyjęcia"
          required
          :rules="[requiredRule]"
          type="date"
          v-model="currentItem.dataPrzyjecia"
        ></v-text-field>
        <v-autocomplete
          outlined
          color
          :rules="[requiredRule]"
          label="Kontrahent"
          :items="contractors"
          item-text="showName"
          return-object
          required
          v-model="currentItem.kontrahent"
        ></v-autocomplete>
        <v-autocomplete
          outlined
          color
          :rules="[requiredRule]"
          label="Lokacja"
          :items="locations"
          item-text="showName"
          return-object
          required
          v-model="currentItem.lokacja"
        ></v-autocomplete>
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
    editMode: Boolean
  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadź wartość dodatnią'
        return true
      }

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
  },
  created () {
  },
  destroyed () {
  }
}
</script>

<style>
</style>
