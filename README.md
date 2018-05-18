# Weather-TelegramBot
It's the simple weather bot for [Telegram](https://telegram.org/).  

##Bot's commands:
/start - display welcome message and list of commands  
/weather city - for find out current weather in city  
/forecast city - for find out the forecast for next few days  
/help - for getting the list of commands

##How to start
###Telegram bot creation
Firstly you must create bot in the Telegram.  
For this you must add the [@BotFather](http://telegram.me/BotFather) in the Telegram and follow a few steps.  
**Steps:**  
1. Input /newbot command for create a new bot  
2. Choose a name for bot. It's name users can see in contacts  
3. Choose a username for bot. It must end in 'bot'.  

After this you must receive messege with link to your bot and access token.

###Weather bot setting
####Bot settings file creation
In bot directory create a new file `config.json`  
**This file contains next code:**  
```JSON
{
  "TelegramAccessToken":  "YOUR_BOT_ACCESS_TOKEN",
  "TelegramBotName": "YOUR_BOT_NAME"
}
```
In this file you paste your bot access token and name.
####Bot installation
Run the console as administrator in work directory and execute the next command for install bot service: `Weather-TelegramBot install`
####Bot service start command
Run the console as administrator in work directory or open Windows service manager.  
In console execute the next command for start bot service: `Weather-TelegramBot start`  
In service manager select 'WeatherBotService' and push 'start' button.
####Bot service stop command
Run the console as administrator in work directory or open Windows service manager.  
In console execute the next command for start bot service: `Weather-TelegramBot stop`  
In service manager select 'WeatherBotService' and push 'stop' button.

###Check bot
Open dialog with your bot in Telegram and write any command from list. Bot must answer on your message.
