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
        auth.oidcCallback()
    }
}
</script>