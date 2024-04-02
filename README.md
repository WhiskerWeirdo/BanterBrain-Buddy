# BanterBrain Buddy
v0.0.4

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
tldr: use Visual studio 2022 with .net 4.7.2. Create a temporary certificate called BanterBrain Buddy_TemporaryKey.pfx. Also install NAudio, MouseKeyHook and OpenAI by OkGoDolt. 

## Resources for local and API
- for most API-services you need a paid account using a creditcard! (OpenAI, Azure, Google, Deepgram, etc.)
- local based LLM's like GPT4All take significant resources; expect to need at least 8 GB memory for a basic model and a solid GPU if you want fast responses

## Roadmap

### KNOWN ISSUES
0.0.2
- [ ] Wav file in use error sometimes on TTS

0.0.3
- [ ] Twitch chat client won't die after you started the "Test" 
- [ ] Twitch Auth Token needs refresh instead of re-auth
- [ ] You need to sign up to https://dev.twitch.tv and get a application ClientID & Secret to build BBB yourself

### 0.0.4 TODO
- [X] Store Azure API key
- [X] Use Azure API for Speech-To-Text Test
- [X] Add an actual logfile
- [X] Use Azure API for STT Normal use
- [ ] Use Azure STT from non-default microphone
- [ ] Use Azure APi for Text-To-Speech test
- [ ] Use Azure API for TTS Normal use
- [ ] Use Azure STT from non-default output device
 
### Short term roadmap
- [X] 0.0.1 MVP where STT-> ChatGPT ->TTS works with PTT
- [X] 0.0.2 MVP use non-default audio devices for input/output
- [X] 0.0.3 MVP ability to read Twitch chat and react to specifyable chat command
- [ ] 0.0.4 MVP Azure TTS/STT support
- [ ] 0.0.5 ALPHA make Twitch client in a class, react to alerts in TTS (read out alert and give reaction) & use bits and channel points for events (pubsub probably)
- [ ] 0.0.5.1 ALPHA INSTALLABLE ALPHA BUILD
- [ ] 0.0.6 ALPHA Google & OpenAI Whisper TTS support 
- [ ] 0.0.7 ALPHA Error handling & Code documentation
- [ ] 0.0.8 ALPHA clean up code: use Logger; make TTS/STT in their own class(es)
- [ ] 0.0.9 - reserved -
- [ ] 0.1.0 BETA Installable Beta build release


