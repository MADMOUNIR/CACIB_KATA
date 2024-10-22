
export default function authHeader() {
  //const user = JSON.parse(sessionStorage.getItem(process.env.REACT_APP_TOKEN_NAME));
 
  const validToken =  getSessionStorageOrDefault(process.env.REACT_APP_TOKEN_NAME,"");
  
  const config = {
    headers: { Authorization: `Bearer ${validToken}` ,
    'Content-Type': 'application/json' }
};

  if (validToken) {
     return config;
    //return { "x-auth-token": user.accessToken };
  } else {
    return {};
  }

  function getSessionStorageOrDefault(key, defaultValue) {
    const stored = sessionStorage.getItem(key);
    if (!stored) {
      return defaultValue;
    }
    return stored;
  }
}