
<template>
  <div>
    <v-layout row wrap justify-space-around elevation-3 width="100%">
      <v-flex xs12>
        <v-toolbar flat color="white">
          <v-toolbar-title>Czesci</v-toolbar-title>
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
            @click="addPartToProduct()"
            v-if="!readonly"
            >Dodaj</v-btn
          >
        </v-toolbar>
        <v-data-table
          :headers="headers"
          :items="currentProduct.produktCzesci"
          :search="search"
        >
          <template v-if="readonly" v-slot:header.actions="{}"> </template>
          <template slot="item" slot-scope="props">
            <tr>
              <template v-for="(header, index) in headers">
                <td :key="index" v-if="header.value == 'actions' && !readonly">
                  <v-layout>
                    <v-flex xs4>
                      <v-icon
                        v-show="props.item.isEdited"
                        @click="editPart(props.item, index)"
                        color="orange"
                        class="mr-2"
                        >check</v-icon
                      >
                      <v-icon
                        v-show="!props.item.isEdited"
                        @click="editPart(props.item, index)"
                        color="green"
                        class="mr-2"
                        >edit</v-icon
                      >
                    </v-flex>
                    <v-flex xs4>
                      <v-icon
                        @click="deletePart(props.item, index)"
                        color="red lighten-1"
                        >delete</v-icon
                      >
                    </v-flex>
                  </v-layout>
                </td>
                <td v-else :key="index">
                  <template v-if="props.item.isEdited && !readonly">
                    <v-autocomplete
                      v-if="header.value == 'czesciId'"
                      color
                      :items="parts"
                      item-text="id"
                      return-object
                      :rules="[requiredRule]"
                      @change="setCzesci(props.item)"
                      v-model="props.item.czesci"
                    ></v-autocomplete>
                    <v-text-field
                      v-else-if="header.value == 'iloscSztuk'"
                      v-model="props.item.iloscSztuk"
                      type="number"
                      :rules="[requiredRule, numberRule, intRule]"
                    ></v-text-field>
                  </template>
                  <template v-else>
                    <template v-if="header.value == 'czescitpz'">
                      {{ props.item.czesci.tpz }}
                    </template>
                    <template v-else-if="header.value == 'czescitj'">
                      {{ props.item.czesci.tj }}
                    </template>
                    <template v-else>
                      {{ props.item[header.value] }}
                    </template>
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
  name: 'ProductParts',
  components: {
  },
  props: {
    currentProduct: Object,
    readonly: Boolean,
    parts: Array
  },
  data () {
    return {
      headers: [
        { text: 'Id części', value: 'czesciId', visible: true },
        { text: 'Ilość sztuk', value: 'iloscSztuk', visible: true },
        { text: 'TPZ', value: 'czescitpz' },
        { text: 'TJ', value: 'czescitj' },
        { text: 'Akcje', value: 'actions', visible: true }
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
      }
    }
  },
  computed: {
  },
  watch: {

  },
  methods: {
    setCzesci (item) {
      // eslint-disable-next-line no-debugger
      debugger
      item.czesciId = item.czesci.id
    },
    addPartToProduct () {
      if (!this.currentProduct.produktCzesci) {
        this.$set(this.currentProduct, 'produktCzesci', [])
      }
      this.currentProduct.produktCzesci.unshift({ czesci: [], czesciId: '', iloscSztuk: '', actions: '', isEdited: true })
    },
    async editPart (part) {
      if (part.isEdited) {
        if (part.czesciId.length === 0 || part.czesciId.iloscSztuk === 0) {
          await this.$dialog.confirm({
            text: 'Uzupełnij dane!'
          })
        } else {
          this.$set(part, 'isEdited', false)
        }
      } else {
        this.$set(part, 'isEdited', true)
      }
    },
    deletePart (part, index) {
      // var res = await this.$dialog.confirm({
      //   text: 'Czy na pewno chcesz usunąć:  ' + part.czesciId + '?',
      //   title: 'Uwaga'
      // })
      // if (res) {
      var indexOfPart = this.currentProduct.produktCzesci.indexOf(part)
      this.currentProduct.produktCzesci.splice(indexOfPart, 1)
    //   }
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
