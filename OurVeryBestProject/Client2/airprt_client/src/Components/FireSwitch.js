import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
function FireSwtich(props) {

    const urlSetEmegency = "http://localhost:7072/api/Flights/ActivateEmergency";
    const urlDeleteEmergency = "http://localhost:7072/api/Flights/DeactivateEmergency";
    const [isEmergency, setIsEmergency] = useState(false);

    const submitEmeregncy = async (e) => {
        e.preventDefault()
        try {
            const res = await axios.post(urlSetEmegency, {
                "StationId": props.name
            });
            console.log(res.data)
        } catch (err) {
            console.log(err);
            return false;
        }
        return true;
    }


    const DeleteEmergency = async (e) => {
        e.preventDefault()
        try {
            const res = await axios.post(urlDeleteEmergency, {
                "StationId": props.name
            });
            console.log(res.data)
        } catch (err) {
            console.log(err);
            return true;
        }
        return false;
    }



    const butFu = async (e) => {
        if (!isEmergency) {
            setIsEmergency(await submitEmeregncy(e));
        }
        else {
            setIsEmergency(await DeleteEmergency(e));
        }
    }
    const isButtonEmergency = (name) => {
        if (isEmergency)
            return "DeactivateEmergency"
        else
            return "ActivateEmergency"
    }

    return (
        <div>
            <button onClick={butFu} style={{ padding: '5px 10px', fontSize: '12px' }}>
                Toggle Emergency
            </button>
        </div>


    )
}
export default FireSwtich



