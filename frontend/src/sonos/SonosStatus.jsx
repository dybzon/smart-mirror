import React, { useEffect } from 'react'
import { useRepeatingFetch } from '../utils/useFetch'
import { SonosStatusDisplay } from './SonosStatusDisplay'

const sonosZonesPath = 'http://localhost:5005/zones'
const sonosStateUpdatesPath = 'http://localhost:5000/api/sonosstate/subscribe'

export const SonosStatus = () => {
  const {
    result: zones,
    error,
    isLoaded,
    setResult,
    setError,
  } = useRepeatingFetch(sonosZonesPath, 1000 * 60 * 5)

  useEffect(() => {
    const source = new EventSource(sonosStateUpdatesPath)

    source.onmessage = function(event) {
      const patch = JSON.parse(event.data)
      setResult(zones => {
        if (!zones) {
          return [ConvertPatchToZone(patch)]
        }

        const zoneIndex = zones.findIndex(z => z.uuid === patch.uuid)
        const zoneToUpdate = zones[zoneIndex]
        const newResult = [...zones]

        if (!zoneToUpdate) {
          newResult.push(ConvertPatchToZone(patch))
        } else {
          const updatedZone = {
            ...zoneToUpdate,
            coordinator: {
              ...zoneToUpdate.coordinator,
              state: {
                ...zoneToUpdate.coordinator.state,
                playbackState: patch.playbackState,
                currentTrack: {
                  ...zoneToUpdate.coordinator.state.currentTrack,
                  absoluteAlbumArtUri: patch.absoluteAlbumArtUri,
                  album: patch.album,
                  artist: patch.artist,
                  title: patch.title,
                  type: patch.type,
                },
              },
            },
          }

          newResult[zoneIndex] = updatedZone
        }

        return newResult
      })

      setError(null)
    }

    return () => source.close()
  }, [setResult, setError])

  return <SonosStatusDisplay zones={zones} error={error} isLoaded={isLoaded} />
}

function ConvertPatchToZone(patch) {
  return {
    uuid: patch.uuid,
    coordinator: {
      uuid: patch.uuid,
      state: {
        playbackState: patch.playbackState,
        currentTrack: {
          absoluteAlbumArtUri: patch.absoluteAlbumArtUri,
          album: patch.album,
          artist: patch.artist,
          title: patch.title,
          type: patch.type,
        },
      },
    },
  }
}
