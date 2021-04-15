<template>
  <v-footer
    v-if="isHomePage"
    dark
    padless
  >
    <v-card
      flat
      tile
      class="dark flex white--text text-center"
    >
    <v-card-text v-if="configurations.loading">
      <v-progress-circular
            indeterminate
            color="white"
        ></v-progress-circular>
      </v-card-text>
      <v-card-text v-else-if="configurations.success" class="pb-0">
        <v-btn
          v-for="item in configurations.data.socialMedia" 
          :key="item.href"
          :href="item.href" 
          icon
          target="_blank" 
          class="mx-4 white--text"
          rel="noopener noreferrer">
          <v-icon size="24px">{{ item.icon }}</v-icon>
        </v-btn>
      </v-card-text>

      <v-card-text class="white--text pt-0">
        <v-skeleton-loader
        width="12rem"
        v-if="configurations.loading"
        type="text"
        class="mx-auto"
        ></v-skeleton-loader>
        <v-card-text v-else-if="configurations.success">{{configurations.data.fullName}}</v-card-text> 
        &copy; {{ new Date().getFullYear() }}
      </v-card-text>
    </v-card>
  </v-footer>
</template>

<script>
import { mapState } from 'vuex'
export default {
    data: () => ({
        items: []
    }),
    computed: {
      ...mapState({
          configurations: state => state.systemConfigurations.configurations,
      }),
      isHomePage() {
          return this.$route.path === '/'
      },
    }
}
</script>