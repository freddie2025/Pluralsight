# Module 1 - Authentication

## 1.1 - Models: Password Column
[tag]: # (@pytest.mark.test_models_password_column_module1)

Project Overview
-----

In this module we'll alter the SQLAlchemy `User` model to include a `password` column. Using `Flask-Migrate`, we will add the new column to the database and populate the required fields. We will create a login HTML form and validate the form data in a new login route. The currently logged in user will be stored in a Flask `session`. The session will be cleared when the user logs out.

First Task
-----

Open the file called `models.py` in the `cms/admin` folder. Find the `User` model, add a column of type `string` with a size of `100`. Make sure `nullable` is `False`. Name this column `password`.

## 1.2 - Models: Check Password
[tag]: # (@pytest.mark.test_models_check_password_module1)

Eventually we are going to need to verify the username and password of a user. There are a few functions that are part of `werkzeug.security` that can help us out. Import `check_password_hash` from `werkzeug.security` below the other imports.

The best place for a password check is in the `User` model itself. Add a function called `check_password` to the `User` model below the `password` column. Since `check_password` is part of a class pass two parameters, `self` and `value`.

In the body of `check_password` return a call to the `check_password_hash` function. Pass in the new class variable `password` (**Hint: self.**), and the `value`.

## 1.3 - Database Migration
[tag]: # (@pytest.mark.test_database_migration_module1)

There is currently no database for the application. Let's create one and migrate the new scheme that includes our new `password` column. Open a terminal, command propmt, or powershell and `cd` to the root folder of the project.

The `Flask-Migrate` extension should be installed. This exenstion provides several `flask db` commands.

- First, to initialize and configure our schema run the `flask db init` command.
- Second, to create a migration run the `flask db migrate` command.
- Third, to create the database and run the migration use `flask db upgrade`.
- Finally, run the custom command `flask add-content` to add content to the database.

## 1.4 - Template: Login Form
[tag]: # (@pytest.mark.test_template_login_form_module1)

Open the `login.html` file found in the `templates` folder of the `admin` blueprint. This template contains a `<form>` element with several empty `<div>`s. Each one having a class of `control`. Let's add a form control to each one. 

Find the label with the text, _Username_. In the _control_ `<div>` below, add a text field that has a name of `username` and a class of `input`.
Find the label with the text, _Password_. In the _control_ `<div>` below, add a text field that has a name of `password` and a class of `input`.

In the last empty `<div>` towards the bottom, add a submit button that has a `value` of `Submit`. Give it two classes, `button` and `is-link`.

## 1.5 - Auth: Imports
[tag]: # (@pytest.mark.test_auth_imports_module1)

The `auth.py` file in `cms/admin` will contain all authentication related code. Open it and at the top add the following imports:

- import `wraps` from `functools`
- import `session` and `g` from `flask`

These will be necessary for when we create our custom authentication decorator. _Note: Unless otherwise noted, the rest of the tasks in this module happen in the file `auth.py`._

## 1.6 - Auth: Protected Decorator
[tag]: # (@pytest.mark.test_auth_protected_decorator_module1)

To require users to login when accessing any of the admin dashboard routes i.e. `/admin` lets create a custom route decorator.
Below the imports create a function called `protected`. The first parameter to the function should be called `route_function`.

In the body of this new function create another function called `wrapped_route_function`. To allow this function to accept an arbitry number of arguments use `**kwargs` as the first parameter. 

For now the only statement in the body of the `wrapped_route_function` should return a call to the `route_function`. Pass `**kwargs` as the first argument.

## 1.7 - Auth: Redirect User
[tag]: # (@pytest.mark.test_auth_redirect_user_module1)

For the decorator to correctly wrap the route function, add the `@wraps` decorator to the `wrapped_route_function` function. Make sure to pass `route_function` to the decorator.

Next, in the body of `wrapped_route_function` above the `return` statement, add an `if` statement that tests whether `g.user` is `None`.
In the `if` return a redirect that points to `admin.login`. *Hint: you will need a url_for function.*

At the bottom of the `protected` function, make sure you are not in the `wrapped_route_function` function, `return` `wrapped_route_function`.

## 1.8 - Auth: Load User
[tag]: # (@pytest.mark.test_auth_load_user_module1)

Below the new decorator create a new function called `load_user`. As the first line _get_ the `user_id` from the _session_ and store it in a variable called `user_id`. Decorate the function with the `before_app_request` decorator. *Hint: auth.py is part of the `admin_bp` blueprint*.

As the last line of `load_user` use a ternary `if` to assign `g.user` the result of `User.query.get(user_id)` if `user_id is not None` else assign it `None`.

## 1.9 - Auth: Login Route
[tag]: # (@pytest.mark.test_auth_login_route_module1)

Let's create a new route function called `login`. Add a route decorator with a URL pattern of `/login`. Make sure this new route allows _GET_ and _POST_ requests. _Note: this route is part of our `admin_bp` blueprint._

In the body render the 'admin/login.html' template, make sure to `return` the results.

## 1.10 - Auth: Post Request
[tag]: # (@pytest.mark.test_auth_post_request_module1)

Above the redirect statements, add an `if` that checks if the request method is _POST_. If so, assign create two variable, `username` and `password` and assign each the appropriate form data. We are going to validate some of our form data on the server side, so, set a new variable called `error` to `None`.

## 1.11 - Auth: Get User & Check Password
[tag]: # (@pytest.mark.test_auth_get_user_module1)

Let's check if a user exists with the `username` that is provided. _query_ the `User` model and _filter_by()_ the `username`. Make sure to take just the _first()_ row and assign it to a `user` variable.

Check the password of the `user` by calling `check_password` on the `user` object. Assign the result to a variable called `check`.

## 1.12 - Auth: Validate Form Data
[tag]: # (@pytest.mark.test_auth_validate_form_data_module1)

We want to make sure the `user` exists, so in an `if` statement check if `user` is `None`.  Also verify the password is correct. _Note:  Use an `elif`, `check` should not be `None`._
Both the `if` and `elif` should set `error` to an appropriate message.

## 1.13 - Auth: Store User in Session
[tag]: # (@pytest.mark.test_auth_store_user_in_session_module1)

If there is  no `error` _clear()_ the the _session_. Also, store the value of `user.id` in the _session_ key `user_id`. Then finally _redirect_ to `admin.content` with `type` set to `'page'`.

Outside the error `if` statement _flash()_ the `error`.

## 1.14 - Auth: Logout Route
[tag]: # (@pytest.mark.test_auth_logout_route_module1)

Let's create a new route function called `logout`. Add a route decorator with a URL pattern of `/logout`. _Note: this route is part of our `admin_bp` blueprint._ 

In the body _clear()_ the _session_, and return a redirect to `admin.login`.

Switch to `layout.html` and find the `<div>` that has a class of `nav-item`. To this `<div>` add an anchor element that says `Logout` and has two classes, `button` and `is-light`. In the `href` attribute use the `url_for()` function to point to the `admin.logout` route.

## 1.15 - Admin: Protect Routes
[tag]: # (@pytest.mark.test_admin_protect_routes_module1)

A few last things, open the `cms/admin/__init__.py` file. Below the `admin_bp` variable, import `auth` from `cms.admin`. This placement is important.

Finally, protect all the routes in the admin blueprint with the `@auth.protected` custom decorator. This should include these routes: `content`, `create`, `edit`, `users` and `settings`.
