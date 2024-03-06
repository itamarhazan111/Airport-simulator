import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
function AdminActions() {

    const [stationId, setStationId] = useState(4);
    const urlSetEmegency = "http://localhost:7072/api/Flights/ActivateEmergency";
    const urlDeleteEmergency = "http://localhost:7072/api/Flights/DeactivateEmergency";


    const navigate = useNavigate();
    const submitEmeregncy = async (e) => {
        e.preventDefault()
        try {
            const res = await axios.post(urlSetEmegency, {
                "StationId": stationId
            });
            console.log(res.data)
            navigate("/")
        } catch (err) {
            console.log(err);
        }
    }


    const DeleteEmergency = async (e) => {
        e.preventDefault()
        try {
            const res = await axios.post(urlDeleteEmergency, {
                "StationId": stationId
            });
            console.log(res.data)
            navigate("/")
        } catch (err) {
            console.log(err);
        }
    }
    const Validate = (e) => {
        if (e.target.value >= 1 && e.target.value <= 9)
            return true;
        else
            return false;
    }
    const Validate_Station = (e) => {
        const element = document.getElementById("input");
        const button1 = document.getElementById("submit_Emeregncy");
        const button2 = document.getElementById("delete_Emeregncy");

        if (Validate(e)) {
            element.style.color = "black";  // Reset color to default
            button1.disabled = false;        // Enable the button
            button2.disabled = false;        // Enable the button
            setStationId(e.target.value);
        } else {
            element.style.color = "red";    // Set color to red
            button1.disabled = true;         // Disable the button
            button2.disabled = true;         // Disable the button

        }
    }


    return (
        <div className="row">
            <div className="col-6">
                <form onSubmit={submitEmeregncy}>
                    <h1 className="h3 mb-3 fw-normal">station id</h1>
                    <div className="form-floating">
                        <input id="input" type="number" className="form-control" placeholder="please choose number between 1-9"
                            onChange={Validate_Station}
                        />
                    </div>
                    <button id="submit_Emeregncy" className="btn btn-primary w-100 py-2  mb-2" type="submit" disabled={true}>Submit</button>
                </form>
                <button id="delete_Emeregncy" onClick={DeleteEmergency} className="btn btn-primary w-100 py-2" disabled={true}>Cancel</button>
            </div>

        </div>
    )
}
export default AdminActions