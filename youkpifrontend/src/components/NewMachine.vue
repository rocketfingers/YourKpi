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
              v-model="currentItem.id"
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
              label="Opis"
              v-model="currentItem.opis"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-autocomplete
              outlined
              color
              :rules="[requiredRule]"
              label="Status"
              :items="['sprawna', 'awaria', 'obsługa', 'przezbrojenie']"
              required
              v-model="currentItem.status"
            ></v-autocomplete>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              :rules="[requiredRule]"
              color
              label="Tryb pracy"
              v-model="currentItem.trybPracy"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              :rules="[requiredRule]"
              color
              label="Rodzaj"
              v-model="currentItem.rodzaj"
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
              :rules="[requiredRule]"
              label="OS_X"
              type="number"
              v-model.number="currentItem.osX"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="OS_Y"
              type="number"
              v-model.number="currentItem.osY"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="OS_Z"
              type="number"
              v-model.number="currentItem.osZ"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Miejsce pracy"
              v-model="currentItem.miejscePracy"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Data uruchomienia"
              type="date"
              v-model="currentItem.dataUruchomienia"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-autocomplete
              outlined
              color
              :rules="[requiredRule]"
              label="Krytyczność"
              :items="['A', 'B', 'C']"
              required
              v-model="currentItem.krytycznosc"
            ></v-autocomplete>
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
    editMode: Boolean
  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane'
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
