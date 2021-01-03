<template>
  <div>
    <v-layout row wrap elevation-3>
      <v-flex xs12>
        <v-toolbar flat color="white">
          <v-toolbar-title>{{ title }}</v-toolbar-title>
          <v-divider class="mx-2" inset vertical></v-divider>
          <v-spacer></v-spacer>
          <v-text-field
            append-icon="search"
            label="Search"
            single-line
            hide-details
            class="elevation-1"
            v-model="searchTable"
          ></v-text-field>
          <v-dialog v-model="showDialog" width="auto" max-width="none">
            <template v-slot:activator="{ on, attrs }">
              <v-btn v-on="on" v-bind="attrs" color="primary" dark class="mb-2">
                Nowy
              </v-btn>
            </template>

            <v-toolbar dark elevation-4 color="primary lighten-1">
              <span class="headline">{{ formTitle }}</span>
            </v-toolbar>
            <v-card>
              <v-form ref="newPartForm">
                <NewPart
                  :editedItem="editedItem"
                  :editMode="editMode"
                  @editedProduct="editCurrentProductRes"
                  :components="components"
                  :readonly="false"
                  :processes="processes"
                ></NewPart>
              </v-form>
              <v-card-actions class="blue lighten-5">
                <v-btn
                  outline
                  round
                  large
                  color="blue darken-1"
                  flat
                  @click="showDialog = false"
                  >Anuluj<v-icon dark>cancel</v-icon></v-btn
                >
                <v-spacer></v-spacer>
                <v-btn
                  outline
                  round
                  large
                  color="blue darken-1"
                  flat
                  @click.native="save"
                  >Zapisz<v-icon dark>save</v-icon></v-btn
                >
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
        <v-data-table
          :headers="headers"
          :items="data"
          :search="searchTable"
          :loading="tableLoading"
          :footer-props="footer"
          class="elevation-2"
        >
          <template slot="item" slot-scope="props">
            <tr>
              <!-- Columns settings  -->
              <td class="text-xs-left">{{ props.item.id }}</td>
              <td class="text-xs-left">{{ props.item.nazwa }}</td>
              <td class="text-xs-left">{{ props.item.gatPodstawowy }}</td>
              <td class="text-xs-left">{{ props.item.numerRysNorma }}</td>
              <td class="text-xs-left">
                <v-icon v-show="props.item.hasComponent" color="green"
                  >check</v-icon
                >
              </td>

              <td class="justify-center px-0">
                <v-layout>
                  <v-flex xs6>
                    <v-icon
                      class="mr-2"
                      color="green"
                      @click="editItem(props.item)"
                    >
                      edit
                    </v-icon>
                  </v-flex>
                  <v-flex xs6>
                    <v-icon
                      @click="deleteProduct(props.item, index)"
                      color="red lighten-1"
                      >delete</v-icon
                    >
                  </v-flex>
                </v-layout>
              </td>
            </tr>

            <tr></tr
          ></template>
        </v-data-table>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import * as v from '../main.js'
import NewPart from '../components/NewPart'

export default {
  name: '',
  components: {
    NewPart: NewPart
  },
  props: {
  },
  data () {
    return {
      // Settings
      title: 'Części',
      editTitle: 'Edytuj część',
      newTitle: 'Dodaj część',
      getAllApi: 'api/Parts/GetAll',
      getAllFreeComponents: 'api/Component/GetAllUnassign',
      postNewApi: 'api/Parts/Create',
      putEditApi: 'api/Parts/Update',
      deleteApi: 'api/Parts/Delete',
      getAllProcesses: 'api/Process/GetAll',

      defaultItem: {
        id: '',
        nazwa: '',
        gatPodstawowy: '',
        numerRysNorma: ''
      },
      // Settings
      editedItem: {},
      editedIndex: -1,
      showDialog: false,
      data: [],
      currentId: 0,
      // Columns headers
      headers: [
        { text: 'Id', value: 'id' },
        { text: 'Nazwa', value: 'nazwa' },
        { text: 'GatPodstawowy', value: 'gatPodstawowy' },
        { text: 'NumerRysNorma', value: 'numerRysNorma' },
        { text: 'Przypisano komponent', value: 'hasComponent' },

        { text: 'Akcje', value: 'actions' }
      ],
      editMode: false,
      components: [],
      processes: []
    }
  },
  computed: {
    formTitle: function () {
      if (this.editedIndex >= 0) {
        return this.editTitle
      } else {
        return this.newTitle
      }
    }
  },
  watch: {
    showDialog (val) {
      val || this.close()
    }
  },
  methods: {
    editItem (item) {
      this.editMode = true

      this.editedIndex = this.data.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.showDialog = true
    },
    editCurrentProductRes (edited) {
      this.editedItem = edited
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
    close () {
      // if (this.$refs.newPartForm) {
      //   this.$refs.newPartForm.reset()
      // }
      this.editMode = false
      this.editedItem = Object.assign({}, this.defaultItem)
      this.editedIndex = -1
      this.showDialog = false
      // Deletion of form errors
    },
    async deleteProduct (part, index) {
      var res = await this.$dialog.confirm({
        text: 'Czy na pewno chcesz usunąć?',
        title: 'Uwaga'
      })
      if (res) {
        var indexOfPart = this.data.indexOf(part)
        this.$http.delete(this.deleteApi, {
          params: { id: part.id }
        })
          .then(Result => {
            this.data.splice(indexOfPart, 1)
          }
          )
      }
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
            id: 0
          })
          $this.getProcesses()
        })
        .catch((e) => {
          this.tableLoading = false
        })
    },
    save () {
      if (!this.$refs.newPartForm.validate()) {
        return
      }
      // Native form submission is not yet supported
      if (this.editedIndex > -1) {
        this.$http.put(this.putEditApi,
          this.editedItem
        ).then(Result => {
          Object.assign(this.data[this.editedIndex], this.editedItem)
          this.close()
        })
      } else {
        // eslint-disable-next-line no-debugger
        debugger
        this.$http.post(this.postNewApi,
          this.editedItem)
          .then(Result => {
            this.editedItem.id = Result.data.id
            this.data.push(this.editedItem)
            this.close()
          })
      }
    }
  },
  created () {
    this.editedItem = Object.assign({}, this.defaultItem)

    v.axiosInstance.get(this.getAllApi)
      .then(Response => {
        this.data = Response.data
        this.data.forEach(p => {
          if (p.komponent != null) {
            p.hasComponent = true
          } else {
            p.komponent = {}
          }
        })
        this.getComponents()
      })
  },
  destroyed () {
  }
}
</script>

<style>
</style>
