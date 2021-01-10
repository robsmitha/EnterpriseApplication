import { send } from './api'

export function b2cLoginUrl(appConfig) {
    return `https://${appConfig.directory}.b2clogin.com/${appConfig.directory}.onmicrosoft.com/oauth2/v2.0/authorize?p=${appConfig.signin_userflow}&client_id=${appConfig.client_id}&nonce=${appConfig.nonce}&redirect_uri=${appConfig.redirect_uri}&scope=${appConfig.scope}&response_type=${appConfig.response_type}&prompt=${appConfig.prompt}&response_mode=${appConfig.response_mode}`
}

export default {
    getConfigurations
}

async function getConfigurations(){
    const token = localStorage.getItem('token')
    var request = {
        method: 'get',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` 
        }
    }
    return send(`/api/systemConfigurations`, request)
}