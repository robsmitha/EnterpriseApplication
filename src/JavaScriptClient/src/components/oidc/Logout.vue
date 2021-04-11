<template>
    <div>
        Are you sure?
        <v-btn @click="logout">Yes, sign me out.</v-btn>
    </div>
</template>
<script>
import { mapState } from 'vuex'

  export default {
    data: () => ({
  
    }),
    computed: {
        ...mapState({
            user: state => state.auth.user
        }),
    },
    created () {
        this.$store.dispatch('auth/getUser')
    },
    watch:{
        user(val){
            console.log(val)
        }
    },
    methods: {
        logout() {
            this.$store.dispatch('auth/logout');
        }
    }
  }
</script>