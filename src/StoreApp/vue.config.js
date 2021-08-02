const fs = require('fs');
const path = require('path');


const devServer = {
    host: "localhost",
    port: 8081
}

if(process.env.VUE_APP_USE_HTTPS == 'true') {
    Object.assign(devServer, {      
        // these three properties are for using https during local development; if you do not use this then you can skip these
        pfx: fs.readFileSync(path.resolve(__dirname, 'localhost.pfx')),
        pfxPassphrase: 'password', // this password is also hard coded in the build script which makes the certificates
        https: true
    });
}

module.exports = {
    devServer
  }