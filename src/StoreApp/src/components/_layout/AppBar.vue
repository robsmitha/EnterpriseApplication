<template>
    <div>
        <v-navigation-drawer
        v-model="drawerLeft"
        clipped
        app
        :permanent="$vuetify.breakpoint.lgAndUp && !drawerLeft"
        left
        >
            <v-list dense>
                <v-list-item>
                <v-list-item-content>
                    <v-list-item-title class="grey--text text--darken-1 overline">Merchants</v-list-item-title>
                </v-list-item-content>
                </v-list-item>
                <div 
                v-if="merchants.loading"
                >
                    <v-skeleton-loader
                        v-for="i in 3"
                        :key="i"
                        type="list-item"
                        class="mx-auto"
                    ></v-skeleton-loader>
                </div>
                <v-list-item 
                v-else-if="!merchants.success"
                >
                    <v-list-item-icon>
                        <v-icon>mdi-alert-circle</v-icon>
                    </v-list-item-icon>
                    <v-list-item-content>
                        Could not load items
                    </v-list-item-content>
                </v-list-item>
                <v-list-item
                v-else
                v-for="(merchant, i) in merchants.data"
                :key="i"
                link
                @click="goToMerchant(merchant)">
                    <v-list-item-content>
                        <v-list-item-title>
                        {{ merchant.name }}
                        </v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
            </v-list>
        </v-navigation-drawer>
        
        <v-app-bar
        app 
        dark
        clipped-right
        clipped-left
        >
            <v-app-bar-nav-icon v-if="$vuetify.breakpoint.mdAndDown" @click.stop="drawerLeft = !drawerLeft"></v-app-bar-nav-icon>
            <v-skeleton-loader
                v-if="activeMerchant.loading"
                type="text"
                width="250"
            ></v-skeleton-loader>
            <v-toolbar-title
                v-else
                class="text-uppercase"
            >
                <v-btn
                    large
                    text
                    @click="onBrandClick"
                >
                    <span v-if="activeMerchant.success">{{ activeMerchant.data.name }}</span>
                    <span v-else>Merchant Tax App</span>
                </v-btn>
            </v-toolbar-title>
            <v-spacer />
            <v-btn
                :loading="openOrderLoading"
                color="primary"
                @click="onBrandClick"
            >
                <v-icon>mdi-cart</v-icon>&nbsp;{{openOrder.data ? openOrder.data.displaySubTotal : '$0'}}
            </v-btn>
            
        </v-app-bar>

    </div>
</template>

<script>
import { mapState, mapMutations, mapActions } from 'vuex'

  export default {
    data: () => ({
      left: true
    }),
    computed: {
        ...mapState({
            merchants: state => state.merchants.merchants,
            activeMerchant: state => state.merchants.activeMerchant,
            openOrder: state => state.merchants.openOrder,
            openOrderLoading: state => state.merchants.openOrderLoading
        }),
        drawerLeft: {
            get: function () {
                return this.left
            },
            set: function (val) {
                this.left = val
            }
        },
        isHomePage() {
            return this.$route.path === '/'
        },
    },
    mounted () {
        this.getMerchants()
    },
    methods: {
        onBrandClick(){
            if(this.isHomePage){
                this.$vuetify.goTo('body', { duration: 300, easing: 'easeInCubic' })
            }
            else{
                this.$router.push({ path: '/' })
            }
        },
        ...mapMutations('merchants', [
            'setActiveMerchant'
        ]),
        ...mapActions('merchants', [
            'getMerchants'
        ]),
        goToMerchant(merchant){
            this.setActiveMerchant(merchant); 
            this.drawerLeft = false; 
            if(!this.isHomePage){
                this.$router.push('/')
            }
            
        }
    }
  }
</script>