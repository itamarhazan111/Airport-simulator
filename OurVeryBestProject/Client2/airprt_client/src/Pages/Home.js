import React from 'react'
import image from '../Images/Airport.jpeg';
import './Home.css';
import logo from '../logo.svg';
import Station from '../Components/Station'

function Home() {
  return (
    <div className="App">
    <img src={image} className='MapImage'></img>
      <table className='MapTable'>
        <tbody>
          <tr>
            <td><Station className="station" name="1"></Station></td>
            <td></td>
            <td><Station className="station" name="2"></Station></td>
            <td></td>
            <td><Station className="station" name="3"></Station></td>
            <td></td>
            <td><Station className="station" name="4"></Station></td>
            <td></td>
            <td><Station className="station" name="9"></Station></td>
          </tr>
          <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
          </tr>
          <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td><Station className="station" name="8"></Station></td>
            <td></td>
            <td><Station className="station" name="5"></Station></td>
            <td></td>
          </tr>
          <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td ></td>
            <td><Station className="station" name="6"></Station></td>
            <td><Station className="station" name="7"></Station></td>
            <td></td>
            <td></td>
          </tr>
        </tbody>
      </table>
    </div>
  )
}

export default Home