# Setting up the Hex TCG API

The primary method for setting up the API is via an `api.ini` file.  This file will contain lines indicating where the data should be sent and how much information should be sent there.  Here's the example format:

```
location|eventType|eventType|eventType...
```

You can send the information to different locations.  You need one line per location with the messages to be sent to that location.  The messageTypes can be different for each location (to allow you to tune which information you share with which application).  Here are some example lines from an `api.ini` file:

```
http://localhost:7777|All
http://127.0.0.1:1234|Login|SaveDeck|Collection
file://C:/temp/api.log|SaveDeck|Logout
```

With this example `api.ini` file, the Hex client will send:
* all events to http://localhost:777/
* Login, SaveDec and Collection events to http://127.0.0.1:1234/
* SaveDeck and Logout events to the file C:/temp/api.log

The possible Collection events are detailed in the [Example API Messages](http://github.com/dylannorthrup/hexapidocs/blob/MASTER/02_Example_Messages.md) file, but here's a quick list of the possible events:

* All - Not an event per se, but an indication that all events should be forwarded to the specified Location
* CardUpdated - Triggered when a card is modified by the game state
* Collection - Triggered to show cards in a player's collection
* DraftCardPicked - Triggered when a card is picked in a draft game
* DraftPack - Triggered when a pack is shown to the player in a draft game
* GameStarted - Triggered when a game begins
* Login - Triggered when the player logs in
* Logout - Triggered when the player logs out
* PlayerUpdated - Triggered when player information (such as Thresholds or Life totals) change

## Windows Instructions

Create the `api.ini` file in the main Hex directory and start Hex.  

If you are using the standalone Hex client (downloaded from http://hextcg.com/), the typical location of this will be "C:\Program Files (x86)\HEX"

If you are using the Steam installed version, it'll be under your Steam installation folder.  If you've chosen the default installation location for Steam, then the default location for your Hex installation (and where you should put your 'api.ini' file) should be "C:\Program Files (x86)\Steam\steamapps\common\HEX SHARDS OF FATE\"

## OS X Instructions
### Standalone install
For the standalone installation of Hex, the `api.ini` file needs to be located in the application installation directory.  If you copied the client into your `/Applications` directory, the correct location is `/Applications/Hex.app/Contents/HexEnv.app/Contents/Resources`.  The following is a script that you can use to copy an `api.ini` file from your `~/temp` directory into the correct location (and, if necessary, remove the old one).

```
#!/bin/bash
#
# A thing to make sure I can run Hex with api.ini settings

CFG_DIR="/Applications/Hex.app/Contents/HexEnv.app/Contents/Resources"

# Make sure api.ini is in there
if [ ! -f "${CFG_DIR}/api.ini" ]; then
  # Copy the file over
  cp ~/temp/api.ini ${CFG_DIR}/api.ini
  # Set the immutable flag so the Hex client doesn't _helpfully_
  # remove the api.ini when it starts up
  chflags uchg ${CFG_DIR}/api.ini
else
  # Unset the immutable flag, so we can copy over a new version
  chflags nouchg ${CFG_DIR}/api.ini
  cp ~/temp/api.ini ${CFG_DIR}/api.ini
  chflags uchg ${CFG_DIR}/api.ini
fi

```

Once this is done, start up the Hex client as normal and your API messages should be sent out as configured.

### Steam Install
**NOTE: THESE INSTRUCTION HAVE NOT BEEN VERIFIED AND LIKELY DO NOT WORK**

You should likely use the same method as above (after finding the location where Steam installed the client), but I have not used the Steam install on a Mac and cannot verify this.

~~For the version of Hex installed by Steam, the location is "~/Library/Application Support/Steam/steamapps/common/HEX SHARDS OF FATE".  This shell script should copy ~~

```
#!/bin/bash
#
# A thing to make sure I can run Hex with api.ini settings

CFG_DIR="~/Library/Application Support/Steam/SteamApps/common/HEX SHARDS OF FATE"

# Make sure api.ini is in there
if [ ! -f "${CFG_DIR}/api.ini" ]; then
  cp ~/temp/api.ini "${CFG_DIR}/api.ini"
fi
```
