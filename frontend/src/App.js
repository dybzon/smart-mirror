import './App.css'
import { HueFetcher } from './hue/HueFetcher'
import { HueLightStatus } from './hue/HueLightStatus'
import { TadoStatus } from './tado'
import { Clock } from './clock'
import { Calendar } from './calendar'
import { SonosStatus } from './sonos'
import { WeatherStatus, PollenStatus } from './weather'
import { DateLabel } from './clock/Date'

function App() {
  return (
    <>
      <div className='container'>
        <div className='top-row'>
          <div className='row-container row-divider'>
            <DateLabel />
          </div>
          <div className='row-container row-divider'>
            <WeatherStatus />
          </div>
          <div className='row-container'>
            <PollenStatus />
          </div>
        </div>
        <div className='clock-row'>
          <Clock />
        </div>
        <div className='house-row'>
          <div className='house-left'>
            <HueFetcher>
              <HueLightStatus />
            </HueFetcher>
            <TadoStatus />
          </div>
          <div className='house-right'>
            <SonosStatus />
          </div>
        </div>
        <div className='bottom-row'>
          <Calendar />
        </div>
      </div>
    </>
  )
}

export default App
