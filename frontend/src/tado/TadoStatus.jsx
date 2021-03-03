import React from 'react'
import { useRepeatingFetch } from '../utils'
import {
  faThermometerQuarter,
  faThermometerFull,
  faThermometerHalf,
  faTint,
  faTintSlash,
} from '@fortawesome/free-solid-svg-icons'
import { StatusMessage } from '../utils'

const tadoApiUrl = 'http://localhost:5000/api/tado'
const tadoZoneStatesUrl = `${tadoApiUrl}/zones`

export const TadoStatus = () => {
  const { result, error, isLoaded } = useRepeatingFetch(
    tadoZoneStatesUrl,
    5 * 60 * 1000
  )
  if (!isLoaded) {
    return <div>Tado state is loading...</div>
  } else if (error) {
    return <div>Failed fetching Tado status: {error.message}</div>
  }

  return (
    <div>
      <TemperatureStatus zones={result} />
      <HumidityStatus zones={result} />
    </div>
  )
}

const TemperatureStatus = props => {
  const { zones } = props
  const hotZones = zones.filter(zone => zone.temperature.celcius > 23)
  const coldZones = zones.filter(zone => zone.temperature.celcius < 20)

  if (hotZones.length === 0 && coldZones.length === 0) {
    return (
      <StatusMessage
        icon={faThermometerHalf}
        message='God temperatur i alle rum'
      />
    )
  }

  if (hotZones.length > 0 && coldZones.length > 0) {
    return (
      <>
        <HotMessage zones={hotZones} />
        <ColdMessage zones={coldZones} />
      </>
    )
  }

  if (hotZones.length > 0) {
    return <HotMessage zones={hotZones} />
  }

  return <ColdMessage zones={coldZones} />
}

const ColdMessage = props => (
  <StatusMessage
    icon={faThermometerQuarter}
    message={`Der er koldt i ${getZoneNames(props.zones)}`}
  />
)
const HotMessage = props => (
  <StatusMessage
    icon={faThermometerFull}
    message={`Der er varmt i ${getZoneNames(props.zones)}`}
  />
)

const HumidityStatus = props => {
  const { zones } = props
  const humidZones = zones.filter(zone => zone.humidity.percentage > 60)
  if (humidZones.length > 0) {
    if (humidZones.length === 1) {
      return (
        <StatusMessage
          icon={faTint}
          message={`${humidZones[0].name} er fugtigt. Luft ud!`}
        />
      )
    }
    return (
      <StatusMessage
        icon={faTint}
        message={`${getZoneNames(humidZones)} er fugtige... Luft ud!`}
      />
    )
  }
  return (
    <StatusMessage icon={faTintSlash} message='God luftfugtighed i alle rum' />
  )
}

function getZoneNames(zones) {
  const lastZone = zones.pop()
  return `${zones.map(z => z.name).join(', ')} og ${lastZone.name}`
}
