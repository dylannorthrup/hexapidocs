# Example Messages

## Message Types
The following are actual messages that were sent out by the API.  You can use these to test your application code.  

More documentation to be done later... Just getting JSON stuff here for the moment.

### CardUpdated
`{"Cost":3,"Attack":7,"Defense":7,"Controller":12334,"Name":"Dreamsmoke Mystic","Gems":0,"BaseTemplate":{"m_Guid":"ac050a11-3651-4b8b-9e67-6085e716fc00"},"State":16516,"Shards":48,"Abilities":["You can see the top card of your deck.","[(2)] [ARROWR] Put the top card of your deck on the bottom of your deck.","<b>Prophecy</b> - When this enters play, the next troop in your deck gets  2[ATK]/ 2[DEF] and <b>Flight</b>. If it is a Coyotle, it gets this power."],"Attributes":2,"Collection":8,"User":"","Message":"CardUpdated"}`

The `Collection` attribute can have several different values. These appear to correspond to the zone in which the card is located.

* 0: Your Champion Zone (this is where your champion is located)
* 1: The Deck Zone (seems to include your deck and your opponent's deck)
* 2: The Hand Zone (again, seems to include your hand and your opponent's hand)
* 4: Opposing Champion Zone (this is where your opponent's champion is located)
* 8: In Play Zone/Battle Zone (where all the action happens)
* 16: Discard Pile (may include your opponent's discard pile as well)
* 32: Void Zone (*SPECULATIVE. PLEASE UPDATE THIS WHEN THIS IS VERIFIED*)
* 64: Shard Zone (where shards go when they're played)
* 128: The Chain Zone (where cards are when they're played but not yet resolved)

The `State` attribute seems to indicate whether a rard is ready or exhausted.  This is solely based on some preliminary guesses:
* 8192 - Ready
* 16384 - Ready on opponent's turn?
* 16517 - Exhausted
* 16513 - Exhausted on opponent's turn?


### Collection

A collection Update (eg. from a Draft)

`{"Action":"Update","CardsAdded":[{"Name":"Ashwood Blademaster","Flags":"","Guid":{"m_Guid":"b22a516f-104f-483f-ac89-f6ee51be608f"},"Gems":[]}],"CardsRemoved":[],"User":"InGameName","Message":"Collection"}`

A collection Overwrite (when you first log in)

`{"Action":"Overwrite","CardsAdded":[{"Name":"Abominate","Flags":"","Guid":{"m_Guid":"8eebaeb5-5ff9-48c2-ae2f-2edf2f7cad59"},"Gems":[]},{"Name":"Adamanthian Scrivener","Flags":"","Guid":{"m_Guid":"2ce8233b-c5bd-4be0-86c6-a17021b071ee"},"Gems":[]},{"Name":"Adamanthian Scrivener","Flags":"","Guid":{"m_Guid":"2ce8233b-c5bd-4be0-86c6-a17021b071ee"},"Gems":[]},{"Name":"Adamanthian Scrivener","Flags":"","Guid":{"m_Guid":"2ce8233b-c5bd-4be0-86c6-a17021b071ee"},"Gems":[]},{"Name":"Adamanthian Scrivener","Flags":"","Guid":{"m_Guid":"2ce8233b-c5bd-4be0-86c6-a17021b071ee"},"Gems":[]},{"Name":"Adamanthian Scrivener","Flags":"","Guid":{"m_Guid":"2ce8233b-c5bd-4be0-86c6-a17021b071ee"},"Gems":[]},{"Name":"Adamanthian Scrivener","Flags":"","Guid":{"m_Guid":"2ce8233b-c5bd-4be0-86c6-a17021b071ee"},"Gems":[]},{"Name":"Zombie Vulture","Flags":"","Guid":{"m_Guid":"82739458-39ca-4e80-816a-25bf1eb5343f"},"Gems":[]},{"Name":"Zombie Vulture","Flags":"","Guid":{"m_Guid":"82739458-39ca-4e80-816a-25bf1eb5343f"},"Gems":[]},{"Name":"Zombie Vulture","Flags":"","Guid":{"m_Guid":"82739458-39ca-4e80-816a-25bf1eb5343f"},"Gems":[]},{"Name":"Zombie Vulture","Flags":"","Guid":{"m_Guid":"82739458-39ca-4e80-816a-25bf1eb5343f"},"Gems":[]},{"Name":"Zombie Vulture","Flags":"","Guid":{"m_Guid":"82739458-39ca-4e80-816a-25bf1eb5343f"},"Gems":[]},{"Name":"Zombie Vulture","Flags":"","Guid":{"m_Guid":"82739458-39ca-4e80-816a-25bf1eb5343f"},"Gems":[]},{"Name":"Zombie Vulture","Flags":"","Guid":{"m_Guid":"82739458-39ca-4e80-816a-25bf1eb5343f"},"Gems":[]},{"Name":"Zombie Vulture","Flags":"","Guid":{"m_Guid":"82739458-39ca-4e80-816a-25bf1eb5343f"},"Gems":[]},{"Name":"Zombie Vulture","Flags":"","Guid":{"m_Guid":"82739458-39ca-4e80-816a-25bf1eb5343f"},"Gems":[]},{"Name":"Zombie Vulture","Flags":"","Guid":{"m_Guid":"82739458-39ca-4e80-816a-25bf1eb5343f"},"Gems":[]}],"CardsRemoved":[],"User":"InGameName","Message":"Collection"}`


Different m_Guid numbers for the same card indicate one is an AA and the other a normal art card (as showin for the Adamanthian Scriveners)

### DraftCardPicked

`{"Card":{"Name":"Angel of Judgement","Flags":"","Guid":{"m_Guid":"60bc34db-11d4-41f8-97fa-4c7be958e52d"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}``

### DraftPack
A first pick draft pack:
`{"Cards":[{"Name":"Lunacy","Flags":"","Guid":{"m_Guid":"4dc30220-0e92-4c58-9b4f-1fba1e35b773"},"Gems":[]},{"Name":"Mettle","Flags":"","Guid":{"m_Guid":"bbcd7407-d82a-47ab-8f78-494dcc3041f1"},"Gems":[]},{"Name":"Giant Spiderspawn","Flags":"","Guid":{"m_Guid":"5d49bc7d-9bfe-4789-b32a-f40ae44c6935"},"Gems":[]},{"Name":"Granite Giant","Flags":"","Guid":{"m_Guid":"52a8f460-82e7-4376-a94e-4251d21cd653"},"Gems":[]},{"Name":"Pride's Fall","Flags":"","Guid":{"m_Guid":"6e13ec0a-4b77-465b-b205-7d6b687b103f"},"Gems":[]},{"Name":"Entangling Webs","Flags":"","Guid":{"m_Guid":"273d11a7-992d-4423-abd2-138a62eabaee"},"Gems":[]},{"Name":"Ethereal Caller","Flags":"","Guid":{"m_Guid":"27982190-836b-40e4-9dec-dee7ed6e88d8"},"Gems":[]},{"Name":"Shadowblade Assassin","Flags":"","Guid":{"m_Guid":"ac08a668-208d-40a0-9447-776746f6ce12"},"Gems":[]},{"Name":"Spirit Eagle","Flags":"","Guid":{"m_Guid":"a5ca6295-6760-4961-b7a3-0e03c9ddd978"},"Gems":[]},{"Name":"Ashwood Soloist","Flags":"","Guid":{"m_Guid":"df8445b8-7c51-470a-b6aa-cc53e0d3360d"},"Gems":[]},{"Name":"Sylvan Duet","Flags":"","Guid":{"m_Guid":"86eca898-00b7-4642-a7c3-0e75a47e03aa"},"Gems":[]},{"Name":"Lullaby","Flags":"","Guid":{"m_Guid":"aea0bbb5-3a29-4d9c-82da-66291c9ad46d"},"Gems":[]},{"Name":"Inflict Doubt","Flags":"","Guid":{"m_Guid":"ab77f906-4ac7-42e0-be1e-939ede7d5e11"},"Gems":[]},{"Name":"Bombwright","Flags":"","Guid":{"m_Guid":"de31cf44-324f-4a50-acf9-6d0b5f2b236c"},"Gems":[]},{"Name":"Angel of Judgement","Flags":"","Guid":{"m_Guid":"60bc34db-11d4-41f8-97fa-4c7be958e52d"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}`

Last pick of the pack:
```
{"Cards":[{"Name":"Touch of Xentoth","Flags":"","Guid":{"m_Guid":"69744127-4a29-4845-9889-3ce9565154e2"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Touch of Xentoth","Flags":"","Guid":{"m_Guid":"69744127-4a29-4845-9889-3ce9565154e2"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
```

### GameEnded
`{"Winners":["Warmaster Fuzzuko"],"Losers":["Uzume, Grand Concubunny"],"User":"InGameName","Message":"GameEnded"}`

### GameStarted
`{"Players":[],"User":"InGameName","Message":"GameStarted"}`

### Login
`{"User":"","Message":"Login"}`

or

`{"User":"InGameName","Message":"Login"}`


### Logout
`{"User":"InGameName","Message":"Logout"}`

### PlayerUpdated
`{"Resources":0,"Id":12334,"Thresholds":{"Blood":0,"Sapphire":0,"Wild":0,"Diamond":0,"Ruby":0},"User":"","Message":"PlayerUpdated"}`

## Testing Message Sending
If you've got curl installed on your system, you can use it to simulate an API call to your application.  As an example, the following will allow you to send a Login event to the application running on localhost port 5000:

```
curl -H "Content-Type: application/json" -X POST -d '"User":"","Message":"Login"}' http://localhost:5000/
```

## Example Draft Pack
```
{"Cards":[{"Name":"Lunacy","Flags":"","Guid":{"m_Guid":"4dc30220-0e92-4c58-9b4f-1fba1e35b773"},"Gems":[]},{"Name":"Mettle","Flags":"","Guid":{"m_Guid":"bbcd7407-d82a-47ab-8f78-494dcc3041f1"},"Gems":[]},{"Name":"Giant Spiderspawn","Flags":"","Guid":{"m_Guid":"5d49bc7d-9bfe-4789-b32a-f40ae44c6935"},"Gems":[]},{"Name":"Granite Giant","Flags":"","Guid":{"m_Guid":"52a8f460-82e7-4376-a94e-4251d21cd653"},"Gems":[]},{"Name":"Pride's Fall","Flags":"","Guid":{"m_Guid":"6e13ec0a-4b77-465b-b205-7d6b687b103f"},"Gems":[]},{"Name":"Entangling Webs","Flags":"","Guid":{"m_Guid":"273d11a7-992d-4423-abd2-138a62eabaee"},"Gems":[]},{"Name":"Ethereal Caller","Flags":"","Guid":{"m_Guid":"27982190-836b-40e4-9dec-dee7ed6e88d8"},"Gems":[]},{"Name":"Shadowblade Assassin","Flags":"","Guid":{"m_Guid":"ac08a668-208d-40a0-9447-776746f6ce12"},"Gems":[]},{"Name":"Spirit Eagle","Flags":"","Guid":{"m_Guid":"a5ca6295-6760-4961-b7a3-0e03c9ddd978"},"Gems":[]},{"Name":"Ashwood Soloist","Flags":"","Guid":{"m_Guid":"df8445b8-7c51-470a-b6aa-cc53e0d3360d"},"Gems":[]},{"Name":"Sylvan Duet","Flags":"","Guid":{"m_Guid":"86eca898-00b7-4642-a7c3-0e75a47e03aa"},"Gems":[]},{"Name":"Lullaby","Flags":"","Guid":{"m_Guid":"aea0bbb5-3a29-4d9c-82da-66291c9ad46d"},"Gems":[]},{"Name":"Inflict Doubt","Flags":"","Guid":{"m_Guid":"ab77f906-4ac7-42e0-be1e-939ede7d5e11"},"Gems":[]},{"Name":"Bombwright","Flags":"","Guid":{"m_Guid":"de31cf44-324f-4a50-acf9-6d0b5f2b236c"},"Gems":[]},{"Name":"Angel of Judgement","Flags":"","Guid":{"m_Guid":"60bc34db-11d4-41f8-97fa-4c7be958e52d"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Angel of Judgement","Flags":"","Guid":{"m_Guid":"60bc34db-11d4-41f8-97fa-4c7be958e52d"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Return To The Soil","Flags":"","Guid":{"m_Guid":"a2eb5f02-f8cd-4baf-894e-80d5c6538aa4"},"Gems":[]},{"Name":"Scraptooth Cackler","Flags":"","Guid":{"m_Guid":"38a3fb57-20e7-4e61-9732-d96f01105d4c"},"Gems":[]},{"Name":"Incubate","Flags":"","Guid":{"m_Guid":"ae6ffe36-c358-4ea1-94cc-d4294c1d9b1c"},"Gems":[]},{"Name":"Gossamer Tears","Flags":"","Guid":{"m_Guid":"74634a66-eb3b-4f7a-bf5f-71bf8845d93b"},"Gems":[]},{"Name":"Runeweb Infiltrator","Flags":"","Guid":{"m_Guid":"e50468fe-6e6f-4319-80e9-c138748e18b4"},"Gems":[]},{"Name":"Wrathwood Larch","Flags":"","Guid":{"m_Guid":"f172254b-5bc5-4bcd-9250-8d7edfe518a0"},"Gems":[]},{"Name":"Sacred Seekers","Flags":"","Guid":{"m_Guid":"522ace85-0811-4df8-b979-defdbf913bf3"},"Gems":[]},{"Name":"Vilefang Eremite","Flags":"","Guid":{"m_Guid":"95254101-e69a-48af-a46f-d9b3aa7efd8e"},"Gems":[]},{"Name":"Pride's Fall","Flags":"","Guid":{"m_Guid":"6e13ec0a-4b77-465b-b205-7d6b687b103f"},"Gems":[]},{"Name":"Cry of Adamanth","Flags":"","Guid":{"m_Guid":"488d5c88-871a-4ea5-8c2f-364c758a0337"},"Gems":[]},{"Name":"Frigid Buffalo","Flags":"","Guid":{"m_Guid":"4310820a-bbc5-430d-a34f-7783bb26032b"},"Gems":[]},{"Name":"Rune Ear Elite","Flags":"","Guid":{"m_Guid":"0bb7add7-f4ee-456b-a0c1-e1468d0f3605"},"Gems":[]},{"Name":"Eye of Lixil","Flags":"","Guid":{"m_Guid":"8ef8747b-9d54-4423-98bb-f4d3bdc58665"},"Gems":[]},{"Name":"Emperor's Lackey","Flags":"","Guid":{"m_Guid":"9340708d-1b10-4c53-b12c-20fb140ee3ad"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Eye of Lixil","Flags":"","Guid":{"m_Guid":"8ef8747b-9d54-4423-98bb-f4d3bdc58665"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Touch of Xentoth","Flags":"","Guid":{"m_Guid":"69744127-4a29-4845-9889-3ce9565154e2"},"Gems":[]},{"Name":"Startouched Brave","Flags":"","Guid":{"m_Guid":"2d5154be-a224-4c69-bb66-888e358baf56"},"Gems":[]},{"Name":"Stinkhorn Soup","Flags":"","Guid":{"m_Guid":"0d194a64-721d-43ee-b90f-8a82af730c82"},"Gems":[]},{"Name":"Scraptooth Cackler","Flags":"","Guid":{"m_Guid":"38a3fb57-20e7-4e61-9732-d96f01105d4c"},"Gems":[]},{"Name":"Return To The Soil","Flags":"","Guid":{"m_Guid":"a2eb5f02-f8cd-4baf-894e-80d5c6538aa4"},"Gems":[]},{"Name":"Snarling Ambusher","Flags":"","Guid":{"m_Guid":"e33bb5b7-1b69-481e-99cc-b5fcdc352cf6"},"Gems":[]},{"Name":"Sun Seer","Flags":"","Guid":{"m_Guid":"e2e91539-94fd-4730-8968-73e2943b2fb6"},"Gems":[]},{"Name":"Hatchery Broodguard","Flags":"","Guid":{"m_Guid":"a4ef5dd1-4aac-4922-82c7-dc234d413609"},"Gems":[]},{"Name":"Pyroknight","Flags":"","Guid":{"m_Guid":"123fa2ad-5368-4741-a7c1-9ccecdacad0f"},"Gems":[]},{"Name":"Thunderfield Seer","Flags":"","Guid":{"m_Guid":"a9224795-471a-48b7-9c79-80ca547a920b"},"Gems":[]},{"Name":"Granite Giant","Flags":"","Guid":{"m_Guid":"52a8f460-82e7-4376-a94e-4251d21cd653"},"Gems":[]},{"Name":"Rotting Knight","Flags":"","Guid":{"m_Guid":"3421758e-cc56-4ec5-88cf-55c6b19566a4"},"Gems":[]},{"Name":"Relic of Nulzann","Flags":"","Guid":{"m_Guid":"9c18fd16-86b6-4f6a-9393-3b9da08ef2c3"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Relic of Nulzann","Flags":"","Guid":{"m_Guid":"9c18fd16-86b6-4f6a-9393-3b9da08ef2c3"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Lithe Lyricist","Flags":"","Guid":{"m_Guid":"182ae5d6-d9ad-4710-914d-879a9c5fd651"},"Gems":[]},{"Name":"Pride's Fall","Flags":"","Guid":{"m_Guid":"6e13ec0a-4b77-465b-b205-7d6b687b103f"},"Gems":[]},{"Name":"Windbourne Disciple","Flags":"","Guid":{"m_Guid":"e7dd9fe9-c495-4933-af7e-4ad5eb432f9b"},"Gems":[]},{"Name":"Vilefang Eremite","Flags":"","Guid":{"m_Guid":"95254101-e69a-48af-a46f-d9b3aa7efd8e"},"Gems":[]},{"Name":"Merry Minstrels","Flags":"","Guid":{"m_Guid":"730ea063-830a-4433-a3e9-e62972dda465"},"Gems":[]},{"Name":"Arcane Focus","Flags":"","Guid":{"m_Guid":"8f5511ee-bd51-45fb-8348-6072e41c681b"},"Gems":[]},{"Name":"Woolvir Baa'sher","Flags":"","Guid":{"m_Guid":"81ca3f9c-53e8-4b2d-b2db-15a6e20f3620"},"Gems":[]},{"Name":"Hatchery Broodguard","Flags":"","Guid":{"m_Guid":"a4ef5dd1-4aac-4922-82c7-dc234d413609"},"Gems":[]},{"Name":"Rotroot Enchanter","Flags":"","Guid":{"m_Guid":"a5b56a7b-4f1a-499b-b7af-b67c1dcecddd"},"Gems":[]},{"Name":"Smoke Signals","Flags":"","Guid":{"m_Guid":"fe285ccb-50b8-4ad1-a1a8-96192b43734b"},"Gems":[]},{"Name":"Vampiric Kiss","Flags":"","Guid":{"m_Guid":"272a340e-6f41-4289-908b-83a294b3a178"},"Gems":[]},{"Name":"Twisted Taunter","Flags":"","Guid":{"m_Guid":"c6f70b89-a22d-4bc7-8035-05fc475c0785"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Twisted Taunter","Flags":"","Guid":{"m_Guid":"c6f70b89-a22d-4bc7-8035-05fc475c0785"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Lunacy","Flags":"","Guid":{"m_Guid":"4dc30220-0e92-4c58-9b4f-1fba1e35b773"},"Gems":[]},{"Name":"Granite Giant","Flags":"","Guid":{"m_Guid":"52a8f460-82e7-4376-a94e-4251d21cd653"},"Gems":[]},{"Name":"Giant Spiderspawn","Flags":"","Guid":{"m_Guid":"5d49bc7d-9bfe-4789-b32a-f40ae44c6935"},"Gems":[]},{"Name":"Smoke Signals","Flags":"","Guid":{"m_Guid":"fe285ccb-50b8-4ad1-a1a8-96192b43734b"},"Gems":[]},{"Name":"Arcane Zephyr","Flags":"","Guid":{"m_Guid":"5f07bb40-52d6-47e0-b392-b4f098af4a11"},"Gems":[]},{"Name":"Creepy Conspirators","Flags":"","Guid":{"m_Guid":"93f6e62e-6bd4-46f4-9a2b-adc03d80f873"},"Gems":[]},{"Name":"Tireless Researcher","Flags":"","Guid":{"m_Guid":"7ab6005d-8a8d-4b6a-a71c-1691cb15a083"},"Gems":[]},{"Name":"Pyroknight","Flags":"","Guid":{"m_Guid":"123fa2ad-5368-4741-a7c1-9ccecdacad0f"},"Gems":[]},{"Name":"Stirring Oration","Flags":"","Guid":{"m_Guid":"ef71cc23-ae11-4128-885f-a9850c071ba4"},"Gems":[]},{"Name":"Deathmask Assailant","Flags":"","Guid":{"m_Guid":"58a69739-e54e-4222-9f5d-b6cc46c44528"},"Gems":[]},{"Name":"Redfur Ranger","Flags":"","Guid":{"m_Guid":"0899f91b-215b-48dd-8aac-9c95be80e1e6"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Creepy Conspirators","Flags":"","Guid":{"m_Guid":"93f6e62e-6bd4-46f4-9a2b-adc03d80f873"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Sepulchra Bonewalker","Flags":"","Guid":{"m_Guid":"64d41ff0-fa0e-437e-b88b-48346db1aebd"},"Gems":[]},{"Name":"Predatory Prey","Flags":"","Guid":{"m_Guid":"2c4f416c-4faf-408e-a8c1-f807f9296b4d"},"Gems":[]},{"Name":"Skewer","Flags":"","Guid":{"m_Guid":"0d7abbcc-af4a-457e-b3dd-d5e5ff3b7376"},"Gems":[]},{"Name":"Hatchery Priest","Flags":"","Guid":{"m_Guid":"428b4342-937d-4241-83d1-2c54e8975fb5"},"Gems":[]},{"Name":"Snarling Ambusher","Flags":"","Guid":{"m_Guid":"e33bb5b7-1b69-481e-99cc-b5fcdc352cf6"},"Gems":[]},{"Name":"Sun Seer","Flags":"","Guid":{"m_Guid":"e2e91539-94fd-4730-8968-73e2943b2fb6"},"Gems":[]},{"Name":"Tireless Researcher","Flags":"","Guid":{"m_Guid":"7ab6005d-8a8d-4b6a-a71c-1691cb15a083"},"Gems":[]},{"Name":"Pride's Fall","Flags":"","Guid":{"m_Guid":"6e13ec0a-4b77-465b-b205-7d6b687b103f"},"Gems":[]},{"Name":"Ember Tears","Flags":"","Guid":{"m_Guid":"2d6d0f05-5735-4330-9d01-add3c8305cf6"},"Gems":[]},{"Name":"Scrap Rummager","Flags":"","Guid":{"m_Guid":"319a1b5d-4c90-46fc-8c82-c17524b6d067"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Sepulchra Bonewalker","Flags":"","Guid":{"m_Guid":"64d41ff0-fa0e-437e-b88b-48346db1aebd"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Lightning Brave","Flags":"","Guid":{"m_Guid":"f3250fcd-3d51-428c-9bd5-52732e856f3c"},"Gems":[]},{"Name":"Etherealize","Flags":"","Guid":{"m_Guid":"46eedbaf-fcfa-47f6-b4b1-001c446ff32c"},"Gems":[]},{"Name":"Nelebrin Treeguard","Flags":"","Guid":{"m_Guid":"4194b2be-b65e-452f-9f88-2b33a739ca9c"},"Gems":[]},{"Name":"Bloatcap","Flags":"","Guid":{"m_Guid":"702f45ec-c117-4fc2-9e38-1b9e66ebb8ad"},"Gems":[]},{"Name":"Gossamer Tears","Flags":"","Guid":{"m_Guid":"74634a66-eb3b-4f7a-bf5f-71bf8845d93b"},"Gems":[]},{"Name":"Touch of Xentoth","Flags":"","Guid":{"m_Guid":"69744127-4a29-4845-9889-3ce9565154e2"},"Gems":[]},{"Name":"Startouched Brave","Flags":"","Guid":{"m_Guid":"2d5154be-a224-4c69-bb66-888e358baf56"},"Gems":[]},{"Name":"Stinkhorn Soup","Flags":"","Guid":{"m_Guid":"0d194a64-721d-43ee-b90f-8a82af730c82"},"Gems":[]},{"Name":"Gemsoul Feeder","Flags":"","Guid":{"m_Guid":"7071ddd7-b81b-46b2-8682-9f391ab3f12e"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Gemsoul Feeder","Flags":"","Guid":{"m_Guid":"7071ddd7-b81b-46b2-8682-9f391ab3f12e"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Return To The Soil","Flags":"","Guid":{"m_Guid":"a2eb5f02-f8cd-4baf-894e-80d5c6538aa4"},"Gems":[]},{"Name":"Vampegasus","Flags":"","Guid":{"m_Guid":"429f49b2-cd03-4687-afb3-a1492440031d"},"Gems":[]},{"Name":"Swordplay","Flags":"","Guid":{"m_Guid":"af2345c1-7dde-44e9-9bc3-d3818679e189"},"Gems":[]},{"Name":"Scrapyard Dynamo","Flags":"","Guid":{"m_Guid":"689e69c8-395f-4282-8983-4564bece58c0"},"Gems":[]},{"Name":"Rootforged Regalia","Flags":"","Guid":{"m_Guid":"999085c0-401f-47d0-94aa-27d261b18815"},"Gems":[]},{"Name":"Lunge","Flags":"","Guid":{"m_Guid":"1f3effc1-4132-4348-8561-922561b9c767"},"Gems":[]},{"Name":"Psionic Acolyte","Flags":"","Guid":{"m_Guid":"26dc2aec-ccd1-46d3-9603-8c4a7d3a0980"},"Gems":[]},{"Name":"Oculus of Azathoth","Flags":"","Guid":{"m_Guid":"5cff8a32-0165-4413-b407-18f5d6997056"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Oculus of Azathoth","Flags":"","Guid":{"m_Guid":"5cff8a32-0165-4413-b407-18f5d6997056"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Lunacy","Flags":"","Guid":{"m_Guid":"4dc30220-0e92-4c58-9b4f-1fba1e35b773"},"Gems":[]},{"Name":"Mettle","Flags":"","Guid":{"m_Guid":"bbcd7407-d82a-47ab-8f78-494dcc3041f1"},"Gems":[]},{"Name":"Giant Spiderspawn","Flags":"","Guid":{"m_Guid":"5d49bc7d-9bfe-4789-b32a-f40ae44c6935"},"Gems":[]},{"Name":"Granite Giant","Flags":"","Guid":{"m_Guid":"52a8f460-82e7-4376-a94e-4251d21cd653"},"Gems":[]},{"Name":"Pride's Fall","Flags":"","Guid":{"m_Guid":"6e13ec0a-4b77-465b-b205-7d6b687b103f"},"Gems":[]},{"Name":"Spirit Eagle","Flags":"","Guid":{"m_Guid":"a5ca6295-6760-4961-b7a3-0e03c9ddd978"},"Gems":[]},{"Name":"Sylvan Duet","Flags":"","Guid":{"m_Guid":"86eca898-00b7-4642-a7c3-0e75a47e03aa"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Spirit Eagle","Flags":"","Guid":{"m_Guid":"a5ca6295-6760-4961-b7a3-0e03c9ddd978"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Return To The Soil","Flags":"","Guid":{"m_Guid":"a2eb5f02-f8cd-4baf-894e-80d5c6538aa4"},"Gems":[]},{"Name":"Incubate","Flags":"","Guid":{"m_Guid":"ae6ffe36-c358-4ea1-94cc-d4294c1d9b1c"},"Gems":[]},{"Name":"Runeweb Infiltrator","Flags":"","Guid":{"m_Guid":"e50468fe-6e6f-4319-80e9-c138748e18b4"},"Gems":[]},{"Name":"Sacred Seekers","Flags":"","Guid":{"m_Guid":"522ace85-0811-4df8-b979-defdbf913bf3"},"Gems":[]},{"Name":"Vilefang Eremite","Flags":"","Guid":{"m_Guid":"95254101-e69a-48af-a46f-d9b3aa7efd8e"},"Gems":[]},{"Name":"Frigid Buffalo","Flags":"","Guid":{"m_Guid":"4310820a-bbc5-430d-a34f-7783bb26032b"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Vilefang Eremite","Flags":"","Guid":{"m_Guid":"95254101-e69a-48af-a46f-d9b3aa7efd8e"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Touch of Xentoth","Flags":"","Guid":{"m_Guid":"69744127-4a29-4845-9889-3ce9565154e2"},"Gems":[]},{"Name":"Startouched Brave","Flags":"","Guid":{"m_Guid":"2d5154be-a224-4c69-bb66-888e358baf56"},"Gems":[]},{"Name":"Scraptooth Cackler","Flags":"","Guid":{"m_Guid":"38a3fb57-20e7-4e61-9732-d96f01105d4c"},"Gems":[]},{"Name":"Return To The Soil","Flags":"","Guid":{"m_Guid":"a2eb5f02-f8cd-4baf-894e-80d5c6538aa4"},"Gems":[]},{"Name":"Hatchery Broodguard","Flags":"","Guid":{"m_Guid":"a4ef5dd1-4aac-4922-82c7-dc234d413609"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Touch of Xentoth","Flags":"","Guid":{"m_Guid":"69744127-4a29-4845-9889-3ce9565154e2"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Pride's Fall","Flags":"","Guid":{"m_Guid":"6e13ec0a-4b77-465b-b205-7d6b687b103f"},"Gems":[]},{"Name":"Windbourne Disciple","Flags":"","Guid":{"m_Guid":"e7dd9fe9-c495-4933-af7e-4ad5eb432f9b"},"Gems":[]},{"Name":"Vilefang Eremite","Flags":"","Guid":{"m_Guid":"95254101-e69a-48af-a46f-d9b3aa7efd8e"},"Gems":[]},{"Name":"Arcane Focus","Flags":"","Guid":{"m_Guid":"8f5511ee-bd51-45fb-8348-6072e41c681b"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Vilefang Eremite","Flags":"","Guid":{"m_Guid":"95254101-e69a-48af-a46f-d9b3aa7efd8e"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Giant Spiderspawn","Flags":"","Guid":{"m_Guid":"5d49bc7d-9bfe-4789-b32a-f40ae44c6935"},"Gems":[]},{"Name":"Smoke Signals","Flags":"","Guid":{"m_Guid":"fe285ccb-50b8-4ad1-a1a8-96192b43734b"},"Gems":[]},{"Name":"Tireless Researcher","Flags":"","Guid":{"m_Guid":"7ab6005d-8a8d-4b6a-a71c-1691cb15a083"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Giant Spiderspawn","Flags":"","Guid":{"m_Guid":"5d49bc7d-9bfe-4789-b32a-f40ae44c6935"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Tireless Researcher","Flags":"","Guid":{"m_Guid":"7ab6005d-8a8d-4b6a-a71c-1691cb15a083"},"Gems":[]},{"Name":"Ember Tears","Flags":"","Guid":{"m_Guid":"2d6d0f05-5735-4330-9d01-add3c8305cf6"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Ember Tears","Flags":"","Guid":{"m_Guid":"2d6d0f05-5735-4330-9d01-add3c8305cf6"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
{"Cards":[{"Name":"Touch of Xentoth","Flags":"","Guid":{"m_Guid":"69744127-4a29-4845-9889-3ce9565154e2"},"Gems":[]}],"User":"InGameName","Message":"DraftPack"}
{"Card":{"Name":"Touch of Xentoth","Flags":"","Guid":{"m_Guid":"69744127-4a29-4845-9889-3ce9565154e2"},"Gems":[]},"User":"InGameName","Message":"DraftCardPicked"}
```
