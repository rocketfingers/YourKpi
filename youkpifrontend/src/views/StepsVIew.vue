<template>
  <div>
    <v-layout row wrap elevation-3>
      <v-flex xs12>
        <v-toolbar flat color="white">
          <v-toolbar-title>Steps machines</v-toolbar-title>
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
      </v-flex>
      <v-flex xs12>
        <v-data-table
          :headers="headers"
          :items="items"
          class="elevation-1"
          item-key="id"
          :expanded.sync="expanded"
          :loading="tableLoading"
          :search="search"
        >
          <template slot="item" slot-scope="props">
            <tr>
              <template v-for="(header, index) in headers">
                <td :key="index" v-if="header.value === 'expand'">
                  <v-icon @click="expandRow(props)" v-show="!props.isExpanded"
                    >fa-arrow-down</v-icon
                  >
                  <v-icon @click="expandRow(props)" v-show="props.isExpanded"
                    >fa-arrow-up</v-icon
                  >
                </td>
                <td :key="index" v-else>
                  {{ props.item[header.value] }}
                </td>
              </template>
            </tr>
          </template>
          <template v-slot:expanded-item="{ headers, item }">
            <td :colspan="headers.length">
              <v-layout justify-space-around>
                <v-flex xs11>
                  <StepsMachines
                    :currentMachine="item"
                    :readonly="true"
                  ></StepsMachines>
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
import StepsMachines from '../components/StepsMachines'

export default {
  name: 'StepsView',
  components: {
    StepsMachines: StepsMachines
  },
  props: {},
  data () {
    return {
      // api
      getAll: 'api/StepsMachines/GetAll',
      addApi: 'api/ReasonCode/Create',
      editApi: 'api/ReasonCode/Update',
      deleteApi: 'api/ReasonCode/Delete',
      expanded: [],

      headers: [
        { text: 'Rozwiń', value: 'expand' },
        { text: 'Step id', value: 'stepId' },
        { text: 'Nazwa', value: 'stepName' },
        { text: 'Numer', value: 'stepNum' },
        { text: 'Proces id', value: 'processesId' },
        { text: 'Sekwencja', value: 'sekwencja' }
        // { text: 'Akcje', value: 'actions' }
      ],
      items: [],
      search: '',
      showNewDialog: false,
      showProductPartsDialog: false,
      tableLoading: false,
      formTitle: 'Dodaj produkt',
      currentItem: { produktCzesci: [] },
      editedIndex: -1,
      productTypes: [],
      editMode: false,
      parts: []
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      this.getItems()
    },
    expandRow (item) {
      item.expand(!item.isExpanded)
    },
    getItems () {
      var $this = this
      this.$http
        .get(this.getAll)
        .then((Response) => {
          $this.items = Response.data
          this.tableLoading = false
        })
        .catch((e) => {
          this.tableLoading = false
        })
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
