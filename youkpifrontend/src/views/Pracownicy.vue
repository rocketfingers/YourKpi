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
              <v-form ref="newForm">
                <v-card-text>
                  <v-container>
                    <v-layout row wrap v-if="showDialog">
                      <v-flex xs12 md5>
                        <v-layout wrap>
                          <v-flex xs12>
                            <v-text-field
                              outlined
                              color
                              label="Imię i nazwisko"
                              required
                              v-model="editedItem.name"
                            ></v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-text-field
                              :disabled="editMode"
                              outlined
                              color
                              label="Login"
                              required
                              v-model="editedItem.login"
                            ></v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-text-field
                              type="number"
                              min="1"
                              outlined
                              color
                              label="Tryb pracy"
                              required
                              v-model="editedItem.trybPracy"
                            ></v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-text-field
                              type="number"
                              min="1"
                              outlined
                              color
                              label="Czas dostępu"
                              required
                              v-model="editedItem.czasDostepuMin"
                            ></v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-text-field
                              outlined
                              color
                              label="Mail"
                              :disabled="editMode"
                              required
                              v-model="editedItem.email"
                            ></v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-checkbox
                              label="Administrator"
                              v-model="editedItem.isAdministrator"
                            ></v-checkbox>
                          </v-flex>
                        </v-layout>
                      </v-flex>
                      <v-spacer></v-spacer>
                      <v-flex xs12 md5>
                        <v-layout row wrap>
                          <v-flex xs12>
                            <v-text-field
                              outlined
                              color
                              label="Stawka miesięczna"
                              type="number"
                              :min="0"
                              v-model="editedItem.stawkaMiesieczna"
                            >
                              <template slot="append">
                                <strong>PLN</strong>
                              </template>
                            </v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-text-field
                              outlined
                              color
                              label="Stawka godzinowa"
                              type="number"
                              :min="0"
                              v-model="editedItem.stawkaGodzinowa"
                            >
                              <template slot="append">
                                <strong>PLN</strong>
                              </template>
                            </v-text-field>
                          </v-flex>
                          <v-flex xs12 v-if="editedIndex >= 0">
                            <v-layout row wrap>
                              <v-flex xs7>
                                <v-text-field
                                  v-model="password"
                                  :append-icon="
                                    show1 ? 'mdi-eye' : 'mdi-eye-off'
                                  "
                                  :type="show1 ? 'text' : 'password'"
                                  name="input-10-1"
                                  label="Nowe hasło"
                                  hint="Przynajmniej 8 znaków"
                                  counter
                                  @click:append="showPassword = !showPassword"
                                ></v-text-field>
                              </v-flex>
                              <v-spacer></v-spacer>
                              <v-flex xs4>
                                <v-btn
                                  round
                                  color="primary"
                                  @click="changePassword"
                                  >zmiana hasła</v-btn
                                >
                              </v-flex>
                            </v-layout>
                          </v-flex>
                        </v-layout>
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
              <td class="text-xs-left">{{ props.item.name }}</td>
              <td class="text-xs-left">{{ props.item.login }}</td>
              <td class="text-xs-left">{{ props.item.trybPracy }}</td>
              <td class="text-xs-left">{{ props.item.czasDostepuMin }}</td>
              <td class="text-xs-left">{{ props.item.email }}</td>
              <td class="text-xs-left">
                <b>
                  {{ props.item.stawkaMiesieczna }}
                </b>
                PLN
              </td>
              <td class="text-xs-left">
                <b>{{ props.item.stawkaGodzinowa }} </b> PLN
              </td>

              <td>
                <v-checkbox
                  label=""
                  v-model="props.item.isAdministrator"
                  disabled
                ></v-checkbox>
              </td>

              <td class="justify-center px-0">
                <v-layout>
                  <v-flex xs4>
                    <v-icon
                      class="mr-2"
                      @click="openUserProcessDialog(props.item)"
                      color="primary lighten-1"
                      >fa-universal-access</v-icon
                    >
                  </v-flex>
                  <v-flex xs4>
                    <v-icon
                      class="mr-2"
                      color="green"
                      @click="editItem(props.item)"
                    >
                      edit
                    </v-icon>
                  </v-flex>
                  <v-flex xs4>
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
    <v-dialog
      v-model="showDialogUserProcess"
      v-if="showDialogUserProcess"
      width="auto"
      max-width="none"
    >
      <v-toolbar dark elevation-4 color="primary lighten-1">
        <span class="headline">Processy użytkownika {{ editedItem.name }}</span>
      </v-toolbar>
      <v-card>
        <v-form ref="newForm">
          <v-card-text>
            <UserProcess :userId="editedItem.id"> </UserProcess>
          </v-card-text>
        </v-form>
        <v-card-actions class="blue lighten-5">
          <v-btn
            outlined
            rounded
            large
            color="blue darken-1"
            text
            @click="showDialogUserProcess = false"
            >Wyjdź<v-icon dark>cancel</v-icon></v-btn
          >
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import * as v from '../main.js'
import UserProcess from '../components/UserProcess'
export default {
  name: 'Pracowncy',
  components: {
    UserProcess: UserProcess
  },
  props: {
  },
  data () {
    return {
      // password
      password: '',
      showPassword: false,
      // Settings
      title: 'Pracownicy',
      editTitle: 'Edytuj pracownikat',
      newTitle: 'Dodaj  pracownika',
      getAllApi: 'api/Users/GetAll',
      postNewApi: 'api/Users/Create',
      putEditApi: 'api/Users/Update',
      deleteApi: 'api/Users/Delete',
      uriUserChangePassword: 'api/Users/UserChangePassword',

      defaultItem: {
        id: 0
      },
      // Settings
      editedItem: {},
      editedIndex: -1,
      showDialog: false,
      data: [],
      currentId: 0,
      // dialogProcesy
      showDialogUserProcess: false,
      // Columns headers
      headers: [
        { text: 'Id', value: 'id' },
        { text: 'Imię i nazwisko', value: 'name' },
        { text: 'Login', value: 'login' },
        { text: 'Tryb pracy', value: 'trybPracy' },
        { text: 'Czas dostepu(min)', value: 'czasDostepuMin' },
        { text: 'Mail', value: 'email' },
        { text: 'Stawka miesięczna', value: 'stawkaMiesieczna' },
        { text: 'Stawka godzinowa', value: 'stawkaGodzinowa' },
        { text: 'Administrator', value: 'isAdministrator' },
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
    changePassword () {
      if (this.password.length < 8) {
        this.$dialog.error({
          text: 'Nowe hasło musi mieć przynajmniej 8 znakó!',
          title: 'Error'
        })
        return
      }
      v.axiosInstance.post(this.uriUserChangePassword, { userId: this.editedItem.Id, newPassword: this.password })
        .then(
          this.$dialog.info({
            text: 'Hasło zostało zmienione',
            title: 'Info'
          })
        )
        .finally(
          this.showDialog = false
        )
    },
    openUserProcessDialog (item) {
      this.editedItem = Object.assign({}, item)
      this.showDialogUserProcess = true
    },
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
        v.axiosInstance.delete(this.deleteApi, {
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
