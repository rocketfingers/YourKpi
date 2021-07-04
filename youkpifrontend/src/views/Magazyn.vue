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
                  :commodities="commodities"
                  :contractors="contractors"
                  :locations="locations"
                ></NewStoreElement>
              </v-form>
            </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-btn
            large
            color="blue darken-1"
            text
            outlined
            @click.native="showNewDialog = false"
          >
            Anuluj
            <v-icon dark>cancel</v-icon>
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn
            text
            outlined
            large
            color="blue darken-1"
            @click.native="saveAction"
          >
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
                chips
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
      getAllCommoditiesSimple: 'api/Commodity/GetAllSimpleView',
      getAllPartsSimple: 'api/Parts/GetAllSimpleView',
      getAllLocationsSimple: 'api/Loaction/GetAllSimpleView',
      getAllContractorsSimple: 'api/Contractor/GetAllSimpleView',
      addApi: 'api/Store/Create',
      editApi: 'api/Store/Update',
      deleteApi: 'api/Store/Delete',
      headers: [
        { text: 'Kontrahent', value: 'kontrahentName' },
        { text: 'Data przyjęcia', value: 'dataPrzyjecia' },
        { text: 'Magazyn', value: 'magazynName' },
        { text: 'Lokacja', value: 'lokacjaName' },
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
      locations: [],
      commodities: [],
      stores: [
        { id: 1, name: 'Części' },
        { id: 2, name: 'Produkty' },
        { id: 3, name: 'Komponenty' },
        { id: 4, name: 'Towary' },
        { id: 5, name: 'Produkty niezgodne' }
      ],
      selectedStores: [
        { id: 1, name: 'Części' },
        { id: 2, name: 'Produkty' },
        { id: 3, name: 'Komponenty' },
        { id: 4, name: 'Towary' },
        { id: 5, name: 'Produkty niezgodne' }
      ],
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
      this.getLoactions()
    },
    getLoactions () {
      var $this = this
      this.$http
        .get(this.getAllLocationsSimple)
        .then((Response) => {
          $this.locations = Response.data
          $this.locations.forEach(p => {
            p.showName = p.id + ', ' + p.name
          })
          this.getProducts()
        })
        .catch((e) => {
        })
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
          this.getItems()
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
              p.kontrahentName = p.kontrahent.name
              p.kontrahentId = p.kontrahent.id
            }
            p.lokacjaName = ''
            var lokacjaObj = $this.locations.find(l => l.id === p.lokacjaId?.toString())

            if (lokacjaObj) {
              p.lokacjaName = lokacjaObj.name
              p.lokacjaId = lokacjaObj.id
            }
            switch (p.magazyn.id) {
              case 1:
                var item = $this.parts.find(part => part.id === p.elementId)
                if (item) {
                  p.nazwa = item.name
                } else {
                  p.nazwa = ''
                }
                break
              case 2:
                var item2 = $this.products.find(prod => prod.id === p.elementId)
                if (item2) {
                  p.nazwa = item2.name
                } else {
                  p.nazwa = ''
                }
                break
              case 3:
                var item3 = $this.components.find(comp => comp.id === p.elementId)
                if (item3) {
                  p.nazwa = item3.name
                } else {
                  p.nazwa = ''
                }
                break
              case 4:
                var item4 = $this.commodities.find(com => com.id === p.elementId)

                if (item4) {
                  p.nazwa = item4.name
                } else {
                  p.nazwa = ''
                }
                break
              case 5:
                var item5 = $this.products.find(com => com.id === p.elementId)

                if (item5) {
                  p.nazwa = item5.name
                } else {
                  p.nazwa = ''
                }
                break
            }
            p.dataPrzyjecia = $this.formatDateTimeYYYYMMDD(p.dataPrzyjecia)
          })
          this.tableLoading = false
        })
        .catch((e) => {
          this.tableLoading = false
        })
    },
    getCommodities () {
      var $this = this
      this.$http
        .get(this.getAllCommoditiesSimple)
        .then((Response) => {
          $this.commodities = Response.data
          $this.commodities.forEach(p => {
            p.showName = p.id + ', ' + p.name
          })
          this.getParts()
        })
        .catch((e) => {
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
          this.getCommodities()
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
          this.getComponents()
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
          this.getContractors()
        })
        .catch((e) => {
        })
    },
    saveAction () {
      if (!this.$refs.newForm.validate()) {
        return
      }
      // eslint-disable-next-line no-debugger
      debugger
      if (this.editedIndex > 0) {
        this.editAction(this.currentItem)
      } else {
        this.addAction(this.currentItem)
      }
      this.showNewDialog = false
    },
    add () {
      // if (this.$refs.newForm) {
      //   this.$refs.newForm.reset()
      // }
      this.editMode = false
      this.formTitle = 'Dodaj'
      this.currentItem = { }
      this.editedIndex = -1
      this.showNewDialog = true
    },
    addAction (item) {
      // if (item.magazyn.id === 2 || item.magazyn.id === 5) {
      //   item.elementId = item.id
      // }
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
      this.formTitle = 'Edytuj ' + item.nazwa
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
