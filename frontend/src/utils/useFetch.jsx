import { useState, useEffect, useCallback } from 'react'

export const useFetch = (url, options) => {
  const [isLoaded, setIsLoaded] = useState(false)
  const [error, setError] = useState(null)
  const [result, setResult] = useState(null)

  const onFetch = useCallback(
    () =>
      fetch(url, options)
        .then(response => response.json())
        .then(
          result => {
            setResult(result)
            setIsLoaded(true)
            setError(null)
          },
          error => {
            setError(error)
            setIsLoaded(true)
          }
        ),
    [url, options]
  )

  useEffect(() => onFetch(), [onFetch])

  return { isLoaded, setIsLoaded, error, setError, result, setResult, onFetch }
}

export const useRepeatingFetch = (url, fetchInterval, options) => {
  const {
    isLoaded,
    setIsLoaded,
    error,
    setError,
    result,
    setResult,
    onFetch,
  } = useFetch(url, options)

  useEffect(() => {
    onFetch()
    const interval = setInterval(onFetch, fetchInterval)
    return () => clearInterval(interval)
  }, [onFetch, fetchInterval])

  return { isLoaded, setIsLoaded, error, setError, result, setResult, onFetch }
}
