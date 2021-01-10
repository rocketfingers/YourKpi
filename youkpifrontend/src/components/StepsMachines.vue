
<template>
  <div>
    <v-dialog v-model="showAddMachineDialog" max-width="1600" persistent>
      <v-card>
        <v-toolbar dark elevation-4 color="primary lighten-1">
          <span class="headline">Wybierz maszynę</span>
          <v-spacer></v-spacer>
          <v-btn icon @click="showAddMachineDialog = false" dark>
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-layout row wrap justify-space-around class="ma-4">
          <v-flex xs11>
            <v-form>
              <v-autocomplete
                color
                :items="machines"
                item-text="showName"
                return-object
                v-model="selectedMachine"
              ></v-autocomplete>
            </v-form>
          </v-flex>
        </v-layout>
        <v-card-actions class="blue lighten-5">
          <v-spacer></v-spacer>
          <v-btn
            flat
            large
            color="blue darken-1"
            @click.native="addMachineAction"
          >
            Dodaj
            <v-icon dark>save</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-layout
      row
      wrap
      justify-space-around
      elevation-3
      width="100%"
      class="mb-5"
    >
      <v-flex xs12>
        <v-toolbar flat color="white" v-if="!readonly">
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
          <v-btn
            color="primary"
            dark
            class="mb-2"
            @click="addMachine()"
            v-if="!readonly"
            >Dodaj</v-btn
          >
        </v-toolbar>
        <v-data-table
          :headers="headers"
          :items="currentStep.stepsMachines"
          item-key="tempId"
          :search="search"
          :hide-default-footer="readonly"
          calculate-widths
        >
          <template slot="item" slot-scope="props">
            <tr>
              <template v-for="(header, index) in headers">
                <td :key="index" v-if="header.value == 'actions' && !readonly">
                  <v-layout justify-space-between>
                    <v-flex xs12>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            @click="deleteMachine(props.item, index)"
                            v-on="on"
                            color="red lighten-1"
                            >delete</v-icon
                          >
                        </template>
                        <span>Usuń</span>
                      </v-tooltip>
                    </v-flex>
                  </v-layout>
                </td>
                <td v-else :key="index">
                  <template>
                    {{ props.item.machine[header.value] }}
                  </template>
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

export default {
  name: 'StepsMachines',
  components: {
  },
  props: {
    readonly: Boolean,
    machines: Array,
    currentStep: Object
  },
  data () {
    return {
      // api
      addStepsMachineApi: 'api/StepsMachines/Create',
      deleteStepsMachineApi: 'api/StepsMachines/Delete',

      headers: [
        { text: 'Id', value: 'id' },
        { text: 'Nazwa', value: 'nazwa' },
        { text: 'Rodzaj', value: 'rodzaj' },
        { text: 'Opis', value: 'opis' },
        { text: 'Tryb pracy', value: 'trybPracy' },
        { text: 'Os X', value: 'osX' },
        { text: 'Os Y', value: 'osY' },
        { text: 'Os Z', value: 'osZ' },
        { text: 'Miejsce pracy', value: 'miejscePracy' },
        { text: 'Akcje', value: 'actions' }
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
      showAddMachineDialog: false,
      processesSelectorTitle: 'Wybierz procesy',
      selectedMachine: {}
    }
  },
  computed: {
  },
  watch: {
  },
  methods: {
    addMachine () {
      // eslint-disable-next-line no-debugger
      // debugger
      this.selectedMachine = ''
      this.showAddMachineDialog = true
    },
    async deleteMachine (stepMachine, index) {
      var res = await this.$dialog.confirm({
        text: 'Czy na pewno chcesz usunąć:  ' + stepMachine.machine.id + ', ' + stepMachine.machine.nazwa + '?',
        title: 'Uwaga'
      })
      if (res) {
        var indexOfMachine = this.currentStep.stepsMachines.indexOf(stepMachine)

        this.$http.delete(this.deleteStepsMachineApi, {
          params: { id: stepMachine.id }
        })
          .then(Result => {
            this.currentStep.stepsMachines.splice(indexOfMachine, 1)
          })
      }
    },
    addMachineAction () {
      var $this = this
      if (!this.selectedMachine.showName) {
        this.$dialog.confirm({
          text: 'Nie wybrano maszyny',
          title: 'Uwaga'
        })
      }
      // eslint-disable-next-line no-debugger
      // debugger
      var stepMachineObj = { machineId: $this.selectedMachine.id, stepId: this.currentStep.id }
      this.$http
        .post(this.addStepsMachineApi, stepMachineObj)
        .then((Result) => {
          stepMachineObj.id = Result.data.id
          stepMachineObj.machine = $this.selectedMachine
          stepMachineObj.machine.showName = stepMachineObj.machine.id + stepMachineObj.machine.nazwa
          stepMachineObj.machine.actions = ''
          $this.currentStep.stepsMachines.unshift(stepMachineObj)
        })
        .catch((e) => {
        })
      this.showAddMachineDialog = false
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
