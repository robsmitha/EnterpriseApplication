<template>
  <div>
    <v-sheet v-if="configurations.loading" height="500px">
        <v-container
          fluid
          fill-height
          >
          <v-row
          align="center"
          justify="center">
            <v-col class="text-center">
              <div class="mb-7">
                <v-skeleton-loader
                type="text"
                class="mx-auto mb-2"
                width="25rem"
                ></v-skeleton-loader>
                <v-skeleton-loader
                type="text"
                class="mx-auto"
                width="15rem"
                height="3rem"
                ></v-skeleton-loader>
              </div>
              <v-btn x-large disabled class="mt-7" tile>Loading</v-btn>
            </v-col>
          </v-row>
        </v-container>
    </v-sheet>
    <v-parallax v-if="configurations.success" src="https://smitha-cdn.s3.us-east-2.amazonaws.com/Content/images/bg-chart.jpg">
        <v-container
          fluid
          >
          <v-row>
            <v-col class="text-center">
               <span class="text-lg-h3 text-h2 text-uppercase">
                {{configurations.data.tagLine}}
              </span>
              <p class="subtitle-2">
                {{configurations.data.displayName}}
              </p>
            </v-col>
          </v-row>
        </v-container>
         <div class="text-center">
          <v-btn color="black" dark x-large tile @click="login" v-if="!user.success">
            Get Started
          </v-btn>
        </div>
      </v-parallax>
      <v-sheet color="grey lighten-3 py-7 text-center" id="callToAction">
        <v-skeleton-loader
        width="12rem"
        v-if="configurations.loading"
        type="text"
        class="mx-auto"
        ></v-skeleton-loader>
        <span v-else-if="configurations.success" class="text-uppercase text-h5 d-block">
          {{configurations.data.introductionText}}
        </span>
      </v-sheet>
      <v-container class="py-7">
        <div class="mb-2">
          <h3 class="subtitle-1 text-uppercase">
            About Us
          </h3>
          <span class="text-h4">
            Our Mission
          </span>
        </div>
        <v-skeleton-loader v-if="configurations.loading"
          type="paragraph"
        ></v-skeleton-loader>
        <p v-else-if="configurations.success" class="text-body-1">
          {{configurations.data.aboutText}}
        </p>
      </v-container>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import auth from './../../api/auth'

export default {
  computed: {
    ...mapState({
        configurations: state => state.systemConfigurations.configurations,
        user: state => state.auth.user
      }),
  },
  created () {
    this.$store.dispatch('systemConfigurations/getConfigurations')
    this.$store.dispatch('auth/getUser')
  },
  methods: {
    login(){
      auth.login();
    }
  }
}
</script>
