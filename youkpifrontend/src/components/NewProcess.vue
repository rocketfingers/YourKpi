<template>
  <div>
    <v-layout row wrap justify-space-around>
      <v-flex xs5>
        <v-layout row wrap>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              label="Id"
              :disabled="editMode"
              required
              :rules="[requiredRule]"
              v-model="editedProcess.id"
            ></v-text-field>
          </v-flex>

          <v-flex xs12>
            <v-text-field
              outlined
              :rules="[requiredRule]"
              color
              label="Nazwa grupy procesu"
              v-model="editedProcess.nazwaGrupyProcesu"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Business area"
              v-model="editedProcess.businessArea"
            ></v-text-field>
          </v-flex>
        </v-layout>
      </v-flex>
      <v-flex xs1> </v-flex>
      <v-flex xs5>
        <v-layout row wrap>
          <v-flex xs12>
            <v-text-field
              outlined
              color
              :rules="[requiredRule]"
              label="Nazwa procesu"
              v-model="editedProcess.nazwaProcesu"
            ></v-text-field>
          </v-flex>
          <v-spacer></v-spacer>

          <v-flex xs12>
            <v-autocomplete
              outlined
              color
              :rules="[requiredRule]"
              label="Typ zlecenia"
              :items="['O', 'P', 'R']"
              return-object
              required
              v-model="editedProcess.typZlecenia"
            ></v-autocomplete>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex xs12>
        <Steps :currentProcess="editedProcess" :readonly="false"> </Steps>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import Steps from '../components/Steps'

export default {
  name: 'NewProduct',
  components: {
    Steps: Steps
  },
  props: {
    editedProcess: Object,
    editMode: Boolean

  },
  data () {
    return {
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadź wartość dodatnią'
        return true
      }
    }
  },
  computed: {
  },
  watch: {
    editedProcess (val) {
      this.$emit('editedProcess', this.editedProcess)
    }
  },
  methods: {
    // editOfferProcesses (offer, selectedProcesses) {
    //   if (this.editedProcess) {
    //     if (this.editedProcess.id === offer.id) {
    //       this.editedProcess.offerProcess = []
    //       selectedProcesses.forEach(sp => {
    //         var item = {
    //           offerId: this.editedProcess.id,
    //           processId: sp.id
    //         }
    //         this.editedProcess.offerProcess.push(item)
    //       })
    //     }
    //   }
    // }
  },
  created () {
  },
  destroyed () {
  }
}
</script>

<style>
</style>
