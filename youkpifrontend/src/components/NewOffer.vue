<template>
  <div>
    <v-dialog v-model="showNewCustomerDialog" max-width="1800" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">Dodaj klienta</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showNewCustomerDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-container grid-list-md>
          <v-layout row wrap justify-space-around>
            <v-flex xs11>
              <v-form ref="newForm">
                <NewCustomer
                  :currentItem="currentCustomerItem"
                  :editMode="editMode"
                  @editedCustomer="editcurrentItemRes"
                ></NewCustomer>
              </v-form>
            </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-btn
            large
            color="blue darken-1"
            flat
            @click.native="showNewCustomerDialog = false"
          >
            Anuluj
            <v-icon dark>cancel</v-icon>
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn
            flat
            large
            color="blue darken-1"
            @click.native="addCustomerAction"
          >
            Zapisz
            <v-icon dark>save</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

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
                :filteredProcesses="filteredProcesses"
                :parentItem="currentOffer"
                @editedOfferLine="editOfferProcesses"
                :showRequiredTime="true"
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
    <v-layout row wrap justify-space-around>
      <v-flex xs5>
        <v-layout row wrap>
          <!-- <v-flex xs12>
            <v-text-field
              outlined
              color
              label="Id"
              :disabled="editMode"
              required
              :rules="[requiredRule]"
              v-model="currentOffer.id"
            ></v-text-field>
          </v-flex> -->
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Nazwa"
              v-model="currentOffer.name"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-autocomplete
              outlined
              color
              :rules="[requiredRule]"
              label="Projekt Id"
              :items="projects"
              item-text="showName"
              return-object
              required
              v-model="currentOffer.project"
            ></v-autocomplete>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              :rules="[requiredRule]"
              color
              label="Typ zamówienia"
              v-model.number="currentOffer.orderType"
            ></v-text-field>
          </v-flex>
        </v-layout>
      </v-flex>
      <v-flex xs1> </v-flex>
      <v-flex xs5>
        <v-layout row wrap>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Data oferty"
              type="date"
              v-model="currentOffer.offerDate"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-autocomplete
              outlined
              color
              :rules="[requiredRule]"
              label="Klient"
              :items="customers"
              item-text="showName"
              return-object
              required
              v-model="currentOffer.client"
            >
              <template slot="prepend">
                <v-tooltip bottom>
                  <template v-slot:activator="{ on }">
                    <v-icon v-on="on" @click="addCustomer()" color="green"
                      >fa-user-plus</v-icon
                    >
                  </template>
                  <span>Dodaj nowego klienta</span>
                </v-tooltip>
              </template>
            </v-autocomplete>
          </v-flex>
        </v-layout>
        <v-layout row wrap>
          <v-flex xs5>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              type="date"
              label="Data zamówienia"
              v-model="currentOffer.orderDate"
            >
            </v-text-field>
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs5>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              type="date"
              label="Planowane zakończenie"
              v-model="currentOffer.plannedEnd"
            ></v-text-field>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
    <v-layout row wrap justify-space-around>
      <v-flex xs11>
        <v-layout row wrap justify-space-around>
          <v-flex xs4>
            <v-select
              label="Status"
              outlined
              color
              v-model="currentOffer.status"
              :readonly="true"
              :items="statuses"
            ></v-select>
          </v-flex>
          <v-spacer></v-spacer>

          <v-flex xs3 md2>
            <v-btn
              color="primary"
              :disabled="disableChangeStatusBtn"
              dark
              class="mb-2"
              @click="checkStatus()"
              v-if="!readonly && editMode"
              >{{ btnChangeStatusText }}</v-btn
            >
          </v-flex>
          <v-spacer></v-spacer>

          <v-flex xs3 md2>
            <v-btn
              v-show="currentOffer.status === 'otwarte'"
              color="primary"
              dark
              class="mb-2"
              @click="selectOfferProcesses()"
              v-if="!readonly"
              >Wybierz procesy</v-btn
            >
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs3 md2>
            <!-- v-show="currentOffer.status" -->

            <v-checkbox
              v-model="showOfferProcesses"
              label="Pokaż procesy"
            ></v-checkbox>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
    <v-layout row wrap justify-space-around mb-2 v-show="showOfferProcesses">
      <v-flex xs12>
        <v-card>
          <v-data-table
            :headers="expandedHeaders"
            :items="selectedOfferLineProcesses"
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
                  {{ props.item.czasWymagany }}
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
    <v-layout>
      <v-flex xs12>
        <OfferLines
          :currentOffer="currentOffer"
          :products="products"
          :processes="processes"
        ></OfferLines>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import OfferLines from '../components/OfferLines'
import ProcessSelector from '../components/ProcessSelector'
import NewCustomer from '../components/NewCustomer.vue'

