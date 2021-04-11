

const state = () => ({
    layout: 'default-layout'
})

const getters = {
    layout (state) {
        return state.layout
      }
    
}

const actions = {

}

const mutations = {
    SET_LAYOUT (state, payload) {
        state.layout = payload
      }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}