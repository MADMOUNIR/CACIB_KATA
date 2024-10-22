import axios from "axios";



const login = async (email, password) => {

    const URL_LOGIN = `${process.env.REACT_APP_URL_BACKEND}/auth/`;
    const msg = process.env.REACT_APP_URL_BACKEND;
   
    
    return axios.post(URL_LOGIN, {
        username: email,
        password : password,
      })
      .then((response) => {        
        
        if (response.data.token) {
            sessionStorage.setItem(process.env.REACT_APP_TOKEN_NAME, response.data.token);
        
        }
  
        return response.data;
      });
  };

  
const logout = () => {       
    sessionStorage.removeItem(process.env.REACT_APP_TOKEN_NAME);      
    return "OK"
    
  };

  const authService = {  
    login,
    logout
  
  };
  
  export default authService;
  