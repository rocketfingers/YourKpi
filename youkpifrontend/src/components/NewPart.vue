<template>
  <div>
    <v-card-text>
      <v-container grid-list-md>
        <v-layout row wrap justify-space-around>
          <v-flex xs5>
            <v-layout row wrap>
              <v-flex xs12>
                <v-text-field
                  v-model="editedItem.id"
                  outlined
                  :disabled="editMode || readonly"
                  :rules="[requiredRule, isItemAlreadyExists]"
                  label="Id"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  v-model="editedItem.nazwa"
                  ref="nazwaInput"
                  outlined
                  v-show="!showNazwaAutocomplete"
                  :disabled="readonly"
                  :rules="[requiredRule]"
                  label="Nazwa"
                  autocomplete
                >
                  <template slot="append">
                    <v-tooltip bottom>
                      <template v-slot:activator="{ on }">
                        <v-icon v-on="on" @click="foncusOnNazwaAutocomplete"
                          >fa-question</v-icon
                        >
                      </template>
                      <span>Sugestia</span>
                    </v-tooltip>
                  </template>
                </v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-autocomplete
                  label="Nazwa"
                  ref="nazwaAutocomplete"
                  outlined
                  :items="parts"
                  :rules="[requiredRule]"
                  item-value="nazwa"
                  autocomplete
                  v-show="showNazwaAutocomplete"
                  @input="foncusOnNazwaInput"
                  item-text="nazwa"
                  v-model="editedItem.nazwa"
                ></v-autocomplete>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  v-model="editedItem.tpz"
                  outlined
                  :disabled="readonly"
                  :rules="[requiredRule, tpztjRule]"
                  type="number"
                  min="0"
                  label="TPZ"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  v-model="editedItem.numerProgramu"
                  outlined
                  :disabled="readonly"
                  counter="10"
                  maxlength="10"
                  label="Numer programu"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-autocomplete
                  label="Wykonanie"
                  outlined
                  :items="['Własne', 'Outsourcing', 'Zakup']"
                  autocomplete
                  v-model="editedItem.wykonanie"
                ></v-autocomplete>
              </v-flex>
            </v-layout>
          </v-flex>
          <v-flex xs1> </v-flex>
          <v-flex xs5>
            <v-layout row wrap>
              <v-flex xs12>
                <v-text-field
                  v-model="editedItem.gatPodstawowy"
                  outlined
                  :disabled="readonly"
                  :rules="[requiredRule]"
                  label="Gatunek podstawowy"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  v-model="editedItem.numerRysNorma"
                  outlined
                  :disabled="readonly"
                  :rules="[requiredRule]"
                  label="Numer rysunku norma"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  v-model="editedItem.tj"
                  outlined
                  :disabled="readonly"
                  :rules="[requiredRule, tpztjRule]"
                  type="number"
                  min="0"
                  label="TJ"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  v-model="editedItem.czasProgramisty"
                  outlined
                  :disabled="readonly"
                  :rules="[czasProgramistyRule]"
                  type="number"
                  min="1"
                  max="100"
                  label="Czas programisty"
                ></v-text-field>
              </v-flex>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card-text>
    <v-divider> </v-divider>

    <v-layout dark color="grey" justify-space-around ma-2>
      <v-flex xs6>
        <!-- <v-checkbox
          v-model="showComponent"
          label="Pokaż komponent"
        ></v-checkbox> -->
        <v-autocomplete
          outlined
          color
          label="Komponent"
          :items="componentsToAutocomplete"
          item-text="showName"
          item-value="id"
          required
          v-if="!hideComponent"
          clearable
          :disabled="readonly"
          return-object
          @change="changeComponent"
          v-model="editedItem.komponent"
        ></v-autocomplete>
      </v-flex>
      <!-- <v-flex xs1>
        <v-tooltip bottom>
          <template v-slot:activator="{ on }">
            <v-btn
              flat
              large
              v-on="on"
              fab
              v-show="!readonly"
              color="blue lighten-4"
              @click.native="addNewComponent()"
            >
              <v-icon dark>add</v-icon>
            </v-btn>
          </template>
          <span>Dodaj nowy komponent</span>
        </v-tooltip>
      </v-flex> -->
    </v-layout>
    <!-- v-show="
        !readonly && (showComponent || editedItem.komponentId.length <= 0)
      " -->
    <NewComponent
      v-show="!readonly"
      :currentItem="editedItem.komponent"
      :editMode="editedItem.komponent.isNewFlag !== true"
      :processes="processes"
      @editedProduct="editCurrentProductRes"
      :notValidate="notValidateComponent"
      :components="components"
      :validateId="editedItem.komponent.isNewFlag === true"
    ></NewComponent>
  </div>
</template>

<script>
import NewComponent from '../components/NewComponent'

export default {
  name: 'NewPart',
  components: {
    NewComponent: NewComponent
  },
  props: {
    editedItem: Object,
    editMode: Boolean,
    components: Array,
    hideComponent: Boolean,
    readonly: Boolean,
    processes: Array,
    parts: Array
  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadz dodatnia wartosc'
        return true
      },
      tpztjRule: val => {
        if (val < 0) return 'Wprowadz dodatnia wartosc'
        if (val > 200) return 'Maksymalna wartość: 200'
        return true
      },
      czasProgramistyRule: val => {
        if (val < 1) return 'Wprowadz wartość większą od 1'
        if (val > 100) return 'Maksymalna wartość: 100'
        return true
      },
      isItemAlreadyExists: (v) => (!(this.parts.some(p => p.id.toUpperCase().trim() === v.toUpperCase().trim())) || this.notValidate || this.editMode) || 'Część o tym id już istnieje!!',
      showComponent: true,
      notValidateComponent: false,
      showNazwaAutocomplete: true
    }
  },
  computed: {
    componentsToAutocomplete (val) {
      var $this = this
      var cmps = this.components.filter(c => {
        if (c.czesci) {
          if (c.czesci.length > 0 && $this.editedItem.komponentId !== c.id) {
            return false
          }
        }
        return true
      })
      return cmps
    }
  },
  watch: {
    editedItem (val) {
      if (val.komponentId) {
        this.notValidateComponent = true
      } else {
        this.notValidateComponent = false
      }
      this.$emit('editedProduct', this.editedItem)
    }

  },
  methods: {
    editCurrentProductRes (editedProduct) {
      this.editedItem.komponent = editedProduct
    },
    changeComponent () {
      if (this.editedItem.komponent) {
        this.editedItem.komponentId = this.editedItem.komponent.id
      }
    },
    foncusOnNazwaInput () {
      this.showNazwaAutocomplete = false
      this.$refs.nazwaInput.focus()
    },
    foncusOnNazwaAutocomplete () {
      this.showNazwaAutocomplete = true
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
