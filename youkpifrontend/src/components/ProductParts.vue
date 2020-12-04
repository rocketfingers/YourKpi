
<template>
  <div>
    <v-layout row wrap justify-space-around elevation-3 width="100%">
      <v-data-table :headers="headers" :items="currentProduct.produktCzesci">
        <template slot="item" slot-scope="props">
          <tr>
            <template v-for="(header, index) in headers">
              <td :key="index">
                {{ props.item[header.value] }}
              </td>
            </template>
          </tr>
        </template>
      </v-data-table>
    </v-layout>
  </div>
</template>

<script>
export default {
  name: 'ProductParts',
  components: {
  },
  props: {
    currentProduct: Object
  },
  data () {
    return {
      headers: [
        { text: 'Id czesci', value: 'czesciId' },
        { text: 'Ilosc sztuk', value: 'iloscSztuk' }]
    }
  },
  computed: {
  },
  watch: {

  },
  methods: {
  },
  created () {
  },
  destroyed () {
  }
}
</script>

<style>
</style>
