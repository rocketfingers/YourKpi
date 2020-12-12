
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
          <v-toolbar-title>Offer Lines</v-toolbar-title>
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
            @click="addOfferLineToOffer()"
            v-if="!readonly"
            >Dodaj</v-btn
          >
        </v-toolbar>
        <v-data-table
          :headers="headers"
          :items="currentOffer.offerLines"
          :search="search"
          :hide-default-footer="readonly"
          calculate-widths
        >
          <template v-if="readonly" v-slot:header.actions="{}"> </template>
          <template slot="item" slot-scope="props">
            <tr>
              <template v-for="(header, index) in headers">
                <td :key="index" v-if="header.value == 'actions' && !readonly">
                  <v-layout justify-space-between>
                    <v-flex xs4>
                      <v-icon
                        v-show="props.item.isEdited"
                        @click="editOffer(props.item, index)"
                        color="orange"
                        class="mr-2"
                        >check</v-icon
                      >
                      <v-icon
                        v-show="!props.item.isEdited"
                        @click="editOffer(props.item, index)"
                        color="green"
                        class="mr-2"
                        >edit</v-icon
                      >
                    </v-flex>
                    <v-flex xs4>
                      <v-icon
                        @click="deleteOffer(props.item, index)"
                        color="red lighten-1"
                        >delete</v-icon
                      >
                    </v-flex>
                  </v-layout>
                </td>
                <td v-else :key="index">
                  <template v-if="props.item.isEdited && !readonly">
                    <v-autocomplete
                      v-if="header.value == 'productId'"
                      color
                      :items="products"
                      item-text="id"
                      item-value="id"
                      :rules="[requiredRule]"
                      v-model="props.item.productId"
                    ></v-autocomplete>
                    <v-text-field
                      v-else-if="header.value == 'quantity'"
                      v-model="props.item.quantity"
                      type="number"
                      :rules="[requiredRule, numberRule, intRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'w'"
                      v-model="props.item.w"
                      :rules="[requiredRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'medium'"
                      v-model="props.item.medium"
                      :rules="[requiredRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'additionalEquipment'"
                      v-model="props.item.additionalEquipment"
                      :rules="[requiredRule]"
                    ></v-text-field>
                    <v-text-field
                      v-else-if="header.value == 'sale'"
                      v-model="props.item.sale"
                      :rules="[requiredRule]"
                    ></v-text-field>
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
  name: 'Productoffers',
  components: {
  },
  props: {
    currentOffer: Object,
    readonly: Boolean,
    products: Array
  },
  data () {
    return {
      headers: [
        // { text: 'Id', value: 'id', visible: true },
        { text: 'ProductID', value: 'productId', visible: true },
        { text: 'Ilość', value: 'quantity', visible: true },
        { text: 'W', value: 'w', visible: true },
        { text: 'Medium', value: 'medium', visible: true },
        { text: 'Dodatkowe wyposażenie', value: 'additionalEquipment', visible: true },
        { text: 'Sprzedaź', value: 'sale', visible: true },
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
    addOfferLineToOffer () {
      if (!this.currentOffer.offerLines) {
        this.$set(this.currentOffer, 'offerLines', [])
      }
      this.currentOffer.offerLines.unshift({ id: 0, productId: '', quantity: '', w: '', medium: '', additionalEquipment: '', sale: '', actions: '', isEdited: true })
    },
    async editOffer (offer) {
      if (offer.isEdited) {
        if (offer.productId.length === 0 || offer.quantity === 0 || offer.w.length === 0 || offer.medium.length === 0 || offer.additionalEquipment.length === 0 || offer.sale === 0) {
          await this.$dialog.confirm({
            text: 'Uzupełnij dane!'
          })
        } else {
          this.$set(offer, 'isEdited', false)
        }
      } else {
        this.$set(offer, 'isEdited', true)
      }
    },
    deleteOffer (offer, index) {
      // var res = await this.$dialog.confirm({
      //   text: 'Czy na pewno chcesz usunąć:  ' + offer.czesciId + '?',
      //   title: 'Uwaga'
      // })
      // if (res) {
      var indexOfOffer = this.currentOffer.offerLines.indexOf(offer)
      this.currentOffer.offerLines.splice(indexOfOffer, 1)
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
