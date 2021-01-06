
<template>
  <div>
    <v-dialog v-model="showProcessSelectorDialog" max-width="1600" persistent>
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
              <ProcessSelector
                v-if="!readonly"
                :filteredProcesses="filteredProcesses"
                :parentItem="currentOfferLine"
                @editedOfferLine="editOfferLineProcesses"
              >
              </ProcessSelector>
              <!-- <NewOffer
                :currentOffer="currentOffer"
                :projects="projects"
                :editMode="editMode"
                @editedOffer="editcurrentOfferRes"
                :customers="customers"
                :products="products"
              ></NewOffer> -->
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
    </v-dialog>
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
          <v-btn
            color="primary"
            dark
            class="mb-2"
            @click="addOfferLineToOffer()"
            v-if="!readonly"
            >Dodaj</v-btn
          >
        </v-toolbar>
        <v-data-table
          :headers="headers"
          :items="currentOffer.offerLines"
          :expanded.sync="expanded"
          item-key="tempId"
          :search="search"
          :hide-default-footer="readonly"
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
                            @click="selectProcesses(props, props.item, index)"
                            color="blue lighten-1"
                            >search</v-icon
                          >
                        </template>
                        <span>Pokaż procesy</span>
                      </v-tooltip>
                    </v-flex>
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
                <td :key="index" v-else-if="header.value === 'processesNo'">
                  {{ props.item.offerLineProcess.length }}
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
                    <v-autocomplete
                      v-if="header.value == 'productId'"
                      color
                      :items="products"
                      item-text="id"
                      item-value="id"
                      :rules="[requiredRule]"
                      v-model="props.item.productId"
                    ></v-autocomplete>
                    <v-text-field
                      v-else-if="header.value == 'quantity'"
                      v-model="props.item.quantity"
                      type="number"
                      :rules="[requiredRule, numberRule, intRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'w'"
                      v-model="props.item.w"
                      :rules="[requiredRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'medium'"
                      v-model="props.item.medium"
                      :rules="[requiredRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'additionalEquipment'"
                      v-model="props.item.additionalEquipment"
                      :rules="[requiredRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'sale'"
                      v-model="props.item.sale"
                      :rules="[requiredRule]"
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
import ProcessSelector from '../components/ProcessSelector'

export default {
  name: 'Productoffers',
  components: {
    ProcessSelector: ProcessSelector
  },
  props: {
    currentOffer: Object,
    readonly: Boolean,
    products: Array,
    processes: Array
  },
  data () {
    return {
      headers: [
        // { text: 'Id', value: 'tempId', visible: true },
        { text: 'Rozwiń', value: 'expand', visible: true },
        { text: 'ProductID', value: 'productId', visible: true },
        { text: 'Ilość', value: 'quantity', visible: true },
        { text: 'W', value: 'w', visible: true },
        { text: 'Medium', value: 'medium', visible: true },
        { text: 'Dodatkowe wyposażenie', value: 'additionalEquipment', visible: true },
        { text: 'Sale', value: 'sale', visible: true },
        { text: 'Liczba procesów', value: 'processesNo', visible: true },
        { text: 'Akcje', value: 'actions', visible: true }
      ],
      expandedHeaders: [
        // { text: 'Id', value: 'id', visible: true },
        { text: 'Proces id', value: 'id', visible: true },
        { text: 'Nazwa grupy procesu', value: 'nazwGrupyProcesu', visible: true },
        { text: 'Business area', value: 'businessArea', visible: true },
        { text: 'Nazwa procesu', value: 'nazwaProcesu', visible: true },
        { text: 'Typ zlecenia', value: 'typZlecenia', visible: true }
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
      expanded: []

    }
  },
  computed: {
    // selectedOfferLineProcesses () {
    //   var selPro = []
    //   var $this = this
    //   this.currentOfferLine.offerLineProcess.forEach(p => {
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

      // offerLineProcess
      return filProcesses
    }
  },
  watch: {

  },
  methods: {
    getSelectedOfferLineProcesses (item) {
      var selPro = []
      var $this = this
      // eslint-disable-next-line no-debugger
      // debugger
      if (!item.offerLineProcess) {
        item.offerLineProcess = []
      }
      item.offerLineProcess.forEach(p => {
        var obj = $this.processes.find(pr => pr.id === p.processId)
        selPro.push(obj)
      })
      return selPro
    },
    addOfferLineToOffer () {
      if (!this.currentOffer.offerLines) {
        this.$set(this.currentOffer, 'offerLines', [])
      }

      this.currentOffer.offerLines.unshift({ id: 0, tempId: -1 - this.currentOffer.offerLines.length, productId: '', quantity: '', w: '', medium: '', additionalEquipment: '', sale: '', actions: '', isEdited: true, offerLineProcess: [] })
    },
    async editOffer (offer) {
      if (offer.isEdited) {
        if (offer.productId.length === 0 || offer.quantity === 0 || offer.w.length === 0 || offer.medium.length === 0 || offer.additionalEquipment.length === 0 || offer.sale === 0) {
          await this.$dialog.confirm({
            text: 'Uzupełnij dane!'
          })
        } else {
          this.$set(offer, 'isEdited', false)
        }
      } else {
        this.$set(offer, 'isEdited', true)
      }
    },
    expandRow (item) {
      this.currentOfferLine = item.item
      item.item.fullProcesses = this.getSelectedOfferLineProcesses(item.item)
      item.expand(!item.isExpanded)
    },
    deleteOffer (offer, index) {
      // var res = await this.$dialog.confirm({
      //   text: 'Czy na pewno chcesz usunąć:  ' + offer.czesciId + '?',
      //   title: 'Uwaga'
      // })
      // if (res) {
      var indexOfOffer = this.currentOffer.offerLines.indexOf(offer)
      this.currentOffer.offerLines.splice(indexOfOffer, 1)
    //   }
    },
    editOfferLineProcesses (offerLine, selectedProcesses) {
      if (this.currentOffer.offerLines) {
        this.currentOffer.offerLines.forEach(ol => {
          if (ol.id === offerLine.id) {
            ol.offerLineProcess = []
            selectedProcesses.forEach(sp => {
              var item = {
                // id: sp.offerLineProcessId,
                offerLineId: ol.id,
                processId: sp.id
              }
              ol.offerLineProcess.push(item)
            })
          }
        })
      }
    },
    selectProcesses (props, offer, index) {
      props.expand(false)
      this.currentOfferLine = offer
      this.selectedProcesses = []
      this.processesSelectorTitle = 'Wybierz procesy' + ' (Produkt: ' + offer.productId + ')'
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
