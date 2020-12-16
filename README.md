# Auto Mute Bot
A Discord Bot that mutes users when they join a voice channel for X seconds. 
It has a config file that lets you specify channels, the amount of seconds and roles that bypass it. 

Example config:
```
{
	"Token":"YourToken",
	"Channels":null,
	"MuteSeconds":5.0,
	"BypassRoles":null,
	"MuteWhenChannelEmpty":false
}
```
If you leave Channels as null it will mute in every channel, you can specify channels like this:
```
{
	"Token":"YourToken",
	"Channels":[0000001,000002],  <-- Replace 0000001 with your channel id
	"MuteSeconds":5.0,
	"BypassRoles":null,
	"MuteWhenChannelEmpty":false
}
```
The same effect applies to BypassRoles
 --
 
 If MuteWhenChannelEmpty is true it will mute the user even if the channel is empty.
 
