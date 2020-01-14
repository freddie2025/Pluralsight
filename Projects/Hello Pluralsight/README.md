# Hello Pluralsight



## Short Description

This is a sample project to test out how Projects work at Pluralsight. For this project, you'll be creating a basic website using HTML. If you want to deploy it to a real website, we'll even walkthrough how to do that!



## Full Description

### Project Overview

Learn the basics of Pluralsight Projects with this quick introduction, and become comfortable using Git and GitHub — even if you’ve never used them before.

You'll edit a basic HTML page, as well as fork a GitHub repository and clone it down locally.

Here's what you'll build in this project:

![Screenshot](https://raw.githubusercontent.com/pluralsight-projects/HelloPluralsightProject/master/screenshot.png)

### Applications and Tools You'll Need To Complete This Project

You'll need to have access to the following tools on your local machine to complete this project.

* Git
* GitHub
* Code Editor
* Command Line / Terminal Access

Never used Git and don’t have a code editor? We have a video that walks you through all the steps you’ll need to set this up.

### Prerequisite Knowledge

Completing all of the tasks in this project requires knowledge of basic HTML.  You'll also need to have a working knowledge of git to commit local changes and push them up to a GitHub repository.  We'll walk you through that part, so if you haven't used Git before -- don't worry. We recommend that you should have already completed the following Pluralsight Courses:

* [HTML Fundamentals](https://app.pluralsight.com/library/courses/html-fundamentals/table-of-contents)

And have an understanding of the following topics:

* Know what an HTML tag looks like and how to add one to an existing HTML file.
* Know some of the most common tags, like `title`, `h1`, `ul`, and `li`.

If you know these, you should be all set to jump in and give this project a shot!

### Live Demo

Wondering what this project will look like when you've completed it? [Follow this link](#) to see a live version of it.



## Setting Up The Project

In order to get this working, you'll need to have [Git](https://git-scm.com/) installed on your computer, and have a GitHub account. If this is your first time setting up Git, I'd recommend checking out Pluralsight's video on How to Setup Git for Pluralsight Projects in 5 Minutes to learn what you need to know.

The very first step is to fork this repository to your personal GitHub account and clone it down locally. We'll be editing the `index.html` file in the root directory for this project.

### Associate Project with Pluralsight

After cloning this repository down, copy the ".projects_config" file from the [HelloPluralsightProject](#) and save that to this directory. This will allow your status to be reflected on the website while you're working through the project locally!

[//]: # (install: "npm install")
### Installation

Run the following command from root folder of the `HelloPluralsightProject` to install all dependencies.

```
$ npm install
```

[//]: # (test: "npm test")
[//]: # (test-watch: "npm test-watch")
### Verify Setup

In order to verify that everything is setup correctly, run the following command, which should show you a list of failing tests. This is good! Each of these tests corresponds to something we'll be working on in this project. By the end, all of the tests will pass.

```
$ npm test
```

We recommend also running the following command, which will watch for any changes to your files and then re-run the tests automatically. This makes things easier, since you'll see updates immediately when you save your files! You can run this command once, and then look back at the terminal after you've made changes to the "index.html" file.

```
$ npm test-watch
```



[//]: # (project_id: hello-pluralsight)
[//]: # (test: node_modules/.bin/mocha test/hello-pluralsight_test.js)
## Hello Pluralsight

[//]: # (task_id: @title)
### Add a Page Title

Create a `title` element with your name that's a child of the `head` element.

[//]: # (task_id: @h1)
### Add a Header Element

Add an `h1` that's a child of the `body` that says "Hello, Pluralsight!".

[//]: # (task_id: @ul)
## Create an Unordered List

Create a `ul` element that's a child of the `body` that contains at least 2 `li` elements.

[//]: # (task_id: @li)
## What Do You Want to Learn?

In each of those `li` elements, list out one thing you'd like to learn.





## Next Steps

Once all tests are passing, try pushing your master branch up to the `gh-pages` branch -- this will make your webpage available on the web! Here's a command to do that:

```
$ npm run deploy:github-pages
```

This will make your `index.html` file available at the URL:

`http://<username>.github.io/HelloPluralsightProject/`
