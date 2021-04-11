import Vue from 'vue'
import Vuex from 'vuex'
import systemConfigurations from './modules/systemConfigurations'
import auth from './modules/auth'
import layout from './modules/layout'

Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
    modules: {
      systemConfigurations,
      auth,
      layout
    },
    strict: debug
  })