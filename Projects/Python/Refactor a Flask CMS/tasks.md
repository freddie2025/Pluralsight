# Module 1 - Admin Blueprint

## Admin Blueprint - Folder Structure
@pytest.mark.test_admin_blueprint_folder_structure_module1

Over the course of this module we will create a Flask Blueprint. You can think of a Flask Blueprint as an application component. It isn't an application itself, but can be registered in an application. This allows for a more modular architecture. The Blueprint has to have somewhere to live, so we will need to add a new folder and two new files.

To start, create a new folder called `admin` in the `cms` folder. Also, add the special file `__init__.py` to this new `admin` folder. This file will be empty for now. _Note: `admin` is now a module._

The Blueprint will allow us to restructure the main `cms/__init__.py` file so that it isn't so cluttered. First, we will prepare a new home for our SQLAlchemy model classes. Create an empty file called `models.py` in the `admin` folder.

## Admin Blueprint - Models File Imports
@pytest.mark.test_admin_blueprint_models_file_imports_module1

In order to move our model classes to the newly created `admin/models.py` file we need to have a couple imports.

* Import `SQLAlchemy` from `flask_sqlalchemy`
* Import `datetime` from `datetime`

All of the model classes require a `SQLAlchemy` instance. Create a new instance by calling the `SQLAlchemy` constructor with no arguments and assign it to a variable called `db`. _Note: Make sure the instance name is `db`. It is what is currently used in the model classes._

## Admin Blueprint - Move Model Classes
@pytest.mark.test_admin_blueprint_move_model_classes_module1

Now that `models.py` has the correct imports and has a SQLAlchemy instance, we can move the model classes over.

Move the `Type`,  `Content`, `Setting`, and `User` classes from `cms/__init__.py` to `cms/admin/models.py`. _Note: Make sure to remove the model classes from `cms/__init__.py`._

The routes that are in `cms/__init__.py` still need access to these models, let's add them back with an import.

- Import `Type`,  `Content`, `Setting`, and `User` from `cms.admin.models`

Remove the `db` `SQLAlchemy` instance from `cms/__init__.py`.

_Note: If you try to preview the application at this point you will receive an error about `SQLAlchemy` not being connected to the `app`. We will fix this shortly._

## CMS Module - Import db
@pytest.mark.test_cms_module_import_db_module1

Since we have removed the model classes and `SQLAlchemy` instance from `cms/__init__.py` we need a way to reconnect the `db` to the `app`.

First import the `db` instance from the `models` file. **Hint: you already have an import statement.**

In `cms/__init__.py`, below the `SQLAlchemy` configuration lines, call the `init_app` method on `db`. Pass in the `app`.

## CMS Module - Remove Imports
@pytest.mark.test_cms_module_remove_imports_module1

All of the models are located in the new `admin/models.py`. So, we can clean up some unnecessary code in `cms/__init__.py`.

Remove the following imports: 
- `flask_sqlalchemy` 
- `datetime` imports.

## Admin Blueprint - Create a Blueprint
@pytest.mark.test_admin_blueprint_create_blueprint_module1

It is now time to create the actual Blueprint. Open `cms/admin/__init__.py` and at the top import the `Blueprint` class from `flask`.

Next, create a new variable called `admin_bp` and assign it a call to the  `Blueprint` class constructor. Pass in the correct arguments to create a blueprint that has a name of `admin`, the correct import name, and a url prefix of `/admin`.

## Admin Blueprint - Imports
@pytest.mark.test_admin_blueprint_imports_module1

In preparation for moving the existing routes to the blueprints we will need to import a few things from `flask`.

We are already importing the `Blueprint` class so let's use that same import to import the `render_template` and `abort` methods.

We also need the `SQLAlchemy` models. Import them from `models.py`.

## Admin Blueprint - Move Routes
@pytest.mark.test_admin_blueprint_move_routes_module1

The core of any Blueprint are its routes. Our app currently has 4 routes that would be better suited in the `admin` Blueprint. 
Move the `content`, `create`, `users` and `settings` routes to `cms/admin/__init__.py`.
_Note: Make sure that you also move the `requested_type` method._
_Note: remove the routes from `cms/__init__.py`._

Once the routes are in `cms/admin/__init__.py` make them a part of the Blueprint by changing the first part each decorator.   _Note: if you get the *NameError: name 'app' is not defined* you have possibly missed changing a decorator._

Remove the `/admin` URL prefix from each route pattern.

_Note: If you preview the application, the `admin` routes will not exists. You will get a 404. We will fix this shortly._

## CMS Module - Register Blueprint
@pytest.mark.test_cms_module_register_blueprint_module1

Now that the Blueprint is complete it can be registered in the application.

