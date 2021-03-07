<template>
  <div>
    <v-dialog v-model="showAvailabilityDialog" max-width="1800" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">{{ offerAvailabilityTitle }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showAvailabilityDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-layout row wrap justify-space-around class="ma-4">
          <v-flex xs11>
            <v-form ref="newOfferForm">
              <Availability
                :offerAvailability="offerAvailability"
              ></Availability>

              <!-- <NewOffer
                :currentOffer="currentOffer"
                :projects="projects"
                :editMode="editMode"
                @editedOffer="editcurrentOfferRes"
                :customers="customers"
                :products="products"
                :processes="processes"
              ></NewOffer> -->
            </v-form>
          </v-flex>
        </v-layout>
        <v-card-actions class="blue lighten-5">
          <v-spacer></v-spacer>
          <v-btn
            large
            color="blue darken-1"
            text
            @click.native="showAvailabilityDialog = false"
          >
            Zamknij
            <v-icon dark>cancel</v-icon>
          </v-btn>
          <!-- <v-spacer></v-spacer>
          <v-btn
            text
            large
            color="blue darken-1"
            @click.native="saveOfferAction"
          >
            Zapisz
            <v-icon dark>save</v-icon>
          </v-btn> -->
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-dialog v-model="showNewOfferDialog" max-width="1800" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">{{ offerTitle }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showNewOfferDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-layout row wrap justify-space-around class="ma-4">
          <v-flex xs11>
            <v-form ref="newOfferForm">
              <NewOffer
                :currentOffer="currentOffer"
                :projects="projects"
                :editMode="editMode"
                @editedOffer="editcurrentOfferRes"
                :customers="customers"
                :products="products"
                :processes="processes"
              ></NewOffer>
            </v-form>
          </v-flex>
        </v-layout>
        <v-card-actions class="blue lighten-5">
          <v-btn
            large
            color="blue darken-1"
            text
            @click.native="showNewOfferDialog = false"
          >
            Anuluj
            <v-icon dark>cancel</v-icon>
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn
            text
            large
            color="blue darken-1"
            @click.native="saveOfferAction"
          >
            Zapisz
            <v-icon dark>save</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-layout row wrap elevation-2>
      <v-flex xs12>
        <v-toolbar text color="white">
          <v-toolbar-title>Oferta</v-toolbar-title>
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
          <v-btn color="primary" dark class="mb-2" @click="addOffer()"
            >Nowy</v-btn
          >
        </v-toolbar>
      </v-flex>
      <v-flex xs12>
        <v-data-table
          mt-2
          :headers="headers"
          :items="items"
          class="elevation-2"
          :expanded.sync="expanded"
          item-key="id"
          :loading="tableLoading"
          :search="search"
        >
          <template slot="item" slot-scope="props">
            <tr :class="props.isExpanded ? 'primary lighten-5' : 'white'">
              <template v-for="(header, index) in headers">
                <td :key="index" v-if="header.value === 'clientsId'">
                  {{ props.item.clients.name }}
                </td>
                <td :key="index" v-else-if="header.value === 'projectsId'">
                  {{
                    props.item.projectsId === "NULL"
                      ? ""
                      : props.item.projectsId
                  }}
                </td>
                <td :key="index" v-else-if="header.value === 'expand'">
                  <v-icon @click="expandRow(props)" v-show="!props.isExpanded"
                    >fa-arrow-down</v-icon
                  >
                  <v-icon @click="expandRow(props)" v-show="props.isExpanded"
                    >fa-arrow-up</v-icon
                  >
                </td>
                <td :key="index" v-else-if="header.value === 'processesNo'">
                  {{ calculateProcesses(props.item) }}
                </td>
                <td :key="index" v-else-if="header.value === 'actions'">
                  <v-layout justify-space-between>
                    <v-flex xs3>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            v-on="on"
                            @click="showAvailability(props.item, index)"
                            color="green"
                            class="mr-2"
                            >fa-tasks</v-icon
                          >
                        </template>
                        <span>Dostępność</span>
                      </v-tooltip>
                    </v-flex>
                    <v-spacer></v-spacer>
                    <v-flex xs3>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            v-on="on"
                            @click="editOffer(props.item, index)"
                            color="green"
                            class="mr-2"
                            >edit</v-icon
                          >
                        </template>
                        <span>Edycja</span>
                      </v-tooltip>
                    </v-flex>
                    <v-spacer></v-spacer>
                    <v-flex xs3>
                      <v-layout row wrap class="justify-center layout">
                        <v-tooltip bottom>
                          <template v-slot:activator="{ on }">
                            <v-icon
                              v-on="on"
                              color="green"
                              @click="downloadPdf(props.item)"
                              >far fa-file-pdf</v-icon
                            >
                          </template>
                          <span>Pobierz pdf</span>
                        </v-tooltip>
                      </v-layout>
                    </v-flex>
                    <v-spacer></v-spacer>
                    <v-flex xs3>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            v-on="on"
                            @click="deleteOffer(props.item, index)"
                            color="red lighten-1"
                            >delete</v-icon
                          ></template
                        >
                        <span>Usuń</span>
                      </v-tooltip>
                    </v-flex>
                  </v-layout>
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
                  <OfferLines
                    :currentOffer="item"
                    :processes="processes"
                    :readonly="true"
                  ></OfferLines>
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
import NewOffer from '../components/NewOffer'
import OfferLines from '../components/OfferLines'
import Availability from '../components/Availability'

