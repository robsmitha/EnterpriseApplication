import merchants from '../../api/merchants'

const state = () => ({
    merchants: {
        loading: true,
        success: false,
        data: null
    },
    activeMerchant: {
        loading: true,
        success: false,
        data: null
    },
    openOrder: {
        loading: true,
        success: false,
        data: null
    },
    openOrderLoading: true
})

const getters = {

}

const actions = {
    async getMerchants({ commit })  {
        const data = await merchants.getMerchants();
        commit('setMerchants', data);
        if (data.length > 0) {
            commit('setActiveMerchant', data[0]);
        }
    },
    async getOpenOrder({ commit }, args)  {
        commit('setOpenOrderLoading', true);
        try {
            const data = await merchants.getOpenOrder(args.id);
            commit('setOpenOrder', data);
        } catch {
            commit('setOpenOrder', { id: 0 });
        } finally {  
            commit('setOpenOrderLoading', false);
        }
    }
}

const mutations = {
    setMerchants(state, data) {
        state.merchants = {
            loading: false,
            success: data !== null,
            data: data
        }
    },
    setActiveMerchant(state, data) {
        state.activeMerchant = {
            loading: false,
            success: data !== null,
            data: data
        }
    },
    setOpenOrder(state, data) {
        state.openOrder = {
            loading: false,
            success: data !== null,
            data: data
        }
    },
    setOpenOrderLoading(state, data) {
        state.openOrderLoading = data
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}