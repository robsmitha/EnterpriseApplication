import auth from './../../api/auth'

const b2cMixin = {
    computed: {
        login() {
            auth.login();
        },
        logout() {
            auth.logout();
        }
    }
}

export default b2cMixin