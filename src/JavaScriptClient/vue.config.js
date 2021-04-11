const fs = require('fs');
const path = require('path');

module.exports = {
    devServer: {      
        // these three properties are for using https during local development; if you do not use this then you can skip these
        pfx: fs.readFileSync(path.resolve(__dirname, 'localhost.pfx')),
        pfxPassphrase: 'password', // this password is also hard coded in the build script which makes the certificates
        https: true,
        host: "localhost"
    },
  }