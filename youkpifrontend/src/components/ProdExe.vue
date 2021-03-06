<template>
  <div>
    <v-layout row wrap elevation-3>
      <v-flex xs12 sm6 md4 xl3>
        <v-select
          :items="[
            { id: 'O', name: 'Oferta' },
            { id: 'P', name: 'Produkcja' },
            { id: 'R', name: 'Rozwój' },
          ]"
          v-model="selectedTypes"
          label="Typ"
          multiple
          chips
          item-text="name"
          item-value="id"
        ></v-select>
      </v-flex>
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
          <v-divider class="mx-2" inset vertical></v-divider>

          <v-btn @click="getData()" rounded color="primary"
            ><v-icon>refresh</v-icon></v-btn
          >
        </v-toolbar>
        <v-data-table
          :headers="headers"
          :items="filterData"
          :search="searchTable"
          :loading="tableLoading"
          :footer-props="footer"
          class="elevation-2"
          :expanded.sync="expanded"
          single-expand
          no-data-text="Brak przypisanych procesów albo ofert do wykonania"
        >
          <template slot="item" slot-scope="props">
            <tr>
              <template v-for="header in headers">
                <!-- Columns settings  -->
                <td :key="header.value" v-if="header.value === 'wykonaneStepy'">
                  <v-progress-circular
                    :rotate="-90"
                    :value="
                      (props.item.wykonaneStepy / props.item.iloscStepow) * 100
                    "
                    color="primary"
                  >
                    {{ props.item.wykonaneStepy }} </v-progress-circular
                  >/{{ props.item.iloscStepow }}
                </td>
                <td
                  :key="header.value"
                  v-else-if="header.value === 'plannedEnd'"
                >
                  <span class="text-no-wrap">{{
                    formatDateTimeYYYYMMDD(props.item.plannedEnd)
                  }}</span>
                </td>
                <td :key="header.value" v-else-if="header.value === 'expand'">
                  <v-icon @click="expandRow(props)" v-show="!props.isExpanded"
                    >fa-arrow-down</v-icon
                  >
                  <v-icon @click="expandRow(props)" v-show="props.isExpanded"
                    >fa-arrow-up</v-icon
                  >
                </td>
                <td :key="header.value" class="text-xs-center" v-else>
                  {{ props.item[header.value] }}
                </td>
              </template>
            </tr>
          </template>
          <template v-slot:expanded-item="{ headers }">
            <td :colspan="headers.length">
              <v-layout justify-start>
                <v-flex xs11>
                  <v-data-table
                    :headers="stepsHeaders"
                    :items="stepsData"
                    class="elevation-1"
                    :loading="stepsLoading"
                    hide-default-footer
                    :footer-props="footerStepTable"
                  >
                    <template slot="item" slot-scope="props">
                      <tr>
                        <template v-for="header in stepsHeaders">
                          <!-- Columns settings  -->
                          <td
                            :key="header.value"
                            v-if="header.value === 'zakonczone'"
                          >
                            {{ props.item.zakonczone ? 'Tak' : 'Nie' }}
                            <v-icon
                              :color="
                                getStatusColor(props.item.plannedStartStatus)
                              "
                              >fa-circle</v-icon
                            >
                          </td>
                          <td
                            :key="header.value"
                            v-else-if="header.value === 'stepStarted'"
                          >
                            {{ props.item.stepStarted ? 'Tak' : 'Nie' }}
                          </td>
                          <td
                            :key="header.value"
                            v-else-if="header.value === 'stepUsedBySomeoneElse'"
                          >
                            {{
                              props.item.stepUsedBySomeoneElse ? 'Tak' : 'Nie'
                            }}
                          </td>
                          <td
                            :key="header.value"
                            v-else-if="header.value === 'shouldStartBefore'"
                          >
                            {{
                              formatDateTimeYYYYMMDD(
                                props.item.shouldStartBefore
                              )
                            }}
                          </td>
                          <td
                            :key="header.value"
                            v-else-if="header.value === 'gScore'"
                          >
                            <span
                              :class="{
                                'red--text': props.item.gScore > 0,
                                'green--text': props.item.gScore === 0,
                                'blue--text': props.item.gScore < 0,
                              }"
                              >{{ props.item.gScore }}</span
                            >
                          </td>
                          <td
                            :key="header.value"
                            class="justify-center px-0"
                            v-else-if="header.value === 'actions'"
                          >
                            <v-layout>
                              <v-flex xs3>
                                <v-progress-circular
                                  v-if="
                                    !props.item.zakonczone &&
                                    props.item.stepStartedByMe
                                  "
                                  indeterminate
                                  color="purple"
                                ></v-progress-circular>
                              </v-flex>

                              <v-flex xs3>
                                <v-tooltip bottom>
                                  <template v-slot:activator="{ on, attrs }">
                                    <v-icon
                                      class="mr-2"
                                      color="primary"
                                      @click="registerTime(props.item, 0)"
                                      v-bind="attrs"
                                      v-on="on"
                                      :disabled="
                                        props.item.zakonczone ||
                                        props.item.stepStartedByMe ||
                                        props.item.blocked
                                      "
                                    >
                                      hourglass_top
                                    </v-icon>
                                  </template>
                                  <span>Zacznij rejestrowanie czas</span>
                                </v-tooltip>
                              </v-flex>
                              <v-flex xs3>
                                <v-tooltip bottom>
                                  <template v-slot:activator="{ on, attrs }">
                                    <v-icon
                                      class="mr-2"
                                      color="primary"
                                      @click="registerTime(props.item, 1)"
                                      v-bind="attrs"
                                      v-on="on"
                                      :disabled="
                                        props.item.zakonczone ||
                                        !props.item.stepStartedByMe ||
                                        props.item.blocked
                                      "
                                    >
                                      hourglass_bottom
                                    </v-icon>
                                  </template>
                                  <span>Zakończ rejestrowanie czasu</span>
                                </v-tooltip>
                              </v-flex>
                              <v-flex xs3>
                                <v-tooltip bottom>
                                  <template v-slot:activator="{ on, attrs }">
                                    <v-icon
                                      class="mr-2"
                                      color="primary"
                                      @click="closeStep(props.item)"
                                      v-bind="attrs"
                                      v-on="on"
                                      :disabled="
                                        props.item.zakonczone ||
                                        !props.item.stepStarted ||
                                        props.item.blocked
                                      "
                                    >
                                      arrow_forward_ios
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
                  </v-data-table>
                </v-flex>
              </v-layout>
            </td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
    <v-layout row justify-center>
      <v-dialog
        v-model="dialogZamknijStep"
        v-if="dialogZamknijStep"
        persistent
        max-width="600"
      >
        <v-card>
          <v-card-title
            dark
            elevation-4
            color="primary lighten-1"
            class="headline"
            >Proces: {{ currentItem.nazwaProcesu }} - Step:
            {{ currentStepItem.stepNum }} -
            {{ currentStepItem.stepName }}</v-card-title
          >
          <v-card-text>
            <v-layout row wrap>
              <v-flex xs12>
                <v-text-field
                  v-model="liczbaPomiarow"
                  outlined
                  label="Liczba pomiarów"
                  type="Number"
                  min="0"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  v-model="liczbaPomiarowNok"
                  outlined
                  type="Number"
                  min="0"
                  label="Liczba pomiarów Nok"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-autocomplete
                  label="Część"
                  :items="parts"
                  outlined
                  :loading="partsLoading"
                  item-text="showName"
                  item-value="id"
                ></v-autocomplete>
              </v-flex>

              <v-flex xs12>
                <v-autocomplete
                  v-model="reasonCodeId"
                  outlined
                  item-text="name"
                  item-value="id"
                  :items="reasons"
                  :loading="reasonsLoading"
                  label="Reason code"
                  clearable
                ></v-autocomplete>
              </v-flex> </v-layout
          ></v-card-text>
          <v-card-actions class="blue lighten-5">
            <v-btn
              outlined
              rounded
              large
              color="blue darken-1"
              text
              @click="dialogZamknijStep = false"
              >Anuluj<v-icon dark>cancel</v-icon></v-btn
            >
            <v-spacer></v-spacer>
            <v-btn
              outlined
              rounded
              large
              color="blue darken-1"
              text
              @click.native="saveStep()"
              >Zapisz<v-icon dark>save</v-icon></v-btn
            >
            <v-btn
              :disabled="!canStepBeClosed"
              rounded
              large
              color="blue darken-1"
              @click.native="saveCloseZamknijStep()"
              >Zapisz i zamknij<v-icon dark>save</v-icon></v-btn
            >
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-layout>
  </div>
