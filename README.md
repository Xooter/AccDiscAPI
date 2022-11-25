# **Índice**

1. [**AccDiscAPI**](#AccDiscAPI)
3. [**TODO**](#TODO)
4. [**Documentation**](#Documentation)
    1. [Dialog](#dialog)
    2. [Mesas](#mesas)
---

# AccDiscAPI 

### **Description** 
AccDiscAPI is an unofficial open source wrapper capable of helping you manage a discord account.


### **Initial configuration**

This package need the cookies of an account and the auth.
This can be achieved in the header of any packet using a network traffic capture program.


>**Important**If this is not respected the program will not work.

```csharp
using AccDiscAPI.Models;

//initial setup
Global.Authorization = "XXXXX.XXXXX.XXXXX[...]";
Global.Cookie = "_XxXX=XXXxXXxXxXxXX;[...]";

```

---

# TODO

- **User**
- [ ] Add Roll
- [ ] Delete Rolls
- **Guilds**
- [X] Get all rolls count
- [ ] Emojis
- [ ] Edit configuration
- **Messages**
- [ ] Reactions
- [ ] Create Threads
- **Invites**
- [ ] Create Invitation
- [x] Get Channel invitations
- **User**
- [ ] Direct message
- [ ] Add friend
- **Channels**
- [ ] Send message in channel
- [ ] Edit permissions
- [ ] Delete channel
- [ ] Create channel
- [ ] Title channel Class
---

# Documentation

All the classes with their respective methods and attributes are found in this section.
</br>
This section will be updated with the updates.

## AccDisc

#### Methods
| Method             | Description                        | Perms | Return        |
|--------------------|------------------------------------|-------|---------------|
| GetChannelMessages | Get last X message of any channel. | False | List Message |
| GetUserInfo        | Get user by the id.                | False | User          |
| GetGuildInfo       | Get Guild by id.                   | False | Guild         |
| AddNote            | Add personal note by user id.      | False |               |
| EditMessage        | Edit this message.                 | True  | Message       |
| DeleteMessage      | Delete any message.                | True  | Bool          |
| SendMessage        | Send channel message.              | False | Message       |
| ChangeNick         | Change username nickname by id.    | True  | Message       |
| FullMute           | Mute or Deaf an user by id.        | True  |               |
| MoveToChannel      | Moves the user to a channel.       | True  |               |
| Annoy              | Moves the user x number of times.  | True  |               |

## Guild

#### Atributes
#### Methods

## User

#### Atributes
#### Methods

## UserGuild

#### Atributes
#### Methods

## Rooll

#### Atributes
#### Methods

## Message

#### Atributes
#### Methods

## Invite

#### Atributes
#### Methods

## Global

#### Atributes
#### Methods

## Attachment

#### Atributes
#### Methods

## Accounts

#### Atributes
#### Methods

## Channel

#### Atributes
#### Methods

### ChannelText

#### Atributes
#### Methods

### ChannelVoice

#### Atributes
#### Methods

