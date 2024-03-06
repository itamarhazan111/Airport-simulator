import React,{useState,SyntheticEvent,useEffect} from 'react'
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import FormInput from '../components/FormInput';

function Login(props) {
    const navigate = useNavigate();
    const url="http://localhost:8000/api/login"
    axios.defaults.withCredentials = true;
    const [values, setValues] = useState({
      email: "",
      password: "",
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
      {
        id: 2,
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
              email:values.email,
              password:values.password,
        });
            console.log(res.data)
            props.setName(res.data.name)
            navigate("/")
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
            <form onSubmit={submit}>
            <div className="ui divider"><h1 className="h3 mb-3 fw-normal">Please sign in</h1></div>
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
              <div className="form-floating mb-3 ">
                <p><a className="link-underline-primary" href="/SendEmail">i forgot my password</a></p>
              </div>
              </div>
              <button className="btn btn-primary w-100 py-2" type="submit">Sign in</button>
            </form>
    </div>
  )
}

export default Login