
<template>
    <div>
        <AppBar />
        <v-main>
            <router-view></router-view>
        </v-main>
        <Footer />
    </div>
</template>

<script>
  import AppBar from './AppBar'
  import Footer from './Footer'
  export default {
    components: {
      AppBar,
      Footer
    }
  }
</script>