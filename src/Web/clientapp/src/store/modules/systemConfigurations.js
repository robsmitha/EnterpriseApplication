import systemConfigurations from '../../api/systemConfigurations'

const state = () => ({
    configurations: {
        loading: true,
        success: false,
        data: null
    }
})

const getters = {
    
    
}

const actions = {
    async getConfigurations({ commit })  {
        const data = await systemConfigurations.getConfigurations()
        let configurations = {}
        for (const key in data.configurations) {
            if (Object.prototype.hasOwnProperty.call(data.configurations, key)) {
                const element = data.configurations[key];
                configurations[element.name] = element.value;
            }
        }
        commit('setConfigurations', configurations)
    }
}

const mutations = {
    setConfigurations(state, configurations){
        state.configurations = {
            loading: false,
            success: configurations !== null,
            data: configurations
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