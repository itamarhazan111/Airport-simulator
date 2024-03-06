import React, { useEffect } from 'react'
import { HubConnectionBuilder } from '@microsoft/signalr';
import { useState } from 'react';
import image from '../Images/image.jpg';
import Plane from './Plane';
import Fire from './Fire';
import FireSwtich from './FireSwitch';

import './Station.css'

function Station({ name }) {


  const connection = new HubConnectionBuilder().withUrl("http://localhost:7072/airport").build();
  // connection.start().then(()=> console.log("ariport is operiting")).catch((err)=>console.error(err));
  const [planName, SetPlanName] = useState("");
  const [isEmergency, setIsEmergency] = useState(false);

  useEffect(() => {
    connection.start().catch(err => console.error("Error starting SignalR:", err));
    return () => {
      connection.stop().catch(err => console.error("Error stopping SignalR:", err));
    };
  }, []);


    connection.on(name, msg => {
      console.log(name + "  " + msg);
      SetPlanName(msg);
    });

    connection.on(`${name}-emergency`, onOff => {
      console.log("Emergency status for ", name, ": ", onOff);
      setIsEmergency(onOff);
    });


  const isPlane = (name) => {
    if (name != "") {
      if (name.includes('landing')) {
        return <Plane dirction='135' color="blue" />
      }
      return <Plane dirction='45' color="Red" />
    }
  }



  return (
    <div>

      <div className={`station-container s-${name}`}>
        <h4 >Station {name} </h4>
        {isEmergency && <Fire></Fire>}
        <p className="plane">{isPlane(planName)}</p>
        <h5 className="plane-title">{planName}</h5>
      </div>
      <div>
        <FireSwtich name={name}></FireSwtich>
      </div>
    </div>
  )
}

export default Station