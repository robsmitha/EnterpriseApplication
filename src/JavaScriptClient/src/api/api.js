const endpoint = process.env.VUE_APP_API_ENDPOINT


export async function send(func, request) {
    try {
        const uri = formatUri(func)
        console.log(process.env.VUE_APP_API_ENDPOINT, uri)
        const response = await fetch(uri, request)
        if(response.ok){
            return await response.json()
        }
        else{
            switch(response.status){
                case 400:
                    // eslint-disable-next-line no-case-declarations
                    const data = await response.json()
                    // eslint-disable-next-line no-case-declarations
                    let msg = "";
                    for (const errorKey in data) {
                        if (Array.isArray(data[errorKey])){
                            data[errorKey].map(m => {
                                msg += `<br/>${m}`;
                                return m;
                            });
                        }
                        else {
                            msg += `<br/>${data[errorKey]}`;
                        }
                    }
                    if (msg.length > 0){
                        return { error: msg };
                    }
                    else {
                        return { error: JSON.stringify(response) }
                    }
                case 401: return { error: 'Unauthorized api call' }    
                default: return { error: JSON.stringify(response) }
            }
        }
    } catch (error) {
        return { error: error.message } 
    }
}

function formatUri(func){
    return endpoint + func
}