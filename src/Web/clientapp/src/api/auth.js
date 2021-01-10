import { send } from './api'

export default {
    authorize,
    authenticate,
    login,
    logout,
    register
}


async function authorize(){
  var request = {
      method: 'POST',
  }
  return send(`/api/Authentication/Authorize`, request)
}

async function authenticate(token){
    var request = {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` 
        }
    }
    return send(`/api/Authentication/SignIn`, request)
}

async function login(user) {
    //TODO: test token
    localStorage.setItem('user', JSON.stringify(user));
  }

  async function logout() {
    localStorage.removeItem('user');
  }

  async function register(user) {
    //TODO: test token
    localStorage.setItem('user', JSON.stringify(user));
  }