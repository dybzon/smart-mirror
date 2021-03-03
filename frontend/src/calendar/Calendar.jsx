import React from 'react'
import { useRepeatingFetch } from '../utils'
import './calendar.css'
import { Flag } from './Flag'
import settings from '../settings.json'

const calendarEventsUrl = 'http://localhost:5000/api/googlecalendar/events'

export const Calendar = () => {
  const { result: events, error, isLoaded } = useRepeatingFetch(
    calendarEventsUrl,
    5 * 60 * 1000 // 5 minutes
  )
  if (!isLoaded) {
    return <div>Calendar is loading...</div>
  } else if (error) {
    return <div>Failed fetching calendar events: {error.message}</div>
  }

  const dates = getDatesToDisplay()
  return (
    <div className='calendar-container'>
      {dates.map(date => (
        <CalendarDay
          events={events.filter(e => e.startDate === getFormattedDate(date))}
          date={date}
          key={date}
        />
      ))}
    </div>
  )
}

const CalendarDay = props => {
  const { events, date } = props

  const birthdayEvents = events.filter(
    event => event.calendarId === settings.calendar.birthdayCalendarId
  )
  const regularEvents = events.filter(
    event => event.calendarId !== settings.calendar.birthdayCalendarId
  )

  return (
    <div className='calendar-day'>
      <div className='calendar-day-header'>{getDateLabel(date)}</div>
      <div className='calendar-event-container'>
        <div className='regular-event-container'>
          {regularEvents.map(event => (
            <CalendarEvent event={event} key={event.id} />
          ))}
        </div>
        <div className='birthday-container'>
          {birthdayEvents.map(event => (
            <BirthdayEvent event={event} key={event.id} />
          ))}
        </div>
      </div>
    </div>
  )
}

const CalendarEvent = props => {
  const { event } = props
  const className =
    event.calendarId === settings.calendar.rasmusCalendarId
      ? 'calendar-event calendar-event-rasmus'
      : event.calendarId === settings.calendar.familyCalendarId
      ? 'calendar-event calendar-event-family'
      : ''

  return (
    <div className={className}>
      <div className='event-time'>{getFormattedTime(event.startTime)}</div>
      <div className='event-text'>{event.summary}</div>
    </div>
  )
}

const BirthdayEvent = props => {
  const { event } = props
  return (
    <div className='calendar-event'>
      <div className='flag-container'>
        <Flag />
      </div>
      <div className='calendar-event'>{event.summary}</div>
    </div>
  )
}

function getFormattedTime(d) {
  if (!d) {
    return ''
  }

  const date = new Date(d)
  let hours = date.getHours()
  if (hours < 10) {
    hours = '0' + hours
  }

  let minutes = date.getMinutes()
  if (minutes < 10) {
    minutes = '0' + minutes
  }

  return `${hours}:${minutes}`
}

function getDatesToDisplay() {
  const today = new Date()
  const tomorrow = new Date(today).setDate(today.getDate() + 1)
  const dayAfter = new Date(today).setDate(today.getDate() + 2)

  return [today, tomorrow, dayAfter]
}

function getFormattedDate(date) {
  const d = new Date(date)
  const year = d.getFullYear()
  let month = d.getMonth() + 1
  if (month < 10) {
    month = '0' + month
  }

  let day = d.getDate()
  if (day < 10) {
    day = '0' + day
  }

  return `${year}-${month}-${day}`
}

function getDateLabel(date) {
  const d = new Date(date)
  return `${d.getDate()}. ${months[d.getMonth()]}`
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
