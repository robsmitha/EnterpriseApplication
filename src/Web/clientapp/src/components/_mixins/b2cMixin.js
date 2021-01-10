import { mapState } from 'vuex'

const b2cMixin = {
    ...mapState({
        configurations: state => state.systemConfigurations.configurations
    }),
    computed: {
        b2cSignInUrl() {
            if(this.configurations.success) {
                //TODO: Get current url or config
                const redirectUri = process.env.VUE_APP_B2C_REDIRECT_URL
                const { b2cDirectory, b2cClientId, b2cSignInPolicy, b2cNonce, b2cScope, b2cResponseType, b2cPromptLogin, b2cResponseMode } = this.configurations.data
                return `https://${b2cDirectory}.b2clogin.com/${b2cDirectory}.onmicrosoft.com/oauth2/v2.0/authorize?p=${b2cSignInPolicy}&client_id=${b2cClientId}&nonce=${b2cNonce}&redirect_uri=${redirectUri}&scope=${b2cScope}&response_type=${b2cResponseType}&prompt=${b2cPromptLogin}&response_mode=${b2cResponseMode}`
            }
            return ''
        },
        b2cSignUpUrl() {
            if(this.configurations.success) {
                //TODO: Get current url or config
                const redirectUri = process.env.VUE_APP_B2C_REDIRECT_URL
                const { b2cDirectory, b2cClientId, b2cSignUpPolicy, b2cNonce, b2cScope, b2cResponseType, b2cPromptLogin, b2cResponseMode } = this.configurations.data
                return `https://${b2cDirectory}.b2clogin.com/${b2cDirectory}.onmicrosoft.com/oauth2/v2.0/authorize?p=${b2cSignUpPolicy}&client_id=${b2cClientId}&nonce=${b2cNonce}&redirect_uri=${redirectUri}&scope=${b2cScope}&response_type=${b2cResponseType}&prompt=${b2cPromptLogin}&response_mode=${b2cResponseMode}`
            }
            return ''
        },
        b2cSignUpSignInUrl() {
            if(this.configurations.success) {
                //TODO: Get current url or config
                const redirectUri = process.env.VUE_APP_B2C_REDIRECT_URL
                const { b2cDirectory, b2cClientId, b2cSignUpSignInPolicy, b2cNonce, b2cScope, b2cResponseType, b2cPromptLogin, b2cResponseMode } = this.configurations.data
                return `https://${b2cDirectory}.b2clogin.com/${b2cDirectory}.onmicrosoft.com/oauth2/v2.0/authorize?p=${b2cSignUpSignInPolicy}&client_id=${b2cClientId}&nonce=${b2cNonce}&redirect_uri=${redirectUri}&scope=${b2cScope}&response_type=${b2cResponseType}&prompt=${b2cPromptLogin}&response_mode=${b2cResponseMode}`
            }
            return ''
        }
    },
    created () {
        this.$store.dispatch('systemConfigurations/getConfigurations')
    },
}

export default b2cMixin