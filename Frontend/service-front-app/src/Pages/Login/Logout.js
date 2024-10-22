import React from "react";
import AuthService from "../../Services/auth.service";
import { useNavigate } from "react-router-dom";


function Logout() {

    const navigate = useNavigate();

    const signout = () => {

        console.log("signout");
        

         let resp = AuthService.logout();
         console.log(resp);
         navigate("/");
         window.location.reload(false);

         
          
          

    }


    return (
      <div style={{ padding: 20 }}>
        <h2>Logout</h2>
        <p>really ?</p>
        <button type="button" onClick={() => {
                                                 signout();                                                             
                                            }}>
        Logout
        </button>
      </div>
    );
}
  export default Logout;