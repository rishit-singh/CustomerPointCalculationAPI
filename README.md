# CustomerPointCalculationAPI
A simple API that provides the points earned by a specific employee.

## Endpoints

The API has 2 endpoints, the default `/` which returns the API's status on the server, and `/GetPoints/{userID}` which returns the calculated points of the user with the provided `userID`.

Both can endpoints can be tested with any HTTP request tool if the server is running. In the following example, the API server is running on `http://localhost:5026`, and we can test the same with curl (with insecure flag as the localhost doesnt have TLS enabled).

```sh
$ curl --insecure -X GET http://localhost:5026;
> {"Type":0,"TypeString":"Success","Payload":{"TotalRequests":2,"ServerDateTime":"2022-06-04T00:18:44.4836235+05:30","DatabaseServerStatus":1}}
```

```sh
$ curl --insecure -X GET http://localhost:5026/GetPoints/efc0509e0aef7f2e5a2ad5dd46f3c2c6e4c237b58045df328dac9e937170408f
> {"Type":0,"TypeString":"Success","Payload":{"TotalPoints":44864,"UserID":"efc0509e0aef7f2e5a2ad5dd46f3c2c6e4c237b58045df328dac9e937170408f","MonthlyPoints":{"January":0,"March":0,"November":13736,"May":0,"October":0,"April":0,"December":0,"September":0,"June":15884,"Febuary":0,"July":15244,"August":0}}}

```

Making such requests to the API from the client side code is discussed further in this documentation. 

## Usage

## Client Side

The API can be called by making a GET request to a specific endpoint from any language of choice. The following example shows how to achieve that with C#. 

```csharp
public class Program
{
        public static void Main()
        {
            HttpClient client = new HttpClient();

            string response = null;

            string APIUrl = "http://localhost:5026";
            
            try
            {
                response = client.GetStringAsync(APIUrl).Result; // GET request to the default end point.

                Console.WriteLine(response);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }
 }

```

## Tests

There are unit tests written for both client and server side utilities, and all of them follow a similar patten of execution.

### Server Side

A test can be executing by creating an instance of it's corresponding class and calling the Run() method.

```csharp
(new PointCalculationTest()).Run();
(new TransactionCreationTest()).Run();
(new UserCreationTest()).Run();
```

### Client Side

Client side tests have been isolated from the server side ones for testing the API externally. Those tests can be run by fetching the source files from the repository and running them locally.

Clone the repository and change the directory to it.

```
$ git clone https://github.com/rishit-singh/CustomerPointCalculationClientTests
$ cd CustomerPointCalculationClientTests
```

Execute the tests by creating and instance of them and calling the Run() method from Main().

```csharp
(new APIStatusTest()).Run();
(new UserPointsTest()).Run();
```

## Building

### From source

The API has to be configured with a simple JSON configuration file `DatabaseConfig.json` which must store the data required to access the PostGRESql server.

```json
{	
	"Host": "your_server_hostname", 
	"Username": "you_postgres_username",
	"Password": "your_postgres_password",
	"Database": "database_name"
}
```

Once this file is placed in the root directory with the correct configuration, the test data in the PostGRES dump must be restored in the current installed PostGRES instance on the server by running:

```bash
$ psql template1 -c 'create database cust_db with owner your_postgres_username';
$ psql cust_db < cust_db.sql
```

Once the database is setup, the server can be run by simply executing:

```sh
dotnet run
```

