<template>
   <v-container fluid>
     <v-row>
      <v-col
        cols="12"
        md="8">
          <v-toolbar flat>
            <v-toolbar-title class="text-uppercase text-h5">
              Order #{{orderId}}
            </v-toolbar-title>
          </v-toolbar>
          <v-card v-if="order.loading">
            <v-card-text>
              <v-skeleton-loader
                type="table-heading, card-heading, list-item-three-line"
              ></v-skeleton-loader>
            </v-card-text>
          </v-card>
          <v-card v-else>
            <v-card-title>{{ order.data.orderStatusTypeName }}</v-card-title>
            <v-card-subtitle>{{ order.data.orderDate }}</v-card-subtitle>
            <v-card-text>
              <div>{{ order.data.merchantName }}</div>
              <div>Ship to: {{ order.data.merchantZip }}</div>
            </v-card-text>
          </v-card>
        </v-col>
        <v-col
        cols="12"
        md="4">
          <v-toolbar flat>
            <v-toolbar-title class="text-uppercase text-h5">
              Items
            </v-toolbar-title>
          </v-toolbar>
          <v-card v-if="order.loading">
            <v-skeleton-loader 
              v-for="i in 3" 
              :key="i" 
              type="list-item-three-line">
            </v-skeleton-loader>
          </v-card>
          <v-card v-else-if="order.success && order.data.lineItems && order.data.lineItems.length > 0">
            <v-list>
              <v-list-item
                v-for="lineItem in order.data.lineItems"
                :key="lineItem.id"
              >
                <v-list-item-content>
                  <v-list-item-title v-text="lineItem.itemName"></v-list-item-title>
                  <v-list-item-subtitle>
                    <span>Qty {{lineItem.quantity}}</span>
                    <v-spacer/>
                    <span>${{lineItem.itemTotal}}</span>
                    <v-spacer/>
                    <span>Shipping {{lineItem.shippingTotal}}</span>
                  </v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
              <v-list-item v-if="order.data">
                <v-list-item-content>
                  <v-list-item-title class="d-flex">
                    Subtotal
                    <v-spacer />
                    {{order.data.displaySubTotal}}
                  </v-list-item-title>
                  <v-list-item-title class="d-flex">
                    Shipping
                    <v-spacer />
                    {{order.data.displayShippingTotal}}
                  </v-list-item-title>
                  <v-list-item-title class="d-flex">
                    Tax&nbsp;<span class="caption">{{order.data.displayTaxRate}}</span>
                    <v-spacer />
                    {{order.data.displayTaxAmount}}
                  </v-list-item-title>
                  <v-list-item-title class="d-flex">
                    Total
                    <v-spacer />
                    {{order.data.displayOrderTotal}}
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-card>
        </v-col>
      </v-row>
   </v-container>
</template>

<script>
import { mapState } from 'vuex'
import merchants from '../../api/merchants'
export default {
  data: () => ({
    order: {
      loading: true,
      success: false,
      data: null
    }
  }),
  watch: {
    activeMerchant(val){
      if(val){
        this.getOrder()
      }
    }
  },
  mounted(){
    if(this.merchantId && !this.order.success){
        this.getOrder()
      }
  },
  computed: {
    ...mapState({
        activeMerchant: state => state.merchants.activeMerchant
      }),
      merchantId(){
        return this.activeMerchant.data?.id
      },
      orderId() {
        return this.$route.params.id;
      }
  },
  methods: {
    getOrder(){
      this.order = {
        loading: true,
        success: false,
        data: null
      }
      merchants.getOrder(this.merchantId, this.orderId)
          .then(items => this.order = {
            loading: false,
            success: true,
            data: items
          });
    }
  }
}
</script>
