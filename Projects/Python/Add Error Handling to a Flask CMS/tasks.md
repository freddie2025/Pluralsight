# Module 1 - Access Log

## 1.1 - Disable Werkzeug Logging
[tag]: # "@pytest.mark.test_disable_werkzeug_logging_module1"
[code]: # "from logging import getLogger; request_log = getLogger('werkzeug'); request_log.disabled = True"

### Module Overview
In this module we'll create a function to configure logging. The function creates and configures a rotating file handler. This function will then be used to configure an access log. Then using an `after_request` decorator we'll write all requests to this access log.

### First Task
By default, the Werzeug library logs each request to the console when debug mode is on. Let's disable this default log. Open the `cms/handlers.py` file, below the existing imports, import the `getLogger` method from `logging`.

Below the imports, call the `getLogger()` function and pass in the log we need, `'werkeug'`. Assign the result to a variable named `request_log`.
Then set the disabled property of `request_log` to `True`.

_Note: Unless otherwise noted, the rest of the tasks in this module happen in the file `cms/handlers.py`._

## 1.2 - Configure Logging
[tag]: # "@pytest.mark.test_configure_logging_module1"
[code]: # "def configure_logging(name, level): log = getLogger(name); log.setLevel(level)"

Below all other code, create a new function called `configure_logging`. The function should have two parameters, `name` and `level`. In the function body create a variable called log and assign it a call to the `getLogger()` function. Pass in `name`. 

On a new line call the `setLevel()` method on `log`. Pass in `level`.

## 1.3 - Rotate File Handler
[tag]: # "@pytest.mark.test_rotating_file_handler_module1"
[code]: # "from logging.handlers import RotatingFileHandler; handler = RotatingFileHandler('logs/{}.log'.format(name), maxBytes=1024*1024, backupCount=10)"
Back at the top, by the other imports, import `RotatingFileHandler` from `logging.handlers`.

Return back to the `configure_logging` function and add a new variable called `handler`. Assign this variable a new `RotatingFileHandler`. Configure the instance with the file path `'logs/{}.log'.format(name)`. Also, set the max bytes to `1024*1024` and the backup count to `10`.

## 1.4 - Add Log Handler
[tag]: # "@pytest.mark.test_add_handler_module1"
[code]: # "log.addHandler(handler); return log"
Still in the `configure_logging` function add the `handler` to `log`. Finally, return the `log` from the function.

## 1.5 - Timestamp Formatting
[tag]: # "@pytest.mark.test_timestamp_module1"
[code]: # "from time import strftime; timestamp = strftime('[%d/%b/%Y %H:%M:%S]')"
Below the `configure_logging` function, use the `strftime()` method to get the current date and time. Format the date and time as follows: `[20/Nov/2019 14:59:12]`. Save the result in a variable named `timestamp`.

## 1.6 - Access Log
[tag]: # "@pytest.mark.test_access_log_module1"
[code]: # "from logging import INFO, WARN, ERROR; access_log = configure_logging('access', INFO)"
To log certain types of events import the levels `INFO`, `WARN`, and `ERROR` from the correct module.

With these imported, use the `configure_logging` function to create a log called `access.log`. **Hint: pass the correct `name`.** Make sure to log events at the `INFO` level. Save the result of this call in a variable called `access_log`.

## 1.7 - After Request
[tag]: # "@pytest.mark.test_after_request_module1"
[code]: # "@app.after_request; def after_request(response): return response"
Create a new function called `after_request`. It should have one parameter called `response`. In the body return `response`. Decorate the function with the `after_request` decorator. *Hint: Use `@app` as the first part of the decorator.* 

## 1.8 - Access Log Format
[tag]: # "@pytest.mark.test_access_log_format_module1"
[code]: # "access_log.info('%s - - %s "%s %s %s" %s -', request.remote_addr, timestamp, request.method, request.path, request.scheme.upper(), response.status_code)"
We have at this point created an `access_log`. Let's now write all info level events to this log. Call the `info` method on `access_log` and pass in the following information in the order noted:

1. Format: `'%s - - %s "%s %s %s" %s -'`
2. Request remote address
3. timestamp *Hint: Previously declared*
4. Request method
5. Request path
6. Request scheme *Hint: Should be uppercase*
7. Response status code

Example: `127.0.0.1 - - [20/Nov/2019 14:59:12] "GET / HTTP" 200 -`

