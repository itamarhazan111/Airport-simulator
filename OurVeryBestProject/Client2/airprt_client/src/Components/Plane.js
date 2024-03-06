import React from 'react'
import { BsAirplane, BsAirplaneFill } from'react-icons/bs';

function Plane(props) {
  return (
    <div>
        <BsAirplaneFill style={{width:"80px", height: "80px", color: props.color, transform: 'rotate('+props.dirction+'deg)'}}/>
    </div>
  )
}

export default Plane