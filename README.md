# LuxoftChange

This is a console project in C# where we can get the bill & coins to give as a change ina transaction

The executable file change.exe is call using the next parameters: 1) The price 2) A list of bills & coins delivered in the format bill - number of bills, separeted by a space

Example:

Change.exe 1035.45 500-2 200-1 (price=1035.45 Bills= 500-2 200-1)

The application will send to screen the bills & coins to return in the same format, for the example will be:

100-1, 50-1, 10-1, 2-2, 0.5-1, 0.05-1
All this dependes on the DefaultCurrency and the BillsAndCoins for using that is configured in the app.config

In the app.config must be indicated the DefaultCurrency in the as follow:

	<add key="DefaultCurrency" value="MXN"/>

The bills and coins values for this currency must be declared too in the same section:

	<add key="MXN" value="500,200,100,50,20,10,2,1,0.50,0.20,0.10,0.05"/>

This way, the system could validate the right values for bill and coins to receive and give money.

If a new currency is required we need add other key and change the DefaultCurrency. Or if we need a new value for a bill, we only added to the key.

The project include a UnitTest project with only two methods for testing, when we put the right parametes and when we put no parameters.

	
