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
                  :currentOffer="currentOffer"
                  :editMode="editMode"
                  @editedoffer="editcurrentOfferRes"
                  :offerTypes="offerTypes"
                  :parts="parts"
                ></NewOffer> -->
                <NewOffer :currentOffer="currentOffer"></NewOffer>
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
            @click.native="saveofferAction"
          >
            Zapisz
            <v-icon dark>save</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- <v-dialog v-model="showofferPartsDialog" max-width="1200" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">Części produktu: {{ currentOffer.id }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showofferPartsDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-container grid-list-md>
          <offerParts
            :currentOffer="currentOffer"
            :readonly="true"
          ></offerParts>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-spacer></v-spacer>
          <v-btn
            large
            color="blue darken-1"
            @click.native="showofferPartsDialog = false"
          >
            Zamknij
            <v-icon dark>cancel</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog> -->
    <v-container>
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
            item-key="id"
            :loading="tableLoading"
            :search="search"
          >
            <template slot="item" slot-scope="props">
              <tr>
                <template v-for="(header, index) in headers">
                  <td :key="index" v-if="header.value == 'actions'">
                    <v-layout>
                      <v-flex xs4>
                        <v-tooltip bottom>
                          <template v-slot:activator="{ on }">
                            <v-icon
                              v-on="on"
                              v-show="props.item.produktCzesci.length > 0"
                              @click="showOffer(props.item, index)"
                              color="blue"
                              class="mr-2"
                              >search</v-icon
                            >
                          </template>
                          <span>Podgląd części</span>
                        </v-tooltip>
                      </v-flex>
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
          </v-data-table>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import NewOffer from '../components/NewOffer'

export default {
  name: 'Produkty',
  components: {
    NewOffer: NewOffer
    // offerParts: offerParts
  },
  props: {},
  data () {
    return {
      // api
      getAllOffer: 'api/Offer/GetAll',
      addofferApi: 'api/offers/Create',
      editofferApi: 'api/offers/Update',
      deleteofferApi: 'api/offers/Delete',
      getAllofferTypesApi: 'api/offerTypes/GetAll',
      getAllPartsApi: 'api/Parts/GetAll',

      headers: [
        { text: 'Id', value: 'id' },
        { text: 'Nazwa', value: 'name' },
        { text: 'Projekt Id', value: 'projectId' },
        { text: 'Status', value: 'status' },
        { text: 'Typ zamówienia', value: 'orderType' },
        { text: 'Data oferty', value: 'offerDate' },
        { text: 'Klient Id', value: 'clientId' },
        { text: 'Data zamówienia', value: 'orderDate' },
        { text: 'Planowane zakończenie', value: 'plannedEnd' },
        { text: 'Akcje', value: 'actions' }
      ],
      items: [],
      search: '',
      showNewOfferDialog: false,
      showofferPartsDialog: false,
      tableLoading: false,
      offerTitle: 'Dodaj ofertę',
      currentOffer: { produktCzesci: [] },
      editedIndex: -1,
      offerTypes: [],
      editMode: false,
      parts: []
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      this.getOffer()
      // this.getParts()
      // this.getofferTypes()
    },
    // getParts () {
    //   this.$http.get(this.getAllPartsApi)
    //     .then(Response => {
    //       this.parts = Response.data
    //     })
    // },
    getOffer () {
      var $this = this
      this.$http
        .get(this.getAllOffer)
        .then((Response) => {
          $this.items = Response.data
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
    saveofferAction () {
      if (!this.$refs.newOfferForm.validate()) {
        return
      }
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
        .post(this.addofferApi, offer)
        .then((Result) => {
          this.items.push(offer)
        })
        .catch((e) => {
        })
    },
    showOffer (offer, index) {
      this.currentOffer = offer
      this.editedIndex = index
      this.showofferPartsDialog = true
    },
    editOffer (offer, index) {
      this.offerTitle = 'Edytuj ofertę ' + offer.name
      this.editMode = true
      this.editedIndex = index
      this.showNewOfferDialog = true
      this.currentOffer = offer
    },
    editcurrentOfferRes (editedoffer) {
      this.currentOffer = editedoffer
    },
    editofferAction (offer) {
      this.$http
        .put(this.editofferApi, offer)
        .then((Result) => {
        })
        .catch((e) => {})
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