## 1.9 - Valid Status Codes
[tag]: # "@pytest.mark.test_valid_status_codes_module1"
[code]: # "if int(response.status_code) < 400:"
The access log should only contain valid requests. Above the `info()` in the `after_request()` function add an `if` statement. The condition should check in the response status code is less than 400. **Hint: You will need to convert the status code to an `int()`.**

# Module 2 - Error Log

## 2.1 - Inject Titles
[tag]: # "@pytest.mark.test_inject_titles_module2"
[code]: # "@app.context_processor; def inject_titles(): titles = Content.query.with_entities(Content.slug, Content.title).join(Type).filter(Type.name == 'page'); return dict(titles=titles)"

### Module Overview
In this module we'll handle 404 and 500 errors. When these errors happen the user will be redirected to the appropriate template. The 500 errors will also be logged to an error log.

### First Task
We would like you used the `titles` template variable in both of our custom templates. To do this we can use a context processor. 

First, open the `cms/handlers.py` file and create a function called `inject_titles`. In the body declare a variable called `titles` and assign it a call to `Content.query.with_entities()`. To `with_entities()`, pass the _Content_ `slug` and `title`.

Second, refine our query to only select the content type of _page_. Chain a call to `join()` on `with_entities()` and pass the `Type` class. Chain another call to `filter()` with a conditional that filters by page. **Hint: Where `Type.name` equals `'page'`.** 

Third, below the `titles` variable, return a `dict()` with a keyword argument of `titles` set to `titles`.

Finally, add the `@app.context_processor` decorator to the `inject_titles()` function.

## 2.2 - Not Found Template
[tag]: # "@pytest.mark.test_not_found_template_module2"
[code]: # "Create `templates/error.html`"
In preparation for handling 404 errors we need a template to render. Create a new file called `not_found.html` in the `cms/templates` folder. 

As the first line of the template, extend the `base.html` template. Next, create a template block called `content`.  Lastly, create a link in the template block that points the user back to the home page. **Hint: url_for,  `index` route, and slug is `'home'`.**

## 2.3 -Not Found Handler
[tag]: # "@pytest.mark.test_not_found_handler_module2"
[code]: # "@app.errorhandler(404); def page_not_found(e): return render_template('not_found.html'), 404"
When 404 errors happen let\'s now render the template we just created. Open `cms/handlers.py` and create a new function called `page_not_found`. It should have one parameter called `e`. In the body render the `not_found.html` template. Make sure to add a status code of `404`.

Attach the `errorhandler` decorator to this function with the `404` status code.

## 2.4 - Error Log
[tag]: # "@pytest.mark.test_error_log_module2"
[code]: # "error_log = configure_logging('error', ERROR)"
Still in `cms/handlers.py`, below the existing code, use the `configure_logging` function to create a log called `error.log`. **Hint: pass the correct `name`.** Make sure to log events at the `ERROR` level. Save the result of this call in a variable called `error_log`.

## 2.5 - Error Handler
[tag]: # "@pytest.mark.test_error_handler_module2"
[code]: # "from traceback import format_exc; @app.errorhandler(Exception); def handle_exception(e): tb = format_exc()"
Still in `cms/handlers.py`, below the existing imports, import the `format_exec` method from `traceback`. Then, below the 404 error handler, create a new function called `handle_exception`. It should have one parameter called `e`. In the body call the `format_exc()` function and assign the result to a variable called `tb`.

Attach the `errorhandler` decorator to this function with the `404` status code.

## 2.6 - Error Log Format
[tag]: # "@pytest.mark.test_error_log_format_module2"
[code]: # "error_log.error('%s - - %s "%s %s %s" 500 -\n%s', request.remote_addr, timestamp, request.method, request.path, request.scheme.upper(), tb)"
We have at this point created an `error_log`. Let's now write all error level events to this log. In the `handle_exception` function, call the `error` method on `error_log` and pass in the following information in the order noted:

1. Format: `'%s - - %s "%s %s %s" 500 -\n%s'`
2. Request remote address
3. timestamp *Hint: Previously declared*
4. Request method
5. Request path
6. Request scheme *Hint: Should be uppercase*
7. Traceback *Hint: Previously declared*

Example: 
```
127.0.0.1 - - [20/Nov/2019 14:59:12] "GET / HTTP" 200 -
Traceback (Error) ...
  File ...
TypeError ...
```

## 2.7 - Error Template
[tag]: # "@pytest.mark.test_error_template_module2"
[code]: # "Create `templates/error.html`"
In preparation for handling 500 errors we need a template to render. Create a new file called `error.html` in the `cms/templates` folder. 

