UI (REACT):

Entry Point:

1. Auth Form:
    ✅ Summary: 
          --> Collects username/password

 	  --> Validates them

          --> Sends to the backend (/api/Auth/login)

          --> Handles JWT token in cookie

          --> Redirects user based on their role

          --> Displays errors on failure.

   Note : useLocation gives access to the current location object, 
          which contains data like the pathname and state (data passed via navigation).

   Note : Inside the custom hook, we call useNavigate() to get the navigate function, 
          which we can use to programmatically redirect the user to another page.
          This is similar to calling history.push() in older versions of React Router.

2. App.js:

      Note: BrowserRouter (aliased as Router): the main router component that 
            uses the HTML5 history API to keep the UI in sync with the URL.
            Routes: a container for all the <Route> elements.
            Route: defines a mapping from a URL path to a React component.

      Summary: This code sets up client-side routing for your React app using 
               react-router-dom.
               It maps URL paths to React components to render specific pages.
               / loads login form, /dashboard loads user dashboard, /admin-dashboard 
               loads admin dashboard.
               Router listens for URL changes and renders the matching route component 
               dynamically without a page reload.

REST API (Using C# and .Net Core 8 version) :

Data Flow:  Login Request --> Controller --> Service --> Repository/Interface --> Helper --> MiddleWare --> Authorization Attribute

1. Controller : (AuthController.cs)

-> ✅ Summary:

📥 Accepts login data from React frontend.

🔐 Validates user with a service.

🔑 Generates a JWT with role info.

🍪 Stores JWT in a secure cookie.

🚫 Rejects unauthorized logins.

✅ Follows SRP (controller only routes and delegates logic).

2. Service : (UserService.cs)

--> ✅ Summary
Responsibility	Description
🔐 ValidateUser	Authenticates user credentials.
💾 SaveUser	Saves new user to JSON file if not already present.
📂 LoadUsers	Reads and parses user data from file.
⚙️ Uses Dependency Injection	Implements IUserService to follow SOLID's Dependency Inversion Principle(High level --> AuthController, Low Level --> USerService , Abstraction : IuserService.
🔐 Password verification	Uses helper for password checking – separation of concerns (SRP).
📁 File I/O	Persists data without using a database (good for prototypes/testing).

3. Helper: (JwtHelper.cs)

--> ✅ Summary
Part	Description
Claim:	Adds identity info (name, role) inside token
SymmetricSecurityKey:	Creates cryptographic key from secret string
SigningCredentials:	Specifies the algorithm (HMAC-SHA256) to sign token
JwtSecurityToken:       Creates the actual JWT
WriteToken():	Serializes the token for HTTP response

-----------> (PasswordHelper.cs)
✅ Summary
Method	Purpose
VerifyPassword():	Compares user input with stored hash
ComputeHash():	Hashes a plain-text password using SHA256


4. Middleware : (JWTRefreshMiddleware.cs)

✅ Summary of Behavior
Step	Description
1	Checks if user is authenticated.
2	Extracts username and role from token claims.
3	Generates a fresh JWT token.
4	Sets the new token in the response cookie.
5	Passes control to the next middleware.


5. MainProgram: (Program.cs)

🔄 Summary of Flow
React sends login data → API issues JWT + sets it in a secure cookie.

For each request:

JWT is read from the cookie.

Token is validated.

If valid, a refreshed token is issued.

Roles and authorization are enforced.

React receives responses, with CORS and credentials allowed.
