import './clock.css'
import React, { useState, useEffect } from 'react'

export const Clock = () => {
  return (
    <div className='clock-container'>
      <div className='clock'>
        <div className='clock-face'>
          <ClockHands />
        </div>
        <ClockNumbers />
      </div>
    </div>
  )
}

const ClockHands = () => {
  const [degrees, setDegrees] = useState(getClockHandDegrees())
  useEffect(() => {
    setInterval(() => setDegrees(getClockHandDegrees()), 1000 * 60)
  }, [setDegrees])

  return (
    <>
      <div
        className='hand hour-hand'
        style={{ transform: `rotate(${degrees.hourDegrees}deg)` }}></div>
      <div
        className='hand min-hand'
        style={{ transform: `rotate(${degrees.minuteDegrees}deg)` }}></div>
      {/* <div
        className='hand second-hand'
        style={{ transform: `rotate(${degrees.secondDegrees}deg)` }}></div> */}
    </>
  )
}

const ClockNumbers = () => (
  <>
    <div className='number number-zero'>
      <div className='rotation-correction-zero'>12</div>
    </div>
    <div className='number number-one'>
      <div className='number-stripe'></div>
    </div>
    <div className='number number-two'>
      <div className='number-stripe'></div>
    </div>
    <div className='number number-three'>
      <div className='rotation-correction-three'>3</div>
    </div>
    <div className='number number-four'>
      <div className='number-stripe'></div>
    </div>
    <div className='number number-five'>
      <div className='number-stripe'></div>
    </div>
    <div className='number number-six'>
      <div className='rotation-correction-six'>6</div>
    </div>
    <div className='number number-seven'>
      <div className='number-stripe'></div>
    </div>
    <div className='number number-eight'>
      <div className='number-stripe'></div>
    </div>
    <div className='number number-nine'>
      <div className='rotation-correction-nine'>9</div>
    </div>
    <div className='number number-ten'>
      <div className='number-stripe'></div>
    </div>
    <div className='number number-eleven'>
      <div className='number-stripe'></div>
    </div>
  </>
)

function getClockHandDegrees() {
  const now = new Date()
  const seconds = now.getSeconds()
  const secondDegrees = (seconds / 60) * 360 + 90
  const minutes = now.getMinutes()
  const minuteDegrees = (minutes / 60) * 360 + (seconds / 60) * 6 + 90
  const hours = now.getHours()
  const hourDegrees = (hours / 12) * 360 + (minutes / 60) * 30 + 90
  return { secondDegrees, minuteDegrees, hourDegrees }
}
