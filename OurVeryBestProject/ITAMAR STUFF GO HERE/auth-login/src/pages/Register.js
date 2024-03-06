import axios, { AxiosError } from 'axios';
import React,{useState,SyntheticEvent, useEffect} from 'react'
import { useNavigate, redirect } from 'react-router-dom';
import FormInput from '../components/FormInput';


function Register() {
  const navigate = useNavigate();
  const url="http://localhost:8000/api/register"
  const [isAdmin, setIsAdmin] = useState(0);
    const [values, setValues] = useState({
      name: "",
      email: "",
      password: "",
    });
    const inputs = [
      {
        id: 1,
        name: "name",
        type: "text",
        placeholder: "name",
        errorMessage:
          "name should be 3-16 characters and shouldn't include any special character!",
        label: "name",
        pattern: "^[A-Za-z0-9]{3,16}$",
        required: true,
      },
      {
        id: 2,
        name: "email",
        type: "email",
        placeholder: "Email",
        errorMessage: "It should be a valid email address!",
        label: "Email",
        required: true,
      },
      {
        id: 3,
        name: "password",
        type: "password",
        placeholder: "Password",
        errorMessage:
          "Password should be 8-20 characters and include at least 1 letter and 1 number!",
        label: "Password",
        pattern: `^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9!@#$%^&*]{8,20}$`,
        required: true,
      },
    ];
  
    const onChange = (e) => {
      setValues({ ...values, [e.target.name]: e.target.value });
    };

    const admin=(e)=>{
        if(e==true){
            setIsAdmin(1);
        }else{
            setIsAdmin(0);
        }

    }


  
    const handleSubmit = async(e) => {
        e.preventDefault();
        try{
          const res=await axios.post(url,{
              name:values.name,
              email:values.email,
              password:values.password,
              isAdmin:isAdmin
      });
          console.log(res.data)
          navigate("/login")
      }catch(err){
        if(err.response===null || err.response===undefined)
          alert("server error");
        else
          alert(err.response.data.message)
        console.log(err);
      }     
    }
  

    return (
      <div className="container">
  
        <form onSubmit={handleSubmit}>
          <div className="ui divider"><h1>Register</h1></div>
          <div className="ui form">
          {inputs.map((input) => (
          <div className="form-floating">
          <FormInput
            key={input.id}
            {...input}
            value={values[input.name]}
            onChange={onChange}
          />
          </div>
          ))}
          
            <div className="form-check text-start my-3">
                <input className="form-check-input" type="checkbox" value="remember-me" id="flexCheckDefault"
                    onChange={e=>admin(e.target.checked)}
                />
                <label className="form-check-label">
                    Is Admin
                </label>
              </div>
            <button  className="btn btn-primary w-100 py-2">Submit</button>
          </div>
        </form>
      </div>
    );
  }
  


export default Register