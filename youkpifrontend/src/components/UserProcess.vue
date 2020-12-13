<template>
  <v-layout class="elevation-1">
    <v-flex xs12>
      <v-toolbar flat color="white">
        <v-toolbar-title>Procesy</v-toolbar-title>
        <v-divider class="mx-2" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-text-field
          append-icon="search"
          label="Szukaj"
          single-line
          hide-details
          class="elevation-1"
          v-model="searchTable"
        ></v-text-field>
      </v-toolbar>
      <v-data-table
        :headers="headers"
        :items="data"
        :search="searchTable"
        :loading="tableLoading"
        class="elevation-2"
      >
        <template slot="item" slot-scope="props">
          <tr>
            <template v-for="header in headers">
              <td
                class="checkboxRole"
                :key="header.value"
                v-if="header.value === 'userAssigned'"
              >
                <v-checkbox
                  @change="change(props.item)"
                  v-model="props.item.userAssigned"
                ></v-checkbox>
              </td>
              <td :key="header.value" v-else>
                {{ props.item[header.value] }}
              </td>
            </template>
          </tr>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>

<script>
import * as v from '../main'
export default {
  name: 'UserProcess',
  components: {
  },
  props: { userId: Number },
  data () {
    return {
      data: [],
      headers: [
        {
          text: 'Id procesu',
          align: 'left',
          value: 'processId'
        },
        {
          text: 'Nazwa procesu',
          align: 'left',
          value: 'processName'
        },
        {
          text: 'Odpowiedzialny',
          align: 'left',
          value: 'userAssigned'
        }]
    }
  },
  computed: {

  },
  watch: {
    userId (newVal, oldVal) {
      this.getData(newVal)
    }
  },
  methods: {
    change (item) {
      var this2 = this
      if (item.userAssigned) {
        item.userId = this2.userId
        v.axiosInstance.post('api/Users/AddUserProcess',
          item)
          .then(function (res) {
          })
          .catch(function (error) {
            this2.getData(this.userId)
            console.log(error)
          })
      } else {

      }
    },
    getData (id) {
      var this2 = this
      v.axiosInstance.get('api/Users/GetAllUserProcess?userId=' + id)
        // eslint-disable-next-line no-debugger
        .then(function (result) { debugger; this2.data = result.data; this2.tableLoading = false })
    }
  },
  created () {
    this.getData(this.userId)
  },
  destroyed () {
  }
}
</script>

<style>
</style>
