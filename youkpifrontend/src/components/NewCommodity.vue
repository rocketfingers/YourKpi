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
