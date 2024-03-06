import React,{useState,SyntheticEvent,useEffect} from 'react'
import { useNavigate, useParams } from 'react-router-dom';
import axios from 'axios';
import VerifyCode from './VerifyCode';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import FormInput from '../components/FormInput';


function ChangePassword() {
    const params=useParams();
    const navigate = useNavigate();
    const url="http://localhost:8000/api/ChangePassword"
    const [values, setValues] = useState({
      password: "",
    });
    const inputs = [
      {
        id: 1,
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

    const submit=async (e)=>{
        e.preventDefault()
        try{
            const res=await axios.post(url,{
                email:params.email,
                password:values.password
        });
            console.log(res.data)
            navigate("/login")
        }catch(err){
          if(err.response===null || err.response===undefined)
            alert("server error");
          else
            alert(err.response.data.message)
        }     
      
    }
  return (
    <div className="container">
            <form onSubmit={submit}>
            <div className="ui divider">
              <h1 className="h3 mb-3 fw-normal">Change your password</h1>
              </div>
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
              </div>
              <button className="btn btn-primary w-100 py-2" type="submit">Change</button>
            </form>
    </div>
  )
}

export default ChangePassword