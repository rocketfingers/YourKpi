<template>
  <div>
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

        <v-card>
          <v-card-title>
            <span class="headline">{{ formTitle }}</span>
          </v-card-title>
          <v-form ref="form">
            <v-card-text>
              <v-container grid-list-md>
                <v-layout wrap>
                  <v-flex xs12 sm8 offset-sm2>
                    <v-text-field
                      v-model="editedItem.Id"
                      label="Id"
                    ></v-text-field>
                  </v-flex>
                  <v-flex xs12 sm8 offset-sm2>
                    <v-text-field
                      v-model="editedItem.nazwa"
                      label="Nazwa"
                    ></v-text-field>
                  </v-flex>
                  <v-flex xs12 sm8 offset-sm2>
                    <v-text-field
                      v-model="editedItem.gatPodstawowy"
                      label="Gatunek podstawowy"
                    ></v-text-field>
                  </v-flex>
                  <v-flex xs12 sm8 offset-sm2>
                    <v-text-field
                      v-model="editedItem.numerRysNorma"
                      label="Numer rysunku norma"
                    ></v-text-field>
                  </v-flex>
                </v-layout>
              </v-container>
            </v-card-text>
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
          <td class="justify-center px-0">
            <v-icon small class="mr-2" @click="editItem(props.item)">
              edit
            </v-icon>
            <v-icon
              @click="deleteProduct(props.item, index)"
              color="red lighten-1"
              >delete</v-icon
            >
          </td>
        </tr>

        <tr></tr
      ></template>
    </v-data-table>
  </div>
</template>

<script>
import * as v from '../main.js'
export default {
  name: '',
  components: {
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
      postNewApi: 'api/Parts/Create',
      putEditApi: 'api/Parts/Update',
      deleteApi: 'api/Parts/Delete',
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
        { text: 'Akcje', value: 'actions' }
      ]
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
      this.editedIndex = this.data.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.showDialog = true
    },
    close () {
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
        this.$http.delete(this.deleteApi, part)

        var indexOfPart = this.data.indexOf(part)
        this.data.splice(indexOfPart, 1)
      }
    },
    save () {
      // Native form submission is not yet supported
      if (this.editedIndex > -1) {
        this.$http.put(this.putEditApi,
          this.editedItem
        ).then(Result => {
          Object.assign(this.data[this.editedIndex], this.editedItem)
          this.close()
        })
      } else {
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
      .then(Response => { this.data = Response.data; this.tableLoading = false })
  },
  destroyed () {
  }
}
</script>

<style>
</style>
