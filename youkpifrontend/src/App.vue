<template>
  <v-app id="inspire">
    <v-app-bar app color="primary lighten-1" dense>
      <v-app-bar-nav-icon @click="drawer = true"></v-app-bar-nav-icon>
      <v-toolbar-title class="white--text">YouKPI</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-tooltip right>
        <template v-slot:activator="{ on }">
          <v-btn v-on="on" icon>
            <v-icon color="white">fa-user</v-icon>
          </v-btn>
        </template>
        <span>New User</span>
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
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
  </v-app>
</template>

<script>
// import HelloWorld from './components/HelloWorld'

export default {
  name: 'App',

  components: {
    // HelloWorld
  },

  data: () => ({
    drawer: false,
    group: null
  })
}
</script>
