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
                <NewStoreElement
                  :currentItem="currentItem"
                  :stores="stores"
                  :editMode="editMode"
                  @editedItem="editCurrentItemRes"
                  :components="components"
                  :products="products"
                  :parts="parts"
                  :contractors="contractors"
                ></NewStoreElement>
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
        <v-layout row wrap ma-3 elevation-3>
          <v-flex xs12 md4>
            <v-layout row wrap ma-3>
              <v-autocomplete
                label="Wybierz magazyn"
                :items="stores"
                :rules="[requiredRule]"
                hide-details
                item-text="name"
                return-object
                v-model="selectedStores"
                multiple
                autocomplete
              ></v-autocomplete>
            </v-layout>
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs12 md2>
            <v-layout row wrap justify-space-around>
              <v-switch
                v-model="storeReport"
                label="Raport magazynu"
              ></v-switch>
            </v-layout>
          </v-flex>
          <v-spacer></v-spacer>
          <v-flex xs12 md5>
            <v-layout row wrap ma-3>
              <v-text-field
                append-icon="search"
                label="Wyszukaj"
                single-line
                hide-details
                class="elevation-1"
                v-model="search"
              >
              </v-text-field>
              <v-btn
                color="primary"
                dark
                class="mb-2"
                @click="add()"
                v-show="!storeReport"
                >Nowy</v-btn
              >
            </v-layout>
          </v-flex>
        </v-layout>
      </v-flex>
      <v-flex xs12>
        <v-data-table
          :headers="headers"
          :items="filteredItems"
          class="elevation-1"
          item-key="id"
          :loading="tableLoading"
          :search="search"
        >
          <template slot="item" slot-scope="props">
            <tr>
              <template v-for="(header, index) in headers">
                <td :key="index" v-if="header.value == 'actions'">
                  <v-layout v-show="!storeReport">
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
import NewStoreElement from '../components/NewStoreElement'

export default {
  name: 'Produkty',
  components: {
    NewStoreElement: NewStoreElement
  },
  props: {},
  data () {
    return {
      // api
      getAll: 'api/Store/GetAll',
      getAllComponentsSimple: 'api/Component/GetAllSimpleView',
      getAllProductsSimple: 'api/Products/GetAllSimpleView',
      getAllPartsSimple: 'api/Parts/GetAllSimpleView',
      getAllContractorsSimple: 'api/Contractor/GetAllSimpleView',

      addApi: 'api/Store/Create',
      editApi: 'api/Store/Update',
      deleteApi: 'api/Store/Delete',
      headers: [
        // { text: 'Id', value: 'id' },
        // { text: 'Lp', value: 'lp' },
        { text: 'Kontrahent', value: 'kontrahent' },
        { text: 'Data przyjęcia', value: 'dataPrzyjecia' },
        { text: 'Magazyn', value: 'magazynName' },
        { text: 'Numer faktury', value: 'nrFakturyId' },
        { text: 'Id', value: 'elementId' },
        { text: 'Nazwa', value: 'nazwa' },
        { text: 'Ilość', value: 'ilosc' },
        { text: 'Jednostka', value: 'jednostka' },
        { text: 'Cena jednostkowa netto', value: 'cenaJdnNetto' },
        { text: 'Akcje', value: 'actions' }
      ],
      items: [],
      search: '',
      showNewDialog: false,
      showProductPartsDialog: false,
      tableLoading: false,
      formTitle: 'Dodaj ',
      currentItem: { produktCzesci: [] },
      editedIndex: -1,
      editMode: false,
      parts: [],
      products: [],
      components: [],
      stores: [
        { id: 1, name: 'Części' },
        { id: 2, name: 'Produkty' },
        { id: 3, name: 'Komponenty' }
      ],
      selectedStores: [
        { id: 1, name: 'Części' },
        { id: 2, name: 'Produkty' },
        { id: 3, name: 'Komponenty' }],
      storeReport: false
    }
  },
  computed: {
    filteredItems () {
      var res = this.items.filter(p => {
        return (this.selectedStores.some(s => s.id === p.magazyn.id))
      })

      return res
    }
  },
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      this.getProducts()
      this.getParts()
      this.getComponents()
      this.getContractors()
      this.getItems()
    },

    getContractors () {
      var $this = this
      this.$http
        .get(this.getAllContractorsSimple)
        .then((Response) => {
          $this.contractors = Response.data
          $this.contractors.forEach(p => {
            p.showName = p.id + ', ' + p.name
          })
        })
        .catch((e) => {
        })
    },
    getItems () {
      var $this = this
      this.$http
        .get(this.getAll)
        .then((Response) => {
          $this.items = Response.data
          $this.items.forEach(p => {
            p.magazynName = p.magazyn.name
            p.kontrahent = $this.contractors.find(c => c.id === p.kontrahentId.toString())
            if (p.kontrahent) {
              p.kontrahent = p.kontrahent.name
            }
            p.dataPrzyjecia = $this.formatDateTimeYYYYMMDD(p.dataPrzyjecia)
          })
          this.tableLoading = false
        })
        .catch((e) => {
          this.tableLoading = false
        })
    },
    getProducts () {
      var $this = this
      this.$http
        .get(this.getAllProductsSimple)
        .then((Response) => {
          $this.products = Response.data
          $this.products.forEach(p => {
            p.showName = p.id + ', ' + p.name
          })
        })
        .catch((e) => {
        })
    },
    getParts () {
      var $this = this
      this.$http
        .get(this.getAllPartsSimple)
        .then((Response) => {
          $this.parts = Response.data
          $this.parts.forEach(p => {
            p.showName = p.id + ', ' + p.name
          })
        })
        .catch((e) => {
        })
    },
    getComponents () {
      var $this = this
      this.$http
        .get(this.getAllComponentsSimple)
        .then((Response) => {
          $this.components = Response.data
          $this.components.forEach(p => {
            p.showName = p.id + ', ' + p.name
          })
        })
        .catch((e) => {
        })
    },
    formatDateTimeYYYYMMDD (date) {
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

    edit (item, index) {
      this.formTitle = 'Edytuj ' + item.idDifferenceReasonCode
      this.editMode = true
      this.editedIndex = index
      this.showNewDialog = true
      this.currentItem = item
    },
    editCurrentItemRes (editedItem) {
      this.currentItem = editedItem
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
          params: {
            id: item.id,
            magazynId: item.magazyn.id
          }
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
