<template>
  <div>
    <v-card-text>
      <v-container grid-list-md>
        <v-layout row wrap justify-space-around class="elevation-3">
          <v-flex 12>
            <v-toolbar flat color="white">
              <v-toolbar-title>Procesy</v-toolbar-title>
              <v-divider class="mx-2" inset vertical></v-divider>
              <v-spacer></v-spacer>
              <v-text-field
                append-icon="search"
                label="Search"
                single-line
                hide-details
                class="elevation-1"
                v-model="search"
              ></v-text-field>
            </v-toolbar>
          </v-flex>
          <v-flex xs12>
            <v-data-table
              :headers="headers"
              v-model="selectedProcesses"
              :items="filteredProcesses"
              :search="search"
              show-select
              :footer-props="footer"
              footer-props.items-per-page-options="rows"
              class="elevation-2"
            >
              <template slot="item" slot-scope="props">
                <tr>
                  <td>
                    <v-checkbox
                      v-model="props.isSelected"
                      @click="selectItem(props)"
                    >
                    </v-checkbox>
                  </td>
                  <td class="text-xs-left">{{ props.item.id }}</td>
                  <td class="text-xs-left">
                    {{ props.item.nazwGrupyProcesu }}
                  </td>
                  <td class="text-xs-left">{{ props.item.businessArea }}</td>
                  <td class="text-xs-left">{{ props.item.nazwaProcesu }}</td>
                  <td class="text-xs-left">{{ props.item.typZlecenia }}</td>
                  <!-- <td class="justify-center px-0">
                    <v-layout>
                      <v-flex xs6>
                        <v-icon
                          class="mr-2"
                          color="green"
                          @click="editItem(props.item)"
                        >
                          edit
                        </v-icon>
                      </v-flex>
                      <v-flex xs6>
                        <v-icon
                          @click="deleteProduct(props.item, index)"
                          color="red lighten-1"
                          >delete</v-icon
                        >
                      </v-flex>
                    </v-layout>
                  </td> -->
                </tr>

                <tr></tr
              ></template>
            </v-data-table>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card-text>
  </div>
</template>

<script>

export default {
  name: 'ProcessSelector',
  components: {
  },
  props: {

    offerLine: Object,
    processes: Array
  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadz dodatnia wartosc'
        return true
      },
      showComponent: true,
      notValidateComponent: false,
      headers: [
        // { text: 'Wybierz', value: 'select', visible: true },
        { text: 'Id', value: 'id', visible: true },
        { text: 'Nazwa grupy procesu', value: 'nazwGrupyProcesu', visible: true },
        { text: 'Business area', value: 'businessArea', visible: true },
        { text: 'Nazwa procesu', value: 'nazwaProcesu', visible: true },
        { text: 'Typ zlecenia', value: 'typZlecenia', visible: true }
      ],
      search: '',
      selectedProcesses: [],
      rows: [
        100,
        { text: '$vuetify.dataIterator.rowsPerPageAll', value: 1000 }
      ]
    }
  },
  computed: {
    filteredProcesses () {
      var filProcesses = this.processes.filter(p => {
        if (p.typZlecenia.toUpperCase().trim().includes('O') || p.typZlecenia === '0') {
          return false
        }
        return true
      })

      // offerLineProcess
      return filProcesses
    },
    componentsToAutocomplete (val) {
      // // eslint-disable-next-line no-debugger
      // debugger
      var $this = this
      var cmps = this.components.filter(c => {
        if (c.czesci) {
          if (c.czesci.length > 0 && $this.editedItem.komponentId !== c.id) {
            return false
          }
        }
        return true
      })
      return cmps
    }
  },
  watch: {
    editedItem (val) {
      if (val.komponentId) {
        this.notValidateComponent = true
      } else {
        this.notValidateComponent = false
      }
      this.$emit('editedProduct', this.editedItem)
    },
    selectedProcesses (val) {
      this.$emit('editedOfferLine', this.offerLine, this.selectedProcesses)
    }

  },
  methods: {
    selectItem (props) {
      props.select(props.isSelected)
    },
    editCurrentProductRes (editedProduct) {
      this.editedItem.komponent = editedProduct
    },
    changeComponent () {
      if (this.editedItem.komponent) {
        this.editedItem.komponentId = this.editedItem.komponent.id
      }
    }
  },
  created () {
  },
  destroyed () {
  }
}
</script>

<style>
</style>
