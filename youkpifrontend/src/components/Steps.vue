
<template>
  <div>
    <!-- <v-dialog v-model="showProcessSelectorDialog" max-width="1600" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">{{ processesSelectorTitle }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showProcessSelectorDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-layout row wrap justify-space-around class="ma-4">
          <v-flex xs11>
            <v-form ref="newOfferForm">
               <NewOffer
                :currentProcess="currentProcess"
                :projects="projects"
                :editMode="editMode"
                @editedOffer="editcurrentProcessRes"
                :customers="customers"
                :products="products"
              ></NewOffer>
            </v-form>
          </v-flex>
        </v-layout>
        <v-card-actions class="blue lighten-5">
          <v-spacer></v-spacer>
          <v-btn
            flat
            large
            color="blue darken-1"
            @click.native="saveOfferProcesses"
          >
            Zapisz
            <v-icon dark>save</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog> -->
    <v-layout
      row
      wrap
      justify-space-around
      elevation-3
      width="100%"
      class="mb-5"
    >
      <v-flex xs12>
        <v-toolbar flat color="white" v-if="!readonly">
          <v-toolbar-title>Kroki</v-toolbar-title>
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
          <v-btn
            color="primary"
            dark
            class="mb-2"
            @click="addStepToProcess()"
            v-if="!readonly"
            >Dodaj</v-btn
          >
        </v-toolbar>
        <v-data-table
          :headers="headers"
          :items="currentProcess.steps"
          :expanded.sync="expanded"
          item-key="tempId"
          :search="search"
          disable-sort
          calculate-widths
        >
          <template v-if="readonly" v-slot:header.actions="{}"> </template>
          <template slot="item" slot-scope="props">
            <tr :class="props.isExpanded ? 'primary lighten-5' : 'white'">
              <!-- <td>
                {{ props.item.tempId }}
              </td> -->
              <template v-for="(header, index) in headers">
                <td :key="index" v-if="header.value == 'actions' && !readonly">
                  <v-layout justify-space-between>
                    <v-flex xs4>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            v-on="on"
                            v-show="props.item.isEdited"
                            @click="editOffer(props.item, index)"
                            color="orange"
                            class="mr-2"
                            >check</v-icon
                          >
                        </template>
                        <span>Akceptuj</span>
                      </v-tooltip>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            v-on="on"
                            v-show="!props.item.isEdited"
                            @click="editOffer(props.item, index)"
                            color="green"
                            class="mr-2"
                            >edit</v-icon
                          >
                        </template>
                        <span>Edytuj</span>
                      </v-tooltip>
                    </v-flex>
                    <v-flex xs4>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            @click="deleteOffer(props.item, index)"
                            v-on="on"
                            color="red lighten-1"
                            >delete</v-icon
                          >
                        </template>
                        <span>Usuń</span>
                      </v-tooltip>
                    </v-flex>
                  </v-layout>
                </td>
                <td :key="index" v-else-if="header.value === 'expand'">
                  <v-icon @click="expandRow(props)" v-show="!props.isExpanded"
                    >fa-arrow-down</v-icon
                  >
                  <v-icon @click="expandRow(props)" v-show="props.isExpanded"
                    >fa-arrow-up</v-icon
                  >
                </td>
                <td v-else :key="index">
                  <template v-if="props.item.isEdited && !readonly">
                    <v-text-field
                      v-if="header.value == 'stepId'"
                      v-model="props.item.stepId"
                      disabled
                      :rules="[requiredRule, numberRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'stepName'"
                      v-model="props.item.stepName"
                      :rules="[requiredRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'stepNum'"
                      v-model="props.item.stepNum"
                      @input="caluculateStepId(props.item)"
                      type="number"
                      min="0"
                      :rules="[requiredRule, intRule]"
                    ></v-text-field>
                    <v-text-field
                      disabled
                      v-else-if="header.value == 'processesId'"
                      v-model="props.item.processesId"
                      :rules="[requiredRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'sekwencja'"
                      v-model="props.item.sekwencja"
                      type="number"
                      min="0"
                      :rules="[requiredRule, intRule]"
                    ></v-text-field>
                  </template>
                  <template v-else>
                    {{ props.item[header.value] }}
                  </template>
                </td>
              </template>
            </tr>
          </template>
          <template v-slot:expanded-item="{ headers, item }">
            <td :colspan="headers.length">
              <v-layout justify-space-around>
                <v-flex xs11>
                  <v-card elevation-3>
                    <v-data-table
                      :headers="expandedHeaders"
                      :items="item.fullProcesses"
                    >
                      <template slot="item" slot-scope="props">
                        <tr>
                          <td>{{ props.item.id }}</td>
                          <td class="text-xs-left">
                            {{ props.item.nazwGrupyProcesu }}
                          </td>
                          <td class="text-xs-left">
                            {{ props.item.businessArea }}
                          </td>
                          <td class="text-xs-left">
                            {{ props.item.nazwaProcesu }}
                          </td>
                          <td class="text-xs-left">
                            {{ props.item.typZlecenia }}
                          </td>
                        </tr>
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
  name: 'Steps',
  components: {
  },
  props: {
    currentProcess: Object,
    readonly: Boolean
  },
  data () {
    return {
      headers: [
        { text: 'Id', value: 'stepId', visible: true },
        { text: 'Nazwa', value: 'stepName', visible: true },
        { text: 'Numer kroku', value: 'stepNum', visible: true },
        { text: 'Id procesu', value: 'processesId', visible: true },
        { text: 'Sekwencja', value: 'sekwencja', visible: true },
        { text: 'Akcje', value: 'actions', visible: true }
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
      }
      // showProcessSelectorDialog: false,
      // processesSelectorTitle: 'Wybierz procesy',
      // currentProcessLine: {},
      // expanded: []

    }
  },
  computed: {
    // selectedOfferLineProcesses () {
    //   var selPro = []
    //   var $this = this
    //   this.currentProcessLine.processLineProcess.forEach(p => {
    //     var item = $this.processes.find(pr => pr.id === p.processId)
    //     selPro.push(item)
    //   })
    //   item.fullProcesses = selPro
    //   return selPro
    // },
    filteredProcesses () {
      var filProcesses = this.processes.filter(p => {
        if (p.typZlecenia.toUpperCase().trim().includes('O') || p.typZlecenia === '0') {
          return false
        }
        return true
      })

      // processLineProcess
      return filProcesses
    }
  },
  watch: {

  },
  methods: {
    caluculateStepId (item) {
      item.stepId = item.processesId + '.' + item.stepNum
    },
    getSelectedOfferLineProcesses (item) {
      var selPro = []
      var $this = this
      // eslint-disable-next-line no-debugger
      // debugger
      if (!item.processLineProcess) {
        item.processLineProcess = []
      }
      item.processLineProcess.forEach(p => {
        var obj = $this.processes.find(pr => pr.id === p.processId)
        selPro.push(obj)
      })
      return selPro
    },
    addStepToProcess () {
      if (!this.currentProcess.steps) {
        this.$set(this.currentProcess, 'steps', [])
      }
      if (!this.currentProcess.id) {
        this.$dialog.confirm({
          text: 'Dodaj id procesu!',
          title: 'Uwaga'
        })
      } else {
        this.currentProcess.steps.unshift({ id: 0, tempId: -1 - this.currentProcess.steps.length, actions: '', isEdited: true, stepName: '', stepNum: '', processesId: this.currentProcess.id })
      }
    },
    async editOffer (process) {
      if (process.isEdited) {
        if (process.stepName.length === 0 || process.stepNum.length === 0 || process.sekwencja.length === 0) {
          await this.$dialog.confirm({
            text: 'Uzupełnij dane!'
          })
        } else {
          this.$set(process, 'isEdited', false)
        }
      } else {
        this.$set(process, 'isEdited', true)
      }
    },
    expandRow (item) {
      this.currentProcessLine = item.item
      item.item.fullProcesses = this.getSelectedOfferLineProcesses(item.item)
      item.expand(!item.isExpanded)
    },
    deleteOffer (process, index) {
      // var res = await this.$dialog.confirm({
      //   text: 'Czy na pewno chcesz usunąć:  ' + process.czesciId + '?',
      //   title: 'Uwaga'
      // })
      // if (res) {
      var indexOfOffer = this.currentProcess.steps.indexOf(process)
      this.currentProcess.steps.splice(indexOfOffer, 1)
    //   }
    },
    editOfferLineProcesses (processLine, selectedProcesses) {
      if (this.currentProcess.steps) {
        this.currentProcess.steps.forEach(ol => {
          if (ol.id === processLine.id) {
            ol.processLineProcess = []
            selectedProcesses.forEach(sp => {
              var item = {
                // id: sp.processLineProcessId,
                processLineId: ol.id,
                processId: sp.id
              }
              ol.processLineProcess.push(item)
            })
          }
        })
      }
    },
    selectProcesses (props, process, index) {
      props.expand(false)
      this.currentProcessLine = process
      this.selectedProcesses = []
      this.processesSelectorTitle = 'Wybierz procesy' + ' (Produkt: ' + process.productId + ')'
      this.showProcessSelectorDialog = true
    },
    saveOfferProcesses () {
      this.showProcessSelectorDialog = false
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
