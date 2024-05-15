### v0.1.1-BETA release of BanterBrain Buddy.
Download: https://github.com/WhiskerWeirdo/BanterBrain-Buddy/releases/tag/0.1.1-beta

**This BETA requires a valid OpenAI ChatGPT API key to operate _or_ a local Ollama installation.** 

This is a beta, please report all bugs to the discord or here on github. 

### Requisites to run
This release depends on .NET Runtime Desktop 8. After installation and trying to run BBB you will be asked to download and install this from the Microsoft website if you do not have it installed yet.

You can also download Runtime Desktop here: [Windows Runtime Desktop 8.0.4](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.4-windows-x64-installer)

### KNOWN ISSUES
- Plugging in/out audio devices while BBB runs can have an unforseen effect on your input/output and throw errors
- No ability to check for Twitch followers
- ElevenLabs API check (preloading voices) can sometimes timeout after 15 seconds. This makes the first time you use it, probably quite a bit slower when editing persona's, but that's only until it works. This cannot really be fixed on my side.
- Invalid API keys are not handled well and generate errors. See https://github.com/WhiskerWeirdo/BanterBrain-Buddy/issues/42 for a workaround to fix the crashing if you cannot supply a valid API key. You will still have to after that fix the persona's and/or pick different ones that don't use the now-invalid API key.

### RELEASE V0.1.0 BETA
- many fixes for text input issues, like saving on the correct time or not allowing empty fields
- Twitch OAUTH fix to allow configurable redirect if default port is occupied
- Elevenlabs optimizations
- Fixes to PTT button
- Fixes to Hotkeys
- Added timestamps to Main text window 
- many code hygiene fixes
- Ollama roletext fix & now Ollama supports and remembers previous content
- Optimization of API verifications 
- added more logging to help debugging
- Added a HELP setting that opens the logfile directory
- New Elevenlabs library

## License
This project uses the GNU General Public License v3.0 as you can read [here](./LICENSE.txt). The TLDR is:

>You may copy, distribute and modify the software as long as you track changes/dates in source files. Any modifications to or software including (via compiler) GPL-licensed code must also be made available under the GPL along with build & install instructions.

## About
BanterBrain Buddy is a Windows .Net based Speech-To-Text to LLM to Text-To-Speech program for general entertainment or as a streaming companion.

The goal is to provide local PC or streaming entertainment by talking to an AI and hearing the responses back, based on a role you can set for the AI. The goa is to support both local only-resources or API services.
For the streaming side of things, integration with Twitch to respond to chat commands and stream events is planned.

Example: https://www.youtube.com/watch?v=TawapT1WEEo

## Social
Come talk or chat at https://discord.banterbrain.tv

## Build instructions
[todo]
tldr: use Visual studio 2022 with .net 8. 

## Resources for local and API
- for most API-services you need a paid account using a creditcard! (OpenAI, Azure, Google, Deepgram, etc.)
- local based LLM's like GPT4All take significant resources; expect to need at least 8 GB memory for a basic model and a solid GPU if you want fast responses

## Roadmap


### Short term roadmap
- make beta stable

