<template>
  <div>
    <v-layout row wrap elevation-3>
      <v-flex xs12>
        <v-toolbar flat color="white">
          <v-toolbar-title>{{ title }}</v-toolbar-title>
          <v-divider class="mx-2" inset vertical></v-divider>
          <v-spacer></v-spacer>
          <v-text-field
            append-icon="search"
            label="Search"
            single-line
            hide-details
            class="elevation-1"
            v-model="searchTable"
          ></v-text-field>
        </v-toolbar>
        <v-data-table
          :headers="headers"
          :items="data"
          :search="searchTable"
          :loading="tableLoading"
          :footer-props="footer"
          class="elevation-2"
          :expanded.sync="expanded"
          single-expand
        >
          <template slot="item" slot-scope="props">
            <tr>
              <template v-for="header in headers">
                <!-- Columns settings  -->
                <td :key="header.value" v-if="header.value === 'wykonaneStepy'">
                  <v-progress-circular
                    :rotate="-90"
                    :value="props.item.wykonaneStepy * 10"
                    color="primary"
                  >
                    {{ props.item.wykonaneStepy }}
                  </v-progress-circular>
                </td>
                <td :key="header.value" v-else-if="header.value === 'expand'">
                  <v-icon @click="expandRow(props)" v-show="!props.isExpanded"
                    >fa-arrow-down</v-icon
                  >
                  <v-icon @click="expandRow(props)" v-show="props.isExpanded"
                    >fa-arrow-up</v-icon
                  >
                </td>
                <td
                  :key="header.value"
                  class="justify-center px-0"
                  v-else-if="header.value === 'actions'"
                >
                  <v-layout>
                    <v-flex xs6>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on, attrs }">
                          <v-icon
                            class="mr-2"
                            color="primary"
                            @click="registerTime(props.item)"
                            v-bind="attrs"
                            v-on="on"
                          >
                            fa-hourglass-half
                          </v-icon>
                        </template>
                        <span>Zarejestruj czas</span>
                      </v-tooltip>
                    </v-flex>
                    <v-flex xs6>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on, attrs }">
                          <v-icon
                            class="mr-2"
                            color="primary"
                            @click="closeStep(props.item)"
                            v-bind="attrs"
                            v-on="on"
                          >
                            fa-step-forward
                          </v-icon>
                        </template>
                        <span>Zamknij step</span>
                      </v-tooltip>
                    </v-flex>
                  </v-layout>
                </td>
                <td :key="header.value" class="text-xs-center" v-else>
                  {{ props.item[header.value] }}
                </td>
              </template>
            </tr>
          </template>
          <template v-slot:expanded-item="{ headers, item }">
            <td :colspan="headers.length">
              <v-layout justify-space-around>
                <v-flex xs11>{{ item.id }} </v-flex>
              </v-layout>
            </td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import * as v from '../main.js'
import ProdExeExpand from '../components/ProdExeExpand.vue'
export default {
  name: 'ProdExe',
  components: {
    // eslint-disable-next-line vue/no-unused-components
    ProdExeExpand
  },
  props: {
  },
  data () {
    return {
      expanded: [],
      // Settings
      title: 'Akcje do wykonania',
      editTitle: 'Edytuj typ wyrobu',
      newTitle: 'Dodaj  typ wyrobu',
      getAllApi: 'api/Production/GetMainPage',
      postNewApi: 'api/ProductTypes/Create',
      putEditApi: 'api/ProductTypes/Update',
      deleteApi: 'api/ProductTypes/Delete',
      defaultItem: {
        id: ''
      },
      // Settings
      editedItem: {},
      editedIndex: -1,
      showDialog: false,
      data: [],
      currentId: 0,
      // Columns headers
      headers: [
        { text: 'Rozwiń', value: 'expand' },
        { text: 'Id', value: 'id' },
        { text: 'Id zamówienia', value: 'identyfikatorZamowienia' },
        { text: 'Wiersz oferty', value: 'wierszOferty' },
        { text: 'Klient', value: 'klient' },
        { text: 'Numer Klienta', value: 'numerKlienta' },
        { text: 'Data końcowa', value: 'plannedEnd' },
        { text: 'Nazwa procesu', value: 'nazwaProcesu' },
        { text: 'Id wyrobu', value: 'identyfikatorWyrobu' },
        { text: 'Typ wyrobu', value: 'typWyrobu' },
        { text: 'Dn', value: 'dn' },
        { text: 'Ilosc wyrobow', value: 'iloscWyrobow' },
        { text: 'Wersja', value: 'wersja' },
        { text: 'Czas spędzony', value: 'czasSpedzony' },
        { text: 'Wykonanie', value: 'wykonaneStepy' },
        { text: 'Akcje', value: 'actions' }
      ],
      editMode: false
    }
  },
  computed: {
    formTitle: function () {
      if (this.editedIndex >= 0) {
        return this.editTitle
      } else {
        return this.newTitle
      }
    }
  },
  watch: {
    showDialog (val) {
      val || this.close()
    }
  },
  methods: {
    expandRow (item) {
      item.expand(!item.isExpanded)
    }
  },
  created () {
    v.axiosInstance.get(this.getAllApi)
      .then(Response => { this.data = Response.data; this.tableLoading = false })
  },
  destroyed () {
  }
}
</script>

<style>
</style>
