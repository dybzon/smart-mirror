import React from 'react'
import { useRepeatingFetch } from '../../utils'
import { faCloudRain, faSnowflake } from '@fortawesome/free-solid-svg-icons'
import './weather.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import settings from '../../settings.json'

const lat = settings.openWeatherMap.lat
const lon = settings.openWeatherMap.lon

export const WeatherStatus = () => {
  const { result, error, isLoaded } = useRepeatingFetch(
    `http://api.openweathermap.org/data/2.5/onecall?lat=${lat}&lon=${lon}&units=metric&exclude=hourly,minutely&appid=${settings.openWeatherMap.apiKey}`,
    1000 * 60 * 30
  )

  if (!isLoaded) {
    return <div>Weather is loading...</div>
  } else if (error) {
    return <div>Failed fetching weather status: {error.message}</div>
  }

  return <Weather {...result} />
}

const Weather = ({ current, daily }) => {
  const today = daily[0]

  return (
    <div className='weather-container'>
      <WeatherIcon iconCode={current.weather[0].icon} />
      <div className='temperature-container'>
        <div className='temperature'>{Math.round(current.temp)}°</div>
        <div className='temperature-range'>
          ({Math.round(today.temp.min)}° til {Math.round(today.temp.max)}°)
        </div>
      </div>
      <Rain {...today} />
    </div>
  )
}

const Rain = ({ rain, snow }) => {
  if (!rain && !snow) {
    return <div className='rain-container'>Ingen regn</div>
  }

  const icon = rain > snow ? faCloudRain : faSnowflake
  const totalRain = Math.round(rain ?? 0 + snow ?? 0)
  return (
    <div className='rain-container'>
      <div className='rain-inner'>
        <div className='icon-container'>
          <div className='icon-vertical-center'>
            <FontAwesomeIcon icon={icon} color='white' />
          </div>
        </div>
        <div>{totalRain} mm</div>
      </div>
    </div>
  )
}

const WeatherIcon = ({ iconCode }) => {
  return (
    <div className='weather-icon-container weather-icon'>
      <div className='weather-icon-vertical-center'>
        <img
          src={`http://openweathermap.org/img/wn/${iconCode}@2x.png`}
          alt=''
        />
      </div>
    </div>
  )
}
