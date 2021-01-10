<template>
  <div>
    <v-layout
      row
      wrap
      align-center
      justify-space-around
      class="elevation-3"
      mb-3
    >
      <v-flex>
        <v-layout row wrap align-center justify-space-around>
          <v-flex md5 xs12 ma-3>
            <v-layout row wrap align-center justify-space-around>
              <v-autocomplete
                v-model="selectedProcesses"
                :items="processesArray"
                multiple
                clearable
                label="Id procesu"
              ></v-autocomplete>
            </v-layout>
          </v-flex>
          <v-flex md5 xs12 ma-3>
            <v-layout row wrap align-center justify-space-around>
              <v-autocomplete
                v-model="selectedMachines"
                :items="usedMachinesArray"
                multiple
                clearable
                label="Id maszyny"
              ></v-autocomplete>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
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
          :items="filteredItems"
          class="elevation-1"
          item-key="id"
          :expanded.sync="expanded"
          :loading="tableLoading"
          :search="search"
        >
          <template slot="item" slot-scope="props">
            <tr :class="props.isExpanded ? 'primary lighten-5' : 'white'">
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
                    :readonly="false"
                    :currentStep="item"
                    :machines="machines"
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
      getAllMachines: 'api/Machine/GetAll',

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
      tableLoading: false,
      editMode: false,
      machines: [],
      selectedProcesses: [],
      selectedMachines: [],
      currentStep: {}
    }
  },
  computed: {
    usedMachinesArray () {
      var usedMachines = []
      this.items.forEach(i => {
        if (i.stepsMachines) {
          i.stepsMachines.forEach(sm => {
            if (!usedMachines.includes(sm.machineId) && sm.machineId != null) {
              usedMachines.push(sm.machineId)
            }
          })
        }
      })

      return usedMachines
    },
    processesArray () {
      var processes = []
      this.items.forEach(i => {
        if (!processes.includes(i.processesId) && i.processesId != null) {
          processes.push(i.processesId)
        }
      })
      // eslint-disable-next-line no-debugger
      // debugger
      return processes
    },
    filteredItems () {
      var res = this.items
      if (this.selectedProcesses.length > 0) {
        res = this.items.filter(i => {
          if (this.selectedProcesses.includes(i.processesId)) {
            return true
          }
        })
      }
      if (this.selectedMachines.length > 0) {
        res = res.filter(i => {
          if (i.stepsMachines) {
            if (i.stepsMachines.some(sm => {
              if (this.selectedMachines.some(selMach => selMach === sm.machineId)) {
                return true
              }
            })) {
              return true
            }
          }
        })
      }
      return res
    }
  },
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      this.getMachines()
      this.getItems()
    },
    expandRow (item) {
      this.currentStep = item.item
      item.expand(!item.isExpanded)
    },
    getItems () {
      var $this = this
      this.$http
        .get(this.getAll)
        .then((Response) => {
          $this.items = Response.data
          $this.items.forEach(i => {
            if (!i.stepsMachines) {
              i.stepsMachines = []
            } else {
              i.stepsMachines.forEach(j => {
                j.showName = j.id + ', ' + j.nazwa
                j.actions = ''
              })
            }
          }
          )
          $this.tableLoading = false
        })
        .catch((e) => {
          $this.tableLoading = false
        })
    },
    getMachines () {
      var $this = this
      this.$http
        .get(this.getAllMachines)
        .then((Response) => {
          $this.machines = Response.data
          $this.machines.forEach(i => {
            i.showName = i.id + ', ' + i.nazwa
            i.actions = ''
          })
        })
        .catch((e) => {
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
