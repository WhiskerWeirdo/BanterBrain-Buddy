# BanterBrain Buddy
v0.0.1

## License
This project uses the GNU General Public License v3.0 as you can read [here](./LICENSE.txt). The TLDR is:

>You may copy, distribute and modify the software as long as you track changes/dates in source files. Any modifications to or software including (via compiler) GPL-licensed code must also be made available under the GPL along with build & install instructions.

## About
BanterBrain Buddy is a Windows .Net based Speech-To-Text to LLM to Text-To-Speech program for general entertainment or as a streaming companion.

The goal is to provide local PC or streaming entertainment by talking to an AI and hearing the responses back, based on a role you can set for the AI. The goa is to support both local only-resources or API services.
For the streaming side of things, integration with Twitch to respond to chat commands and stream events is planned.

## Social
Come talk or chat at https://discord.gg/2EEhKpEMMT

## Build instructions
[todo]
tldr: use Visual studio 2022 with .net 4.7.2 and pray. Also install NAudio and OpenAI by OkGoDolt.


## Resources for local and API
- for most API-services you need a paid account using a creditcard! (OpenAI, Azure, Google, Deepgram, etc.)
- local based LLM's like GPT4All take significant resources; expect to need at least 8 GB memory for a basic model and a solid GPU if you want fast responses

## ToDo before public release
### Voice input
- [ ] Set input device
- [ ] Set Push-To-Talk
- [ ] Global hooking for PTT


### Main program
- [X] Full run from Native STT to ChatGPT to Native TTS
- [X] Store and read setings file & API keys
- [ ] Use selected Voice Input device instead of default
- [ ] 0.0.1 release!