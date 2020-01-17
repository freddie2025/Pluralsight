package com.pluralsight.blog;

import com.pluralsight.blog.model.Post;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import com.pluralsight.blog.data.PostRepository;
import org.jsoup.select.Elements;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mockito;
import org.powermock.core.classloader.annotations.PrepareForTest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.autoconfigure.web.servlet.MockMvcPrint;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.mock.web.MockHttpServletResponse;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.ui.ModelMap;

import java.lang.reflect.Constructor;
import java.lang.reflect.Field;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.List;

import static org.junit.Assert.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc(print = MockMvcPrint.NONE)
@PrepareForTest(BlogController.class)
public class Module2_Tests {

    @Autowired
    private MockMvc mvc;

    @Autowired
    private BlogController blogController;


    @Autowired
    private PostRepository postRepository;

    private PostRepository spyRepository;

    private Class c = null;
    private Method method = null;

    private List<Post> ALL_POSTS;

    @Before
    public void setup() {


        // Task 1
        String packageName = getClass().getPackage().getName();
        String className = "BlogController";

        try {
            c = Class.forName(packageName + "." + className);
        } catch (ClassNotFoundException e) {
            ////e.printStackTrace();
        }

        Constructor<BlogController> constructor = null;
        try {
            constructor = BlogController.class.getDeclaredConstructor(PostRepository.class);
        } catch (NoSuchMethodException e) {
            //e.printStackTrace();
        }

        spyRepository = Mockito.spy(postRepository);
        try {
            blogController = constructor.newInstance(spyRepository); //new BlogController(spyRepository);
        } catch (Exception e) {
            //e.printStackTrace();
        }

        // Task 2(a) - setup - Check if method listPosts() exists
        try {
            method = c.getMethod("listPosts");
        } catch (Exception e) {
            ////e.printStackTrace();
        }

        // Task 4 setup
        try {
            method = c.getMethod("listPosts", ModelMap.class);
        } catch (Exception e) {
            ////e.printStackTrace();
        }

        // TODO Make this work if Post constructor doesn't exist
        ALL_POSTS = new ArrayList<>(Arrays.asList(
                new Post(1l, "Earbuds",
                        "You have got to try these in your ears. So tiny and can even block the sounds of screaming toddlers if you so desire.",
                        "You have got to try these in your ears. So tiny and can even block the sounds of screaming toddlers if you so desire.",
                        "Sarah Holderness", new Date()),
                new Post(2l, "Smart Speakers",
                        "Smart speakers listen to you all right.  Sometimes they get a little snippy but will still order your favorite takeout.",
                        "Smart speakers listen to you all right.  Sometimes they get a little snippy but will still order your favorite takeout.",
                        "Sarah Holderness", new Date()),
                new Post(3l, "Device Charger",
                        "We all do a little too much scrolling in lieu of human interaction. This charger will keep you isolated.",
                        "We all do a little too much scrolling in lieu of human interaction. This charger will keep you isolated.",
                        "Sarah Holderness", new Date()),
                new Post(4l, "Smart Home Lock",
                        "Want to play tricks on your teenager? This smart home lock will lock them out when they act like they run the house.",
                        "Want to play tricks on your teenager? This smart home lock will lock them out when they act like they run the house.",
                        "Sarah Holderness", new Date()),
                new Post(5l, "Smart Instant Pot",
                        "This Instant Pot can do your shopping for you. When it gets home it will also put your meal together.",
                        "This Instant Pot can do your shopping for you. When it gets home it will also put your meal together.",
                        "Sarah Holderness", new Date()),
                new Post(6l, "Mobile Tripod",
                        "Best gift for that older adult in your life who cannot keep their face in the FaceTime window.",
                        "Best gift for that older adult in your life who cannot keep their face in the FaceTime window.",
                        "Sarah Holderness", new Date()),
                new Post(7l, "Travel Keyboard",
                        "You never know when inspiration for your latest novel will strike. Meet the perfect travel keyboard for your random thoughts.",
                        "You never know when inspiration for your latest novel will strike. Meet the perfect travel keyboard for your random thoughts.",
                        "Sarah Holderness", new Date()),
                new Post(8l, "SD Card Reader",
                        "When a stranger passes us a top secret SD card the adventure begins.  Jason Bourne says, \"Hi\".",
                        "When a stranger passes us a top secret SD card the adventure begins.  Jason Bourne says, \"Hi\".",
                        "Sarah Holderness", new Date())
        ));
    }

    @Test
    public void task_1() {
        Field[] fields = PostRepository.class.getDeclaredFields();

        boolean allPostsExists = false;
        for (Field field : fields) {
            if (field.getName().equals("ALL_POSTS") && field.getType().equals(List.class))
                allPostsExists = true;

        }
        assertTrue("Task 1: A field called `ALL_POSTS` of type List does not exist in PostRepository", allPostsExists);
    }

    @Test
    public void task_2() {
        Method method = null;
        List<Post> postList = null;
        try {
            method = PostRepository.class.getMethod("getAllPosts");
            postList = (List<Post>)method.invoke(spyRepository);
        } catch (Exception e) {
            //e.printStackTrace();
        }
        String message = "Task 2: The method `getAllPosts()` does not exist in the PostRepository class.";
        assertNotNull(message, method);

        message = "Task 2: The method `getAllPosts()` returns `null`.";
        assertNotNull(message, postList);

        message = "Task 2: The method `getAllPosts()` does not return the correct `List`.";
        assertEquals(message, spyRepository.getAllPosts(), postList);
    }

