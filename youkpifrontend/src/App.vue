<template>
  <v-app id="inspire">
    <v-app-bar v-if="this.$route.name !== 'Login'" app color="primary" dense>
      <v-app-bar-nav-icon @click="drawer = true"></v-app-bar-nav-icon>
      <v-img
        class="mx-2"
        src="./assets/logo.png"
        max-height="40"
        max-width="120"
        contain
      ></v-img>
      <!-- <v-toolbar-title class="ml-1 white--text">YouKPI</v-toolbar-title> -->
      <v-spacer></v-spacer>
      <v-tooltip bottom>
        <template v-slot:activator="{ on }">
          <v-btn v-on="on" icon @click="logout()">
            <v-icon color="white">fa-sign-out-alt</v-icon>
          </v-btn>
        </template>
        <span>{{ $store.getters.UserName }}</span>
      </v-tooltip>
    </v-app-bar>
    <v-main>
      <v-container ml-1 fluid>
        <router-view />
      </v-container>
    </v-main>
    <v-navigation-drawer v-model="drawer" absolute temporary>
      <v-list nav dense>
        <v-list-item-group
          v-model="group"
          active-class="deep-purple--text text--accent-4"
        >
          <v-list-item @click="$router.push({ name: 'Home' })">
            <v-list-item-icon>
              <v-icon>fa-home</v-icon>
            </v-list-item-icon>
            <v-list-item-title class="title"> YouKPI </v-list-item-title>
            <v-list-item-subtitle> Home </v-list-item-subtitle>
          </v-list-item>
          <v-divider></v-divider>
          <v-list-item @click="$router.push({ name: 'Produkty' })">
            <v-list-item-icon>
              <v-icon>fa-industry</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Produkty</v-list-item-title>
          </v-list-item>

          <v-list-item @click="$router.push({ name: 'Czesci' })">
            <v-list-item-icon>
              <v-icon>fa-tools</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Części</v-list-item-title>
          </v-list-item>
          <v-list-item @click="$router.push({ name: 'TypWyrobu' })">
            <v-list-item-icon>
              <v-icon>fa-wrench</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Typ wyrobu</v-list-item-title>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
  </v-app>
</template>

<script>
// import HelloWorld from './components/HelloWorld'
// eslint-disable-next-line no-unused-vars
import auth from './auth'
export default {
  name: 'App',
  created: function () {
    this.$store.watch(
      state => state.snackErrorText,
      () => {
        const msg = this.$store.getters.GetError
        if (msg !== '' && this.$route.name !== 'Login') {
          this.$dialog.error({
            text: msg,
            title: 'Error'
          })
          this.$store.commit('SETERRORSNACK', '')
        }
      }
    )
  },
  components: {
    // HelloWorld
  },

  data: () => ({
    drawer: false,
    group: null
  }),
  methods: {
    logout () {
      auth.logout()
    }
  }
}
</script>
<style >
.container {
  height: 100%;
}
</style>
