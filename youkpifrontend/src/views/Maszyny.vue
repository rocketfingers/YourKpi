<template>
  <div>
    <v-dialog v-model="showNewDialog" max-width="1800" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">{{ title }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showNewDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-container grid-list-md>
          <v-layout row wrap justify-space-around>
            <v-flex xs11>
              <v-form ref="newProductForm">
                <NewMachine
                  :currentItem="currentItem"
                  :editMode="editMode"
                  @editedProduct="editCurrentProductRes"
                ></NewMachine>
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
          <v-btn
            flat
            large
            color="blue darken-1"
            @click.native="saveProductAction"
          >
            Zapisz
            <v-icon dark>save</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!--   <v-dialog v-model="showProductPartsDialog" max-width="1200" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">Części produktu: {{ currentItem.id }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showProductPartsDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-container grid-list-md>
          <ProductParts
            :currentItem="currentItem"
            :readonly="true"
          ></ProductParts>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-spacer></v-spacer>
          <v-btn
            large
            color="blue darken-1"
            @click.native="showProductPartsDialog = false"
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
          <v-toolbar-title>Maszyny</v-toolbar-title>
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
import NewMachine from '../components/NewMachine'

export default {
  name: 'Produkty',
  components: {
    NewMachine: NewMachine
  },
  props: {},
  data () {
    return {
      // api
      getAll: 'api/Machine/GetAll',
      // addProductApi: 'api/Products/Create',
      // editProductApi: 'api/Products/Update',
      // deleteProductApi: 'api/Products/Delete',
      // getAllProductTypesApi: 'api/ProductTypes/GetAll',
      // getAllPartsApi: 'api/Parts/GetAll',

      headers: [
        { text: 'Id', value: 'id' },
        { text: 'Nazwa', value: 'nazwa' },
        { text: 'Rodzaj', value: 'rodzaj' },
        { text: 'Opis', value: 'opis' },
        { text: 'Tryb pracy', value: 'trybPracy' },
        { text: 'OS_X', value: 'osX' },
        { text: 'OS_Y', value: 'osY' },
        { text: 'OS_Z', value: 'osZ' },
        { text: 'Miejsce pracy', value: 'miejscePracy' },
        { text: 'Akcje', value: 'actions' }
      ],
      items: [],
      search: '',
      showNewDialog: false,
      showProductPartsDialog: false,
      tableLoading: false,
      title: 'Dodaj',
      currentItem: { },
      editedIndex: -1,
      editMode: false
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      this.getMachines()
    },
    getMachines () {
      var $this = this
      this.$http
        .get(this.getAll)
        .then((Response) => {
          $this.items = Response.data
          this.tableLoading = false
        })
        .catch((e) => {
          this.tableLoading = false
        })
    },
    // initialise () {
    //   this.tableLoading = true
    //   this.getProducts()
    //   this.getParts()
    //   this.getProductTypes()
    // },
    // getParts () {
    //   this.$http.get(this.getAllPartsApi)
    //     .then(Response => {
    //       this.parts = Response.data
    //     })
    // },
    // getProducts () {
    //   var $this = this
    //   this.$http
    //     .get(this.getAllProducts)
    //     .then((Response) => {
    //       $this.products = Response.data
    //       this.tableLoading = false
    //     })
    //     .catch((e) => {
    //       this.tableLoading = false
    //     })
    // },
    // getProductTypes () {
    //   var $this = this
    //   this.$http
    //     .get(this.getAllProductTypesApi)
    //     .then((Response) => {
    //       $this.productTypes = Response.data
    //     })
    //     .catch((e) => {})
    // },
    // saveProductAction () {
    //   if (!this.$refs.newProductForm.validate()) {
    //     return
    //   }
    //   if (this.editedIndex > 0) {
    //     this.editProductAction(this.currentItem)
    //   } else {
    //     this.addProductAction(this.currentItem)
    //   }
    //   this.showNewDialog = false
    // },
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
    // addProductAction (product) {
    //   this.$http
    //     .post(this.addProductApi, product)
    //     .then((Result) => {
    //       this.products.push(product)
    //     })
    //     .catch((e) => {
    //     })
    // },
    // showProductParts (product, index) {
    //   this.currentItem = product
    //   this.editedIndex = index
    //   this.showProductPartsDialog = true
    // },
    edit (item, index) {
      this.formTitle = 'Edytuj ' + item.idDifferenceReasonCode
      this.editMode = true
      this.editedIndex = index
      this.showNewDialog = true
      this.currentItem = item
    }
    // editCurrentProductRes (editedProduct) {
    //   this.currentItem = editedProduct
    // },
    // editProductAction (product) {
    //   this.$http
    //     .put(this.editProductApi, product)
    //     .then((Result) => {
    //     })
    //     .catch((e) => {})
    // },
    // async deleteProduct (product, index) {
    //   var res = await this.$dialog.confirm({
    //     text: 'Czy na pewno chcesz usunąć?',
    //     title: 'Uwaga'
    //   })
    //   if (res) {
    //     var indexOfProd = this.products.indexOf(product)
    //     this.$http.delete(this.deleteProductApi, {
    //       params: { id: product.id }
    //     })
    //       .then(Result => {
    //         this.products.splice(indexOfProd, 1)
    //       }
    //       )
    //   }
    // }
  },
  created () {
    this.initialise()
  },
  destroyed () {}
}
</script>

<style>
</style>
