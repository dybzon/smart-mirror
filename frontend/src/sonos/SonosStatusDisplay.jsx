import React from 'react'
import { faMusic } from '@fortawesome/free-solid-svg-icons'
import { StatusMessage } from '../utils'

const sonosStates = {
  playing: 'PLAYING',
  stopped: 'STOPPED',
  paused: 'PAUSED',
}

const includedTrackTypes = ['track', 'radio'] // tv streaming is type 'line_in'.

export const SonosStatusDisplay = ({ zones, error, isLoaded }) => {
  if (!isLoaded) {
    return <div>Sonos is loading</div>
  }

  if (error) {
    return <div>{error.message}</div>
  }

  if (!zones) {
    return null
  }

  // We'll only display speakers that are playing types "radio" or "track". We don't want to display that the TV is streaming etc.
  const relevantZones = zones.filter(
    z =>
      z.coordinator.state.playbackState === sonosStates.playing &&
      includedTrackTypes.includes(z.coordinator.state.currentTrack.type)
  )

  if (relevantZones.length === 0) {
    return null
  }

  return (
    <div>
      {relevantZones.map(z => {
        const key = z.coordinator.uuid
        return <SonosZoneStatus zone={z} key={key} />
      })}
    </div>
  )
}

const SonosZoneStatus = ({ zone }) => {
  const currentTrack = zone.coordinator.state.currentTrack
  return (
    <StatusMessage
      icon={faMusic}
      message={
        <>
          {currentTrack.title} ({currentTrack.artist})
          {/* 
          Skip album art for now - doesn't look good.
          <img
            src={zone.coordinator.state.currentTrack.absoluteAlbumArtUri}
            alt=''
            height='31px'
            width='31px'
          /> */}
        </>
      }
    />
  )
}
