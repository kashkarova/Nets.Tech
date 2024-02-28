# Nets.Tech
This is a short user guide how to start Server and Client apps.

Tech
Programming language(mostly): C#
Target framework: .NET 8
Client: .NET MAUI Blazor Hybrid app
Server: Blazor Web app
Technology to support two-way communication: WebSockets
Tools: Visual Studio 2022, tea/coffee ;)

HOW-TO:
1. Clone, please, the repository Nets.Tech to your machine. In the repo you will see two folders with corresponding solutions Nets.Tech.Server and Nets.Tech.Client for server and client respectively.

- START A SERVER

	1. In Nets.Tech.Server folder find, please, the solution and open it in Visual Studio. 
	2. In Start button choose, please, https and start the application
	3. When the app starts for the first time, you will see some popups with asking you to generate trusted certificates (we need to show, that our server is 
     trustworthy and also for proper working with HTTPS). Please, accept all of them. 
  4. When app starts, it opens a new page in browser (or new window of browser) with unpleasant stuff related to unsecurity connection etc. Please, close all 
     browser windows in your PC/laptop and re-build the solution. Then start the app again.
  5. If you see the text "Message" and one empty imput field, then Server app is running.

Now we should start a client to make them both work.

- START A CLIENT

  1. 1. In Nets.Tech.Client folder find, please, the solution and open it in Visual Studio. Start the app.
  2. You will see the textbox with address "wss://localhost:7186/ws" and "Connect" button. This is an address of my server, set by defaut. 

     ATTENTION! your port can be different, so put, please, your numbers after "localhost:". Then click "Connect";

  3. Enter any message to the empty textbox under "Message" text and the entered text should appear in the empty textbox in Server app. 

Repeat with any combitations of messages on both Client and Server apps: it will be synchronized.
