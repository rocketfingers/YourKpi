
<template>
  <div>
    <v-layout row wrap justify-space-around elevation-3 width="100%">
      <v-flex xs12>
        <v-dialog v-model="showNewPartDialog" width="auto" max-width="none">
          <v-toolbar dark elevation-4 color="primary lighten-1">
            <span class="headline">Dodaj część</span>
          </v-toolbar>
          <v-card>
            <v-form ref="newPartForm">
              <NewPart
                :editedItem="newPartItem"
                :editMode="false"
                @editedProduct="editCurrentProductRes"
                :components="components"
                :readonly="false"
                :processes="processes"
                :parts="parts"
              ></NewPart>
            </v-form>
            <v-card-actions class="blue lighten-5">
              <v-btn
                outlined
                rounded
                large
                color="blue darken-1"
                text
                @click="showNewPartDialog = false"
                >Anuluj<v-icon dark>cancel</v-icon></v-btn
              >
              <v-spacer></v-spacer>
              <v-btn
                outlined
                rounded
                large
                color="blue darken-1"
                text
                @click.native="saveNewPart"
                >Zapisz<v-icon dark>save</v-icon></v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-toolbar flat color="white">
          <v-toolbar-title>Czesci</v-toolbar-title>
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
          <v-btn
            color="primary"
            dark
            class="mb-2"
            @click="addPartToProduct()"
            v-if="!readonly"
            >Dodaj</v-btn
          >
        </v-toolbar>
        <v-data-table
          :headers="headers"
          :items="currentProduct.produktCzesci"
          :search="search"
        >
          <template v-if="readonly" v-slot:header.actions="{}"> </template>
          <template slot="item" slot-scope="props">
            <tr>
              <template v-for="(header, index) in headers">
                <td :key="index" v-if="header.value == 'actions' && !readonly">
                  <v-layout>
                    <v-flex xs4>
                      <v-icon
                        v-show="props.item.isEdited"
                        @click="editPart(props.item, index)"
                        color="orange"
                        class="mr-2"
                        >check</v-icon
                      >
                      <v-icon
                        v-show="!props.item.isEdited"
                        @click="editPart(props.item, index)"
                        color="green"
                        class="mr-2"
                        >edit</v-icon
                      >
                    </v-flex>
                    <v-flex xs4>
                      <v-icon
                        @click="deletePart(props.item, index)"
                        color="red lighten-1"
                        >delete</v-icon
                      >
                    </v-flex>
                  </v-layout>
                </td>
                <td v-else :key="index">
                  <template v-if="props.item.isEdited && !readonly">
                    <v-autocomplete
                      v-if="header.value == 'czesciId'"
                      color
                      :items="parts"
                      item-text="showName"
                      return-object
                      :rules="[requiredRule]"
                      @change="setCzesci(props.item)"
                      v-model="props.item.czesci"
                    >
                      <template slot="prepend">
                        <v-tooltip bottom>
                          <template v-slot:activator="{ on }">
                            <v-btn
                              @click="addPart(props.item)"
                              outlined
                              rounded
                              v-on="on"
                            >
                              <v-flex xs6>
                                <v-icon color="green">fa-tools</v-icon>
                              </v-flex>
                              <v-flex xs6>
                                <v-icon color="green">fa-plus</v-icon>
                              </v-flex>
                            </v-btn>
                          </template>
                          <span>Dodaj nową część</span>
                        </v-tooltip>
                      </template></v-autocomplete
                    >
                    <v-text-field
                      v-else-if="header.value == 'iloscSztuk'"
                      v-model="props.item.iloscSztuk"
                      type="number"
                      :rules="[numberRule, intRule]"
                    ></v-text-field>
                  </template>
                  <template v-else>
                    <template v-if="header.value == 'czescitpz'">
                      {{ props.item.czesci.tpz }}
                    </template>
                    <template v-if="header.value == 'czesciNazwa'">
                      {{ props.item.czesci.nazwa }}
                    </template>
                    <template v-else-if="header.value == 'czescitj'">
                      {{ props.item.czesci.tj }}
                    </template>
                    <template v-else-if="header.value == 'componentValue'">
                      <v-layout row wrap>
                        <b>
                          {{
                            (
                              props.item.czesci.komponent.cenaJednostkowa *
                              props.item.czesci.komponent.ilosc
                            ).toFixed(2)
                          }}</b
                        >
                        <v-spacer></v-spacer>
                        <span>
                          ({{ props.item.czesci.komponent.cenaJednostkowa }} *
                          {{ props.item.czesci.komponent.ilosc }})
                        </span>
                        <v-spacer></v-spacer>
                      </v-layout>
                    </template>

                    <template v-else>
                      {{ props.item[header.value] }}
                    </template>
                  </template>
                </td>
              </template>
            </tr>
          </template>
          <template slot="body.append" slot-scope="props">
            <tr class="green lighten-4">
              <td>
                <b>Suma:</b>
              </td>
              <td></td>

              <td>
                {{ calculateNoOfParts(props) }}
              </td>
              <td>
                {{ calculateTpz(props) }}
              </td>
              <td>
                {{ calculateTj(props) }}
              </td>
              <td>
                {{ calculateComponentsValue(props) }}
              </td>
              <td></td>
            </tr>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import NewPart from '../components/NewPart'

