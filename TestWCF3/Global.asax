void Application_Error(object sender, EventArgs e) 
{ 
    // Code that runs when an unhandled error occurs
    // Give the user some information, but
    // stay on the default page
    Exception exc = Server.GetLastError();
    Response.Write("<h2>Global Page Error</h2>\n");
    Response.Write(
        "<p>" + exc.Message + "</p>\n");
    Response.Write("Return to the <a href='Default.aspx'>" +
        "Default Page</a>\n");

    // Log the exception and notify system operators
    ExceptionUtility.LogException(exc, "DefaultPage");
    ExceptionUtility.NotifySystemOps(exc);

    // Clear the error from the server
    Server.ClearError();
}