<template>
  <v-app id="inspire">
    <component v-bind:is="layout"></component>
  </v-app>
</template>

<script>
import { mapState } from 'vuex'
import DefaultLayout from './components/_layout/DefaultLayout'
import auth from './api/auth'

  export default {
    computed:{
     ...mapState({
        layout: state => state.layout.layout
      }),
    },
    components: {
      'default-layout': DefaultLayout
    },
    created(){
      auth.authorize().then(r => {
        if(r){
          //todo: set auth
        }
      })
    }
  }
</script>