Open `cms/__init__.py` and import the `admin_bp` instance from the admin `Blueprint` module.

Once it has been imported register `admin_bp` in the `app`.

## Admin Blueprint - Templates Folder
@pytest.mark.test_admin_blueprint_template_folder_module1

We can also move the templates that pertain to the admin blueprint routes to the `cms/admin` folder, so everything is self-contained.

First, create a `templates` folder in the `cms/admin` folder. Second, move the `admin` folder from the `cms/templates` folder to the newly created `cms/admin/templates` folder.

The new structure:

```
cms
├── __init__.py
├── admin
│   ├── __init__.py
│   ├── models.py
│   └── templates
│       └── admin
│           ├── content.html
│           ├── content_form.html
│           ├── layout.html
│           ├── settings.html
│           └── users.html
└── templates
    └── index.html
```

Open `cms/admin/__init__.py` and add a new keyword argument to the `Blueprint` instance that sets the template folder to `templates`.

Finally, open `cms/admin/templates/admin/layout.html` and add `admin.` to the beginning of the first argument of each `url_for()` call. There is also a `url_for()` call that needs to be changed in `cms/admin/templates/admin/content.html`

# Module 2 - Create Route

## Template - Add Form Controls
@pytest.mark.test_template_add_from_controls_module2

In this module we will make it possible to create content in our CMS. We'll start by adding form controls to our HTML template.

Open the `content_form.html` file that can now be found in the `templates` folder of the `admin` blueprint. This template contains a `<form>` element with several empty `<div>`s. Each one having a class of `control`. Let's add a form control to each one.

Find the label with the text, _Title_. In the _control_ `<div>` below, add a text field that has a name of `title` and a class of `input`.

Find the label with the text, _Slug_. In the _control_ `<div>` below, add a text field that has a name of `slug` and a class of `input`.

Find the label with the text, _Content_. In the _control_ `<div>` below, add a multi-line text field that has a name of `body` and a class of `textarea`.

## Template - Type Dropdown
@pytest.mark.test_template_type_dropdown_module2

Still in `content_form.html` find the label with the content, _Type_. In the _control_ `<div>` below, add a dropdown that has a name of `type_id`. 

The `types` template variable contains the `id` and `name` of each type in the database. We'll use this to create the `<option>`s of the dropdown. 

In the dropdown use a `for` loop to cycle through the `types` variable, call the current type, `type`. 

In the body of the loop add an `<option>` that has a `value` set to the `type.id`, and set the content to the `type.name`.

In the `<option>` opening tag add this code: ```{{ 'selected' if (type.name == type_name) }}``` . This ensures the dropdown is populated correctly.

## Template - Buttons
@pytest.mark.test_template_buttons_module2

Still in the `content_form.html` file, find the `<div>` towards the bottom that has a class of `is-grouped`. There are two nested _control_ `<div>`s. In the first, add a submit button that has a `value` of `Submit` and give it two classes, `button` and  `is-link`.

In the second, add an anchor element that says `Cancel` and has two classes, `button` and `is-text`. In the `href` attribute use the `url_for()` function to point to the `admin.content` route. Make sure to also pass the `type` keyword argument set to `type_name`.

## Create Route - Methods
@pytest.mark.test_create_route_methods_module2

The current `create` route that is found in `admin/__init__.py` is only setup for _GET_ requests. Allow _POST_ requests too by providing the correct keyword argument and values to the `create` route decorator.

Now that we allow _POST_ requests, let's adapt the `create` route to gather the _POST_ data.

First import `request` from `flask`. Then, in the `requested_type` `if` statement add a nested `if` that checks if the request method is _POST_. If so assign a new variable called `title` the data from the `title` form element.

## Create Route - Form Data
@pytest.mark.test_create_route_form_data_module2

In the `if` that you just created, create three more variables `slug`, `type_id`, and `body`. Assign each variable the data from each respective form element.

We are going to validate some of this form data on the server side, so set a new variable called `error` to `None`.

## Create Route - Validate Data
@pytest.mark.test_create_route_validate_data_module2

The two values we are going to validate from the form are the `title` and the `type_id`. 

In the _POST_ `if`, nest an `if elif` statement to first check if `title` is empty, then second check if `type_id` is empty. If either is empty set `error` to an appropriate message.

## Create Route - Insert Data
@pytest.mark.test_create_route_insert_data_module2

Once the validation is completed we can check the value of `error` with an `if `statement. If `error` is still set to `None`, we can add the data we collected from the form to the database.  SQLAlchemy makes this really easy. We'll use the `Content` model class.

In the `if` statement, create a variable called `content` and assign it a new `Content` instance. To the constructor pass in four keyword arguments `title`, `slug`, `type_id`, and `body`. Set each to the form variables with the same names.