    @Test
    public void task_3() {
        // Task 1 - Add field PostRepository postRepository; to BlogController
        Field[] fields = BlogController.class.getDeclaredFields();

        boolean postRepositoryExists = false;
        boolean annotationExists = false;
        for (Field field : fields) {
            if (field.getName().equals("postRepository") && field.getType().equals(PostRepository.class)) {
                postRepositoryExists = true;
            }
        }

        String message = "Task 3: A field called `postRepository` of type `PostRepository` does not exist in BlogController.";
        assertTrue(message, postRepositoryExists);
    }
    @Test
    public void task_4() {
        // Check for BlogController constructor with PostRepository parameter
        Constructor<BlogController> constructor = null;
        try {
            constructor = BlogController.class.getDeclaredConstructor(PostRepository.class);
        } catch (NoSuchMethodException e) {
            //e.printStackTrace();
        }

        String message = "Task 4: A `BlogController` constructor with a `PostRepository` parameter does not exist.";
        assertNotNull(message, constructor);
    }

    @Test
    public void task_5() {
        // Task 5 - Query PostRepository getAllPosts() in BlogController, and save to List
        // Task 6 - Verify modelMap.put() is called
        ModelMap modelMap = Mockito.mock(ModelMap.class);
        Mockito.when(spyRepository.getAllPosts()).thenReturn(ALL_POSTS);

        //blogController.listPosts(modelMap);
        try {
            method.invoke(blogController, modelMap);
        } catch (Exception e) {
            //e.printStackTrace();
        }

        boolean calledGetAllPosts = false;
        try {
            Mockito.verify(spyRepository).getAllPosts();
            calledGetAllPosts = true;
        } catch (Error e) {
            //e.printStackTrace();
        }

        String message = "Task 5: Did not call PostRepository's `getAllPosts()` in BlogController.";
        assertTrue(message, calledGetAllPosts);

    }

    @Test
    public void task_6() {
        // Task 6 - Verify modelMap.put() is called
        ModelMap modelMap = Mockito.mock(ModelMap.class);
        Mockito.when(spyRepository.getAllPosts()).thenReturn(ALL_POSTS);
        Mockito.when(modelMap.put("posts", ALL_POSTS)).thenReturn(null);

        //blogController.listPosts(modelMap);
        try {
            method.invoke(blogController, modelMap);
        } catch (Exception e) {
            //e.printStackTrace();
        }

        boolean calledPut = false;
        try {
            Mockito.verify(modelMap).put("posts", ALL_POSTS);
            calledPut = true;
        } catch (Error e) {
            //e.printStackTrace();
        }

        String message = "Task 6: Did not call ModelMap's `put()` method with the key `\"posts\"` and the `List<Post>`.";
        assertTrue(message, calledPut);

    }

    @Test
    public void task_7() {
        // Task 7 - Display list with foreach loop in template
        // TODO - How to more robustly test?  Right now just confirming 3 divs show up.
        Document doc = null;
        String errorInfo = "";
        try {
            MvcResult result = this.mvc.perform(get("/")).andReturn();
            MockHttpServletResponse response = result.getResponse();
            String content = response.getContentAsString();

            doc = Jsoup.parse(content);
        } catch (Exception e) {
            errorInfo = e.getLocalizedMessage();
            //e.printStackTrace();
        }
        String message = "Task 7: The template has errors - " + errorInfo + ".";
        assertNotNull(message, doc);

        Elements divElements = doc.getElementsByTag("div");

        message = "Task 7: A `<div>` tag with `th:each` does not exist in the home.html template.";
        assertTrue(message,
                divElements.size() > 0);
    }

    @Test
    public void task_8() {
        // Task 7 - Display list with foreach loop in template
        // TODO - How to more robustly test?  Right now just confirming 3 divs show up.
        Document doc = null;
        String errorInfo = "";
        try {
            MvcResult result = this.mvc.perform(get("/")).andReturn();
            MockHttpServletResponse response = result.getResponse();
            String content = response.getContentAsString();

            doc = Jsoup.parse(content);
        } catch (Exception e) {
            errorInfo = e.getLocalizedMessage();
            //e.printStackTrace();
        }
        String message = "Task 8: The template has errors - " + errorInfo + ".";
        assertNotNull(message, doc);

        Elements h2Elements = doc.getElementsByTag("h2");
        message = "Task 8: An `<h2>` tag does not exist in the home.html template.";
        assertTrue(message,
                h2Elements.size() > 0);

        message = "Task 8: "+ALL_POSTS.size()+" `<h2>` tags should have been generated by the Thymeleaf each loop with an `<h2>` tag inside.";
        assertTrue(message,
                h2Elements.size() == ALL_POSTS.size());

        // Check each h2 content
        for (int i=0; i < h2Elements.size(); i++) {
            Element element = h2Elements.get(i);
            message = "Task 8: The `<h2>` tag displays the title \"" + element.html() + "\" instead of \"" + ALL_POSTS.get(i).getTitle() + "\".";
            assertEquals(message, ALL_POSTS.get(i).getTitle(), element.html());
        }
    }
}
