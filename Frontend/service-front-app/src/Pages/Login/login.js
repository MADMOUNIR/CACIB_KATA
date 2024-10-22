import React, { useState,useRef } from "react";
import { useNavigate } from "react-router-dom";
import "./Login.css";
import AuthService from "../../Services/auth.service";


const Login = () => {

    const [user, setUser] = useState("");
    const [password, setPassword] = useState("");
    const [errorMsg, setErrorMsg] = useState("");
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();
    const msgsError = useRef(null);

    const handleLogin = async (e) => {
        e.preventDefault();
        setLoading(true);      
        // setTimeout(() => {
        //   console.log('Hello, World!')
        // }, 3000);
        try {
          await AuthService.login(user, password).then(
            (resp) => {         
             
                setErrorMsg("");
          
              if(resp)
              {
                
                setLoading(false);
                navigate("/");
                window.location.reload(false);
              }
              
           
            },
            (error) => {
              console.log(error);   
              setLoading(false);  
              setErrorMsg("invalide login password !");        
             
            }
          );
         // window.location.reload();
      
        } catch (err) {
          console.log(err);
          setLoading(false);
          setErrorMsg("error : " + err);

        }
      };

      

    return  (
    <div id="login-form">
        <h1>Login</h1>
        <form onSubmit={handleLogin}>
        <label htmlFor="username">Username:</label>
        <input type="text" id="username" name="username" value={user} onChange={(e) => setUser(e.target.value)}/>
        <label htmlFor="password" >Password:</label>
        <input type="password" id="password" name="password" value={password} onChange={(e) => setPassword(e.target.value)} />
        <input type="submit" value="Submit" />
        </form>
        <p>{errorMsg}</p>
  </div>
  );
  };
  
  export default Login;