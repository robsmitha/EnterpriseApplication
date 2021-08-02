<template>
  <v-footer
    v-if="isHomePage"
    dark
    padless
    app
    absolute
  >
    <v-card
      flat
      tile
      class="dark flex white--text text-center"
    >
      <v-card-text class="white--text pt-0">
        <v-card-text>Merchant Tax App</v-card-text> 
        &copy; {{ new Date().getFullYear() }}
      </v-card-text>
    </v-card>
  </v-footer>
</template>

<script>
export default {
    data: () => ({
        items: []
    }),
    computed: {
      isHomePage() {
          return this.$route.path === '/'
      },
    }
}
</script>