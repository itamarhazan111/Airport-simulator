import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import axios from 'axios';



function Simulator() {
    const [landing, setLanding] = useState(1000);
    const [departing, setDeparting] = useState(1000);
    const urlDeparting = "http://localhost:8001/api/Simulator/CreateDeprature";
    const urlLanding = "http://localhost:8001/api/Simulator/CreateLanding";
    const urldelete = "http://localhost:8001/api/Simulator/Delete";



    const navigate = useNavigate();
    const submitLanding = async (e) => {
        e.preventDefault()
        try {
            console.log(landing);
            const res = await axios.post(urlLanding, {
                "IntervalForTask": landing
            });
            console.log(res.data)
            navigate("/")
        } catch (err) {
            console.log(err);
        }
    }
    const submitDeparting = async (e) => {
        e.preventDefault()
        try {
            console.log(departing);
            const res = await axios.post(urlDeparting, {
                "IntervalForTask": departing

            });
            console.log(res.data)
            navigate("/")
        } catch (err) {
            console.log(err);
        }
    }
    const DeleteDep = async (e) => {
        e.preventDefault()
        try {
            const res = await axios.post(urldelete, {
                "IsLanging": false
            });
            console.log(res.data)
            navigate("/")
        } catch (err) {
            console.log(err);
        }
    }
    const DeleteLan = async (e) => {
        e.preventDefault()
        try {
            const res = await axios.post(urldelete, {
                "IsLanging": true
            });
            console.log(res.data)
            navigate("/")
        } catch (err) {
            console.log(err);
        }
    }
    const Validate_Interval = (e) => {
        if (e.target.value >= 500 && e.target.value <= 10000)
            return true;
        else
            return false;
    }
    const Validate_Interval_Landing = (e) => {
        const element = document.getElementById("input_landing");
        const button = document.getElementById("submit_landing");
    
        if (Validate_Interval(e)) {
            element.style.color = "black";  // Reset color to default
            button.disabled = false;        // Enable the button
            setLanding(e.target.value);
        } else {
            element.style.color = "red";    // Set color to red
            button.disabled = true;         // Disable the button
        }
    }
    const Validate_Interval_Deprature = (e) => {
        const element = document.getElementById("input_Deprature");
        const button = document.getElementById("submit_Deprature");
    
        if (Validate_Interval(e)) {
            element.style.color = "black";  // Reset color to default
            button.disabled = false;        // Enable the button
            setDeparting(e.target.value);
        } else {
            element.style.color = "red";    // Set color to red
            button.disabled = true;         // Disable the button
        }
    }
    return (
        <div className="row">
            <div className="col-6">
                <form onSubmit={submitLanding}>
                    <h1 className="h3 mb-3 fw-normal">time for landing</h1>
                    <div className="form-floating">
                        <input id="input_landing" type="number" className="form-control" placeholder="please choose number between 500-10000"
                            onChange={Validate_Interval_Landing }
                        />
                    </div>
                    <button id="submit_landing" className="btn btn-primary w-100 py-2  mb-2" type="submit"  disabled={true}>Submit</button>
                </form>
                <button onClick={DeleteLan} className="btn btn-primary w-100 py-2" >Cancel landing</button>
            </div>
            <div className="col-6">
                <form onSubmit={submitDeparting}>
                    <h1 className="h3 mb-3 fw-normal">time for departing</h1>
                    <div className="form-floating">
                        <input id ="input_Deprature" name="deprature_interval_field" type="number" className="form-control" placeholder="please choose number between 500-10000"
                            onChange={Validate_Interval_Deprature}
                        />
                    </div>
                    <button id ="submit_Deprature" className="btn btn-primary w-100 py-2 mb-2" type="submit"disabled={true}>Submit</button>
                </form>
                <button onClick={DeleteDep} className="btn btn-primary w-100 py-2" >Cancel departing</button>

            </div>
        </div>
    )
}

export default Simulator