<template>
  <div>
    <v-dialog v-model="showNewDialog" max-width="1800" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">{{ formTitle }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showNewDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-container grid-list-md>
          <v-layout row wrap justify-space-around>
            <v-flex xs11>
              <v-form ref="newForm">
                <NewCommodity
                  :currentItem="currentItem"
                  :editMode="editMode"
                  :customers="customers"
                  @editedProduct="editcurrentItemRes"
                ></NewCommodity>
              </v-form>
            </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-btn
            large
            color="blue darken-1"
            flat
            @click.native="showNewDialog = false"
          >
            Anuluj
            <v-icon dark>cancel</v-icon>
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn flat large color="blue darken-1" @click.native="saveAction">
            Zapisz
            <v-icon dark>save</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-layout row wrap elevation-3>
      <v-flex xs12>
        <v-toolbar flat color="white">
          <v-toolbar-title>Towary</v-toolbar-title>
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
          <v-btn color="primary" dark class="mb-2" @click="add()">Nowy</v-btn>
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
                            @click="edit(props.item, index)"
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
                            @click="deleteItem(props.item, index)"
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
  </div>
</template>

<script>
import NewCommodity from '../components/NewCommodity'

export default {
  name: 'Towary',
  components: {
    NewCommodity: NewCommodity
  },
  props: {},
  data () {
    return {
      // api
      getAll: 'api/Commodity/GetAll',
      addApi: 'api/Commodity/Create',
      editApi: 'api/Commodity/Update',
      getAllCustomersApi: 'api/Customer/GetAll',
      deleteApi: 'api/Commodity/Delete',
      getAllContractorsSimple: 'api/Contractor/GetAllSimpleView',
      getAllLocationsSimple: 'api/Loaction/GetAllSimpleView',
      customers: [],
      headers: [
        { text: 'Id', value: 'id' },
        { text: 'Nazwa', value: 'nazwa' },
        { text: 'Cena zakupu', value: 'cenaZakupu' },
        { text: 'Waluta zakupu', value: 'walutaZakupu' },
        { text: 'Cena sprzedaży', value: 'cenaSprzedazy' },
        { text: 'Waluta sprzedaży', value: 'walutaSprzedazy' },
        { text: 'Akcje', value: 'actions' }
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
      this.getCustomers()
    },
    getItems () {
      var $this = this
      this.$http
        .get(this.getAll)
        .then((Response) => {
          $this.items = Response.data
          $this.items.forEach(p => {
            p.dataPrzyjeciaShow = this.formatDateTimeYYYYMMDD(p.dataPrzyjecia)

            p.kontrahent = $this.contractors.find(c => c.id === p.kontrahentId.toString())
            if (p.kontrahent) {
              p.kontrahentId = p.kontrahent.id
              p.kontrahentName = p.kontrahent.name
            }
            p.lokacjaName = ''
            var lokacja = $this.locations.find(l => l.id === p.lokacjaId?.toString())

            if (lokacja) {
              p.lokacjaName = lokacja.name
              p.lokacjaId = lokacja.id
            }
          })

          this.tableLoading = false
        })
        .catch((e) => {
          this.tableLoading = false
        })
    },

    saveAction () {
      if (!this.$refs.newForm.validate()) {
        return
      }
      if (this.editedIndex > 0) {
        this.editAction(this.currentItem)
      } else {
        this.addAction(this.currentItem)
      }
      this.showNewDialog = false
    },
    add () {
      if (this.$refs.newForm) {
        this.$refs.newForm.reset()
      }
      this.editMode = false
      this.formTitle = 'Dodaj'
      this.currentItem = { }
      this.editedIndex = -1
      this.showNewDialog = true
    },
    addAction (item) {
      this.$http
        .post(this.addApi, item)
        .then((Result) => {
          this.items.push(Result.data)
          this.initialise()
        })
        .catch((e) => {
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
    edit (item, index) {
      this.formTitle = 'Edytuj ' + item.idDifferenceReasonCode
      this.editMode = true
      this.editedIndex = index
      this.showNewDialog = true
      this.currentItem = item
    },
    editCurrentProductRes (editedProduct) {
      this.currentItem = editedProduct
    },
    editAction (item) {
      this.$http
        .put(this.editApi, item)
        .then((Result) => {
          this.initialise()
        })
        .catch((e) => {})
    },
    async deleteItem (item, index) {
      var res = await this.$dialog.confirm({
        text: 'Czy na pewno chcesz usunąć?',
        title: 'Uwaga'
      })
      if (res) {
        var indexOfItem = this.items.indexOf(item)
        this.$http.delete(this.deleteApi, {
          params: { id: item.id }
        })
          .then(Result => {
            this.items.splice(indexOfItem, 1)
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
