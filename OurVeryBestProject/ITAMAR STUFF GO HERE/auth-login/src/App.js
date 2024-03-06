import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import React, { useEffect,useState } from 'react'
import axios from 'axios';
import Login from './pages/Login';
import Home from './pages/Home';
import Nav from './components/Nav';
import Register from './pages/Register';
import Simulator from './pages/Simulator';
import Edit from './pages/Edit';
import SendEmail from './pages/SendEmail';
import VerifyCode from './pages/VerifyCode';
import ChangePassword from './pages/ChangePassword';

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
          <main className="form-signin w-100 m-auto">

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
