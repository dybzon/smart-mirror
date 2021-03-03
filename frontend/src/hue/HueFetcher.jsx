import React from 'react'
import { useRepeatingFetch } from '../utils'
import settings from '../settings.json'

const hueBaseApiAddress = `${settings.hue.apiBaseAddress}/${settings.hue.localUser}`

export const HueFetcher = ({ children }) => {
  const {
    isLoaded: lightsLoaded,
    error: lightsError,
    result: lights,
  } = useRepeatingFetch(`${hueBaseApiAddress}/lights`, 30 * 1000)
  const {
    isLoaded: groupsLoaded,
    error: groupsError,
    result: groups,
  } = useRepeatingFetch(`${hueBaseApiAddress}/groups`, 30 * 1000)

  if (lightsError || groupsError) {
    return (
      <>
        Error: {lightsError?.message} {groupsError?.message}
      </>
    )
  } else if (!lightsLoaded || !groupsLoaded) {
    return <>Loading hue lights...</>
  } else {
    return React.Children.map(children, child => {
      if (React.isValidElement(child)) {
        return React.cloneElement(child, { lights, groups })
      }

      return child
    })
  }
}
