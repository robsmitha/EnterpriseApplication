<template>
    <v-container>
       <div
        v-if="loading">
        <p class="text-overline">Logging you in...</p>
            <v-progress-linear
      indeterminate
    ></v-progress-linear>
       </div>
    <v-row v-else-if="!success">
        <v-col>
            <v-alert
            prominent
            type="error"
            >
            <v-row align="center">
                <v-col class="grow">{{error}}</v-col>
                <v-col class="shrink">
                <v-btn to="/">Take me home</v-btn>
                </v-col>
            </v-row>
            </v-alert>
        </v-col>
    </v-row>
    <v-overlay
            :absolute="absolute"
            :opacity="opacity"
            :value="success"
            :z-index="zIndex"
        >
            <h2>Logging you in...Please wait.</h2>
        </v-overlay>
    </v-container>
</template>
<script>
import auth from './../../api/auth'
export default {
    data: () => ({
      absolute: false,
      opacity: 0.46,
      success: false,
      loading: true,
      zIndex: 5,
      error: ''
    }),
    created() {
        //TODO: Move to mixin for reuse on multiple callback routes
        const search = window.location.search
        const params = new URLSearchParams(search)
        if(params){
            const error = params.get('error')
            const token = params.get('id_token')
            if(error){
                this.loading = false
                const error_description = params.get('error_description')
                this.error = error_description
            } else if(token && token.length > 0) {
                auth.authenticate(token).then(r => {
                    this.loading = false
                    if(r.error) {
                        this.error = 'It appears we\'ve issued u a token that\'s not good or the api blew up.'
                    }else{
                        this.success = true
                        this.$store.dispatch('auth/login', { token })
                        this.$router.push('/')
                    }
                })
            }
            else {
                this.error = 'Token not present.'
            }
        }
        else{
            this.error = 'Missing parameters.'
        }
    }
}
</script>