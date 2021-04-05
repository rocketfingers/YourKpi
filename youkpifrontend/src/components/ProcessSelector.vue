<template>
  <div>
    <v-card-text>
      <v-container grid-list-md>
        <v-layout row wrap justify-space-around class="elevation-3">
          <v-flex 12>
            <v-toolbar text color="white">
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
              :expanded.sync="expanded"
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
                  <td :key="index">
                    <v-icon @click="expandRow(props)" v-show="!props.isExpanded"
                      >fa-arrow-down</v-icon
                    >
                    <v-icon @click="expandRow(props)" v-show="props.isExpanded"
                      >fa-arrow-up</v-icon
                    >
                  </td>
                  <td class="text-xs-left">{{ props.item.id }}</td>
                  <td class="text-xs-left">
                    {{ props.item.nazwGrupyProcesu }}
                  </td>
                  <td class="text-xs-left">{{ props.item.businessArea }}</td>
                  <td class="text-xs-left">{{ props.item.nazwaProcesu }}</td>
                  <td class="text-xs-left">
                    <v-text-field
                      v-if="showRequiredTime"
                      v-model="props.item.czasWymagany"
                      label="Czas wymagany"
                      prepend-icon="mdi-clock-time-four-outline"
                      type="number"
                      :rules="[czasWymaganyRule]"
                      hint="1-10000"
                      mask="time"
                      @change="selectItem(props, true)"
                      return-masked-value
                    ></v-text-field>
                    <span v-else>Nie dotyczy</span>
                  </td>

                  <td class="text-xs-left">{{ props.item.typZlecenia }}</td>
                </tr>
                <tr></tr
              ></template>
              <template v-slot:expanded-item="{ headers, item }">
                <td :colspan="headers.length">
                  <v-layout justify-space-around>
                    <v-flex xs11>
                      <Steps :currentProcess="item" :readonly="true"> </Steps>
                    </v-flex>
                  </v-layout>
                </td>
              </template>
            </v-data-table>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card-text>
  </div>
</template>

<script>
import Steps from '../components/Steps'

export default {
  name: 'ProcessSelector',
  components: {
    Steps: Steps

  },
  props: {
    parentItem: Object,
    filteredProcesses: Array,
    showRequiredTime: Boolean,
    expanded: []

  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadz dodatnia wartosc'
        return true
      },
      czasWymaganyRule: val => {
        if (val > 10000 || val < 1) return 'Wprowadź wartość z dopuszczalnego zakresu'
        return true
      },
      headers: [
        { text: 'Rozwiń', value: 'expand' },
        { text: 'Id', value: 'id', visible: true },
        { text: 'Nazwa grupy procesu', value: 'nazwaGrupyProcesu', visible: true },
        { text: 'Business area', value: 'businessArea', visible: true },
        { text: 'Nazwa procesu', value: 'nazwaProcesu', visible: true },
        { text: 'Czas wymagany', value: 'czasWymagany', visible: this.showRequiredTime },
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

  },
  watch: {
    selectedProcesses (val) {
      this.$emit('editedOfferLine', this.parentItem, this.selectedProcesses)
    }

  },
  methods: {
    expandRow (item) {
      item.expand(!item.isExpanded)
    },
    selectItem (props, val) {
      if (val === true) {
        props.select(false)
        props.select(true)
      } else {
        props.select(props.isSelected)
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