As the first line of the template, extend the `base.html` template. Next, create a template block called `content`.  In the template block add an `error` template variable. Lastly, create a link in the template block that points the user back to the home page. **Hint: url_for,  `index` route, and slug is `'home'`.**

## 2.8 - Render Original Error Template
[tag]: # "@pytest.mark.test_render_original_error_template_module2"
[code]: # "original = getattr(e, 'original_exception', None); return render_template('error.html', error=original), 500"

Return back to the `handle_exception` function in `cms/handlers.py`. Add a statement that <i>get</i>'s the `'original_exception'` <i>attr</i>ibute from `e`. Assign this to a variable named `original`. Then render the `error.html` template, pass an `error` keyword argument set to `original`. Make sure to add a status code of `500`.

## 2.9 - Render Simple Error Template
[tag]: # "@pytest.mark.test_render_simple_error_template_module2"
[code]: # "if original is None: return render_template('error.html'), 500"
Just above the existing `return` statement in the `handle_exception` function add an `if` statement that checks if `original` is `None`. If so render the `error.html` template with a `500` status code.

# Module 3 - Unauthorized Log

## 3.1 - Signals
[tag]: # "@pytest.mark.test_namespace_module3"
[code]: # "from blinker import Namespace; _signals = Namespace()"

### Module Overview
In this module we'll create a signal to write unauthorized login attempts to a log file.

### First Task
Open the `cms/admin/auth.py` file and at the top below the other imports, import `Namespace` from `blinker`.

Also at the top create an instance of `Namespace` named `_signals`.

## 3.2 - Unauthorized Signal
[tag]: # "@pytest.mark.test_unauthorized_signal_module3"
[code]: # "unauthorized = _signals.signal('unauthorized')"
Still in `cms/admin/auth.py`, create a new signal by calling `signal()` on `_signals`. Pass the name `'unauthorized'` to the `signal()` method and then assign the result to a variable with the same name.

## 3.3 - Send Unauthorized Signal
[tag]: # "@pytest.mark.test_send_unauthorized_signal_module3"
[code]: # "unauthorized.send(current_app._get_current_object(), user_id=user.id, username=user.username)"
Before sending the signal, import `current_app` from `flask` at the top of `cms/admin/auth.py`.

The signal should be sent when there is an unauthorized login attempt. Find the `else` statement in the `login` route of `cms/admin/auth.py`. Add a call to the `send()` method of the `unauthorized` signal. 

Pass three values to the `send()` method the current app object (`current_app._get_current_object()`), the `user.id` as `user_id`, and `user.username` as `username`.

## 3.4 - Import Unauthorized Signal
[tag]: # "@pytest.mark.test_import_unauthorized_signal_module3"
[code]: # "from cms.admin.auth import unauthorized"

Switch back to the `cms/handlers.py` file and import the new `unauthorized` signal from `auth.py`.

## 3.5 - Unauthorized Log
[tag]: # "@pytest.mark.test_unauthorized_log_module3"
[code]: # "unauthorized_log = configure_logging('unauthorized', WARN)"

Still in `cms/handlers.py`, below the existing code, use the `configure_logging` function to create a log called `unauthorized.log`. **Hint: pass the correct `name`.** Make sure to log events at the `WARN` level. Save the result of this call in a variable called `unauthorized_log`.

## 3.6 - Unauthorized Log Format
[tag]: # "@pytest.mark.test_unauthorized_log_format_module3"
[code]: # "def log_unauthorized(app, user_id, username, **kwargs): unauthorized_log.warning('Unauthorized: %s %s %s', timestamp, user_id, username)"
At the bottom of `cms/handlers.py` create a function called `log_unauthorized` that will eventually connect to the `unauthorized` signal.

The `log_unauthorized` function should accept four parameters named and ordered as follows, `app`, `user_id`, `username` and ``**kwargs`. 

The function body should have a single line that calls the `warning()` method of `unauthorized_log`. The format of each log entry should be: 
`Unauthorized: [20/Nov/2019 14:59:12] 1 psdemo` where `1` is the `user_id` and `psdemo` is the `username`.

## 3.7 - Connect Decorator
[tag]: # "@pytest.mark.test_connect_decorator_module3"
[code]: # "@unauthorized.connect"

Decorate the `log_unauthorized` function with the correct decorator to _connect_ it to the `unauthorized` signal. 
