<template>
  <div>
    <v-card-text>
      <v-container grid-list-md>
        <v-layout wrap>
          <v-flex xs12 sm8 offset-sm2>
            <v-text-field
              v-model="editedItem.id"
              outlined
              :rules="[requiredRule]"
              label="Id"
            ></v-text-field>
          </v-flex>
          <v-flex xs12 sm8 offset-sm2>
            <v-text-field
              v-model="editedItem.nazwa"
              outlined
              :rules="[requiredRule]"
              label="Nazwa"
            ></v-text-field>
          </v-flex>
          <v-flex xs12 sm8 offset-sm2>
            <v-text-field
              v-model="editedItem.gatPodstawowy"
              outlined
              :rules="[requiredRule]"
              label="Gatunek podstawowy"
            ></v-text-field>
          </v-flex>
          <v-flex xs12 sm8 offset-sm2>
            <v-text-field
              v-model="editedItem.numerRysNorma"
              outlined
              :rules="[requiredRule]"
              label="Numer rysunku norma"
            ></v-text-field>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card-text>
  </div>
</template>

<script>
export default {
  name: 'NewPart',
  components: {
  },
  props: {
    editedItem: Object
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
    editedItem (val) {
      this.$emit('editedPart', this.editedItem)
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
