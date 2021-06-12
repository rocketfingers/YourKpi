
<template>
  <div>
    <v-layout
      row
      wrap
      justify-space-around
      elevation-3
      width="100%"
      class="mb-5"
    >
      <v-flex xs12>
        <v-toolbar text color="white" v-if="!readonly">
          <v-toolbar-title>Offer Lines</v-toolbar-title>
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
        </v-toolbar>
        <v-data-table
          :headers="headers"
          :items="offerAvailability.availabilityProducts"
          :expanded.sync="expanded"
          item-key="id"
          :search="search"
          :hide-default-footer="readonly"
          calculate-widths
        >
          <template slot="item" slot-scope="props">
            <tr :class="props.isExpanded ? 'primary lighten-5' : 'white'">
              <template v-for="(header, index) in headers">
                <td :key="index" v-if="header.value === 'expand'">
                  <v-icon
                    @click="expandProductRow(props)"
                    v-show="!props.isExpanded"
                    >fa-arrow-down</v-icon
                  >
                  <v-icon
                    @click="expandProductRow(props)"
                    v-show="props.isExpanded"
                    >fa-arrow-up</v-icon
                  >
                </td>
                <td
                  :key="index"
                  v-else-if="header.value === 'availableProducts'"
                >
                  <v-layout row wrap justify-space-around>
                    <v-icon v-show="props.item.status > 0" color="green"
                      >check</v-icon
                    >
                    <v-icon v-show="props.item.status < 2" color="red"
                      >fa-exclamation-triangle</v-icon
                    >
                    <v-spacer></v-spacer>
                    {{ props.item[header.value] }}
                    <v-spacer></v-spacer>
                  </v-layout>
                </td>
                <td v-else :key="index">
                  <template>
                    {{ props.item[header.value] }}
                  </template>
                </td>
              </template>
            </tr>
          </template>
          <template v-slot:expanded-item="{ headers, item }">
            <td :colspan="headers.length">
              <v-layout justify-space-around mb-3>
                <v-flex xs11>
                  <v-card elevation-3>
                    <v-data-table
                      :headers="expandedPartsHeaders"
                      :items="item.parts"
                      :expanded.sync="expandedParts"
                      item-key="id"
                    >
                      <template slot="item" slot-scope="props">
                        <tr>
                          <td>
                            <v-icon
                              @click="expandPartRow(props)"
                              v-show="!props.isExpanded"
                              >fa-arrow-down</v-icon
                            >
                            <v-icon
                              @click="expandPartRow(props)"
                              v-show="props.isExpanded"
                              >fa-arrow-up</v-icon
                            >
                          </td>
                          <td>{{ props.item.id }}</td>
                          <td class="text-xs-left">
                            {{ props.item.nazwa }}
                          </td>
                          <td class="text-xs-left">
                            <v-icon v-show="props.item.komponentId"
                              >fa-check-square</v-icon
                            >
                          </td>
                          <td class="text-xs-left">
                            {{ props.item.ilosc }}
                          </td>
                          <td class="text-xs-left">
                            <v-layout row wrap justify-space-around>
                              <v-icon
                                v-show="props.item.status > 0"
                                color="green"
                                >check</v-icon
                              >
                              <v-icon v-show="props.item.status < 2" color="red"
                                >fa-exclamation-triangle</v-icon
                              >
                              <v-spacer></v-spacer>
                              {{ props.item.availableParts }}
                              <v-spacer></v-spacer>
                            </v-layout>
                          </td>
                        </tr>
                      </template>
                      <template v-slot:expanded-item="{ headers, item }">
                        <td :colspan="headers.length">
                          <v-layout
                            justify-space-around
                            mb-3
                            v-if="item.komponent"
                          >
                            <v-flex xs11>
                              <v-card elevation-3>
                                <v-data-table
                                  :headers="expandedComponentsHeaders"
                                  :items="[item.komponent]"
                                >
                                  <template slot="item" slot-scope="props">
                                    <tr>
                                      <td>{{ props.item.komponentId }}</td>
                                      <td>{{ props.item.nazwa }}</td>
                                      <td>{{ props.item.ilosc }}</td>
                                      <td class="text-xs-left">
                                        <v-layout row wrap justify-space-around>
                                          <v-icon
                                            v-show="props.item.status > 0"
                                            color="green"
                                            >check</v-icon
                                          >
                                          <v-icon
                                            v-show="props.item.status < 2"
                                            color="red"
                                            >fa-exclamation-triangle</v-icon
                                          >
                                          <v-spacer></v-spacer>
                                          {{ props.item.availableComponents }}
                                          <v-spacer></v-spacer>
                                        </v-layout>
                                      </td>
                                    </tr>
                                  </template>
                                </v-data-table>
                              </v-card>
                            </v-flex>
                          </v-layout>
                          <v-layout
                            v-else
                            justify-space-around
                            mb-3
                            color="red"
                          >
                            Wybrana część nie posiada zdefiniowanego komponentu!
                          </v-layout>
                        </td>
                      </template>
                    </v-data-table>
                  </v-card>
                </v-flex>
              </v-layout>
            </td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>

