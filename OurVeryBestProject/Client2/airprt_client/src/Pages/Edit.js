import React,{useState,SyntheticEvent,useRef} from 'react'
import { useNavigate, redirect } from 'react-router-dom';
import axios from 'axios';
import FormInput from '../Components/FormInput';

function Edit(props) {

    const navigate = useNavigate();
    const url='http://localhost:8000/api/user/'+props.id;
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
    const submit=async (e)=>{
        e.preventDefault()
        try{
            const res=await axios.put(url,{
              name:values.name,
              email:values.email,
              password:values.password,
        });
            console.log(res.data)
            props.setName(values.name);
            return navigate("/")
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
    <div className="ui divider">
    <h1 className="h3 mb-3 fw-normal">Please register</h1>
    </div>
      <div className="ui form">
          {inputs.map((input) => (
          <div className="ui form">
          <FormInput
            key={input.id}
            {...input}
            value={values[input.name]}
            onChange={onChange}
          />
          </div>
          ))}
      </div>
      <button className="btn btn-primary w-100 py-2" type="submit">Submit</button>
    </form>
</div>
  )
}

export default Edit