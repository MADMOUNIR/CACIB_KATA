import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import SrvService from "../../Services/services.service"
import './Service.css';



const Services = () => {
   
    const emptySrv = {  
                        id: null,
                        name: '',
                        actions: null
                    };
                    const emptyAction = 
                    {  
                        id: null,
                        name: ''                      
                    };
    const navigate = useNavigate();                    
    const [servicesData, setServicesData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [selectedService, SetSelectedService] = useState(emptySrv);
    const [selectedAction, SetSelectedAction] = useState(emptyAction);
    const [actionUUID, SetActionUUID] = useState("");
    const [logAction, SetLogAction] = useState("");

    useEffect(() => {

        getServices();     
                
      }, []);
    
      const getServices = async () => {
        setLoading(true);
        SrvService.getAllService().then
        ( (data) => {         
            setServicesData(data);
            setLoading(false);
                      
        },        
          (error) => {
             //---Check error    
            console.log(error);
            console.log("not connected !");  
            setLoading(false);     
            if(error.response.status === 401) {
                navigate("/disconnect");
                window.location.reload();
            }
    
          }
         
        ) ;
      }


      const launchService = async (serviceName , action) => {
             
        SrvService.launchAction(serviceName , action).then
        ( (data) => {         
            SetActionUUID(data.uuid);
            setLoading(false);
                      
        },        
          (error) => {
             //---Check error    
            console.log(error);
            console.log("error");  
            setLoading(false);     
            if(error.response.status === 401) {
                navigate("/disconnect");
                window.location.reload();
            }
    
          }
         
        ) ;

      }

      const showLog = async (uuid) => {
        setLoading(true);
        SrvService.showLogAction(uuid).then
        ( (data) => {         
            SetLogAction(data);
            setLoading(false);
                      
        },        
          (error) => {
             //---Check error    
            console.log(error);
            console.log("error");  
            setLoading(false);     
            if(error.response.status === 401) {
                navigate("/disconnect");
                window.location.reload();
            }
    
          }
         
        ) ;

      }
   
    
   return (
   <div>  
        <h1>List of Services </h1>

        <table >
            <tr>
                <th>Service Id</th>
                <th>Service Name</th>
                <th>Show Actions</th>
            </tr>
             { loading ? "loading ..." : 
            
            servicesData && servicesData.map((el) => {
                             
                return(
                    <tr key={el.id}>
                        <td>{el.id}</td>
                       <td>{el.name}</td>
                       <td> <button type="button" onClick={() => {
                               console.log(el);    
                               SetSelectedService(el);    
                                                      
                                }}>
                             Show 
                            </button>
                        </td>
                     </tr>
                )
            })} 
        </table>

        <h2>{selectedService.id &&  <p>List Actions of {selectedService.name}</p>}</h2>

        {selectedService.actions && 
        <table >
            <tr>
                <th>Action Id</th>
                <th>Action Name</th>
                <th>Launch Action</th>
            </tr>
             { loading ? "loading ..." : 
            
            selectedService.actions && selectedService.actions.map((ac) => {                             
                return(
                    <tr key={ac.id}>
                        <td>{ac.id}</td>
                       <td>{ac.name}</td>
                       <td> <button type="button" onClick={() => {
                               console.log(ac);   
                               SetSelectedAction(ac);     
                               launchService(selectedService.name , ac);                                                    
                                }}>
                            Launch
                            </button>
                        </td>
                     </tr>
                )
            })} 
        </table>}

        <h2>{selectedAction.id &&  <p>Launch Action :  {selectedAction.name}</p>}</h2>

        {actionUUID !== "" && 
        <div>
        <h4>Action launched , UUID = {actionUUID}</h4> 
        <button type="button" onClick={() => {
                                                showLog(actionUUID);                                                                 
                                            }}>
         Show Log
         </button> 
         </div>}

         {logAction !== "" ? <div><pre><p>{logAction}</p></pre></div> : ""}
        
        
        </div>


    );
  };
  
  export default Services;