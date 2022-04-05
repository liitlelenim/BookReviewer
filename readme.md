# <p align="middle">Book Reviewer<p>
### <p align="middle"> Book Reviewer is a REST API simulating book reviewing app created for learning purposes using ASP.NET Core. <p>

## Features
- **Roles system**: Every user has at first default role but can be promoted to Moderator or Administrator role. Moderators are responsible for resolving book proposals. Administrators are able to set roles of other users and remove their accounts.
- **Books collection system**: user can add books to their read collection and check how many users have read given book.
- **Book proposing system**: users can propose books that should be added to the site. Administrators and moderators are able to reject or accept a proposal and edit it if it's necessary
- **Reviews system**: users can review any book in their collection, giving it rating from 1 to 5, or optionally writing their opinion about it. Users can check average score of every book as well.
## Tech stack
- #### .NET 6
- #### ASP.NET Core
- #### Entity Framework Core 6
- #### XUnit
- #### JWT Authentication