Now that we have the prepared data stored in `content` we can add it to the database. First, import `db` from `cms.admin.models`. Then, call `db.session.add()` and pass in `content`. This only adds the `content` object to the _session_, let's commit to the database with a call to `db.session.commit()`.

## Create Route - Redirect
@pytest.mark.test_create_route_redirect_module2

Once the form data is committed to the database we can redirect the user back to the correct page in the admin dashboard.

First, import the `redirect`, `url_for`, and `flash` methods from `flask`. Then on the line below the database commit, `return` a `redirect` that points the user back to the `admin.content` route. Pass in the current content `type` as a keyword argument to `url_for`. 

We have to handle the case were there is an error. So, outside of the _error_ `if` we have been working in, but still inside the _POST_ `if`, `flash()` the `error`.

# Module 3 - Edit Route

## Edit Route - Function
@pytest.mark.test_edit_route_module2

At this point content can be created, however, it cannot be edited. Let's create a new route function in `admin/__init__.py` called `edit`. 

Add a route decorator with a URL pattern of `/edit/<id>`. Make sure this new route allows _GET_ and _POST_ request. _Note: this route is part of our `admin_bp` blueprint._

Our URL pattern has a placeholder for `id`. Make sure the `edit` function accepts this as well.

When the user clicks the edit button it would be best if the form was populated with the correct content. 

So, in the body, add a single line to pull the `content` stored in the database for the given `id`.  To do this `query` the `Content` model and use `get_or_404()` to get content for the specified `id`. Assign the result to a variable called `content`.

## Edit Route - Queries
@pytest.mark.test_edit_route_queries_module2

As in the above step, we would like to populate the `content_form.html` with the type of content we are editing. So, _get()_ the `content.type_id` by _querying_
the `Type` module, assign the result to a variable called `type`. 

Get _all()_ of the types by _querying_
the `Type` module, assign the result to a variable called `types`.

## Edit Route - Render Template
@pytest.mark.test_edit_route_render_template_module2

Below the queries, `return` a call to `render_template()`. There are several values that we want to populate in the form, so, there is a lot to pass to the `render_template()` function. These values are listed below: 
- `admin/content_form.html` _(template)_
- `types` as `types`
- `'Edit'` as `title`
- `content.title` as `item_title` 
- `content.slug` as `slug`
- `type.name` as `type_name` 
- `content.type_id` as `type_id`
- `content.body` as `body`

The name on the right is the template variable name.

## Template - Populate Form Controls
@pytest.mark.test_template_populate_form_controls_module2

We are now ready to populate the form with data from the database.

Open the `content_form.html` file and find the `title` text field and add a `value` attribute to the start tag, set it to the `item_title` template variable filtered with `default('')`.

Find the `slug` text field and add a `value` attribute to the start tag, set it to the `slug` template variable filtered with `default('')`.

Finally, find the `body` multi-line text field as the element content add the template variable `body` filtered with `default('')`.

To hook everything together, open `content.html` and find the `Edit` anchor element. For the `href`, add a `url_for` function that points to the `admin.edit` route. Pass in a keyword argument of `id` set to `item.id`.

## Edit Route - Form Data
@pytest.mark.test_edit_route_form_data_module2

Return back to the `cms/admin/__init__.py` file. Below the query statements, add an `if` that checks if the request method is _POST_. If so, assign all properties of the `content` object the correct form data. *Hint: content.title = request.form\['title'\]*. 

There are no fields in our form for the `content.updated_at` property. Below the existing imports, import `datetime` from `datetime`. Then assign `content.updated_at` the current date with a call to `datetime.utcnow()`. 

You should end up with 5 assignment statements that assign new values to the properties of the `content` object.

As with the create route we are going to validate some of our form data on the server side, so, set a new variable called `error` to `None`.

## Edit Route - Validate Data
@pytest.mark.test_edit_route_validate_data_module2

There is only one form value to validate because we are pulling in the `type` already.

In the _POST_ `if`, nest an `if` statement to check if `title` is empty. If empty set `error` to an appropriate message.

## Edit Route - Update Data
@pytest.mark.test_edit_route_update_data_module2

Once the validation is completed we can check the value of `error` with an `if `statement. If `error` is still `None`, we can update the data we have collected from the form in the database. 

The data stored in `content` has already been updated. All we have to do is commit it with `db.session.commit()`.

Once the form data is committed to the database we can redirect the user back to the correct page in the admin dashboard. On the line below the database commit, `return` a `redirect` that points the user back to the `admin.content` route. Pass in the current `type.name` as a keyword argument of `type` to `url_for()`.

We have to handle the case where there is an error. So, outside of the _error_ `if` we have been working in, but still inside the _POST_ `if`, `flash()` the `error`. 
