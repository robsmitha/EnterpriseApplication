<template>
   <v-container fluid>
     <v-row>
      <v-col
        cols="12"
        md="8">
          <v-toolbar flat>
            <v-toolbar-title class="text-uppercase text-h5">
              Inventory
            </v-toolbar-title>
          </v-toolbar>
          <v-card v-if="merchantItems.loading">
            <v-card-text>
              <v-skeleton-loader
                type="table-heading, card-heading, list-item-three-line, text"
              ></v-skeleton-loader>
            </v-card-text>
          </v-card>
          <v-card v-else v-for="item in merchantItems.data" :key="item.id" class="mb-4">
            <v-card-title>{{ item.name }}<v-spacer/><span class="body-2">{{ item.price }}</span></v-card-title>
            <v-card-subtitle class="d-flex">
              <span>Max Allowed: {{ item.maxAllowed }}</span>
              <v-spacer/>
              <span>Shipping: {{ item.shipping }}</span>
            </v-card-subtitle>
            <v-card-text>
              <span>{{ item.description }}</span>
            </v-card-text>
            <v-card-text class="pb-0">
              <v-select
                v-model="item.newQty"
                :items="item.maxAllowedRange"
                label="Qty"
                outlined
                dense
              ></v-select>
            </v-card-text>
            <v-card-actions class="pt-0">
              <v-btn
                color="primary"
                @click="addLineItem(item)">
                Add
              </v-btn>
            </v-card-actions>
          </v-card>
          <v-snackbar
              v-model="snackbar"
              top
              :color="snackbarColor"
            >
              {{ snackbarText }}

              <template v-slot:action="{ attrs }">
                <v-btn
                  text
                  v-bind="attrs"
                  @click="snackbar = false"
                >
                  Close
                </v-btn>
              </template>
            </v-snackbar>
          <v-dialog
            v-model="errorDialog"
            width="350"
          >
            <v-card>
              <v-card-title class="text-h5">
                Validation Message
              </v-card-title>

              <v-card-text>
                {{ errorDialogText }}
              </v-card-text>

              <v-divider></v-divider>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  color="primary"
                  text
                  @click="errorDialog = false"
                >
                  Close
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-col>
        <v-col
        cols="12"
        md="4">
          <v-toolbar flat>
            <v-toolbar-title class="text-uppercase text-h5">
              Open Order
            </v-toolbar-title>
          </v-toolbar>
          <v-card v-if="openOrderLoading">
            <v-skeleton-loader 
              v-for="i in 3" 
              :key="i" 
              type="list-item-three-line">
            </v-skeleton-loader>
          </v-card>
          <v-card v-else-if="openOrder.success && openOrder.data.lineItems && openOrder.data.lineItems.length > 0">
            <v-list>
              <v-list-item
                v-for="lineItem in openOrder.data.lineItems"
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

                <v-list-item-action>
                  <v-btn icon @click="removeLineItem(lineItem)">
                    <v-icon color="red">mdi-close</v-icon>
                  </v-btn>
                </v-list-item-action>
              </v-list-item>
              <v-list-item v-if="openOrder.data">
                <v-list-item-content>
                  <v-list-item-title class="d-flex">
                    Subtotal
                    <v-spacer />
                    {{openOrder.data.displaySubTotal}}
                  </v-list-item-title>
                  <v-list-item-title class="d-flex">
                    Shipping
                    <v-spacer />
                    {{openOrder.data.displayShippingTotal}}
                  </v-list-item-title>
                  <v-list-item-title class="d-flex">
                    Tax&nbsp;<span class="caption">{{openOrder.data.displayTaxRate}}</span>
                    <v-spacer />
                    {{openOrder.data.displayTaxAmount}}
                  </v-list-item-title>
                  <v-list-item-title class="d-flex">
                    Total
                    <v-spacer />
                    {{openOrder.data.displayOrderTotal}}
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list>
            <v-card-actions>
              <v-btn 
                block
                color="green"
                class="white--text"
                @click="updateOrderToPaid"
                >
                Complete Order
              </v-btn>  
            </v-card-actions>
          </v-card>
          <v-card v-else>
            <v-card-text>
              No items to display
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
   </v-container>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import merchants from './../../api/merchants'
export default {
  data: () => ({
    merchantItems: {
      loading: true,
      success: false,
      data: null
    },
    errorDialog: false,
    errorDialogText: null,
    snackbar: false,
    snackbarText: null,
    snackbarColor: null
  }),
  watch: {
    activeMerchant(val){
      if(val){
        this.getItems()
        this.getOpenOrder({ id: this.merchantId })
      }
    }
  },
  mounted(){
    if(this.merchantId && !this.merchantItems.success){
        this.getItems()
        this.getOpenOrder({ id: this.merchantId })
      }
  },
  computed: {
    ...mapState({
        activeMerchant: state => state.merchants.activeMerchant,
        openOrder: state => state.merchants.openOrder,
        openOrderLoading: state => state.merchants.openOrderLoading
      }),
      merchantId(){
        return this.activeMerchant.data?.id
      },
      orderId() {
        return this.openOrder.data?.id;
      }
  },
  methods: {
    ...mapActions('merchants', [
        'getOpenOrder'
    ]),
    addLineItem(item){
      merchants.addLineItem(this.merchantId, {
        merchantId: this.merchantId,
        itemId: item.id,
        orderId: this.orderId > 0 ? this.orderId : 0,
        newQty: item.newQty
      }).then(data => {
        if(data?.orderId > 0){
          this.getItems()
          this.getOpenOrder({ id: this.merchantId})
          this.snackbarColor = 'primary';
          this.snackbarText = `${item.name} x${item.newQty} added to order`;
          this.snackbar = true;
        } else if(data.error){
          this.errorDialog = true
          this.errorDialogText = data.error
        }
      })   
    },
    getItems(){
      this.merchantItems = {
        loading: true,
        success: false,
        data: null
      }
      merchants.getMerchantItems(this.merchantId)
          .then(items => this.merchantItems = {
            loading: false,
            success: true,
            data: items
          });
    },
    removeLineItem(lineItem){
      merchants.removeLineItem(this.merchantId, {
        merchantId: this.merchantId,
        itemId: lineItem.itemId,
        orderId: this.orderId
      }).then(data => {
        if(data){
          this.getOpenOrder({ id: this.merchantId})
          this.snackbarColor = 'red';
          this.snackbarText = `${lineItem.itemName} removed from order.`;
          this.snackbar = true;
        } else if(data.error){
          this.errorDialog = true
          this.errorDialogText = data.error
        }
      })
    },
    updateOrderToPaid(){
      merchants.updateOrderToPaid(this.merchantId, {
          orderId: this.orderId
        }).then(data => {
          if(data){
            this.$router.push(`/order/${this.orderId}`)
          } else if(data.error){
            this.errorDialog = true
            this.errorDialogText = data.error
          }
        })
    }
  }
}
</script>
