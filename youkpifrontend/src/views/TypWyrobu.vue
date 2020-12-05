<template>
  <div>
    <v-container>
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
                <v-btn
                  v-on="on"
                  v-bind="attrs"
                  color="primary"
                  dark
                  class="mb-2"
                >
                  Nowy
                </v-btn>
              </template>

              <v-toolbar dark elevation-4 color="primary lighten-1">
                <span class="headline">{{ formTitle }}</span>
              </v-toolbar>
              <v-card>
                <v-form ref="newForm">
                  <v-card-text>
                    <v-container v-if="showDialog" grid-list-md>
                      <v-layout wrap>
                        <v-flex xs12 sm8 offset-sm2>
                          <v-text-field
                            outlined
                            color
                            label="Id"
                            :disabled="editMode"
                            required
                            v-model="editedItem.id"
                          ></v-text-field>
                        </v-flex>
                        <v-flex xs12 sm8 offset-sm2>
                          <v-autocomplete
                            outlined
                            color
                            label="Id grupy wyrobu"
                            :items="['Kołnierzowe', 'Gwintowane', 'Spawane']"
                            required
                            v-model="editedItem.idgrupyWyrobu"
                          ></v-autocomplete>
                        </v-flex>
                        <v-flex xs12 sm8 offset-sm2>
                          <v-autocomplete
                            outlined
                            color
                            label="Typ wkonstrukcji"
                            :items="[
                              'międzykołnierzowe',
                              'dwuczęsciowe',
                              'trzyczęściowa',
                              'regulacyjny',
                              'trójdrogowy',
                            ]"
                            required
                            v-model="editedItem.typKonstrukcji"
                          ></v-autocomplete>
                        </v-flex>
                        <v-flex xs12 sm8 offset-sm2>
                          <v-autocomplete
                            outlined
                            color
                            label="Typ przelotu"
                            :items="['pełny', 'zredukowany']"
                            required
                            v-model="editedItem.typPrzelotu"
                          ></v-autocomplete>
                        </v-flex>
                        <v-flex xs12 sm8 offset-sm2>
                          <v-select
                            outlined
                            color
                            item-text="name"
                            item-value="name"
                            label="Typ kuli"
                            :items="[{ id: 1, name: 'pływajaca' }]"
                            required
                            v-model="editedItem.typKuli"
                          ></v-select>
                        </v-flex>
                      </v-layout>
                    </v-container>
                  </v-card-text>
                </v-form>
                <v-card-actions class="blue lighten-5">
                  <v-btn
                    outlined
                    rounded
                    large
                    color="blue darken-1"
                    text
                    @click="showDialog = false"
                    >Anuluj<v-icon dark>cancel</v-icon></v-btn
                  >
                  <v-spacer></v-spacer>
                  <v-btn
                    outlineed
                    rounded
                    large
                    color="blue darken-1"
                    text
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
                <td class="text-xs-left">{{ props.item.idgrupyWyrobu }}</td>
                <td class="text-xs-left">{{ props.item.typKonstrukcji }}</td>
                <td class="text-xs-left">{{ props.item.typKuli }}</td>
                <td class="text-xs-left">{{ props.item.typPrzelotu }}</td>
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
                        @click="deleteProduct(props.item)"
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
    </v-container>
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
      title: 'Typy wyrobu',
      editTitle: 'Edytuj typ wyrobu',
      newTitle: 'Dodaj  typ wyrobu',
      getAllApi: 'api/ProductTypes/GetAll',
      postNewApi: 'api/ProductTypes/Create',
      putEditApi: 'api/ProductTypes/Update',
      deleteApi: 'api/ProductTypes/Delete',
      defaultItem: {
        id: ''
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
        { text: 'Id grupy wyrobu', value: 'idgrupyWyrobu' },
        { text: 'Typ konstrukcji', value: 'typKonstrukcji' },
        { text: 'Typ kuli', value: 'typKuli' },
        { text: 'Typ przelotu', value: 'typPrzelotu' },
        { text: 'Akcje', value: 'actions' }
      ],
      editMode: false
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
    close () {
      if (this.$refs.newForm) {
        this.$refs.newForm.reset()
      }
      this.editMode = false
      this.editedItem = Object.assign({}, this.defaultItem)
      this.editedIndex = -1
      this.showDialog = false
      // Deletion of form errors
    },
    async deleteProduct (part) {
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
    save () {
      if (!this.$refs.newForm.validate()) {
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
        this.$http.post(this.postNewApi,
          this.editedItem)
          .then(Result => {
            // eslint-disable-next-line no-debugger
            debugger
            this.editedItem.id = Result.data.id
            this.data.push(Result.data)
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
