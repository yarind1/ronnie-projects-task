# UserDataCollector

.NET console application that pulls users from multiple public APIs, normalizes the data, and exports it to a file in either JSON or CSV format based on user input.

---

## Features

- Fetches users from 4 public APIs:
  - [randomuser.me](https://randomuser.me/api/?results=500)
  - [jsonplaceholder.typicode.com](https://jsonplaceholder.typicode.com/users)
  - [dummyjson.com](https://dummyjson.com/users)
  - [reqres.in](https://reqres.in/api/users)
- Normalizes data to a unified `User` format:
  - `FirstName`, `LastName`, `Email`, `SourceId`
- Parallel fetching for fast execution
- Handles API failures gracefully
- User chooses output format: `json` or `csv`

---

## How to Run

1. Make sure [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download) is installed.
2. Clone the repository:

```bash
git clone https://github.com/your_username/UserDataCollector.git
cd UserDataCollector
```
3. run the app:
4. ```bash
   dotnet run
   ```
5. Follow the prompts:
Enter the folder path to save the file:
[A:\X\Y\Z\]

Enter the file format (json/csv):
[csv]

