import { send } from './api'

export default {
    getConfigurations
}

async function getConfigurations(){
    return send(`/systemConfigurations`)
}