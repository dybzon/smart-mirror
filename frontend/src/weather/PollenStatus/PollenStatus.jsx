import React from 'react'
import { useRepeatingFetch } from '../../utils'
import { faBoxTissue } from '@fortawesome/free-solid-svg-icons'
import { StatusMessage } from '../../utils'
import './pollen.css'

export const PollenStatus = () => {
  const { result, error, isLoaded } = useRepeatingFetch(
    `http://localhost:5000/api/pollen`,
    1000 * 60 * 60 * 2
  )

  if (!isLoaded) {
    return <div>Loading pollen...</div>
  } else if (error) {
    return null
  }

  return <PollenStatusDisplay {...result} />
}

const PollenStatusDisplay = ({ birch, grass }) => {
  if (!birch && !grass) {
    return null
  }

  const birchMessage = birch ? `Birk: ${birch ? birch : 0}` : ''
  const grassMessage = grass ? `Græs: ${grass ? grass : 0}` : ''
  const delimiter = birch && grass ? ' - ' : ''

  return (
    <div className='pollen-container'>
      <StatusMessage
        icon={faBoxTissue}
        message={`${birchMessage}${delimiter}${grassMessage}`}
        hideBorder
      />
    </div>
  )
}
