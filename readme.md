The smart mirror solution consists of

- A frontend built in reactjs.
  - Runs per default on http://localhost:3000 in dev mode
  - Usually hosted on http://localhost (port 80) on a Raspberry pi (this is the default port)
- A .net core web api responsible for all communication with services that require authentication, or which are not accessible due to strict CORS policies.
  - Runs per default on http://localhost:5000.
  - Tado (inside humidity and temperature)
  - Google (calendar and birthdays)
  - Sonos (audio state updates received via web hooks)
- A third party project called node-sonos-http-api. This is a node based web server that communicates locally with the Sonos UPNP api and exposes endpoints to communicate with Sonos over http instead. This service also relays state updates from Sonos via webhooks.
  - Runs per default on http://localhost:5005

# Raspberry pi setup

The Raspberry pi should be set up to automatically do the following things on startup

- Start a browser in kiosk mode on http://localhost/
- Start the .net core web api
- Start the node-sonos-http-api

The following dependencies should also be installed on the Pi:

- dotnet core
- node

## Starting a browser in kiosk mode

To start a browser in kiosk mode on http://localhost/ on startup, add the following lines to `sudo nano /home/pi/.config/lxsession/LXDE-pi/autostart`

```
@xscreensaver -no-splash  # Disables screensaver
@xset s off
@xset -dpms
@xset s noblank
@chromium-browser --incognito --kiosk http://localhost/  # Load chromium after boot and point to the localhost webserver in kiosk mode
```

## Starting the .net core web api

Add a service for the smart-mirror .net api. This can be done through these steps

- Create a service file (see `smart-mirror.service` for more information)
- Move that service file to the `/etc/systemd/system` using this command: `sudo cp smart-mirror.service /etc/systemd/system/`
- Ensure that the service file permissions are set correctly: `sudo chmod u+rw /etc/systemd/system/smart-mirror.service`
- Enable the service: `sudo systemctl enable smart-mirror`

The service should now start on reboot, but should be tested first through this command: `sudo systemctl start smart-mirror`

The service can be stopped using `sudo systemctl stop smart-mirror` if necessary.

Note that this service requires .net core to be installed on the Raspberry pi. It also requires the service to be able to find the dotnet runtime, which does not happen automatically even though dotnet is installed globally.
It seems that the `DOTNET_ROOT` environment variable must be set.
To set this variable, run `export DOTNET_ROOT=...`. Try setting this to `/home/pi/dotnet/shared/Microsoft.NETCore.App`. See dotnet --info ... // didn't work

To view all environment variables, run `printenv`
To view log output from the executed services, go to `/var/log` and find the `syslog` file

Alternatively a self-contained deployment (including framework) can be done.

Run `dotnet --info` to view information about installed versions of dotnet.

## Starting the node-sonos-http-api

Add a service for the sonos node http api, similarly to how the smart-mirror service above was added.

This service requires Node to be installed on the Raspberry pi.

Run `node -v` to get the currently installed version of Node.

# Build and deployment

- Build the .net core web api to target runtime linux-arm
- Build the react frontend using `yarn build`
- Transfer all build output to the Pi. Use WinSCP or a similar tool for this.
- (Remote to the Pi using PuTTY or a similar tool)
- Move frontend files to `/var/www/html` on the Pi. We want all content of the build directory (but not the build directory itself)
  - sudo cp -R build/\* /var/www/html // Assuming we're in the directory that contains the build directory, which contains build output
- Move the web api to `/var/www`, or whereever the `smart-mirror.service` is configured to look for this.
  - sudo cp -R linux-arm /var/www // Assuming we're in the directory that contains the linux-arm directory
