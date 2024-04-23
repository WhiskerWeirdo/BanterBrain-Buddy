# BanterBrain Buddy
v0.0.5

## License
This project uses the GNU General Public License v3.0 as you can read [here](./LICENSE.txt). The TLDR is:

>You may copy, distribute and modify the software as long as you track changes/dates in source files. Any modifications to or software including (via compiler) GPL-licensed code must also be made available under the GPL along with build & install instructions.

## About
BanterBrain Buddy is a Windows .Net based Speech-To-Text to LLM to Text-To-Speech program for general entertainment or as a streaming companion.

The goal is to provide local PC or streaming entertainment by talking to an AI and hearing the responses back, based on a role you can set for the AI. The goa is to support both local only-resources or API services.
For the streaming side of things, integration with Twitch to respond to chat commands and stream events is planned.

## Social
Come talk or chat at https://discord.banterbrain.tv

## Build instructions
[todo]
tldr: use Visual studio 2022 with .net 8. 

## Resources for local and API
- for most API-services you need a paid account using a creditcard! (OpenAI, Azure, Google, Deepgram, etc.)
- local based LLM's like GPT4All take significant resources; expect to need at least 8 GB memory for a basic model and a solid GPU if you want fast responses

## Roadmap

### KNOWN ISSUES
0.0.2
- [ ] Wav file in use error sometimes on TTS

### 0.0.5 TODO
- [ ] FIX SET HOTKEY

- [X] TTS/STT: see how far you can set Native and Azure into their own classes so the main program isn't poluted too much
- [ ] Native STT: fix the output stream issue of native STT instead of using a wav file. (0.0.2 known issue)
- [X] Twitch: Setup Twitch client in its own class
- [X] Twitch: refresh auth token correctly on both timer and restart of application (fixed: using implicit grant)
- [X] Twitch: call /validate on startup to check if token is still valid and then call hourly
- [X] Twitch: eventSub => if listening for chat command subscribe to it and parse that text.
- [X] Twitch: eventSub => if listening for cheers, subscribe to it and check minbits and react when over and message attached.
- [X] Twitch: eventSub => if listening for subscription events check first-monthsubscribers and say a thank you message
- [X] Twitch: eventSub => if listening for resubscription events, react when there's a message attached
- [X] Twitch: eventSub => if listening for giftsub events and say a thank you message
- [X] Twitch: eventSub => allow for reactions to point redemptions and trigger if a specific one is used and react to the mesage

Known issues:
- [	] Store the Audio devices by deviceID instead of name to fix device renaming issues
- [ ] Twitch: add a check to verify if there's an active twitch connection, if not dont do anything twitch related
- [ ] TTS: add a check to verify if there's an active TTS connection, if not dont do anything TTS related
- [ ] TTS: verify filling the TTS option drowdowns with the correct values when something is changed (doesnt always refresh now)
- [X] TTS: Native TTS needs to list all possible voices

### Short term roadmap
- [X] 0.0.1 MVP where STT-> ChatGPT ->TTS works with PTT
- [X] 0.0.2 MVP use non-default audio devices for input/output
- [X] 0.0.3 MVP ability to read Twitch chat and react to specifyable chat command
- [X] 0.0.4 MVP Azure TTS/STT support
- [X] 0.0.5 ALPHA make Twitch client in a class, react to alerts in TTS (read out alert and give reaction) & use bits and channel points for events (pubsub probably)
- [ ] 0.0.5.1 ALPHA INSTALLABLE ALPHA BUILD
- [ ] 0.0.6 ALPHA Google, Elevenlabs & OpenAI Whisper TTS support 
- [ ] 0.0.7 ALPHA Improved Error handling & Code documentation. GUI improvements
- [	] 0.0.8 ALPHA Other languages than English support
- [ ] 0.0.9 ALPHA define persona's. A linked Role text with a specific voice setup.
- [ ] 0.1.0 BETA Installable Beta build release