</template>

<script>
import * as v from '../main.js'
import Vue from 'vue'
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
      getAllApi: 'api/Production/GetMainPage',
      getAllPartsApi: 'api/Parts/GetAllForOfferLineIdSimpleView',

      // Settings
      showDialog: false,
      data: [],
      parts: [],
      partsLoading: true,
      selectedTypes: ['O', 'P', 'R'],
      stepsData: [],
      footerStepTable: { 'items-per-page-options': [-1] },
      stepsLoading: false,
      stepsHeaders: [
        { text: 'Step ', value: 'stepNum' },
        { text: 'Nazwa stepu', value: 'stepName' },
        { text: 'Sekwencja', value: 'sekwencja' },
        { text: 'Liczba pomiarów', value: 'liczbaPomiarow' },
        { text: 'Liczba pomiarów Nok', value: 'liczbaPomiarowNok' },
        { text: 'Step rozpoczęty', value: 'stepStarted' },
        { text: 'Używany przez innego pracowanika', value: 'stepUsedBySomeoneElse' },
        { text: 'Mój czas', value: 'timeSpendMe' },
        { text: 'Czas razem', value: 'timeSpendOther' },
        { text: 'Max rozpoczęcie', value: 'shouldStartBefore' },
        { text: 'GScore', value: 'gScore' },
        { text: 'Zakończone', value: 'zakonczone' },

        { text: 'Akcje', value: 'actions' }
      ],
      currentId: 0,
      // Columns headers
      headers: [
        { text: 'Rozwiń', value: 'expand' },
        { text: 'Id', value: 'id' },
        { text: 'Nazwa procesu', value: 'nazwaProcesu' },
        { text: 'Id zamówienia', value: 'identyfikatorZamowienia' },
        { text: 'Wiersz oferty', value: 'wierszOferty' },
        { text: 'Klient', value: 'klient' },
        { text: 'Numer Klienta', value: 'numerKlienta' },
        { text: 'Data końcowa', value: 'plannedEnd' },
        { text: 'Id wyrobu', value: 'identyfikatorWyrobu' },
        { text: 'Typ wyrobu', value: 'typWyrobu' },
        { text: 'Dn', value: 'dn' },
        { text: 'Ilosc wyrobow', value: 'iloscWyrobow' },
        { text: 'Wersja', value: 'wersja' },
        { text: 'Czas spędzony', value: 'czasSpedzony' },
        { text: 'Wykonanie', value: 'wykonaneStepy' }
      ],
      editMode: false,
      // dialogi
      currentItem: {},
      currentStepItem: {},
      liczbaPomiarow: 0,
      liczbaPomiarowNok: 0,
      reasonCodeId: null,
      dialogZamknijStep: false,
      reasons: [],
      reasonsLoading: true
    }
  },
  computed: {
    canStepBeClosed () {
      if (this.currentStepItem.stepNum !== 10) {
        return true
      }
      var countOfClosed = this.stepsData.filter(p => p.zakonczone).length
      var countOfAll = this.stepsData.length
      if (countOfClosed + 1 === countOfAll) {
        return true
      }
      return false
    },
    filterData () {
      return this.data.filter(p => this.selectedTypes.indexOf(p.typProcesu) >= 0)
    },
    formTitle: function () {
      if (this.editedIndex >= 0) {
        return this.editTitle
      } else {
        return this.newTitle
      }
    }
  },
  watch: {
    currentItem: {
      handler: function (newVal, oldval) {
        // eslint-disable-next-line no-debugger
        debugger
        if (newVal !== oldval) {
          this.getParts()
        }
      },
      deep: true
    },
    showDialog (val) {
      val || this.close()
    }
  },
  methods: {

    getStatusColor (number) {
      if (number === 0) {
        return 'green'
      }
      if (number === 1) {
        return 'orange'
      }
      if (number === 2) {
        return 'red'
      }
      return 'yellow lighten-5'
    },
    async registerTime (item, startstop) {
      item.blocked = true
      this.currentStepItem = item
      const this2 = this

      const model = {
        ProcessId: this.currentItem.processId,
        OfferLineId: this.currentItem.offerLineId,
        StepNum: this.currentStepItem.stepNum,
        StartStop: startstop
      }
      const link = startstop === 0 ? 'api/Production/TimeStart' : 'api/Production/TimeStop'
      try {
        var res = await v.axiosInstance.put(link, model)
        if (this2.currentStepItem.stepStarted === false) {
          this2.currentStepItem.stepStarted = true
        }
        if (startstop === 1) {
          this2.currentStepItem.timeSpendMe += res.data
          this2.currentStepItem.timeSpendOther += res.data
          this2.currentItem.czasSpedzony += res.data
        }
        item.blocked = false
        item.stepStartedByMe = !item.stepStartedByMe
      } catch (error) {
        item.blocked = false
      }
    },

    genModelSaveStep () {
      return {
        ProcessId: this.currentItem.processId,
        OfferLineId: this.currentItem.offerLineId,
        StepNum: this.currentStepItem.stepNum,
        LiczbaPomiarow: this.liczbaPomiarow,
        LiczbaPomiarowNok: this.liczbaPomiarowNok,
        ReasonCodeId: this.reasonCodeId
      }
    },
    async saveStep () {
      var model = this.genModelSaveStep()

      model.Zakonczenie = false
      await v.axiosInstance.post('api/Production/SaveStep', model)
      this.currentStepItem.liczbaPomiarow = this.liczbaPomiarow
      this.currentStepItem.liczbaPomiarowNok = this.liczbaPomiarowNok
      this.currentStepItem.reasonCodeId = this.reasonCodeId
      this.dialogZamknijStep = false
    },
    async saveCloseZamknijStep () {
      var model = this.genModelSaveStep()

      if (this.reasonCodeId === null) {
        this.$dialog.error({
          text: 'Musisz wybrać reason code, aby zamknąć step?',
          title: 'Problem'
        })
        return
      }
      model.Zakonczenie = true
      var res = await this.$dialog.confirm({
        text: 'Czy na pewno chcesz zamknąć ten step?',
        title: 'Uwaga'
      })
      if (res) {
        await v.axiosInstance.post('api/Production/SaveStep', model)
        this.currentStepItem.liczbaPomiarow = this.liczbaPomiarow
        this.currentStepItem.liczbaPomiarowNok = this.liczbaPomiarowNok
        this.currentStepItem.StepUsedBySomeoneElse = false
        this.currentStepItem.reasonCodeId = this.reasonCodeId
        this.currentItem.wykonaneStepy++
        this.currentStepItem.zakonczone = true
        this.dialogZamknijStep = false
      }
    },
    closeStep (item) {
      this.currentStepItem = item
      this.liczbaPomiarow = item.liczbaPomiarow
      this.liczbaPomiarowNok = item.liczbaPomiarowNok
      this.reasonCodeId = this.currentStepItem.reasonCodeId
      this.dialogZamknijStep = true
    },
    expandRow (item) {
      if (!item.isExpanded) {
        this.currentItem = item.item
        this.stepsLoading = true
        this.stepsData = []
        v.axiosInstance.get('api/Production/GetSteps?offerLineId=' + item.item.offerLineId + '&processId=' + item.item.processId)
          .then(Response => {
            this.stepsData = Response.data
            this.stepsData.forEach(element => {
              Vue.set(element, 'blocked', false)
            })
            this.stepsLoading = false
          })
      }

      item.expand(!item.isExpanded)
    },

    getData () {
      this.tableLoading = true
      v.axiosInstance.get(this.getAllApi)
        .then(Response => { this.data = Response.data; this.tableLoading = false })
    },
    getParts () {
      this.$http.get(this.getAllPartsApi + '?offerLineId=' + this.currentItem.offerLineId)
        .then(Response => {
          this.parts = Response.data
          this.parts.forEach(p => {
            p.showName = p.id + ', ' + p.nazwa
          })
          this.partsLoading = false
        })
    },
    getReasonCodes () {
      v.axiosInstance.get('api/ReasonCode/GetAllSimple').then(res => { this.reasons = res.data; this.reasonsLoading = false })
    }
  },
  created () {
    this.getData()
    this.getReasonCodes()
  },
  destroyed () {
  }
}
</script>

<style>
</style>
