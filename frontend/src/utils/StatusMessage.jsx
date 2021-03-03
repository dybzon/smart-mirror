import React from 'react'
import './message.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

export const StatusMessage = props => {
  const { icon, message, hideBorder } = props

  const iconContainerClass = hideBorder
    ? 'icon-container'
    : 'icon-container icon-container-border'
  return (
    <div className='message-container'>
      <div className={iconContainerClass}>
        <div className='icon-vertical-center'>
          <FontAwesomeIcon icon={icon} color='white' />
        </div>
      </div>
      <div>{message}</div>
    </div>
  )
}
