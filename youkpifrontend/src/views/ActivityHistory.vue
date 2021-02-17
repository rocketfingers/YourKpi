<template>
  <div>
    <v-layout row wrap elevation-3>
      <v-flex xs12>
        <v-toolbar flat color="white">
          <v-toolbar-title>Historia aktywności</v-toolbar-title>
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
                <td :key="index" v-if="header.value === 'id'">
                  {{ id }}
                </td>
                <td :key="index" v-else>
                  {{ props.item[header.value] }}
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
import * as v from '../main.js'
export default {
  name: 'ActityHistory',
  components: {
  },
  props: {},
  data () {
    return {
      // api
      getAllProcesses: 'api/Production/GetActivityHistory',

      headers: [
        { text: 'Imię i nazwisko', value: 'pracownikName', visible: true },
        { text: 'Id procesu', value: 'processId', visible: true },
        { text: 'Dzień', value: 'day', visible: true },
        { text: 'Nazwa procesu', value: 'processName', visible: true },
        { text: 'Spędzony czas', value: 'timeSpend', visible: true },
        { text: 'Id klienta', value: 'clientId' },
        { text: 'Klient', value: 'client' },
        { text: 'Id produktu', value: 'wyrobId' },
        { text: 'Id części', value: 'partId' },
        { text: 'Nazwa części', value: 'partName' },
        { text: 'Ilość części', value: 'iloscCzesci' }

      ],
      items: [],
      tableLoading: false
    }
  },
  computed: {},
  watch: {},
  methods: {
    initialise () {
      this.tableLoading = true
      v.axiosInstance.get(this.getAllProcesses)
        .then(res => {
          this.items = res.data
          this.tableLoading = false
        })
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