export default {
  name: 'Produkty',
  components: {
    NewOffer: NewOffer,
    OfferLines: OfferLines,
    Availability: Availability
  },
  props: {},
  data () {
    return {
      // api
      getAllOffer: 'api/Offer/GetAll',
      addOfferApi: 'api/Offer/Create',
      editOfferApi: 'api/Offer/Update',
      deleteOfferApi: 'api/Offer/Delete',
      getAllofferTypesApi: 'api/offerTypes/GetAll',
      getAllProjectsApi: 'api/Project/GetAll',
      getAllCustomersApi: 'api/Customer/GetAll',
      getAllProductsApi: 'api/Products/GetAll',
      getAllProcesses: 'api/Process/GetAll',
      getOfferFile: 'api/Offer/GetPdf/',
      getOfferAvailability: 'api/Offer/GetOfferAvailability/',

      expanded: [],

      headers: [
        { text: 'Rozwiń', value: 'expand' },
        { text: 'Id', value: 'id' },
        { text: 'Nazwa', value: 'name' },
        { text: 'Projekt Id', value: 'projectsId' },
        { text: 'Status', value: 'status' },
        { text: 'Typ zamówienia', value: 'orderType' },
        { text: 'Data oferty', value: 'offerDate' },
        { text: 'Klient', value: 'clientsId' },
        { text: 'Data zamówienia', value: 'orderDate' },
        { text: 'Planowane zakończenie', value: 'plannedEnd' },
        { text: 'Suma w dniu oferty', value: 'sumInOfferDay' },
        { text: 'Suma sprzedaży', value: 'sumSale' },

        { text: 'Liczba procesów', value: 'processesNo', visible: true },
        { text: 'Akcje', value: 'actions' }
      ],
      items: [],
      search: '',
      showNewOfferDialog: false,
      showofferprojectsDialog: false,
      tableLoading: false,
      offerTitle: 'Dodaj ofertę',
      currentOffer: { produktCzesci: [] },
      editedIndex: -1,
      offerTypes: [],
      editMode: false,
      projects: [],
      customers: [],
      products: [],
      processes: [],
      offerAvailabilityTitle: 'Dostępność',
      showAvailabilityDialog: false,
      offerAvailability: {}
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      this.getProjects()
    },
    downloadPdf (item) {
      if (item.id) {
        this.openPdf(this.getOfferFile + item.id, 'Oferta')
      }
    },
    openPdf (url, windowName, windowParams) {
      if (url) {
        if (!windowName) {
          windowName = ''
        }
        if (!windowParams) {
          windowName =
            'toolbar=yes,scrollbars=yes,resizable=yes,top=0,left=0,width=960,height=500'
        }
        this.$http({
          url,
          method: 'GET',
          responseType: 'blob'
        })
          .then(response => {
            if (response.data) {
              if (response.data.size > 0) {
                var fileURL = window.URL.createObjectURL(
                  new Blob([response.data], { type: 'application/pdf' })
                )
                var win = window.open(fileURL, windowName, windowParams)

                if (window.focus) {
                  win.focus()
                }
              } else {
                this.showWarning('Plik nie istnieje')
              }
            } else {
              this.showWarning('Plik nie istnieje')
            }
          })
          .catch(e => {
            this.showWarning('Plik nie istnieje')
          })
      }
    },
    showWarning (msg) {
      this.$dialog.confirm({
        text: msg,
        title: 'Uwaga'
      })
    },
    getProducts () {
      this.$http.get(this.getAllProductsApi)
        .then(Response => {
          this.products = Response.data
          this.products.forEach(p => {
            p.showName = p.id + ', ' + p.name
          })
          this.getOffer()
        })
    },
    getProjects () {
      this.$http.get(this.getAllProjectsApi)
        .then(Response => {
          this.projects = Response.data
          this.projects.forEach(p => {
            p.showName = p.id + ', ' + p.description
          })
          this.getCustomers()
        })
    },
    getCustomers () {
      this.$http.get(this.getAllCustomersApi)
        .then(Response => {
          this.customers = Response.data
          this.customers.forEach(p => {
            p.showName = p.id + ', ' + p.name + ', ' + p.nip
          })
          this.getProducts()
        })
    },
    expandRow (item) {
      item.expand(!item.isExpanded)
    },
    getOffer () {
      var $this = this
      this.$http
        .get(this.getAllOffer)
        .then((Response) => {
          $this.items = Response.data
          $this.items.forEach(i => {
            i.sumInOfferDay = 0
            i.sumSale = 0

            i.offerLines.forEach(p => {
              i.sumInOfferDay = i.sumInOfferDay + (p.priceInOfferDay * p.quantity)
              i.sumSale = i.sumSale + (p.salesPrice * p.quantity)
            })

            i.sumInOfferDay = i.sumInOfferDay.toFixed(2)
            i.sumSale = i.sumSale.toFixed(2)

            i.offerDate = this.formatDateTime(i.offerDate)
            i.orderDate = this.formatDateTime(i.orderDate)
            i.plannedEnd = this.formatDateTime(i.plannedEnd)
            if (i.projectsId) {
              i.project = this.projects.find(p => p.id === i.projectsId)
            }
            if (i.clientsId) {
              i.client = this.customers.find(c => c.id === i.clientsId)
            }
            i.sum = ''
            if (i.offerLines) {
              i.offerLines.forEach(p => {
                p.tempId = p.id
              })
            }
          })
          this.getProcesses()
        })
        .catch((e) => {
          this.tableLoading = false
        })
    },
    getProcesses () {
      var $this = this
      this.$http
        .get(this.getAllProcesses)
        .then((Response) => {
          $this.processes = Response.data
          $this.processes.forEach(p => {
            p.showName = p.id + ', ' + p.nazwaProcesu
          })
          this.tableLoading = false
        })
        .catch((e) => {
          this.tableLoading = false
        })
    },
    calculateProcesses (item) {
      var offerProcessesNo = 0
      // eslint-disable-next-line no-debugger
      // debugger
      if (item.offerLines) {
        item.offerLines.forEach(p => {
          offerProcessesNo = p.offerLineProcess.length + offerProcessesNo
        })
      }

      return item.offerProcess.length + ' + ' + offerProcessesNo
    },
    beforeSaveValidation () {
      // eslint-disable-next-line no-debugger
      // debuggers
      if (!this.$refs.newOfferForm.validate()) {
        return false
      }
      if (!this.currentOffer.offerLines) {
        this.$dialog.confirm({
          text: 'Aby zapisać ofertę dodaj pozycje do Offer lines!',
          title: 'Uwaga'
        })
        return false
      }
      if (this.currentOffer.offerLines.length === 0) {
        this.$dialog.confirm({
          text: 'Aby zapisać ofertę dodaj pozycje do Offer lines!',
          title: 'Uwaga'
        })
        return false
      }
      if (!this.currentOffer.offerProcess) {
        this.$dialog.confirm({
          text: 'Aby zapisać ofertę dodaj procesy!',
          title: 'Uwaga'
        })
        return false
      }
      if (this.currentOffer.offerProcess.length <= 0) {
        this.$dialog.confirm({
          text: 'Aby zapisać ofertę dodaj procesy!',
          title: 'Uwaga'
        })
        return false
      }
      if (this.currentOffer.offerProcess.filter(op => !op.czasWymagany || op.czasWymagany === 0 || op.czasWymagany < 1 || op.czasWymagany > 10000).length > 0) {
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
        this.$dialog.confirm({
          text: 'Aby zapisać ofertę dodaj procesy (w offer lines)!',
          title: 'Uwaga'
        })
        return false
      }
      return true
    },
    saveOfferAction () {
      if (!this.beforeSaveValidation()) return
      this.currentOffer.clientsId = this.currentOffer.client.id
      this.currentOffer.projectsId = this.currentOffer.project.id

      if (this.editedIndex > 0) {
        this.editofferAction(this.currentOffer)
      } else {
        this.addOfferAction(this.currentOffer)
      }
      this.showNewOfferDialog = false
    },
    addOffer () {
      // if (this.$refs.newOfferForm) { //powoduje blad w dodawaniu zagniezdzonych obiektow, vue jakby traci referencje
      //   this.$refs.newOfferForm.reset()
      // }
      this.editMode = false
      this.offerTitle = 'Dodaj ofertę'
      this.currentOffer = { offerProcess: [], status: 'otwarte' }
      this.editedIndex = -1
      this.showNewOfferDialog = true
    },
    addOfferAction (offer) {
      var $this = this
      this.$http
        .post(this.addOfferApi, offer)
        .then((Result) => {
          offer.id = Result.data.id
          $this.items.unshift(offer)
          $this.initialise()
        })
        .catch((e) => {
        })
    },
    showOffer (offer, index) {
      this.currentOffer = offer
      this.editedIndex = index
      this.showofferprojectsDialog = true
    },
    editOffer (offer, index) {
      this.offerTitle = 'Edytuj ofertę ' + offer.name
      this.editMode = true
      this.editedIndex = index
      this.showNewOfferDialog = true
      this.currentOffer = offer
    },
    showAvailability (offer, index) {
      this.offerAvailabilityTitle = 'Dostępność dla oferty: ' + offer.name
      this.currentOffer = offer
      this.getAvailability(offer.id)
      this.showAvailabilityDialog = true

      this.editedIndex = index
    },
    getAvailability (offerId) {
      var $this = this
      this.$http
        .get(this.getOfferAvailability + offerId)
        .then((Response) => {
          $this.offerAvailability = Response.data
          $this.offerAvailability.availabilityProducts.forEach(p => {
            p.available = false
            if (!p.availableProducts) {
              p.availableProducts = 'brak pozycji w magazynie'
            } else if (p.availableProducts > 0) {
              p.available = true
            }
            p.parts.forEach(part => {
              part.available = false
              if (!part.availableParts) {
                part.availableParts = 'brak pozycji w magazynie'
              } else if (part.availableParts > 0) {
                part.available = true
              }
            })
          })
        })
        .catch((e) => {
        })
    },
    editcurrentOfferRes (editedOffer) {
      this.currentOffer = editedOffer
    },
    editofferAction (offer) {
      this.$http
        .put(this.editOfferApi, offer)
        .then((Result) => {
          this.initialise()
        })
        .catch((e) => {})
    },
    formatDateTime (date) {
      if (date) {
        if (date instanceof Date) {
          return (
            date.getFullYear() +
            '-' +
            (date.getMonth() + 1) +
            '-' +
            date.getDate()
          )
        }
        if (date.toString().includes('T')) {
          return date.toString().split('T')[0]
        }

        return date
      }
    },
    async deleteOffer (offer, index) {
      var res = await this.$dialog.confirm({
        text: 'Czy na pewno chcesz usunąć?',
        title: 'Uwaga'
      })
      if (res) {
        var indexOfOffer = this.items.indexOf(offer)
        this.$http.delete(this.deleteOfferApi, {
          params: { id: offer.id }
        })
          .then(Result => {
            this.items.splice(indexOfOffer, 1)
          }
          )
      }
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
