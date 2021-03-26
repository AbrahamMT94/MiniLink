# MiniLink

Minilink is simple url shortener with basic reporting such as url visit count. 
This project follows the onion or layers architecture where the outer layers only rely on the ones one level beneath it to avoid tight coupling.

The project is currently live at the following url:
https://minilink.azurewebsites.net/

### Project Structure
#
Minilink is broken down into 3 projects:
1. **Logic**
2. **Server** 
3. **Client**
4. **Test**

### Logic Project
#
The logic project is where our app live it contain separate folder for business logic, data access, etc.

The logic project contains 3 projects:
- **Data:** Where the code for repositories and Data access layer lives
- **Core:** Where the domain entitites live
- **Service:** Where our services live

### Server Project
#
This project is used interact and set up the app. It has few api controllers and also holds our dto mappers and is in charge of shortening our ids.
This project is also in charge of serving our front end.

The project structure is as follows:

- **Controllers:** This folder has controller for link registration as well as handling redirections
- **Mappers**: A Mappers folder where our entity to dto mappers live
- **Utilities**: Where any helper classes live

### Client Project
#
The client project is a SPA written in Blazor Web Assembly. It contains components for supporting the creationg and display of urls as well as search functionality and visit counter

The project structure is as follows:
- **wwwwroot:** Where our content and css lives
- **Pages:** Where our main components live
- **Shared:** WHere our shared components live

### Test Project
#
The test project contains a few unit and integration tests. Rather than mocking our interfaces or layers we use SQLite to generate actual contexts to simulate the database.
This of course might not be always feasable and it might be desirable to perhaps use an actual test database that is closer to our production environment but for this demo it should suffice. Testing is done via XUnit.

The project structure is as follows:
- **MiniLinkTests:** In this folder you'll find folder that test our data access layer and service layer.
- **ServerTests:** In this folder you'll find tests pertaining the server (At the moment its missing controller tests)

