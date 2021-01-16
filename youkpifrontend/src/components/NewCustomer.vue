<template>
  <div>
    <v-layout row wrap justify-space-around>
      <v-spacer></v-spacer>
      <v-flex xs5>
        <v-layout row wrap>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Nazwa"
              v-model="currentItem.name"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-layout row wrap>
              <v-flex xs5>
                <v-text-field
                  outlined
                  color
                  :rules="[requiredRule, postalCodeRule]"
                  label="Kod pocztowy"
                  v-model="currentItem.kodPocztowy"
                ></v-text-field>
              </v-flex>
              <v-spacer></v-spacer>
              <v-flex xs5>
                <v-text-field
                  outlined
                  color
                  :rules="[requiredRule]"
                  label="Miasto"
                  v-model="currentItem.miasto"
                ></v-text-field>
              </v-flex>
            </v-layout>
          </v-flex>

          <v-flex xs12>
            <v-layout row wrap>
              <v-flex xs5>
                <v-text-field
                  outlined
                  color
                  :rules="[requiredRule]"
                  label="Ulica"
                  v-model="currentItem.ulica"
                ></v-text-field>
              </v-flex>
              <v-spacer></v-spacer>
              <v-flex xs5>
                <v-text-field
                  outlined
                  color
                  :rules="[requiredRule]"
                  label="Numer budynku"
                  v-model="currentItem.nrBudynku"
                ></v-text-field>
              </v-flex>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-flex>
      <v-spacer></v-spacer>
      <v-flex xs5>
        <v-layout row wrap>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="NIP"
              v-model="currentItem.nip"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Numer telefonu"
              v-model="currentItem.nrTelefonu"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule, emailRule]"
              label="Email"
              v-model="currentItem.email"
            ></v-text-field>
          </v-flex>
        </v-layout>
      </v-flex>
      <v-spacer></v-spacer>
    </v-layout>
  </div>
</template>

<script>

export default {
  name: 'NewCustomer',
  components: {
  },
  props: {
    currentItem: Object,
    editMode: Boolean
  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadź wartość dodatnią'
        return true
      },
      postalCodeRule: val => {
        if (val.length !== 6) return 'Prawidłowy format 00-000'
        if (val[2] !== '-') return 'Prawidłowy format 00-000'

        return true
      },
      emailRule: val => {
        if (!val.includes('@')) return 'Prawidłowy format sth@sth.sth'
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
