# VirtualMind Test Project
Create a web app API using .NET to expose two endpoints:
1) The first one will retrieve the exchange rate from that day for a given currency, where the
currency ISO Code will be the input endpoint. Only the American dollar (USD) and Brazilian
real (BRL) currencies will be taken as valid. If the exchange currency entered is different than
those, an appropriate error message should appear.
The dollar exchange rate will be retrieved from the following external service:
http://www.bancoprovincia.com.ar/Principal/Dolar
The real (currency) exchange rate will be a quarter of the dollar exchange rate.
Take into account that in future versions the real (currency) exchange rate will be retrieved
from another external service which is being developed by a third-party team. In the
foreseeable future, we intend to incorporate to our API another currency, the Canadian
dollar.
2) The second one will make a currency purchase. Given a user ID, an amount to
exchange/purchase in Argentinian pesos, and a currency code, the endpoint will create a
transaction that will be stored in the database, as well as the user information and the
amount purchased, in the selected currency. For example, for an amount of 1000 and a
currency “dollar”, the purchase will be stored in the database with a return/resulting value of
1000/{dollar currency exchange rate} dollars.
The amount entered will be validated. For the dollar, the limit is USD 200. For the real, the
limit is BRL 300. All limits — beyond which the user will not be able to use the app — will be
set in the foreign currency, per user, and per month. If the exchange currency entered is a
different one, an appropriate error message should appear.
For the database, keep the design as simple as possible. Assume that the users exist, and that
no validations will be applied to them. Only validate the monthly purchases (transactions) by
the user. Include the database scripts you may need, such as those for database and tables
creation.
About the endpoints created above, we would like to know what do you think about using the
user ID as the input endpoint. Also, how would you improve the transaction to ensure that the
user who makes the purchase is the correct user?
We will value:
● That the web API is written in NET Core.
● Service-oriented development.
● Best practices for endpoint development (HTTP verbs and status codes, routes, etc.)
● Good handling of exceptions
● Object-oriented development
● Writing unit tests for services
● Errors log

