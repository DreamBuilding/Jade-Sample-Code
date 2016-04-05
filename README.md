# Jade-Sample-Code
- Sample Web Api 2.2 application to provide the following function.
- Post http://[domain]/api/ [dollar amount]/convert-to-word
- Return English word.

This Api will convert numberic decimal values to their textual representation. 

# Validation requirements:
 - Minimum input dollar amount is 1 dollar.  
 - Maximum input dollar amount is 1 million dollars. 
 - Input dollar amount >= 0, return bad request.
 - Input dollar amount > 1000000 return bad request. 
 - Input dollar amount is null, return 404. 
 - Input dollar amount is invalid such as “abc”, return bad request.
 
 Updated to provide UI front end code test.
 Note: The requirements did not display the text from the api call but I have added this to valid the call is functioning as expected.


