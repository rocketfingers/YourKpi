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
                  :rules="[requiredRule]"
                  label="Id"
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field
                  v-model="editedItem.nazwa"
                  outlined
                  :disabled="readonly"
                  :rules="[requiredRule]"
                  label="Nazwa"
                ></v-text-field>
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
          :items="components"
          item-text="showName"
          item-value="id"
          required
          v-if="!hideComponent"
          clearable
          :disabled="readonly"
          v-model="editedItem.komponentId"
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
      :editMode="editMode"
      :processes="processes"
      @editedProduct="editCurrentProductRes"
      :notValidate="notValidateComponent"
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
    processes: Array
  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadz dodatnia wartosc'
        return true
      },
      showComponent: true,
      notValidateComponent: false
    }
  },
  computed: {
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
