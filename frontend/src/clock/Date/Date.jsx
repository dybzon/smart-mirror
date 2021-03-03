import './date.css'
import React, { useState, useEffect } from 'react'

export const DateLabel = () => {
  const [date, setDate] = useState(new Date())
  useEffect(() => {
    const hours = 23 - date.getHours()
    const mins = 59 - date.getMinutes()
    const secs = 59 - date.getSeconds()

    const ms = (secs + mins * 60 + hours * 60 * 60) * 1000
    setTimeout(function() {
      setDate(new Date())
    }, ms)
  }, [date, setDate])

  return (
    <div className='date-container'>
      {date.getDate()}. {months[date.getMonth()].toLowerCase()}{' '}
      {date.getFullYear()}
    </div>
  )
}

const months = [
  'Januar',
  'Februar',
  'Marts',
  'April',
  'Maj',
  'Juni',
  'Juli',
  'August',
  'September',
  'Oktober',
  'November',
  'December',
]
