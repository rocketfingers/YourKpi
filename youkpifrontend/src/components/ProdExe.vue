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
                                      :disabled="props.item.zakonczone"
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
                                      :disabled="props.item.zakonczone"
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
      // Settings
      showDialog: false,
      data: [],
      stepsData: [],
      stepsLoading: false,
      stepsHeaders: [
        { text: 'Step ', value: 'stepNum' },
        { text: 'Nazwa stepu', value: 'stepName' },
        { text: 'Sekwencja', value: 'sekwencja' },
        { text: 'Liczba pomiarów', value: 'liczbaPomiarow' },
        { text: 'Liczba pomiarów Nok', value: 'liczbaPomiarowNok' },
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
      dialogZamknijStep: false
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
    genModel () {
      return {
        ProcessId: this.currentItem.processId,
        OfferLineId: this.currentItem.offerLineId,
        StepNum: this.currentStepItem.stepNum,
        LiczbaPomiarow: this.liczbaPomiarow,
        LiczbaPomiarowNok: this.liczbaPomiarowNok
      }
    },
    async saveStep () {
      var model = this.genModel()
      // eslint-disable-next-line no-debugger
      debugger
      model.Zakonczenie = false
      await v.axiosInstance.post('api/Production/SaveStep', model)
      this.currentStepItem.liczbaPomiarow = this.liczbaPomiarow
      this.currentStepItem.liczbaPomiarowNok = this.liczbaPomiarowNok
      this.dialogZamknijStep = false
    },
    async saveCloseZamknijStep () {
      var model = this.genModel()
      model.Zakonczenie = true
      var res = await this.$dialog.confirm({
        text: 'Czy na pewno chcesz zamknąć ten step?',
        title: 'Uwaga'
      })
      if (res) {
        await v.axiosInstance.post('api/Production/SaveStep', model)
        this.currentStepItem.liczbaPomiarow = this.liczbaPomiarow
        this.currentStepItem.liczbaPomiarowNok = this.liczbaPomiarowNok
        this.currentItem.wykonaneStepy++
        this.currentStepItem.zakonczone = true
        this.dialogZamknijStep = false
      }
    },
    closeStep (item) {
      this.currentStepItem = item
      this.liczbaPomiarow = item.liczbaPomiarow
      this.liczbaPomiarowNok = item.liczbaPomiarowNok
      this.dialogZamknijStep = true
    },
    expandRow (item) {
      if (!item.isExpanded) {
        this.currentItem = item.item
        this.stepsLoading = true
        this.stepsData = []
        v.axiosInstance.get('api/Production/GetSteps?offerLineId=' + item.item.offerLineId + '&processId=' + item.item.processId)
          .then(Response => { this.stepsData = Response.data; this.stepsLoading = false })
      }

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
