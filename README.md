# Connecting to your database
You need to create your own `connection.txt` file and store it in the `watson_hotel` directory (where the `.sln` file is).

In `connection.txt` you should have your connection string for the **current computer** you're using.

The format of the connection string is: 

`Data Source=; Initial Catalog=aGroup; Integrated Security=True`

The `Data Source` should equal the sql server that is on the **current computer** you're using, and the server needs to have a database called `aGroup`.

___

*connection.txt file location*

![Connection file location](readme-images/connection-file.png)
___

*conneciton.txt text*

![connection.txt example](readme-images/connection-file-text.png)

___

