# BanterBrain Buddy
v0.0.1

## License
This project uses the GNU General Public License v3.0 as you can read [here](./LICENSE.txt). The TLDR is:

>You may copy, distribute and modify the software as long as you track changes/dates in source files. Any modifications to or software including (via compiler) GPL-licensed code must also be made available under the GPL along with build & install instructions.

## About
BanterBrain Buddy is a Windows .Net based Speech-To-Text to LLM to Text-To-Speech program for general entertainment or as a streaming companion.

The goal is to provide local PC or streaming entertainment by talking to an AI and hearing the responses back, based on a role you can set for the AI. The goa is to support both local only-resources or API services.
For the streaming side of things, integration with Twitch to respond to chat commands and stream events is planned.

## Resources for local and API
- for most API-services you need a paid account using a creditcard! (OpenAI, Azure, Google, Deepgram, etc.)
- local based LLM's like GPT4All take significant resources; expect to need at least 8 GB memory for a basic model and a solid GPU if you want fast responses

## ToDo before public release
### Voice input
- [x] Get input devices
- [x] Find default device
- [ ] Set input device
- [ ] Set Push-To-Talk
- [ ] Global hooking for PTT

### Speech-To-Text
- [x] Enable Native STT
- [x] Show example of Native STT

### LLM
- [X] Enable use of ChatGPT with API key
- [X] Show example of text based ChatGPT response
- [X] Show example of STT to ChatGPT
- [X] Set mood (prefix text) for LLM

### Text-to-Speech
- [X] Enable Native TTS
- [X] Audio example of Native TTS
- [X] Audio example of ChatGPT to TTS

### Main program
- [X] Full run from Native STT to ChatGPT to Native TTS
- [ ] Store and read setings file & API keys

## Broad roadmap items in no particular order
- [ ] Enable Azure STT
- [ ] Use selected Voice Input device instead of default
- [ ] 0.1 release with basic functionality
- [ ] Enable use of GPT4All
- [ ] Deepgram STT API
- [ ] Azure API for STT and TTS
- [ ] Google API for STT and TTS
- [ ] OpenAI Whisper for STT
- [ ] Link to Twitch to respond to chat and notifications
- [ ] Link to Youtube to respond to chat
- [ ] Link to OBS/
- [ ] Link to Live2d
- [ ] link to VOSK & Kaldi (Local STT)
- [ ] Add more languages to program
- [ ] Add more languages to STT/TTS
- [ ] integration with SAMMI
- [ ] Link to KICK to respond to chat and notifications
 
