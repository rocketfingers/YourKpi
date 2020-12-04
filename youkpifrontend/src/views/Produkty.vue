<template>
  <div>
    <v-dialog v-model="showNewProductDialog" max-width="1800" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">{{ productTitle }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showNewProductDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-container grid-list-md>
          <v-layout align-center wrap>
            <v-flex xs12 sm8 offset-sm2>
              <v-form ref="newProductForm">
                <NewProduct
                  :currentProduct="currentProduct"
                  :editMode="editMode"
                  @editedProduct="editCurrentProductRes"
                  :productTypes="productTypes"
                ></NewProduct>
              </v-form>
            </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-btn
            outline
            round
            large
            color="blue darken-1"
            flat
            @click.native="showNewProductDialog = false"
          >
            Anuluj
            <v-icon dark>cancel</v-icon>
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn
            outline
            round
            large
            color="blue darken-1"
            flat
            @click.native="saveProductAction"
          >
            Zapisz
            <v-icon dark>save</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="showProductPartsDialog" max-width="1800" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">Czesci produktu</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showProductPartsDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-container grid-list-md>
          <v-layout align-center wrap>
            <v-flex xs12 sm8 offset-sm2>
              <ProductParts></ProductParts>
            </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-spacer></v-spacer>
          <v-btn
            outline
            round
            large
            color="blue darken-1"
            flat
            @click.native="showProductPartsDialog = false"
          >
            Zamknij
            <v-icon dark>cancel</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-container>
      <v-layout row wrap elevation-3>
        <v-flex xs12>
          <v-toolbar flat color="white">
            <v-toolbar-title>Produkty</v-toolbar-title>
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
            <v-btn color="primary" dark class="mb-2" @click="addProduct()"
              >Dodaj</v-btn
            >
          </v-toolbar>
        </v-flex>
        <v-flex xs12>
          <v-data-table
            :headers="headers"
            :items="products"
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
                      <v-flex xs>
                        <v-icon
                          @click="showProductParts(props.item, index)"
                          color="blue"
                          class="mr-2"
                          >search</v-icon
                        >
                      </v-flex>
                      <v-flex xs>
                        <v-icon
                          @click="editProduct(props.item, index)"
                          color="green"
                          class="mr-2"
                          >edit</v-icon
                        >
                      </v-flex>
                      <v-flex xs4>
                        <v-icon
                          @click="deleteProduct(props.item, index)"
                          color="red lighten-1"
                          >delete</v-icon
                        >
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
// import * as v from '../main.js'
import NewProduct from '../components/NewProduct'
import ProductParts from '../components/ProductParts'

export default {
  name: 'Produkty',
  components: {
    NewProduct: NewProduct,
    ProductParts: ProductParts
  },
  props: {},
  data () {
    return {
      // api
      getAllProducts: 'api/Products/GetAllProducts',
      addProductApi: 'api/Products/AddProduct',
      editProductApi: 'api/Products/EditProduct',
      deleteProductApi: 'api/Products/DeleteById',
      getAllProductTypesApi: 'api/ProductTypes/GetAllProductTypes',

      headers: [
        { text: 'Id', value: 'id' },
        { text: 'Typ wyrobu id', value: 'typWyrobuId' },
        { text: 'NumerRysNorma', value: 'numerRysNorma' },
        { text: 'DN', value: 'dn' },
        { text: 'PN', value: 'pn' },
        { text: 'ANSI', value: 'ansi' },
        { text: 'Wersja', value: 'wersja' },
        { text: 'Uszczelnienie', value: 'uszczelnienie' },
        { text: 'Akcje', value: 'actions' }
      ],
      products: [],
      search: '',
      showNewProductDialog: false,
      showProductPartsDialog: false,
      tableLoading: false,
      productTitle: 'Dodaj produkt',
      currentProduct: {},
      editedIndex: -1,
      productTypes: [],
      editMode: false
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      this.getProducts()
      this.getProductTypes()
    },
    getProducts () {
      var $this = this
      this.$http
        .get(this.getAllProducts)
        .then((Response) => {
          $this.products = Response.data
          this.tableLoading = false
        })
        .catch((e) => {
          this.tableLoading = false
        })
    },
    getProductTypes () {
      var $this = this
      this.$http
        .get(this.getAllProductTypesApi)
        .then((Response) => {
          $this.productTypes = Response.data
        })
        .catch((e) => {})
    },
    saveProductAction () {
      if (!this.$refs.newProductForm.validate()) {
        return
      }
      if (this.editedIndex > 0) {
        this.editProductAction(this.currentProduct)
      } else {
        this.addProductAction(this.currentProduct)
      }
      this.showNewProductDialog = false
    },
    addProduct () {
      if (this.$refs.newProductForm) {
        this.$refs.newProductForm.reset()
      }
      this.editMode = false
      this.productTitle = 'Dodaj produkt'
      this.currentProduct = {}
      this.editedIndex = -1
      this.showNewProductDialog = true
    },
    addProductAction (product) {
      this.$http
        .post(this.addProductApi, product)
        .then((Result) => {
          this.products.push(product)
        })
        .catch((e) => {
        })
    },
    showProductParts (product, index) {
      this.showProductPartsDialog = true
    },
    editProduct (product, index) {
      this.productTitle = 'Edytuj produkt'
      this.editMode = true
      this.editedIndex = index
      this.showNewProductDialog = true
      this.currentProduct = product
    },
    editCurrentProductRes (editedProduct) {
      this.currentProduct = editedProduct
    },
    editProductAction (product) {
      this.$http
        .put(this.editProductApi, product)
        .then((Result) => {
        })
        .catch((e) => {})
    },
    async deleteProduct (product, index) {
      var res = await this.$dialog.confirm({
        text: 'Czy na pewno chcesz usunąć?',
        title: 'Uwaga'
      })
      if (res) {
        var indexOfProd = this.products.indexOf(product)
        this.$http.delete(this.deleteProductApi, {
          params: { id: product.id }
        })
          .then(Result => {
            this.products.splice(indexOfProd, 1)
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
