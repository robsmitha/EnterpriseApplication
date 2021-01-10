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
        const user = localStorage.getItem('user')
        commit('setUser', user)
    },
    login({ commit }, user)  {
        auth.login(user)
        commit('setUser', user)
    },
    logout({ commit })  {
        auth.logout()
        commit('setUser', null)
    }
}

const mutations = {
    setUser(state, user){
        state.user = {
            loading: false,
            success: user !== null,
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