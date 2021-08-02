<template>
  <v-app id="inspire">
    <component v-bind:is="layout"></component>
  </v-app>
</template>

<script>
import { mapState } from 'vuex'
import DefaultLayout from './components/_layout/DefaultLayout'

export default {
  computed:{
    ...mapState({
      layout: state => state.layout.layout
    }),
  },
  components: {
    'default-layout': DefaultLayout
  }
}
</script>