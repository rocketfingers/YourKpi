<template>
  <div>
    <v-layout row wrap justify-space-around>
      <v-flex xs5>
        <v-layout row wrap>
          <!-- <v-flex xs12>
            <v-text-field
              outlined
              color
              label="Id"
              :disabled="editMode"
              required
              :rules="[requiredRule]"
              v-model="currentOffer.id"
            ></v-text-field>
          </v-flex> -->
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Nazwa"
              v-model="currentOffer.name"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-autocomplete
              outlined
              color
              :rules="[requiredRule]"
              label="Projekt Id"
              :items="projects"
              item-text="showName"
              return-object
              required
              v-model="currentOffer.project"
            ></v-autocomplete>
          </v-flex>
          <v-flex xs12>
            <v-select
              label="Status"
              outlined
              :rules="[requiredRule]"
              color
              v-model="currentOffer.status"
              :items="['w trakcie', 'otwarte', 'zakończony']"
            ></v-select>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              :rules="[requiredRule]"
              color
              label="Typ zamówienia"
              v-model.number="currentOffer.orderType"
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
              label="Data oferty"
              type="date"
              v-model="currentOffer.offerDate"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-autocomplete
              outlined
              color
              :rules="[requiredRule]"
              label="Klient"
              :items="customers"
              item-text="showName"
              return-object
              required
              v-model="currentOffer.client"
            ></v-autocomplete>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              type="date"
              label="Data zamówienia"
              v-model="currentOffer.orderDate"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              type="date"
              label="Planowane zakończenie"
              v-model="currentOffer.plannedEnd"
            ></v-text-field>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex xs12>
        <OfferLines
          :currentOffer="currentOffer"
          :products="products"
        ></OfferLines>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import OfferLines from '../components/OfferLines'

export default {
  name: 'NewProduct',
  components: {
    OfferLines: OfferLines
  },
  props: {
    currentOffer: Object,
    editMode: Boolean,
    projects: Array,
    customers: Array,
    products: Array
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
    currentOffer (val) {
      this.$emit('editedOffer', this.currentOffer)
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
