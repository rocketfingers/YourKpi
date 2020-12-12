<template>
  <div>
    <v-dialog v-model="showNewOfferDialog" max-width="1800" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">{{ offerTitle }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showNewOfferDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-container grid-list-md>
          <v-layout row wrap justify-space-around>
            <v-flex xs11>
              <v-form ref="newOfferForm">
                <!-- <NewOffer
                  :offerTypes="offerTypes"
                ></NewOffer> -->
                <NewOffer
                  :currentOffer="currentOffer"
                  :projects="projects"
                  :editMode="editMode"
                  @editedOffer="editcurrentOfferRes"
                  :customers="customers"
                ></NewOffer>
              </v-form>
            </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-btn
            large
            color="blue darken-1"
            flat
            @click.native="showNewOfferDialog = false"
          >
            Anuluj
            <v-icon dark>cancel</v-icon>
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn
            flat
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
    <!-- <v-dialog v-model="showofferprojectsDialog" max-width="1200" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">Części produktu: {{ currentOffer.id }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showofferprojectsDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-container grid-list-md>
          <offerprojects
            :currentOffer="currentOffer"
            :readonly="true"
          ></offerprojects>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-spacer></v-spacer>
          <v-btn
            large
            color="blue darken-1"
            @click.native="showofferprojectsDialog = false"
          >
            Zamknij
            <v-icon dark>cancel</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog> -->
    <v-layout row wrap elevation-3>
      <v-flex xs12>
        <v-toolbar flat color="white">
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
          :headers="headers"
          :items="items"
          class="elevation-1"
          :expanded.sync="expanded"
          item-key="id"
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
                <td :key="index" v-else-if="header.value === 'actions'">
                  <v-layout justify-space-between>
                    <v-flex xs4>
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
                    <v-flex xs4>
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
                    :editMode="false"
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

export default {
  name: 'Produkty',
  components: {
    NewOffer: NewOffer,
    OfferLines: OfferLines
  },
  props: {},
  data () {
    return {
      // api
      getAllOffer: 'api/Offer/GetAll',
      addOfferApi: 'api/Offer/Create',
      editofferApi: 'api/Offer/Update',
      deleteofferApi: 'api/Offer/Delete',
      getAllofferTypesApi: 'api/offerTypes/GetAll',
      getAllProjectsApi: 'api/Project/GetAll',
      getAllCustomersApi: 'api/Customer/GetAll',
      expanded: [],

      headers: [
        { text: 'Rozwiń', value: 'expand' },
        { text: 'Id', value: 'id' },
        { text: 'Nazwa', value: 'name' },
        { text: 'Projekt Id', value: 'projectId' },
        { text: 'Status', value: 'status' },
        { text: 'Typ zamówienia', value: 'orderType' },
        { text: 'Data oferty', value: 'offerDate' },
        { text: 'Klient Id', value: 'clientId' },
        { text: 'Data zamówienia', value: 'orderDate' },
        { text: 'Planowane zakończenie', value: 'plannedEnd' },
        { text: 'Suma', value: 'sum' },
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
      customers: []
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      this.getOffer()
      this.getProjects()
      this.getCustomers()
    },
    getProjects () {
      this.$http.get(this.getAllProjectsApi)
        .then(Response => {
          this.projects = Response.data
          this.projects.forEach(p => {
            p.showName = p.id + ', ' + p.description
          })
        })
    },
    getCustomers () {
      this.$http.get(this.getAllCustomersApi)
        .then(Response => {
          this.customers = Response.data
          this.customers.forEach(p => {
            p.showName = p.id + ', ' + p.name + ', ' + p.nip
          })
        })
    },
    expandRow (item) {
      // eslint-disable-next-line no-debugger
      // debugger

      item.expand(!item.isExpanded)
    },
    getOffer () {
      var $this = this
      this.$http
        .get(this.getAllOffer)
        .then((Response) => {
          $this.items = Response.data
          $this.items.forEach(i => {
            i.offerDate = this.formatDateTime(i.offerDate)
            i.orderDate = this.formatDateTime(i.orderDate)
            i.plannedEnd = this.formatDateTime(i.plannedEnd)

            i.sum = 0
          })
          this.tableLoading = false
        })
        .catch((e) => {
          this.tableLoading = false
        })
    },
    // getofferTypes () {
    //   var $this = this
    //   this.$http
    //     .get(this.getAllofferTypesApi)
    //     .then((Response) => {
    //       $this.offerTypes = Response.data
    //     })
    //     .catch((e) => {})
    // },
    saveOfferAction () {
      if (!this.$refs.newOfferForm.validate()) {
        return
      }
      // eslint-disable-next-line no-debugger
      debugger

      if (this.editedIndex > 0) {
        this.editofferAction(this.currentOffer)
      } else {
        this.addOfferAction(this.currentOffer)
      }
      this.showNewOfferDialog = false
    },
    addOffer () {
      if (this.$refs.newOfferForm) {
        this.$refs.newOfferForm.reset()
      }
      this.editMode = false
      this.offerTitle = 'Dodaj ofertę'
      this.currentOffer = { }
      this.editedIndex = -1
      this.showNewOfferDialog = true
    },
    addOfferAction (offer) {
      this.$http
        .post(this.addOfferApi, offer)
        .then((Result) => {
          this.items.push(offer)
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
    editcurrentOfferRes (editedOffer) {
      this.currentOffer = editedOffer
    },
    editofferAction (offer) {
      this.$http
        .put(this.editofferApi, offer)
        .then((Result) => {
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
    async deleteoffer (offer, index) {
      var res = await this.$dialog.confirm({
        text: 'Czy na pewno chcesz usunąć?',
        title: 'Uwaga'
      })
      if (res) {
        var indexOfProd = this.items.indexOf(offer)
        this.$http.delete(this.deleteofferApi, {
          params: { id: offer.id }
        })
          .then(Result => {
            this.items.splice(indexOfProd, 1)
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