export default {
  name: 'Availability',
  components: {
  },
  props: {
    offerAvailability: Object,
    currentOffer: Object,
    readonly: Boolean,
    products: Array,
    processes: Array
  },
  data () {
    return {
      headers: [
        // { text: 'Id', value: 'id', visible: true },
        { text: 'Rozwiń', value: 'expand', visible: true },
        { text: 'ProductID', value: 'productName', visible: true },
        { text: 'W', value: 'w', visible: true },
        { text: 'Medium', value: 'medium', visible: true },
        { text: 'Dodatkowe wyposażenie', value: 'additionalEquipment', visible: true },
        { text: 'Cena w dniu oferty', value: 'priceInOfferDay', visible: true },
        { text: 'Cena sprzedaży', value: 'salesPrice', visible: true },
        { text: 'Ilość', value: 'quantity', visible: true },
        { text: 'Dostępność', value: 'availableProducts', visible: true }
      ],
      expandedPartsHeaders: [
        { text: 'Rozwiń', value: 'expand', visible: true },
        { text: 'Id części', value: 'id', visible: true },
        { text: 'Nazwa', value: 'nazwa', visible: true },
        { text: 'Komponent', value: 'komponentId', visible: true },
        { text: 'Ilość sztuk', value: 'ilosc', visible: true },
        { text: 'Dostępność', value: 'availableParts', visible: true }
      ],
      expandedComponentsHeaders: [
        { text: 'Id komponentu', value: 'komponentId', visible: true },
        { text: 'Nazwa', value: 'nazwa', visible: true },
        { text: 'Ilość sztuk', value: 'ilosc', visible: true },
        { text: 'Dostępność', value: 'availableParts', visible: true }
      ],
      search: '',
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadź wartość dodatnią'
        return true
      },
      intRule: val => {
        if (val.includes(',') || val.includes('.')) return 'Wprowadź wartość całkowitą'
        return true
      },
      showProcessSelectorDialog: false,
      processesSelectorTitle: 'Wybierz procesy',
      currentOfferLine: {},
      expanded: [],
      expandedParts: []

    }
  },
  computed: {

  },
  watch: {

  },
  methods: {
    getSelectedOfferLineProcesses (item) {
      var selPro = []
      var $this = this

      if (!item.offerLineProcess) {
        item.offerLineProcess = []
      }
      item.offerLineProcess.forEach(p => {
        var obj = $this.processes.find(pr => pr.id === p.processId)
        selPro.push(obj)
      })
      return selPro
    },
    expandProductRow (item) {
      this.currentOfferLine = item.item
      item.expand(!item.isExpanded)
    },
    expandPartRow (item) {
      item.expand(!item.isExpanded)
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
