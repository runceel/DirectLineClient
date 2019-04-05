# How to run this sample.

## Create a bot

- SignIn to Azure portal
- Create a Web Apps and a Bot Channels Registrations
- Create a DirectBot.bot file to the Chatbot.Server project
- Edit it like following:

```json
{
  "name": "DirectBot",
  "description": "",
  "services": [
    {
      "type": "endpoint",
      "name": "development",
      "appId": "",
      "appPassword": "",
      "endpoint": "http://localhost:4082/api/messages",
      "id": "155"
    },
    {
      "type": "endpoint",
      "name": "production",
      "appId": "your bot's app id",
      "appPassword": "your bot's app secret",
      "endpoint": "https://yourwebappname.azurewebsites.net/api/messages",
      "id": "135"
    }
  ],
  "padlock": "",
  "version": "2.0"
}
```

- Deploy Chatbot.Server project to Azure Web Apps you created.

## 