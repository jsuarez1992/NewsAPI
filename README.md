# RDI TASK - NEWS 

This is a web application built with .NET6 Razor Pages, and uses the [News API](https://newsapi.org) and the [AdminkitUI](https://adminkit.io/) template with Bootstrap 5 as a base . Users can search for news articles based on keywords and navigate through paginated results.

## Features

- **Display:** The current news are displayed to the user in an interactive way. Users are able to go straight to the news source by clicking any of the cards.
- **Category display:** User can select the category he is interested and see all the news relevant for that field.  
- **Search by source:** User can search news from sites like BBC, CNN, and so on.  
- **Search by keyword:** User can search news related to a certain topic like "agriculture", "eclipse" and so on.
- **Responsive Design:** The app is responsive and works well on both desktop and large screen devices.
- **Input Validation:** Using REGEX to limit the search to only alphanumeric characters.
- **Error Handling:** Catch error for different HTTP requests.
- **API key / Configuration management:** Follow best practices using "appsetings.json" and "IConfiguration" to handle API request, and hiding the key using dotnet user-secrets

## Languages and Technologies Used

- **C#**  
- **CSS** 
- **HTML** 
- **Bootstrap** 
- **News API** 

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



