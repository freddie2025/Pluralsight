# Add Error Handling to a Flask CMS

## Status

**Draft**

## Overview

This project is design to be completed on [Pluralsight](https://pluralsight.com). To find out more see here: [https://www.pluralsight.com/product/projects](https://www.pluralsight.com/product/projects).

## Installation

### Windows
Open a command prompt or powershell and run the following commands, replacing 'project-root' with the path to the root folder of the project.
```
> cd 'project-root'
> python -m venv venv
> venv\Scripts\activate.bat
> pip install -r requirements.txt
```

### macOS
Open a terminal and run the following commands, replacing 'project-root' with the path to the root folder of the project.
```
$ cd 'project-root'
$ python3 -m venv venv
$ source venv/bin/activate
$ pip install -r requirements.txt
```
*Note: If you've installed Python 3 using a method other than Homebrew, you might need to type `python` in the second command instead of `python3`.*

### About pip
Versions pip updates frequently, but versions greater than 10.x.x should work with this project.

## Verify Setup

In order to verify that everything is setup correctly, run the following command from the project root.
```
pytest
```
You should see that all the tests are failing. This is good! We’ll be fixing these tests once we jump into the build step. Every time you want to check your work locally you can type that command, and it will report the status of every task in the project.

## Previewing Your Work
You can preview your work by running `flask run` in the root of your fork. Then visit `http://localhost:5000/admin` in your browser. You will see a working preview after completing the first module.
