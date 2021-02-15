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
      <span class="white--text mr-2">{{
        formatDateTimeYYYYMMDD(new Date())
      }}</span>
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
          <v-list-item @click="$router.push({ name: 'Pracownicy' })">
            <v-list-item-icon>
              <v-icon>fa-users</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Pracownicy</v-list-item-title>
          </v-list-item>
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
          <v-list-item @click="$router.push({ name: 'Oferta' })">
            <v-list-item-icon>
              <v-icon>fa-shopping-basket</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Oferty</v-list-item-title>
          </v-list-item>
          <v-list-item @click="$router.push({ name: 'ActivityHistory' })">
            <v-list-item-icon>
              <v-icon>fa-history</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Historia aktywności</v-list-item-title>
          </v-list-item>
          <v-list-item @click="$router.push({ name: 'Maszyny' })">
            <v-list-item-icon>
              <v-icon>fa-cogs</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Maszymy</v-list-item-title>
          </v-list-item>
          <v-list-item @click="$router.push({ name: 'ReasonCodes' })">
            <v-list-item-icon>
              <v-icon>fa-code</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Reason codes</v-list-item-title>
          </v-list-item>
          <v-list-item @click="$router.push({ name: 'Components' })">
            <v-list-item-icon>
              <v-icon>fa-cubes</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Komponenty</v-list-item-title>
          </v-list-item>
          <v-list-item @click="$router.push({ name: 'Księga procesów' })">
            <v-list-item-icon>
              <v-icon>fa-tasks</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Księga procesów</v-list-item-title>
          </v-list-item>
          <v-list-item @click="$router.push({ name: 'Steps machines' })">
            <v-list-item-icon>
              <v-icon>fa-shoe-prints</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Steps machines</v-list-item-title>
          </v-list-item>
          <v-list-item @click="$router.push({ name: 'Klienci' })">
            <v-list-item-icon>
              <v-icon>fa-user-friends</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Klienci</v-list-item-title>
          </v-list-item>
          <v-list-item @click="$router.push({ name: 'Magazyn' })">
            <v-list-item-icon>
              <v-icon>fa-warehouse</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Magazyn</v-list-item-title>
          </v-list-item>
          <v-divider></v-divider>
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
    // jezeli jestesmy na jakiejs stronie i nie ma tokena
    const token = window.localStorage.getItem('token')
    const constTokenExists = window.localStorage.getItem('token') !== 'undefined'
    if (this.$route.name !== 'Login' && !constTokenExists) {
      this.$router.replace({ name: 'Login' })
    } else if (constTokenExists) {
      auth.loginWithToken(token)
    }

    this.$store.watch(
      state => state.error,
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
