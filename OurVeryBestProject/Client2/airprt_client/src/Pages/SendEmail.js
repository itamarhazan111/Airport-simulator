import React,{useState,SyntheticEvent,useEffect} from 'react'
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import VerifyCode from './VerifyCode';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import FormInput from '../Components/FormInput';



function SendEmail() {

    const navigate = useNavigate();
    const url="http://localhost:8000/api/SendEmail"
    const [values, setValues] = useState({
      email: "",
    });
    const inputs = [
      {
        id: 1,
        name: "email",
        type: "email",
        placeholder: "Email",
        errorMessage: "It should be a valid email address!",
        label: "Email",
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
              email:values.email,
        });
            console.log(res.data)
            navigate("/Verify/"+values.email)
        }catch(err){
          if(err.response.data.message===null || err.response.data.message===undefined)
            alert("server error");
          else
            alert(err.response.data.message)
        }     
      
    }

  return (
    <div className="container">
            <form onSubmit={submit}>
            <div className="ui divider">
              <h1 className="h3 mb-3 fw-normal">send code for your email</h1>
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
              <button className="btn btn-primary w-100 py-2" type="submit">Send</button>
            </form>
    </div>
  )
}

export default SendEmail