export default {
  name: 'NewProduct',
  components: {
    OfferLines: OfferLines,
    ProcessSelector: ProcessSelector,
    NewCustomer: NewCustomer
  },
  props: {
    currentOffer: Object,
    editMode: Boolean,
    projects: Array,
    readonly: Boolean,
    customers: Array,
    products: Array,
    processes: Array
  },
  data () {
    return {
      // api
      addCustomerApi: 'api/Customer/Create',

      showProcessSelectorDialog: false,
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadź wartość dodatnią'
        return true
      },
      statuses: ['otwarte', 'w trakcie', 'zakończony'],
      processesSelectorTitle: '',
      showOfferProcesses: false,

      expandedHeaders: [
        // { text: 'Id', value: 'id', visible: true },
        { text: 'Proces id', value: 'id', visible: true },
        { text: 'Nazwa grupy procesu', value: 'nazwGrupyProcesu', visible: true },
        { text: 'Business area', value: 'businessArea', visible: true },
        { text: 'Nazwa procesu', value: 'nazwaProcesu', visible: true },
        { text: 'Czas wymagany', value: 'czasWymagany', visible: true },
        { text: 'Typ zlecenia', value: 'typZlecenia', visible: true }
      ],
      showNewCustomerDialog: false,
      currentCustomerItem: {}
    }
  },
  computed: {
    disableChangeStatusBtn () {
      if (this.currentOffer.status === 'zakończony') {
        return true
      }

      return false
    },
    btnChangeStatusText () {
      var res = 'Rozpocznij'
      switch (this.currentOffer.status) {
        case 'otwarte':
          return 'Przejdź do realizacji'
        case 'w trakcie':
          return 'Zakończ'
        case 'zakończony':
          return 'Zakończ'
      }
      return res
    },
    filteredProcesses () {
      var filProcesses = this.processes.filter(p => {
        if (p.typZlecenia.toUpperCase().trim().includes('O') || p.typZlecenia === '0') {
          return true
        }
        return false
      })

      // offerLineProcess
      return filProcesses
    },
    selectedOfferLineProcesses () {
      var selPro = []
      var $this = this
      if (this.currentOffer.offerProcess) {
        this.currentOffer.offerProcess.forEach(p => {
          var item = $this.processes.find(pr => pr.id === p.processId)
          item.czasWymagany = p.czasWymagany
          selPro.push(item)
        })
      }

      return selPro
    }
  },
  watch: {
    currentOffer (val) {
      this.$emit('editedOffer', this.currentOffer)
    }
  },
  methods: {
    addCustomer () {
      this.currentCustomerItem = { }
      this.showNewCustomerDialog = true
    },
    addCustomerAction () {
      this.$http
        .post(this.addCustomerApi, this.currentCustomerItem)
        .then((Result) => {
          var customer = Result.data
          customer.showName = customer.id + ', ' + customer.name + ', ' + customer.nip
          this.customers.push(customer)
          this.showNewCustomerDialog = false
        })
        .catch((e) => {
          this.showNewCustomerDialog = false
        })
    },
    editcurrentItemRes () {

    },
    beforeSaveValidation () {
      // eslint-disable-next-line no-debugger
      debugger
      if (!this.currentOffer.offerLines) {
        this.$dialog.warning({
          text: 'Aby zapisać ofertę dodaj pozycje do Offer lines!',
          title: 'Uwaga'
        })
        return false
      }
      if (this.currentOffer.offerLines.length === 0) {
        this.$dialog.warning({
          text: 'Aby zapisać ofertę dodaj pozycje do Offer lines!',
          title: 'Uwaga'
        })
        return false
      }
      if (!this.currentOffer.offerProcess) {
        this.$dialog.warning({
          text: 'Aby zapisać ofertę dodaj procesy!',
          title: 'Uwaga'
        })
        return false
      }
      if (this.currentOffer.offerProcess.length <= 0) {
        this.$dialog.warning({
          text: 'Aby zapisać ofertę dodaj procesy!',
          title: 'Uwaga'
        })
        return false
      }

      alert((this.currentOffer.offerProcess.filter(op => op.czasWymagany < 1 || op.czasWymagany > 10000).length))
      if (this.currentOffer.offerProcess.filter(op => op.czasWymagany < 1 || op.czasWymagany > 10000).length > 0) {
        this.$dialog.warning({
          text: 'Upewnij się, że czasy procesów oferty mieszczą się w wymaganych zakresach!',
          title: 'Uwaga'
        })
        return false
      }
      var emptyOfferLines = this.currentOffer.offerLines.filter(p =>
        p.offerLineProcess.length === 0
      )

      if (emptyOfferLines.length > 0) {
        this.$dialog.warning({
          text: 'Aby zapisać ofertę dodaj procesy (w offer lines)!',
          title: 'Uwaga'
        })
        return false
      }
      return true
    },
    checkStatus () {
      switch (this.currentOffer.status) {
        case 'otwarte':
          if (this.beforeSaveValidation()) {
            this.currentOffer.status = 'w trakcie'

            return
          } break

        case 'w trakcie':
          this.currentOffer.status = 'zakończony'
          return
        case 'zakończony':
          return
      }
      if (this.currentOffer.offerProcess.length <= 0) {
        var res2 = this.$dialog.warning({
          text: 'Aby otworzyć ofertę należy wybrać procesy dla ofery',
          title: 'Uwaga'
        })
        if (res2) {
        }
      } else {
        this.currentOffer.status = 'otwarte'
      }
    },
    saveOfferProcesses () {
      this.showProcessSelectorDialog = false
    },
    selectOfferProcesses () {
      this.processesSelectorTitle = 'Wybierz procesy (Oferta: ' + this.currentOffer.name + ')'
      this.showProcessSelectorDialog = true
    },
    editOfferProcesses (offer, selectedProcesses) {
      if (this.currentOffer) {
        if (this.currentOffer.id === offer.id) {
          this.currentOffer.offerProcess = []
          selectedProcesses.forEach(sp => {
            var item = {
              offerId: this.currentOffer.id,
              processId: sp.id,
              czasWymagany: sp.czasWymagany
            }
            this.currentOffer.offerProcess.push(item)
          })
        }
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
