import React, { useState, useEffect } from 'react';
import './App.css';
import { BrowserRouter as Router, Routes , Route , Link } from 'react-router-dom';
import Home from './Pages/Home'
import Login from './Pages/Login/login'
import Serives from './Pages/Services/Service';
import NotFound from './Pages/NotFound';
import About from './Pages/About';
import SrvService from "./Services/services.service"
import Logout from './Pages/Login/Logout';
import Disconnect from './Pages/Login/disconect';






function App() {

  
  const [connected, setConnected] = useState(false);
  // const [service, setService] = useState(null);
  // const [loading, setLoading] = useState(false);
  

  useEffect(() => {

    getServices();     
      
  
  }, []);

  const getServices = () => {
   
    SrvService.getAllService().then
    ( (data) => {         
       
       console.log("connected !");
       setConnected(true);

       
    },        
      (error) => {
         //---Check error    
        console.log(error);
        console.log("not connected !");  
               

      }
     
    ) ;
  }

  return (
    <div className='App'>
    <Router>
     {connected === true ? <nav style={{ margin: 10 }} >
          <Link to="/" style={{ padding: 5 }}>
            Home
          </Link>         
          <Link to="/Service" style={{ padding: 5 }}>
            Services
          </Link>
          <Link to="/about" style={{ padding: 5 }}>
            About
          </Link>
          <Link to="/logout" style={{ padding: 5 }}>
            Logout
          </Link>
      </nav> 
      : // Not connected
      <nav style={{ margin: 10 }} >
          <Link to="/" style={{ padding: 5 }}>
            Login
          </Link>
          
      </nav> }
      <Routes>
        <Route path="/" element={connected ? <Home /> : <Login /> } />
        <Route path="/home" element={<Home />} />
        <Route path="/service" element={<Serives />}>        
        </Route>
        <Route path="/about" element={<About />} />
        <Route path="/logout" element={<Logout />} />
        <Route path="/disconnect" element={<Disconnect />} />
        Disconnect
        <Route path="*" element={<NotFound />} />
      </Routes>
    </Router>
    </div>
  );
}

export default App;
