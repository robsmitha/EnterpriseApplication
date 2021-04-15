import Oidc from 'oidc-client'

const config = {
  authority: "https://localhost:5001",
  client_id: "js",
  redirect_uri: "https://localhost:5003/oidc-callback",
  response_type: "code",
  scope: "openid profile api1",
  post_logout_redirect_uri: "https://localhost:5003/home",
};
var mgr = new Oidc.UserManager(config);

export default {
  getUser,
  login,
  logout,
  oidcCallback
}

function getUser(){
  return new Promise(function(resolve) {
      mgr.getUser().then(function (user) {
        if (user) {
            log("User logged in", user.profile)
            resolve(user)
        }
        else {
            log("User not logged in");
            resolve()
        }
      })
    })
}

function login() {
  mgr.signinRedirect();
}

function logout() {
  mgr.signoutRedirect();
}

function oidcCallback(){
  new Oidc.UserManager({ response_mode: "query" }).signinRedirectCallback().then(function () {
      window.location = "/";
  }).catch(function (e) {
      console.error(e);
  });
}

function log() {
  let logMessage = '';

  Array.prototype.forEach.call(arguments, function (msg) {
      if (msg instanceof Error) {
          msg = "Error: " + msg.message;
      }
      else if (typeof msg !== 'string') {
          msg = JSON.stringify(msg, null, 2);
      }
      logMessage += msg + '\r\n';
  });

  console.log(logMessage)
}