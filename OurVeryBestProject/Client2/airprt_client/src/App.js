
import './App.css';
import Nav from './Components/Nav';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Home from './Pages/Home';
import Simulator from './Pages/Simulator';
import AdminActions from './Pages/AdminActions';
import logo from './logo.svg';
import './App.css';
import React, { useEffect,useState } from 'react'
import axios from 'axios';
import Login from './Pages/Login';
import Register from './Pages/Register';
import Edit from './Pages/Edit';
import SendEmail from './Pages/SendEmail';
import VerifyCode from './Pages/VerifyCode';
import ChangePassword from './Pages/ChangePassword';

function App() {
  const [id,setId]=useState(0);
  const [name,setName]=useState('');
  const url="http://localhost:8000/api/user"
  axios.defaults.withCredentials = true;
  useEffect(()=>{
      (
          async()=>{
              try{
                  const res=await axios.get(url)
                  console.log(res.data)
                  setName(res.data.name)
                  setId(res.data.id)
              }catch(err){
                  console.log(err);
              }  
          }
      )();
  } 
)

  return (
        <div className="App">
        <BrowserRouter>
            <Nav name={name} setName={setName} />
          <main className="form-signin w-100 m-0 p-0">

               <Routes>
                <Route path='/' Component={()=><Home name={name}/>}/>
                <Route path='/login' Component={()=><Login setName={setName}/>}/>
                <Route path='/register' Component={Register}/>
                <Route path='/simulator' Component={Simulator}/>
                <Route path='/edit' Component={()=><Edit id={id} setName={setName}/>}/>
                <Route path='/SendEmail' Component={SendEmail}/>
                <Route path='/Verify/:email' Component={VerifyCode} />
                <Route path='/Change/:email' Component={ChangePassword}/>
              </Routes> 
          </main>
          
        </BrowserRouter>
    </div>
  );
}

export default App;