<template>
  <div>
    <v-dialog v-model="showPartDialog" width="auto" max-width="none">
      <v-toolbar dark elevation-4 color="primary lighten-1">
        <span class="headline">Część {{ editedPart.id }}</span>
      </v-toolbar>
      <v-card>
        <v-form ref="newPartForm">
          <NewPart
            :editedItem="editedPart"
            :readonly="partEditMode"
            @editedProduct="editCurrentProductRes"
            :components="components"
            :hideComponent="true"
          ></NewPart>
        </v-form>
        <v-card-actions class="blue lighten-5">
          <v-spacer></v-spacer>

          <v-btn
            outlined
            rounded
            large
            color="blue darken-1"
            text
            @click="showPartDialog = false"
            >Ok<v-icon dark>check</v-icon></v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>
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
                <NewComponent
                  :currentItem="currentItem"
                  :editMode="editMode"
                  :processes="processes"
                  :components="items"
                  @editedProduct="editCurrentProductRes"
                  :validateId="validateComponentId"
                ></NewComponent>
              </v-form>
            </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions class="blue lighten-5">
          <v-btn
            outlined
            roundd
            large
            color="blue darken-1"
            text
            @click="showNewDialog = false"
            >Anuluj<v-icon dark>cancel</v-icon></v-btn
          >
          <v-spacer></v-spacer>
          <v-btn
            outlined
            rounded
            large
            color="blue darken-1"
            text
            @click.native="saveAction"
            >Zapisz<v-icon dark>save</v-icon></v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-layout row wrap elevation-3>
      <v-flex xs12>
        <v-toolbar text color="white">
          <v-toolbar-title>Komponenty</v-toolbar-title>
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
                <td
                  :key="index"
                  v-if="
                    header.value == 'czesc' &&
                    props.item.czesc !== 'nie przypisano'
                  "
                >
                  <v-tooltip bottom>
                    <template v-slot:activator="{ on }">
                      <v-btn
                        v-on="on"
                        color="primary"
                        dark
                        class="mb-2"
                        @click="showPart(props.item.czesci)"
                        >{{ props.item.czesc }}</v-btn
                      >
                    </template>
                    <span>Podgląd części</span>
                  </v-tooltip>
                </td>
                <td :key="index" v-else-if="header.value == 'czesc'">
                  {{ props.item.czesc }}
                </td>
                <td :key="index" v-else-if="header.value == 'ltid'">
                  {{ props.item.ltid }}
                </td>
                <td :key="index" v-else-if="header.value == 'actions'">
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
import NewComponent from '../components/NewComponent'
import NewPart from '../components/NewPart'

export default {
  name: 'Komponenty',
  components: {
    NewComponent: NewComponent,
    NewPart: NewPart
  },
  props: {},
  data () {
    return {
      // api
      getAll: 'api/Component/GetAll',
      getAllProcesses: 'api/Process/GetAll',
      getPart: 'api/Parts/GetById/',
      addApi: 'api/Component/Create',
      editApi: 'api/Component/Update',
      deleteApi: 'api/Component/Delete',

      headers: [
        { text: 'Id', value: 'komponentId' },
        { text: 'Nazwa', value: 'nazwa' },
        { text: 'Wymiar', value: 'wymiar' },
        { text: 'Gatunek podstaw.', value: 'gatunekPodst' },
        { text: 'Ilość', value: 'ilosc' },
        { text: 'Jednostka', value: 'jednostka' },
        { text: 'Cena jednostkowa', value: 'cenaJednostkowa' },
        { text: 'Proces id', value: 'processId' },
        { text: 'Część', value: 'czesc' },
        { text: 'LTID', value: 'ltid' },
        { text: 'Akcje', value: 'actions' }
      ],
      items: [],
      search: '',
      showNewDialog: false,
      tableLoading: false,
      title: 'Dodaj',
      currentItem: { komponent: { } },
      editedIndex: -1,
      editMode: false,
      processes: [],
      showPartDialog: false,
      partEditMode: true,
      editedPart: { komponent: {} },
      validateComponentId: false,
      formTitle: ''
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      this.getComponents()
    },
    getComponents () {
      var $this = this
      this.$http
        .get(this.getAll)
        .then((Response) => {
          $this.items = Response.data
          $this.items.forEach(p => {
            if (p.czesci.length > 0) {
              p.czesc = p.czesci[0].id
            } else {
              p.czesc = 'nie przypisano'
            }
          })
          $this.getProcesses()
        })
        .catch((e) => {
          this.tableLoading = false
        })
    },
    showPart (parts) {
      // // eslint-disable-next-line no-debugger
      // debugger

      if (parts.length > 0) {
        this.showPartDialog = true
        this.partEditMode = true
        this.getPartById(parts[0].id)
      }
    },
    getPartById (partId) {
      var $this = this
      partId = partId.replace('/', '%2F')
      this.$http
        .get(this.getPart + partId)
        .then((Response) => {
          $this.editedPart = Response.data
          if (!$this.editedPart.komponent) {
            $this.editedPart.komponent = {}
          }
          this.tableLoading = false
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
      // if (this.$refs.newForm) {
      //   this.$refs.newForm.reset()
      // }
      this.editMode = false
      this.validateComponentId = true
      this.formTitle = 'Dodaj'
      this.currentItem = { komponent: { } }
      this.editedIndex = -1
      this.showNewDialog = true
    },
    addAction (item) {
      this.$http
        .post(this.addApi, item)
        .then((Result) => {
          this.items.push(item)
        })
        .catch((e) => {
        })
    },

    edit (item, index) {
      this.formTitle = 'Edytuj ' + item.komponentId
      this.editMode = true
      this.editedIndex = index
      this.validateComponentId = false
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
