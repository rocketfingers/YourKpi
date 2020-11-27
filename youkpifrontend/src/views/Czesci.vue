<template>
  <v-container>
    <v-layout row wrap elevation-3>
      <v-flex xs12>
        <v-toolbar flat color="white">
          <v-toolbar-title>Części</v-toolbar-title>
          <v-divider class="mx-2" inset vertical></v-divider>
          <v-spacer></v-spacer>
          <v-text-field
            append-icon="search"
            label="Wyszukaj"
            single-line
            hide-details
            class="elevation-1"
            v-model="search"
          ></v-text-field>
          <v-btn color="primary" dark class="mb-2" @click="addPart()"
            >Dodaj</v-btn
          >
        </v-toolbar>
      </v-flex>
      <v-flex xs12>
        <v-data-table
          :headers="headers"
          :items="parts"
          class="elevation-1"
          item-key="id"
          loading="true"
          search="search"
        >
        </v-data-table>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
export default {
  name: 'Czesci',
  components: {},
  props: {},
  data () {
    return {
      headers: [
        { text: 'Id', value: 'id' },
        { text: 'Nazwa', value: 'nazwa' },
        { text: 'GatPodstawowy', value: 'gatPodstawowy' },
        { text: 'NumerRysNorma', value: 'numerRysNorma' },
        { text: 'Akcje', value: 'actions' }
      ],
      parts: [],
      search: ''
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {},

    addPart () {

    }
  },
  created () {
    this.initialise()
  },
  destroyed () {}
}
</script>

<style>
</style>
