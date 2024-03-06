import React,{useState,SyntheticEvent,useEffect} from 'react'
import { useNavigate, useParams } from 'react-router-dom';
import axios from 'axios';
import FormInput from '../components/FormInput';

function VerifyCode() {
    const params=useParams();
    const navigate = useNavigate();
    const url="http://localhost:8000/api/verifyCode"
    const [values, setValues] = useState({
      code:"",
    });
    const inputs = [
      {
        id: 1,
        name: "code",
        type: "text",
        placeholder: "code",
        errorMessage:
          "code should be 6 characters!",
        label: "code",
        pattern: "^[0-9]{6}$",
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
                code:values.code,   
        });
            console.log(res.data)
            navigate("/Change/"+params.email)
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
              <h1 className="h3 mb-3 fw-normal">please write the code</h1>     
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
              <button className="btn btn-primary w-100 py-2" type="submit">Check</button>
            </form>
        </div>
  )
}

export default VerifyCode