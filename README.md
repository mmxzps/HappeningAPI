# EventVault

EventVault is the backend for our project Happening.

Happening is an app to find, save and share upcoming events in the Stockholm area. By gathering all upcoming events on one site it's easy to sort through and find upcoming events for you, your friends and family to share with eachother.

## database structure (ER-diagram)
![image (1)](https://github.com/user-attachments/assets/2c1382a4-115a-47e6-a183-c32ad017864e)

# Rest-API requests


## Admin

### GET - /api/Admin/users/{searchTerm}
returns user through namecomparison with searchterm.

### GET - /api/Admin/users
Returns all users from database

### DELETE - /api/Admin/users/{userId}
Deletes user from database using userId

## Auth

### POST - /api/Auth/register
Post using register DTO in header to register user to database

register DTO example :
{
  "userName": "string",
  "email": "string",
  "password": "string",
  "confirmPassword": "string"
}

### POST - /api/Auth/login
Post using login DTO to login user in database

login DTO example :
{
  "userName": "string",
  "password": "string"
}

### GET - /api/Auth/confirm-email
Returns OK (200) if successfull, requires token (string) and email (string) parameters to confirm email address of new user.

### POST - /api/Auth/forgot-password
Post using Forgot Password DTO for user to recieve forgotten password .

Forgot password DTO: 
{
  "userName": "string",
  "password": "string"
}

### POST - /api/Auth/reset-password
Post using Reset Password DTO to reset password for user

Reset Password DTO : 
{
  "userId": "string",
  "token": "string",
  "newPassword": "string"
}

### GET - /api/Auth/google-login
-

### GET- /api/Auth/google-response
-

### POST - /api/Auth/logout
Will sign out currently signed in user using active token.

##Event

### GET - /api/Event/allEvents
Gets a list of all events in database returned as an eventGetDTO

Response example : 
[
  {
    "id": 0,
    "eventId": "string",
    "category": "string",
    "title": "string",
    "description": "string",
    "imageUrl": "string",
    "apiEventUrlPage": "string",
    "eventUrlPage": "string",
    "date": "2024-11-26T23:01:57.505Z",
    "ticketsRelease": "2024-11-26T23:01:57.505Z",
    "highestPrice": 0,
    "lowestPrice": 0,
    "venue": {
      "id": 0,
      "name": "string",
      "address": "string",
      "city": "string",
      "zipCode": "string",
      "locationLat": "string",
      "locationLong": "string",
      "events": [
        "string"
      ]
    }
  }
]

### GET - /api/Event/{id}
Returns an event with ID from database as a eventGetDTO.

response example : 
{
  "id": 0,
  "eventId": "string",
  "category": "string",
  "title": "string",
  "description": "string",
  "imageUrl": "string",
  "apiEventUrlPage": "string",
  "eventUrlPage": "string",
  "date": "2024-11-26T23:02:38.110Z",
  "ticketsRelease": "2024-11-26T23:02:38.110Z",
  "highestPrice": 0,
  "lowestPrice": 0,
  "venue": {
    "id": 0,
    "name": "string",
    "address": "string",
    "city": "string",
    "zipCode": "string",
    "locationLat": "string",
    "locationLong": "string",
    "events": [
      "string"
    ]
  }
}

### POST - /api/Event/addEvent
Post Event Create DTO to add an event to database. Will not add a new one if it allready exists.

Event Create DTO :
{
  "eventId": "string",
  "category": "string",
  "title": "string",
  "description": "string",
  "imageUrl": "string",
  "apiEventUrlPage": "string",
  "eventUrlPage": "string",
  "date": "2024-11-26T23:03:01.099Z",
  "ticketsRelease": "2024-11-26T23:03:01.099Z",
  "highestPrice": 0,
  "lowestPrice": 0,
  "venue": {
    "name": "string",
    "address": "string",
    "zipCode": "string",
    "city": "string",
    "locationLat": "string",
    "locationLong": "string"
  }
}

### PUT - /api/Event/update/{id}
Update existing event by id in database using Event Update DTO

Event Update DTO: 
{
  "id": 0,
  "eventId": "string",
  "category": "string",
  "title": "string",
  "description": "string",
  "imageUrl": "string",
  "apiEventUrlPage": "string",
  "eventUrlPage": "string",
  "date": "2024-11-26T23:06:39.682Z",
  "ticketsRelease": "2024-11-26T23:06:39.682Z",
  "highestPrice": 0,
  "lowestPrice": 0,
  "venue": {
    "id": 0,
    "name": "string",
    "address": "string",
    "city": "string",
    "zipCode": "string",
    "locationLat": "string",
    "locationLong": "string",
    "events": [
      "string"
    ]
  }
}

### DELETE- /api/Event/delete/{id}
Deletes event from database using events id.


## User

### GET - /api/User/GetAllUsers
Returns all users from database.

### GET - /api/User/GetUserById
Returns user by id from database.

### GET - /api/User/GetUserByUserName
Returns user by username from database.

### POST - /api/User/UpdateUser
Updates userinfo in database by id, using User Update DTO.

User Update DTO example :
{
  "firstName": "string",
  "lastName": "string",
  "nickName": "string",
  "phoneNumber": "string"
}

### POST - /api/User/{userId}/event
Save event to user using user Id, by event create dto.

Event DTO example : 
{
  "eventId": "string",
  "category": "string",
  "title": "string",
  "description": "string",
  "imageUrl": "string",
  "apiEventUrlPage": "string",
  "eventUrlPage": "string",
  "date": "2024-11-26T23:13:19.599Z",
  "ticketsRelease": "2024-11-26T23:13:19.599Z",
  "highestPrice": 0,
  "lowestPrice": 0,
  "venue": {
    "name": "string",
    "address": "string",
    "zipCode": "string",
    "city": "string",
    "locationLat": "string",
    "locationLong": "string"
  }
}

### GET - /api/User/{userId}/event
Returns a list of events saved to user by user id.

### GET - /api/User/{userId}/event/{eventId}
Returns specific saved event from user by user id.

### DELETE - /api/User/{userId}/event/{eventId}
Deletes event from user using users id and event id.

## Venue

### GET - /api/Venue/AllVenues
Returns a list of all venues saved in the database.

### GET - /api/Venue/{id}
Returns the venue with specific Id.

### POST - /api/Venue/addVenue
Adds a venue to database using Venue Create DTO

Venue Create DTO example : 
{
  "name": "string",
  "address": "string",
  "zipCode": "string",
  "city": "string",
  "locationLat": "string",
  "locationLong": "string"
}

### PUT - /api/Venue/update/{id}
Updates venue in database by Id. Venue Update DTO needs to be sent in Header.

Venue Update DTO example:
{
  "id": 0,
  "name": "string",
  "address": "string",
  "city": "string",
  "zipCode": "string",
  "locationLat": "string",
  "locationLong": "string"
}

### DELETE - /api/Venue/delete/{id}
Deletes venue with specific id in database.


##Friendship

### POST - /api/Friendship/SendFriendRequest
Sends friend request using userId and possible friend user id creating a friendship in database.

### POST - /api/Friendship/AcceptFriendRequest
Accept existing friendrequest using friendship id.

### POST - /api/Friendship/DeclineFriendRequest
declines existing friendrequest using friendship id.

### GET - /api/Friendship/ShowFriendRequests
Returns a list of friendrequests by user id.

### GET - /api/Friendship/ShowAllFriends
Returns a list of friends by user id.

## KBEvent

### GET - /KBEventAPI/getEventList
Returns a list of events from KBEvents.

Response example:
[
  {
    "id": 0,
    "eventId": "string",
    "category": "string",
    "title": "string",
    "description": "string",
    "imageUrl": "string",
    "apiEventUrlPage": "string",
    "eventUrlPage": "string",
    "dates": [
      "2024-11-26T23:20:41.856Z"
    ],
    "ticketsRelease": "2024-11-26T23:20:41.856Z",
    "highestPrice": 0,
    "lowestPrice": 0,
    "venue": {
      "id": 0,
      "name": "string",
      "address": "string",
      "city": "string",
      "zipCode": "string",
      "locationLat": "string",
      "locationLong": "string"
    }
  }
]

### GET - /KBEventAPI/getEvents
Returns a list of Ids of events at kbevents API

## TicketMaster

### GET - /TicketMasterAPI/getEvents
Gets a list of 250 events from Ticketmaster containing upcoming events from todays date in the stockholm area.

response example :
[
  {
    "id": 0,
    "eventId": "string",
    "category": "string",
    "title": "string",
    "description": "string",
    "imageUrl": "string",
    "apiEventUrlPage": "string",
    "eventUrlPage": "string",
    "dates": [
      "2024-11-26T23:20:41.856Z"
    ],
    "ticketsRelease": "2024-11-26T23:20:41.856Z",
    "highestPrice": 0,
    "lowestPrice": 0,
    "venue": {
      "id": 0,
      "name": "string",
      "address": "string",
      "city": "string",
      "zipCode": "string",
      "locationLat": "string",
      "locationLong": "string"
    }
  }
]

## VisitStockholm

### GET - /VisitStockholmAPI/getEvents
Returns a list of upcoming events in the stockholm area from todays date.

response example :  
[
  {
    "id": 0,
    "eventId": "string",
    "category": "string",
    "title": "string",
    "description": "string",
    "imageUrl": "string",
    "apiEventUrlPage": "string",
    "eventUrlPage": "string",
    "dates": [
      "2024-11-26T23:20:41.856Z"
    ],
    "ticketsRelease": "2024-11-26T23:20:41.856Z",
    "highestPrice": 0,
    "lowestPrice": 0,
    "venue": {
      "id": 0,
      "name": "string",
      "address": "string",
      "city": "string",
      "zipCode": "string",
      "locationLat": "string",
      "locationLong": "string"
    }
  }
]
