import React, { useEffect,useState } from 'react'
import axios from 'axios';


function Home(props) {

  return (
    <div>{props.name ? 'hi '+props.name :'you are not logged in'}</div>
  )
}

export default Home