
<template>
  <div>
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
          <v-toolbar-title>Kompetencje</v-toolbar-title>
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
            @click="addCompetenceToProcess()"
            v-if="!readonly"
            >Dodaj</v-btn
          >
        </v-toolbar>
        <v-data-table
          :headers="headers"
          :items="currentProcess.processCompetences"
          item-key="id"
          :search="search"
          disable-sort
        >
          <template v-if="readonly" v-slot:header.actions="{}"> </template>
          <template slot="item" slot-scope="props">
            <tr :class="props.isExpanded ? 'primary lighten-5' : 'white'">
              <!-- <td>
                {{ props.item.id }}
              </td> -->
              <template v-for="(header, index) in headers">
                <td :key="index" v-if="header.value == 'actions' && !readonly">
                  <v-layout justify-space-between>
                    <v-flex xs4>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            v-on="on"
                            v-show="props.item.isEdited"
                            @click="editCompetence(props.item, index)"
                            color="orange"
                            class="mr-2"
                            >check</v-icon
                          >
                        </template>
                        <span>Akceptuj</span>
                      </v-tooltip>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            v-on="on"
                            v-show="!props.item.isEdited"
                            @click="editCompetence(props.item, index)"
                            color="green"
                            class="mr-2"
                            >edit</v-icon
                          >
                        </template>
                        <span>Edytuj</span>
                      </v-tooltip>
                    </v-flex>
                    <v-flex xs4>
                      <v-tooltip bottom>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            @click="deleteCompetence(props.item, index)"
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
                  <template v-if="props.item.isEdited && !readonly">
                    <v-autocomplete
                      v-if="header.value == 'typ'"
                      v-model="props.item.typ"
                      :rules="[requiredRule]"
                      :items="competencesTypes"
                      item-text="nazwa"
                      item-value="nazwa"
                      color
                    ></v-autocomplete>
                    <v-text-field
                      v-else-if="header.value == 'nazwa'"
                      v-model="props.item.nazwa"
                      :rules="[requiredRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'wartosc'"
                      v-model="props.item.wartosc"
                      :rules="[requiredRule, percentageRule, intRule]"
                      max="100"
                      min="1"
                      type="number"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'cel'"
                      v-model="props.item.cel"
                      type="number"
                      max="100"
                      min="1"
                      :rules="[requiredRule, percentageRule, intRule]"
                    ></v-text-field>
                    <v-autocomplete
                      v-if="header.value == 'poziom'"
                      v-model="props.item.competenceLevelId"
                      :rules="[requiredRule]"
                      :items="competencesLevels"
                      item-text="nazwa"
                      item-value="id"
                      color
                    ></v-autocomplete>
                    <v-autocomplete
                      v-if="header.value == 'okresOceny'"
                      :items="[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]"
                      v-model="props.item.okresOceny"
                      :rules="[requiredRule]"
                    ></v-autocomplete>
                  </template>
                  <template v-else>
                    {{ props.item[header.value] }}
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
  name: 'Steps',
  components: {
  },
  props: {
    currentProcess: Object,
    readonly: Boolean
  },
  data () {
    return {
      // api
      getAllCompetencesTypes: 'api/Competence/GetCompetencesTypes',
      getAllCompetencesLevels: 'api/Competence/GetCompetencesLevels',
      competencesTypes: [],
      competencesLevels: [],
      headers: [
        // { text: 'Id', value: 'stepId', visible: true },
        { text: 'Typ kompetencji', value: 'typ', visible: true },
        { text: 'Kompetencja', value: 'nazwa', visible: true },
        { text: 'Wartość', value: 'wartosc', visible: true },
        { text: 'Cel', value: 'cel', visible: true },
        { text: 'Poziom', value: 'poziom', visible: true },
        { text: 'Okres oceny', value: 'okresOceny', visible: true },
        { text: 'Akcje', value: 'actions', visible: true }
      ],

      search: '',
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      percentageRule: val => {
        if (val < 0 || val > 100) return 'Wprowadź wartość z zakresu 1-100'
        return true
      },
      intRule: val => {
        if (val.includes(',') || val.includes('.')) return 'Wprowadź wartość całkowitą'
        return true
      }
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.getCompetencesTypes()
      this.getCompetencesLevels()
    },
    getCompetencesTypes () {
      var $this = this
      this.$http
        .get(this.getAllCompetencesTypes)
        .then((Response) => {
          $this.competencesTypes = Response.data
        })
        .catch((e) => {
        })
    },
    getCompetencesLevels () {
      var $this = this
      this.$http
        .get(this.getAllCompetencesLevels)
        .then((Response) => {
          $this.competencesLevels = Response.data
        })
        .catch((e) => {
        })
    },
    addCompetenceToProcess () {
      if (!this.currentProcess.processCompetences) {
        this.$set(this.currentProcess, 'processCompetences', [])
      }
      if (!this.currentProcess.id) {
        this.$dialog.confirm({
          text: 'Dodaj id procesu!',
          title: 'Uwaga'
        })
      } else {
        this.currentProcess.processCompetences.unshift(
          {
            id: 0,
            actions: '',
            isEdited: true,
            nazwa: '',
            typ: '',
            wartosc: '',
            cel: '',
            poziom: '',
            competenceLevelId: 0,
            okresOceny: '',
            processesId: this.currentProcess.id
          })
      }
    },
    async editCompetence (competence) {
      if (competence.isEdited) {
        if (competence.nazwa.length === 0 ||
            competence.wartosc.length === 0 ||
            competence.cel.length === 0 ||
            competence.competenceLevelId === 0 ||
            competence.okresOceny.length === 0) {
          await this.$dialog.confirm({
            text: 'Uzupełnij dane!'
          })
        } else {
          this.$set(competence, 'isEdited', false)
        }
      } else {
        this.$set(competence, 'isEdited', true)
      }
    },

    deleteCompetence (process, index) {
      // var res = await this.$dialog.confirm({
      //   text: 'Czy na pewno chcesz usunąć:  ' + process.czesciId + '?',
      //   title: 'Uwaga'
      // })
      // if (res) {
      var indexOfOffer = this.currentProcess.processCompetences.indexOf(process)
      this.currentProcess.processCompetences.splice(indexOfOffer, 1)
    //   }
    }
  },
  created () {
    this.initialise()
  },
  destroyed () {
  }
}
</script>

<style>
</style>
