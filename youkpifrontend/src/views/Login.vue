<template>
  <v-layout fill-height class="mr-1 ml-1" row wrap align-center justify-center>
    <v-flex md5 xs12>
      <v-layout row wrap>
        <v-flex xs12 elevation-6>
          <v-card>
            <v-card-title class="pt-5 primary darken-1" primary-title>
              <span class="white--text">Witamy w YouKPI</span>
              <v-spacer></v-spacer>

              <v-img
                class="mx-21"
                src="../assets/logo.png"
                max-height="40"
                max-width="120"
                contain
              ></v-img>
            </v-card-title>

            <v-card-text class="pt-4">
              <div>
                <v-form v-model="valid" ref="form">
                  <v-text-field
                    label="Podaj swój login"
                    v-model="login"
                    min="8"
                    :rules="loginRules"
                    required
                  ></v-text-field>
                  <v-text-field
                    label="Podaj swoje hasło"
                    v-model="password"
                    min="8"
                    :append-icon="e1 ? 'visibility_off' : 'visibility'"
                    :type="e1 ? 'password' : 'text'"
                    :rules="passwordRules"
                    counter
                    required
                    @click:append="() => (e1 = !e1)"
                  ></v-text-field>
                  <v-layout justify-space-around>
                    <v-btn
                      @click="submit"
                      :class="{
                        'blue darken-4 white--text': valid,
                        disabled: !valid,
                      }"
                      >Login</v-btn
                    >
                  </v-layout>
                  <v-alert
                    v-if="this.loginErrors.length > 0"
                    class="mt-4"
                    color="red"
                    dismissible
                    elevation="3"
                    text
                    type="error"
                    >{{ this.loginErrors }}</v-alert
                  >
                </v-form>
              </div>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs12 elevation-6>
          <v-footer padless class="small-text">
            <v-col class="text-center" cols="6">
              <v-flex xs12>
                {{ companyInfo.nazwa }}, {{ companyInfo.formaPrawna }}
              </v-flex>
              <v-flex xs12> NIP: {{ companyInfo.nip }} </v-flex>
            </v-col>
            <v-col class="text-center" cols="6">
              <v-flex xs12>
                {{ companyInfo.kodPocztowy }}
                {{ companyInfo.miasto }}
              </v-flex>
              <v-flex xs12>
                {{ companyInfo.ulica }} {{ companyInfo.numer }}
              </v-flex>
            </v-col>
          </v-footer>
          <!-- <v-card>
            <v-card-text class="grey lighten-4">
              <v-layout row wrap justify-space-around>
                <v-flex md3 xs12>
                  <v-flex xs12>
                    {{ companyInfo.nazwa }}, {{ companyInfo.formaPrawna }}
                  </v-flex>
                  <v-flex xs12> NIP: {{ companyInfo.nip }} </v-flex>
                </v-flex>
                <v-flex md3 xs12>
                  <v-flex xs12>
                    {{ companyInfo.kodPocztowy }}
                    {{ companyInfo.miasto }}
                  </v-flex>
                  <v-flex xs12>
                    {{ companyInfo.ulica }} {{ companyInfo.numer }}
                  </v-flex>
                </v-flex>
              </v-layout>
            </v-card-text>
          </v-card> -->
        </v-flex>
      </v-layout>
    </v-flex>
  </v-layout>
</template>

<script>
import * as v from '../main'
import auth from '../auth'
export default {
  name: '',
  components: {
  },
  props: {
  },
  data () {
    return {
      getCompanyInfo: 'api/Login/GetBasicInfo',
      valid: false,
      e1: true,
      password: '',
      loginErrors: '',
      passwordRules: [
        (v) => !!v || 'Hasło jest wymagane'
      ],
      login: '',
      loginRules: [
        (v) => !!v || 'Login jest wymagany'
      ],
      companyInfo: {}
    //   emailRules: [
    //     (v) => !!v || 'E-mail is required',
    //     (v) => /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(v) || 'E-mail must be valid'
    //   ]
    }
  },
  computed: {
  },
  watch: {
  },
  methods: {
    submit () {
      var this2 = this
      if (this.$refs.form.validate()) {
        v.axiosInstance.post('api/Login/Authenticate', { login: this.login, password: this.password })
          .then(response => {
            auth.loginWithToken(response.data)
            this.$router.push({ name: 'Home' })
          })
          .catch(function (error) {
            try {
              this2.loginErrors = error.response.data
            } catch {
              this2.loginErrors = 'Program error'
            }
          })
      }
    },
    getCompanyBasicInfo () {
      var $this = this
      this.$http
        .get(this.getCompanyInfo)
        .then((Response) => {
          $this.companyInfo = Response.data
        })
        .catch((e) => {
        })
    },
    clear () {
      this.$refs.form.reset()
    }
  },
  created () {
    this.getCompanyBasicInfo()
  },
  destroyed () {
  }
}
</script>

<style scoped>
.loginOverlay {
  background: rgba(159, 204, 241, 0.3);
}
.small-text {
  border-top-style: solid;
  border-top-color: gray;
  font-size: 0.7rem;
  font-weight: 300;
}
</style>
