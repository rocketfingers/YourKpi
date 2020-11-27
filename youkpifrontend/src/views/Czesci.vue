<template>
  <div>
    <v-dialog v-model="showNewPartDialog" max-width="1200" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">{{ orderTitle }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showNewPartDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-container grid-list-md>
          <v-layout align-center wrap>
            <v-flex xs12 sm8 offset-sm2> </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-btn
            outlined
            rounded
            large
            color="blue darken-1"
            text
            @click.native="showNewPartDialog = false"
          >
            Zamknij
            <v-icon dark>cancel</v-icon>
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn
            outlined
            rounded
            large
            color="blue darken-1"
            text
            @click.native="addPartAction()"
          >
            Zapisz
            <v-icon dark>save</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
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
  </div>
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
      search: '',
      showNewPartDialog: false
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {},

    addPart () {
      this.showNewPartDialog = true
    },
    addPartAction () {

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
