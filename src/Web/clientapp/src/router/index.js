
import Vue from 'vue';
import VueRouter from 'vue-router'

import Home from './../components/home/Home'

import OdicCallback from './../components/oidc/OidcCallback'
import Logout from './../components/oidc/Logout'

import goTo from 'vuetify/es5/services/goto'


Vue.use(VueRouter);

const routes = [
    { path: '/', component: Home },
    { path: '/oidc-callback', component: OdicCallback },
    { path: '/logout', component: Logout }
  ]
  
export default new VueRouter({
    routes,
    scrollBehavior: (to, from, savedPosition) => {
      let scrollTo = 0
  
      if (to.hash) {
        scrollTo = to.hash
      } else if (savedPosition) {
        scrollTo = savedPosition.y
      }
  
      return goTo(scrollTo)
    },
    mode: 'history'
})