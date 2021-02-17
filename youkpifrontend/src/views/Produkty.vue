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
          <v-layout row wrap justify-space-around>
            <v-flex xs11>
              <v-form ref="newProductForm">
                <NewProduct
                  :currentProduct="currentProduct"
                  :editMode="editMode"
                  @editedProduct="editCurrentProductRes"
                  @duplicateProduct="duplicateProduct"
                  :productTypes="productTypes"
                  :parts="parts"
                ></NewProduct>
              </v-form>
            </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-btn
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
    <v-dialog v-model="showProductPartsDialog" max-width="1200" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">Części produktu: {{ currentProduct.id }}</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showProductPartsDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-container grid-list-md>
          <ProductParts
            :currentProduct="currentProduct"
            readonly="true"
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
    </v-dialog>
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
            >Nowy</v-btn
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
                    <v-flex xs4>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            v-on="on"
                            v-show="props.item.produktCzesci.length > 0"
                            @click="showProductParts(props.item, index)"
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
                            @click="editProduct(props.item, index)"
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
                            @click="deleteProduct(props.item, index)"
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
    <v-snackbar v-model="showMsg">
      {{ showMsgText }}
      <template v-slot:action="{ attrs }">
        <v-btn color="pink" text v-bind="attrs" @click="showMsg = false">
          Close
        </v-btn>
      </template>
    </v-snackbar>
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
      getAllProducts: 'api/Products/GetAll',
      addProductApi: 'api/Products/Create',
      editProductApi: 'api/Products/Update',
      deleteProductApi: 'api/Products/Delete',
      getAllProductTypesApi: 'api/ProductTypes/GetAll',
      getAllPartsApi: 'api/Parts/GetAll',

      headers: [
        { text: 'Id', value: 'id' },
        { text: 'Typ wyrobu id', value: 'typWyrobuId' },
        { text: 'NumerRysNorma', value: 'numerRysNorma' },
        { text: 'DN', value: 'dn' },
        { text: 'PN', value: 'pn' },
        { text: 'ANSI', value: 'ansi' },
        { text: 'Wersja', value: 'wersja' },
        { text: 'Uszczelnienie', value: 'uszczelnienie' },
        { text: 'Cena', value: 'cena' },
        { text: 'Waluta', value: 'waluta' },
        { text: 'Suma TPZ', value: 'tpzSum' },
        { text: 'Suma TJ', value: 'tjSum' },
        { text: 'Wart. komponentów', value: 'componentsValueSum' },
        { text: 'Akcje', value: 'actions' }
      ],
      products: [],
      search: '',
      showNewProductDialog: false,
      showProductPartsDialog: false,
      tableLoading: false,
      productTitle: 'Dodaj produkt',
      currentProduct: { produktCzesci: [] },
      editedIndex: -1,
      productTypes: [],
      editMode: false,
      parts: [],
      emptyProduct: { produktCzesci: [] },
      showMsg: false,
      showMsgText: '',
      parentProduct: {}

    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      this.getProducts()
      this.getParts()
      this.getProductTypes()
    },
    getParts () {
      var $this = this
      this.$http.get(this.getAllPartsApi)
        .then(Response => {
          $this.parts = Response.data
          // eslint-disable-next-line no-debugger
          debugger
          $this.parts.forEach(p => {
            p.showName = p.id + ', ' + p.nazwa
            if (!p.komponent) {
              p.komponent = { cenaJednostkowa: 0, ilosc: 0 }
            } else
            if (!p.komponent.cenaJednostkowa) {
              p.komponent.cenaJednostkowa = 0
              p.komponent.ilosc = 0
            }
          })
        })
    },
    getProducts () {
      var $this = this
      this.$http
        .get(this.getAllProducts)
        .then((Response) => {
          $this.products = Response.data
          $this.products.forEach(product => {
            product.produktCzesci.forEach(pc => {
              if (!pc.czesci) {
                pc.czesci = { komponent: { cenaJednostkowa: 0 } }
              }
              if (!pc.czesci.komponent) {
                pc.czesci = { komponent: { cenaJednostkowa: 0 } }
              }
              if (!pc.czesci.komponent.cenaJednostkowa) {
                pc.czesci.komponent.cenaJednostkowa = 0
              }
            })
            product.tpzSum = 0
            product.tjSum = 0
            product.componentsValueSum = 0
            product.produktCzesci.forEach(procze => {
              // product.tpzSum = product.tpzSum + (procze.czesci.tpz * procze.iloscSztuk)
              product.tpzSum = product.tpzSum + procze.czesci.tpz
            })
            product.produktCzesci.forEach(procze => {
              product.tjSum = product.tjSum + (procze.czesci.tj * procze.iloscSztuk)
            })
            product.produktCzesci.forEach(procze => {
              product.componentsValueSum = product.componentsValueSum + (procze.czesci.komponent.cenaJednostkowa * procze.iloscSztuk * procze.czesci.komponent.ilosc)
            })

            product.tpzSum = product.tpzSum.toFixed(2)
            product.tjSum = product.tjSum.toFixed(2)
            product.componentsValueSum = product.componentsValueSum.toFixed(2)
          })
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
        return false
      }
      if (this.editedIndex > 0) {
        this.editProductAction(this.currentProduct)
        this.showNewProductDialog = false
      } else {
        this.addProductAction(this.currentProduct)
      }
      return true
    },
    addProduct (productTemplate) {
      if (productTemplate) {
        this.currentProduct = productTemplate
      } else {
        this.currentProduct = { produktCzesci: [] }
      }

      this.editMode = false
      this.productTitle = 'Dodaj produkt'
      this.editedIndex = -1
      this.showNewProductDialog = true
    },
    validatePartsTheSame (product, parentProduct) {
      var toCompareProd = { produktCzesci: [] }
      if (parentProduct) {
        if (Object.keys(parentProduct).length > 0) {
          toCompareProd = JSON.parse(JSON.stringify(parentProduct))
          toCompareProd.id = 0
          toCompareProd.produktCzesci.forEach(pc => {
            pc.id = 0
            pc.isEdited = false
          })
          product.produktCzesci.forEach(pc => {
            pc.isEdited = false
          })

          if (JSON.stringify(toCompareProd.produktCzesci) === JSON.stringify(product.produktCzesci)) {
            this.showMsgText = 'Duplikowany produkt składa się dokładnie z tych samych części!'
            this.showMsg = true
            return false
          }
        }
      }
      return true
    },
    addProductAction (product, isDuplicate) {
      if (this.validatePartsTheSame(product, this.parentProduct)) {
        this.$http
          .post(this.addProductApi, product)
          .then((Result) => {
            this.products.unshift(product)
            this.initialise()

            if (this.parentProduct) {
              this.showMsgText = 'Utworzono duplikat produktu'
              this.showMsg = true
            }
          })
          .catch((e) => {
          })
        this.showNewProductDialog = false
      }
    },
    showProductParts (product, index) {
      this.currentProduct = product
      this.editedIndex = index
      this.showProductPartsDialog = true
    },
    editProduct (product, index) {
      this.productTitle = 'Edytuj produkt ' + product.id
      this.editMode = true
      this.editedIndex = index
      this.showNewProductDialog = true
      this.currentProduct = product
    },
    editCurrentProductRes (editedProduct) {
      this.currentProduct = editedProduct
    },
    duplicateProduct (product) {
      if (this.saveProductAction()) {
        this.showMsgText = 'Utworzono duplikat produktu: ' + product.id
        var duplicatedProduct = JSON.parse(JSON.stringify(product))
        duplicatedProduct.id = ''
        duplicatedProduct.produktCzesci.forEach(p => {
          p.id = 0
        })
        this.parentProduct = product

        this.addProduct(duplicatedProduct)
      }
      // this.addProductAction(duplicatedProduct, true)
    },
    editProductAction (product) {
      this.$http
        .put(this.editProductApi, product)
        .then((Result) => {
          this.initialise()
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
