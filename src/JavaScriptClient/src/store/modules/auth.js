import auth from "../../api/auth"

const state = () => ({
    user: {
        loading: true,
        success: false,
        data: null
    }
})

const getters = {
    
    
}

const actions = {
    getUser({ commit })  {
        auth.getUser().then(user => {
            console.log(user)
            commit('setUser', user)
        })
    }
}

const mutations = {
    setUser(state, user){
        state.user = {
            loading: false,
            success: user !== undefined && user !== null,
            data: user
        }
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}