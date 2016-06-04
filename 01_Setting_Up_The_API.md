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
For the standalone installation of Hex, the `api.ini` file needs to be located in a player-specific directory.  For running Hex, this is a shell script that can be run in the Terminal to copy an `api.ini` file from ~/temp/ into the most recent patch directory

```
#!/bin/bash
#
# A thing to make sure I can run Hex with api.ini settings

CFG_DIR=$(\ls -drt ~/Library/Caches/unity.Cryptozoic.HexPatch/h_dl_hex_gameforge_com__live*)

# Make sure api.ini is in there
if [ ! -f "${CFG_DIR}/api.ini" ]; then
  cp ~/temp/api.ini ${CFG_DIR}/api.ini
fi

```

Once in that directory, the `api.ini` file should be ignored by the Patch Updater (correct as of 24 July 2015) and you should be able to run Hex as normal from the Finder, the Dock or wherever you'd prefer to launch it from.

### Steam Install
For the version of Hex installed by Steam, the location is "~/Library/Application Support/Steam/steamapps/common/HEX SHARDS OF FATE".  This shell script should copy 

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
