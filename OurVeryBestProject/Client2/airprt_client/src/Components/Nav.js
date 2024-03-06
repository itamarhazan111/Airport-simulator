import React from 'react'
import { Link } from 'react-router-dom'
import axios from 'axios';

function Nav(props) {
  const url="http://localhost:8000/api/logout"
  axios.defaults.withCredentials = true;
  const logout=async ()=>{
      try{
          const res=await axios.post(url);
          console.log(res.data) 
          props.setName('')
      }catch(err){
      console.log(err);
      }
  }
  let menu;
  let simulator;
  if(props.name===''){
      menu=(
      <ul className="navbar-nav me-auto mb-2 mb-sm-0">
      <li className="nav-item">
        <Link to="/login" className="nav-link active" aria-current="page">Login</Link>
      </li>
      <li className="nav-item">
        <Link to="/register" className="nav-link active" aria-current="page" >Register</Link>
      </li>                    
    </ul>)
  }else{
      menu=(
          <ul className="navbar-nav me-auto mb-2 mb-sm-0">
          <li className="nav-item">
            <Link to="/login" className="nav-link active" onClick={logout} aria-current="page">Logout</Link>
          </li>    
          <li className="nav-item">
            <Link to="/edit" className="nav-link active"  aria-current="page">edit profile</Link>
          </li>                
        </ul>)
  }

return (
  <div>
          <nav className="navbar navbar-expand-sm navbar-dark bg-dark" aria-label="Third navbar example">
            <div className="container-fluid">
              <div>
                  <ul className="navbar-nav me-auto mb-2 mb-sm-0">
                      <Link to="/" className="navbar-brand" >Home</Link>
                      <Link to="/simulator" className="nav-link active"  aria-current="page">simulator</Link>
                  </ul>
              </div>
              <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarsExample03" aria-controls="navbarsExample03" aria-expanded="false" aria-label="Toggle navigation">
                <span className="navbar-toggler-icon"></span>
              </button>

              <div>
                  {menu}
              </div>
            </div>
          </nav>
  </div>
)
}
export default Nav