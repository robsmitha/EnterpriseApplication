import { send } from './api'

export default {
    getMerchants,
    getMerchantItems,
    getOpenOrder,
    getOrder,
    addLineItem,
    removeLineItem,
    updateOrderToPaid
}

async function getMerchants(){
    return send(`/merchants`)
}

async function getMerchantItems(id){
    return send(`/merchants/${id}/items`)
}

async function getOpenOrder(id){
    return send(`/merchants/${id}/openOrder`)
}

async function getOrder(id, orderId){
    return send(`/merchants/${id}/order/${orderId}`)
}

async function addLineItem(id, data){
    const request = {
        method: 'post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }
    return send(`/merchants/${id}/addLineItem`, request)
}

async function removeLineItem(id, data){
    const request = {
        method: 'post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }
    return send(`/merchants/${id}/removeLineItem`, request)
}

async function updateOrderToPaid(id, data){
    const request = {
        method: 'post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }
    return send(`/merchants/${id}/updateOrderToPaid`, request)
}