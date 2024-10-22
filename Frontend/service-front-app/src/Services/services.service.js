import axios from 'axios';
import authHeader from "./auth.header";


const getAllService = () =>
{
 let URL_GET_SERVICE = `${process.env.REACT_APP_URL_BACKEND}/services`;


 
   
    return axios.get(URL_GET_SERVICE ,  authHeader())
    .then((response) => {    
                          
         return response.data;
    })  
}

const launchAction = async (serviceName , action) => {

    let URL_GET_LAUNCH_ACTION = `${process.env.REACT_APP_URL_BACKEND}/services/${serviceName}/`;
  
    

    return axios.post(URL_GET_LAUNCH_ACTION, action ,authHeader())
      .then((response) => {                 
        return response.data;
      })
      .catch( error => {
        console.log(error);
        
      });
     
  };

  const showLogAction = async (uuid) => {

    let URL_GET_SHOW_LOG = `${process.env.REACT_APP_URL_BACKEND}/ws/${uuid}`;

    return axios.get(URL_GET_SHOW_LOG ,  authHeader())
    .then((response) => {    
                            
         return response.data;
    })
    .catch( error => {
        console.log(error);
        
      });

  }




//----------Export function
const SrvService = {  
    getAllService,
    launchAction,
    showLogAction
  
  };
  
  export default SrvService;