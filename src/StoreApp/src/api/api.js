

export function send(func, request) {
    return new Promise(function(resolve, reject) {
        const uri = formatUri(func)
        fetch(uri, request).then(response => {
            if(response.ok){
                response.json().then(data => resolve(data)) 
            }
            else{
                switch(response.status){
                    case 400:
                        response.json().then(data => {
                            let msg = "";
                            for (const errorKey in data) {
                                if (Array.isArray(data[errorKey])){
                                    data[errorKey].map(m => {
                                        msg += `${m}`;
                                        return m;
                                    });
                                }
                                else {
                                    msg += `${data[errorKey]}`;
                                }
                            }
                            resolve({ error: msg ? msg : JSON.stringify(response)})
                        })
                        break;
                    case 401: reject({ error: JSON.stringify(response) })
                        break;
                    case 404: resolve({ error: JSON.stringify(response) })
                        break;
                    default: reject({ error: JSON.stringify(response) })
                        break;
                }
            }
        })
      })
}

function formatUri(func){
    return process.env.VUE_APP_API_ENDPOINT + (func.startsWith('/') ? func : `/${func}`)
}