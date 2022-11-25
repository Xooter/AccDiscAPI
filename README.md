# **Index**

1. [**AccDiscAPI**](#AccDiscAPI)
3. [**TODO**](#TODO)
4. [**Documentation**](#Documentation)
    

---
>### Nugget Package
>[nuget.org](https://www.nuget.org/packages/AccDiscAPI/)

# AccDiscAPI 

### **Description** 
AccDiscAPI is an unofficial open source library capable of helping you manage a discord account.


### **Initial configuration**

This package need the cookies of an account and the auth.
This can be achieved in the header of any packet using a network traffic capture program.


>**Important** If this is not respected the program will not work.

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
- [ ] Download icon
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

### Index
- [AccDisc](#AccDisc)
- [Guild](#Guild)
- [User](#User)
- [UserGuild](#UserGuild)
- [Roll](#Roll)
- [Message](#Message)
- [Invite](#Invite)
- [Global](#Global)
- [Attachment](#Attachment)
- [Accounts](#Accounts)
- [Channel](#Channel)
- [ChannelText](#ChannelText)
- [ChannelVoice](#ChannelVoice)

<hr>

## AccDisc


#### Methods
| Method             | Description                        | Perms | Return        |
|--------------------|------------------------------------|-------|---------------|
| GetChannelMessages | Get last X message of any channel. | False | List<[Message](#Message)> |
| GetUserInfo        | Get user by the id.                | False |  [User](#User)          |
| GetGuildInfo       | Get Guild by id.                   | False |  [Guild](#Guild)         |
| AddNote            | Add personal note by user id.      | False |               |
| EditMessage        | Edit this message.                 | True  |  [Message](#Message)       |
| DeleteMessage      | Delete any message.                | True  | Bool          |
| SendMessage        | Send channel message.              | False |  [Message](#Message)       |
| ChangeNick         | Change username nickname by id.    | True  |  [Message](#Message)           |
| FullMute           | Mute or Deaf an user by id.        | True  |               |
| MoveToChannel      | Moves the user to a channel.       | True  |               |
| Annoy              | Moves the user x number of times.  | True  |               |

---

## Guild

#### Atributes
| Atributes               | Description      | Type   |
|-------------------------|------------------|--------|
| id                      | Guild id         | long   |
| name                    | Guild name       | string |
| afk_channel_id          | AFK channel id   | long   |
| afk_timeout             | AFK time in ms   | int    |
| discovery_splash        | ?                | string |
| explicit_content_filter | ?                | int    |
| features                | ?                | string |
| icon                    | Guild icon url   | string |
| max_members             | Guild max member | int    |
| nsfw                    | NSFW Active      | bool   |
| nsfw_level              | ?                | int    |
| owner_id                | Owner User ID    | long   |
| preferred_locale        | ?                | string |
| premium_tier            | Nitro lvl            | int       |
| public_updates_channel_id | ?                  | long      |
| region                    | Guild region       | string    |
| roles                     | All rolls          | List<[Roll](#Roll)> |
| rules_channel_id          | Rules channel id   | long      |
| verification_level        | Verification level | int       |
| TextChannel          | All Text type channels  | List<[ChannelText](#ChannelText)>     |
| VoiceChannel        | All Voice type channels | List<[ChannelVoice](#ChannelVoice)>    |

#### Methods
| Method        | Description                              | Perms | Return |
|---------------|------------------------------------------|-------|--------|
| GetRolesCount | Count the number of member of each roll. | True  |        |

## User

#### Atributes
| Atributes               | Description      | Type   |
|-------------------------|------------------|--------|
| id                      | User id         | long   |
| avatar                    | User avatar       | string |
| avatar_decoration          | ?   | string   |
| discriminator             | ?   | int    |
| public_flags        | ?                | int |
| flags | ?                | int    |
| username                | User username               | string |
| bio                    | User bio/description   | string |
| accent_color             | Color | string |
| banner                    | ?      | string |
| banner_color              | ?                | string    |
| connected_accounts                | All the aplication related to the user   | List<[Accounts](#Accounts)>   |
| mutual_guilds        | All Guilds in commont to the user                | List<[UserGuild](#UserGuild)> |

#### Methods
| Method            | Description                       | Perms | Return |
|-------------------|-----------------------------------|-------|--------|
| SaveAvatar        | Save user avatar asynchronously   | False |        |
| AddNote           | Add personal note to user         | False |        |
| ChangeNick        | Change username nickname.         | True  |        |
| FullMute          | Mute or Deaf an user.             | True  |        |
| Annoy             | Moves the user x number of times. | True  |        |
| MoveToChannel     | Moves the user to a channel.      | True  |        |
| DisconnectChannel | Disconect user.                   | True  |        |

## UserGuild

#### Atributes
| Atributes | Description                     | Type        |
|-----------|---------------------------------|-------------|
| id        | User Guild id (same as User id) | long        |
| nick      | User Nick in the Guild          | string      |
| rolls     | All rolls in guild              | list string |
| joined_at | User entry Date                 | string      |

## Roll

#### Atributes
| Atributes   | Description                      | Type        |
|-------------|----------------------------------|-------------|
| id          | Roll id                          | long        |
| name        | Roll name                        | string      |
| color       | Roll color                       | list string |
| description | Roll description                 | string      |
| flags       | ?                                | int         |
| hoist       | ?                                | bool        |
| managed     | ?                                | bool        |
| mentionable | If roll is mentionable           | bool        |
| position    | Roll position (related to perms) | int         |
| permissions | Perms of rolls                   | long        |
| members     | Count of members                 | int         |

## Message

#### Atributes
| Atributes          | Description                      | Type            |
|--------------------|----------------------------------|-----------------|
| id                 | Message id                       | long            |
| attachments        | Attachment of the message if any | List<[Attachment](#Attachment)> |
| author             | Message Author                   | [User](#User)            |
| channel_id         | Channel of the message           | long            |
| content            | Message content                  | string          |
| edited_timestamp   | edit date if any                 | string          |
| flags              | ?                                | int             |
| mention_everyone   | If @everyone                     | bool            |
| mention_roles      | If any roll was mentioned        | List<[Roll](#Roll)>       |
| mentions           | If any User was mentioned        | List<[User](#User)>       |
| referenced_message | Replyed message                  | [Message](#Message)         |
| pinned             | If pinned                        | bool            |
| timestamp          | Date of message                  | string          |
| tts                | tts is active                    | bool            |
| type                | message type                 | int            |

#### Methods
| Method        | Description          | Perms      | Return  |
|---------------|----------------------|------------|---------|
| DeleteMessage | Delete this message. | False/True | bool    |
| EditMessage   | Edit this message.   | False/True | Message |

## Invite

#### Atributes
| Atributes  | Description      | Type          |
|------------|------------------|---------------|
| code       | Invitation code  | string        |
| uses       | Number of uses   | int           |
| expires_at | Expiration date  | string        |
| created_at | Creation date    | string        |
| inviter    | Creator user     | [User](#User) |
| temporary  | if temporary     | bool          |
| max_age    | expiration time  | int           |
| max_uses   | expiration usage | int           |


## Attachment

#### Atributes
| Atributes    | Description         | Type   |
|--------------|---------------------|--------|
| id           |                     | long   |
| content_type | ?                   | string |
| filename     | Attachment filename | string |
| height       | Image height        | int    |
| width        | Image width         | int    |
| proxy_url    | Image url           | string |
| size         | Image size          | int    |
| url          |                     | string |

## Accounts

#### Atributes
| Atributes | Description | Type   |
|-----------|-------------|--------|
| id        |             | string |
| name      |             | string |
| type      | ?           | string |
| verified  | if verified | bool   |

## Channel

#### Atributes
| Atributes       | Description       | Type   |
|-----------------|-------------------|--------|
| id              | Channel id        | long   |
| name            | Channel name      | string |
| guild_id        | Channel guild     | long   |
| last_message_id |                   | long   |
| parent_id       | Channel parent id | long   |
| nsfw            |                   | bool   |
| position        | Channel position  | int    |

#### Methods
| Method     | Description          | Perms | Return                 |
|------------|----------------------|-------|------------------------|
| GetInvites | Get invitation info  | True  | List<[Invite](#Invite)> |
| ChangeName | Edit name of channel | True  | Message                |

### ChannelText

#### Atributes
| Atributes           | Description           | Type   |
|---------------------|-----------------------|--------|
| topic               |                       | string |
| rate_limit_per_user | Wait time restriction | int    |
#### Methods
| Method             | Description                           | Perms | Return                   |
|--------------------|---------------------------------------|-------|--------------------------|
| GetChannelMessages | Get last X message of the channel.    | False | List<[Message](#Invite)> |
| ChangeTopic        | Change topic of channel.              | True  |                          |
| ChangeRate         | Change the user wait-time in channel. | True  |                          |

### ChannelVoice

#### Atributes
| Atributes       | Description           | Type   |
|-----------------|-----------------------|--------|
| bitrate         |                       | int    |
| user_limit      | User count Limitation | int    |
| rtc_region      |                       | string |
#### Methods
| Method          | Description                              | Perms | Return |
|-----------------|------------------------------------------|-------|--------|
| ChangeBitrate   | Change bitrate of the channel.           | True  |        |
| ChangeUserLimit | Change the user limit per voice channel. | True  |        |

