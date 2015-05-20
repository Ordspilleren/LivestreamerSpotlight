# LivestreamerSpotlight

LivestreamerSpotlight is a way to quickly open your favorite streams in media players like VLC and MPC-HC.

## Installation

Simply grab the latest release from the Releases section here on GitHub (or compile it yourself), and put in a folder somewhere safe.

LivestreamerSpotlight depends on Livestreamer, so make sure you have that installed too. You can grab it from [here](http://docs.livestreamer.io/install.html#windows-binaries).

## Usage

First off, you should edit the settings.cfg file to match your preferences. The most important setting here is "player". Set this to whichever media player you would like to use. This can be done either via the full path to it, or by simply writing the name of the player as per the default setting.

Next up, simply open the application. When you are ready to watch a stream, press WIN + Shift + S and enter the channelname of the streamer you would like to watch.

## Issues

Probably a lot at this point. Here are some of the major ones:

* Chat only works if Google Chrome is installed.
* Keyboard shortcut is locked to WIN + SHIFT + S.
* Not a lot of error handling, so if it crashes, too bad.
