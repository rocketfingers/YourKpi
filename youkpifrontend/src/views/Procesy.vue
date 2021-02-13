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
        <v-layout row wrap justify-space-around class="ma-4">
          <v-flex xs11>
            <v-form ref="newProcessForm">
              <NewProcess
                :editedProcess="editedProcess"
                :editMode="editMode"
                :processes="items"
                @editedProcess="editedProcessRes"
              ></NewProcess>
            </v-form>
          </v-flex>
        </v-layout>
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
            @click.native="saveProcessAction"
          >
            Zapisz
            <v-icon dark>save</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-layout row wrap elevation-3>
      <v-flex xs12>
        <v-toolbar flat color="white">
          <v-toolbar-title>Księga procesów</v-toolbar-title>
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
          :expanded.sync="expanded"
          item-key="id"
          :loading="tableLoading"
          :search="search"
        >
          <template slot="item" slot-scope="props">
            <tr :class="props.isExpanded ? 'primary lighten-5' : 'white'">
              <template v-for="(header, index) in headers">
                <td :key="index" v-if="header.value === 'expand'">
                  <v-icon @click="expandRow(props)" v-show="!props.isExpanded"
                    >fa-arrow-down</v-icon
                  >
                  <v-icon @click="expandRow(props)" v-show="props.isExpanded"
                    >fa-arrow-up</v-icon
                  >
                </td>
                <td :key="index" v-else-if="header.value === 'actions'">
                  <v-layout justify-space-between>
                    <v-flex xs4>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            v-on="on"
                            @click="editProcess(props.item, index)"
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
                            @click="deleteProcess(props.item, index)"
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
          <template v-slot:expanded-item="{ headers, item }">
            <td :colspan="headers.length">
              <v-layout justify-space-around>
                <v-flex xs11>
                  <Steps :currentProcess="item" :readonly="true"> </Steps>
                </v-flex>
              </v-layout>
            </td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import Steps from '../components/Steps'
import NewProcess from '../components/NewProcess'

export default {
  name: 'Produkty',
  components: {
    Steps: Steps,
    NewProcess: NewProcess
  },
  props: {},
  data () {
    return {
      // api
      addApi: 'api/Process/Create',
      getAllProcesses: 'api/Process/GetAll',
      deleteProcessApi: 'api/Process/Delete',
      editProcessApi: 'api/Process/Update',

      expanded: [],
      headers: [
        { text: 'Rozwiń', value: 'expand', visible: true },
        { text: 'Id', value: 'id', visible: true },
        { text: 'Nazwa grupy procesu', value: 'nazwaGrupyProcesu', visible: true },
        { text: 'Business area', value: 'businessArea', visible: true },
        { text: 'Nazwa procesu', value: 'nazwaProcesu', visible: true },
        { text: 'Typ zlecenia', value: 'typZlecenia', visible: true },
        { text: 'Akcje', value: 'actions', visible: true }

      ],
      items: [],
      search: '',
      showNewDialog: false,
      tableLoading: false,
      title: 'Dodaj proces',
      editedProcess: { steps: [] },
      editedIndex: -1,
      editMode: false
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      this.getProcesses()
    },
    getProcesses () {
      var $this = this
      this.$http
        .get(this.getAllProcesses)
        .then((Response) => {
          $this.items = Response.data
          $this.items.forEach(p => {
            p.showName = p.id + ', ' + p.nazwaProcesu
          })
          this.tableLoading = false
        })
        .catch((e) => {
          this.tableLoading = false
        })
    },
    expandRow (item) {
      item.expand(!item.isExpanded)
    },
    saveProcessAction () {
      if (!this.$refs.newProcessForm.validate()) {
        return
      }
      if (this.editedProcess.steps.length < 10) {
        this.$dialog.confirm({
          text: 'Dodaj minimum 10 kroków do procesu!',
          title: 'Uwaga'
        })
      } else {
        if (this.editedIndex > 0) {
          this.editoProcessAction(this.editedProcess)
        } else {
          this.addAction(this.editedProcess)
        }
        this.showNewDialog = false
      }
    },
    add () {
      // if (this.$refs.newProcessForm) { //powoduje blad w dodawaniu zagniezdzonych obiektow, vue jakby traci referencje
      //   this.$refs.newProcessForm.reset()
      // }
      this.editMode = false
      this.title = 'Dodaj proces'
      this.editedProcess = { steps: [] }
      this.editedIndex = -1
      this.showNewDialog = true
    },
    addAction (process) {
      var $this = this
      this.$http
        .post(this.addApi, process)
        .then((Result) => {
          process.id = Result.data.id
          $this.items.unshift(process)
          $this.initialise()
        })
        .catch((e) => {
        })
    },
    editoProcessAction (process) {
      this.$http
        .put(this.editProcessApi, process)
        .then((Result) => {
          this.initialise()
        })
        .catch((e) => {})
    },

    editProcess (process, index) {
      this.title = 'Edytuj process ' + process.id
      this.editMode = true
      this.editedIndex = index
      this.showNewDialog = true
      this.editedProcess = process
    },
    editedProcessRes (editedProcess) {
      this.editedProcess = editedProcess
    },

    async deleteProcess (process, index) {
      var res = await this.$dialog.confirm({
        text: 'Czy na pewno chcesz usunąć?',
        title: 'Uwaga'
      })
      if (res) {
        var indexOfProcess = this.items.indexOf(process)
        this.$http.delete(this.deleteProcessApi, {
          params: { id: process.id }
        })
          .then(Result => {
            this.items.splice(indexOfProcess, 1)
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