export default {
  name: 'ProductParts',
  components: {
    NewPart: NewPart
  },
  props: {
    currentProduct: Object,
    readonly: Boolean,
    parts: Array

  },
  data () {
    return {
      // api
      getAllProcesses: 'api/Process/GetAll',
      getAllFreeComponents: 'api/Component/GetAllUnassign',
      postNewPartApi: 'api/Parts/Create',

      headers: [
        { text: 'Id części', value: 'czesciId', visible: true },
        { text: 'Nazwa', value: 'czesciNazwa' },
        { text: 'Ilość sztuk', value: 'iloscSztuk', visible: true },
        { text: 'TPZ', value: 'czescitpz' },
        { text: 'TJ', value: 'czescitj' },
        { text: 'Wart.BoM (cena*zuzycie)', value: 'componentValue' },
        { text: 'Akcje', value: 'actions', visible: true }
      ],
      search: '',
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadź wartość dodatnią'
        return true
      },
      intRule: val => {
        if (val.includes(',') || val.includes('.')) return 'Wprowadź wartość całkowitą'
        return true
      },
      rows: [

        50,
        100,
        { text: '$vuetify.dataIterator.rowsPerPageAll', value: 1000 }
      ],
      showNewPartDialog: false,
      components: [],
      processes: [],
      newPartItem: { komponent: {} }

    }
  },
  computed: {
  },
  watch: {

  },
  methods: {
    calculateNoOfParts (props) {
      var res = 0
      props.items.forEach(element => {
        res = res + this.parseFloatOrZero(element.iloscSztuk)
      })

      return 'Ilość sztuk: ' + res.toFixed(2)
    },
    calculateTpz (props) {
      var res = 0
      props.items.forEach(element => {
        // res = res + (this.parseFloatOrZero(element.czesci.tpz) * this.parseFloatOrZero(element.iloscSztuk))
        res = res + (this.parseFloatOrZero(element.czesci.tpz))
      })

      // // eslint-disable-next-line no-debugger
      // debugger
      return 'TPZ: ' + res.toFixed(2)
    },
    calculateTj (props) {
      var res = 0
      props.items.forEach(element => {
        res = res + (this.parseFloatOrZero(element.czesci.tj) * this.parseFloatOrZero(element.iloscSztuk))
      })
      if (res === '0') {
        res = 0
      }
      return 'TJ: ' + res.toFixed(2)
    },
    calculateComponentsValue (props) {
      var res = 0
      props.items.forEach(element => {
        if (element.czesci.komponent) {
          res = res + (this.parseFloatOrZero(element.czesci.komponent.cenaJednostkowa) * this.parseFloatOrZero(element.iloscSztuk) * this.parseFloatOrZero(element.czesci.komponent.ilosc))
        }
      })
      if (res === '0') {
        res = 0
      }
      return 'Wartość komponentów: ' + res.toFixed(2)
    },
    parseFloatOrZero (val) {
      var res = parseFloat(val)
      if (res.toString() === 'NaN') {
        return 0
      }
      return res
    },
    setCzesci (item) {
      item.iloscSztuk = 1
      item.czesciId = item.czesci.id
    },
    addPart (item) {
      this.currentPart = item
      this.showNewPartDialog = true
    },
    addPartToProduct () {
      this.getComponents()
      if (!this.currentProduct.produktCzesci) {
        this.$set(this.currentProduct, 'produktCzesci', [])
      }
      this.currentProduct.produktCzesci.unshift({ czesci: [], czesciId: '', iloscSztuk: '', actions: '', isEdited: true })
    },
    async editPart (part) {
      if (part.isEdited) {
        if (part.czesciId.length === 0 || part.czesciId.iloscSztuk === 0) {
          await this.$dialog.confirm({
            text: 'Uzupełnij dane!'
          })
        } else {
          this.$set(part, 'isEdited', false)
        }
      } else {
        this.$set(part, 'isEdited', true)
      }
    },
    deletePart (part, index) {
      // var res = await this.$dialog.confirm({
      //   text: 'Czy na pewno chcesz usunąć:  ' + part.czesciId + '?',
      //   title: 'Uwaga'
      // })
      // if (res) {
      var indexOfPart = this.currentProduct.produktCzesci.indexOf(part)
      this.currentProduct.produktCzesci.splice(indexOfPart, 1)
    //   }
    },
    editCurrentProductRes (item) {
      // // eslint-disable-next-line no-debugger
      // debugger
      this.newPartItem = item
    },
    saveNewPart () {
      if (!this.$refs.newPartForm.validate()) {
        return
      }
      // Native form submission is not yet supported
      // // eslint-disable-next-line no-debugger
      // debugger
      this.$http.post(this.postNewPartApi,
        this.newPartItem)
        .then(Result => {
          this.newPartItem.id = Result.data.id
          this.parts.push(Result.data)
          this.currentPart.czesci = Result.data
          this.currentPart.czesci.showName = this.currentPart.czesci.id + ', ' + this.currentPart.czesci.nazwa
          this.setCzesci(this.currentPart)
          this.closeNewPart()
        })
    },
    closeNewPart () {
      // if (this.$refs.newPartForm) {
      //   this.$refs.newPartForm.reset()
      // }
      this.newPartItem = Object.assign({ komponent: { } }, this.defaultItem)
      this.showNewPartDialog = false
      // Deletion of form errors
    },
    getComponents () {
      var $this = this
      this.$http
        .get(this.getAllFreeComponents)
        .then((Response) => {
          $this.components = Response.data

          $this.components.forEach(p => {
            p.showName = p.komponentId + ', ' + p.nazwa + ', ' + p.gatunekPodst
            if (p.czesci.length > 0) {
              p.czesc = p.czesci[0].id
            } else {
              p.czesc = 'nie przypisano'
            }
          })
          $this.components.unshift({
            showName: 'zdefiniuj nowy komponent',
            komponentId: '',
            isNewFlag: true,
            id: 0
          })
          $this.getProcesses()
        })
        .catch((e) => {
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
        })
        .catch((e) => {
        })
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
