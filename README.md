# RDI TASK - NEWS 

This is a web application built with .NET6, and uses the [News API](https://newsapi.org). Users can search for news articles based on keywords and navigate through paginated results.

## Features

- **Display:** The current news are displayed to the user in an interactive way.
- **xxx:** Users are able to go straight to the news source by clicking any of the cards.
- **Pagination:** Results are paginated, allowing users to navigate through multiple pages of news articles.
- **Responsive Design:** The app is responsive and works well on both desktop and mobile devices.

## Languages and Technologies Used

- **C#:** xxxx
- **CSS:** xxxx
- **HTML:** xxxx
- **Bootstrap:** xxxx
- **News API:** xxxx

## Getting Started

- Clone the repository  
- Open Visual Studio  
- Select "File/Open/Project/Solution"  
- Select "NEWFLASH.sln"  

After doing this, the next step is to configure your API. This can be done following these steps:  

- In the Solution Explorer, right click on "NEWSFLASH"  
- Select "Open in Terminal"  
-Make sure to have installed dotnet secrets with this command:

dotnet tool install --global DotNetCore.UserSecrets.Tool

- Add your API using this command:  

dotnet user-secrets set "NewsApi:ApiKey" "your-api-key-here"  

- Make sure the changes are done correctly by running

dotnet user-secrets list

- Build the application hitting the green button next to NEWFLASH.



