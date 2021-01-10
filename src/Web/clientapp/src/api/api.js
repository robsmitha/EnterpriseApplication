const endpoint = process.env.VUE_APP_API_ENDPOINT

export async function send(func, request) {
    try {
        //TODO: Create format url method
        const response = await fetch(endpoint + func, request)
        if(response.ok){
            const data = await response.json()
            console.log(endpoint + func,data)
            return data
        }
        else{
            switch(response.status){
                case 401: return { error: 'Unauthorized api call' }    //TODO: remove token
                default: return { error: JSON.stringify(response) }
            }
        }
    } catch (error) {
        return { error: error.message } 
    }
}