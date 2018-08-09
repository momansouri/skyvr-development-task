# Sky VR development task

This is a short Unity development task to give us a feel for how you approach application development.

This is not to test your basic C# abilities, but rather to provide us with an understanding of the practises you would employ to deliver a commercial product, and ensure ongoing stability.

## Prerequisites
- Please use Unity 2017.4

## Task

### Abstract

Imagine you are creating the client side for a login system in one of Sky's mobile applications. The client has requested that the sign in page has two input boxes, one for an email address, and one for a password, along with a "Sign In" button. Upon clicking the "Sign In" button you should submit the email address and password to the login server for authentication. The client expects the application to display the appropriate "Success" or "Fail" screen based on the response. It is reasonable to assume the client would want an error message explaining why the sign in failed, if this occurs.

### Useful information
- The provided Unity scene has a Sign In page, a Success page and a Fail page. These should be switched on/off at the appropriate times.

- There exists a static class "Server" with a single method "SignIn"; this represents the authorisation server
    - Use this method in place of an HTTP request, assume that would be taken care of for you
    - Feel free to take a look at this simple class, but do not modify it


- If valid details are submitted, expect the server to return an OAuth token
- If invalid details are used, expect the server to return an error message with the reason

- To test your implementation, we should be able to log in with some test details
    - Email: *skyvr@sky.com*
    - Password: *v1rtualr3al!ty*

- The server method is expecting the login details to be urlencoded, so you will need to factor this into your solution

- Upon receiving a successful response from the server your application should store the OAuth token for future use
    - Do not worry about implementing an OAuth flow, for now just store the token upon recieving it