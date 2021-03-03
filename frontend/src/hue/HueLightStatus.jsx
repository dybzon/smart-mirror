import React from 'react'
import { faLightbulb } from '@fortawesome/free-solid-svg-icons'
import { StatusMessage } from '../utils'

const roomMessageMap = {
  Soveværelse: 'i soveværelset',
  Køkken: 'over køkkenøen',
  Stue: 'i stuen',
  Spisebord: 'over spisebordet',
  Trappeopgang: 'i trappeopgangen',
  Hoveddør: 'ved hoveddøren',
  'Kælder - stort rum': 'i fitnessrummet',
  Legehus: 'i legehuset',
}

export const HueLightStatus = ({ lights, groups }) => (
  <div>
    <StatusMessage icon={faLightbulb} message={getHueMessage(lights, groups)} />
  </div>
)

function getHueMessage(lights, groups) {
  const groupList = []
  for (const groupId in groups) {
    groupList.push(groups[groupId])
  }
  const activeGroups = groupList.filter(g => g.state.any_on)

  const lightList = []
  for (const lightId in lights) {
    lightList.push(lights[lightId])
  }

  if (activeGroups.length > 2) {
    return `Lys i ${activeGroups.length} rum (${
      lightList.filter(l => l.state.on).length
    } pærer)`
  }

  if (activeGroups.length > 0) {
    const groupMessages = activeGroups.map(
      g => roomMessageMap[g.name] ?? g.name
    )

    if (activeGroups.length === 1) {
      return `Lys ${groupMessages[0]} (${
        lightList.filter(l => l.state.on).length
      } pærer)`
    }

    const lastMessage = groupMessages.pop()
    return `Lys ${groupMessages.join(', ')} og ${lastMessage} (${
      lightList.filter(l => l.state.on).length
    } pærer)`
  }

  return 'Alt lys slukket'
}
