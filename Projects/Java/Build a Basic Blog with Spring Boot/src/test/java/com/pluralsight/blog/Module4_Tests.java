package com.pluralsight.blog;

import com.pluralsight.blog.data.PostRepository;
import com.pluralsight.blog.model.Post;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
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
import org.springframework.web.bind.annotation.RequestMapping;

import java.lang.annotation.Annotation;
import java.lang.reflect.Constructor;
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
public class Module4_Tests {

    @Autowired
    private MockMvc mvc;

    @Autowired
    private BlogController blogController;

    @Autowired
    private PostRepository postRepository;

    private PostRepository spyRepository;

    private List<Post> ALL_POSTS;

    private Method method = null;



    @Before
    public void setup() {
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
            method = BlogController.class.getMethod("postDetails", Long.class, ModelMap.class);
        } catch (Exception e) {
            //e.printStackTrace();
        }

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
        // Verify the anchor tags exist for each post - <a href = "/post/1">

        Document doc = null;
        String errorInfo = "";
        try {
            MvcResult result = this.mvc.perform(get("/")).andReturn();
            MockHttpServletResponse response = result.getResponse();
            String content = response.getContentAsString();

            doc = Jsoup.parse(content);
        } catch (Exception e) {
            errorInfo = e.getLocalizedMessage();
            //assertTrue("Task 1: The anchor tags do not exist for the Posts' ids.", false);
        }

        String message = "Task 1: The template has errors - " + errorInfo + ".";
        assertNotNull(message, doc);

        Elements linkElements = doc.getElementsByClass("post-url");

        assertTrue("Task 1: `<a>` tags with class \"post-url\" do not exist.", linkElements.size()>0);

        boolean anchorTagsExist = true;
        String url = "";
        String wanted = "";
        //int j = 0;
        for (int i=0; i < linkElements.size(); i++) {
            Element element = linkElements.get(i);
            url = element.attr("href");
            wanted = "/post/"+ALL_POSTS.get(i).getId();

            if (!url.equals(wanted)) {
                anchorTagsExist = false;
                break;
            }
            //j+=2;
        }

        message = "Task 1: The `<a>` tag's href is - " + url + " - not - " + wanted + " - .";
        assertTrue(message, anchorTagsExist);
    }

    @Test
    public void task_2() {
        String message = "Task 2: The method `public String postDetails(@PathVariable Long id, ModelMap map)` does not exist in BlogController.";
        assertNotNull(message, method);

        // TODO verify post-details template is displayed
        try {
            MvcResult result = this.mvc.perform(get("/")).andReturn();
            MockHttpServletResponse response = result.getResponse();
            String content = response.getContentAsString();

            Document doc = Jsoup.parse(content);
            Elements divElements = doc.getElementsByTag("div");


        } catch (Exception e) {
            //e.printStackTrace();
        }
    }

    @Test
    public void task_3() {
        String message = "Task 3: The method `public String postDetails(@PathVariable Long id, ModelMap map)` does not exist in BlogController.";
        assertNotNull(message, method);

        // Verify the method has @RequestMapping
        Annotation[] annotations = method.getDeclaredAnnotationsByType(RequestMapping.class);

        assertNotNull(annotations);
        assertTrue("Task 3: There are no annotation on the `postDetails()` method.", annotations.length >= 1);

        assertTrue("Task 3: The `@RequestMapping` annotation doesn't have the value `\"/post/{id}\"`.",
                annotations[0].toString().contains("value={\"/post/{id}\"}") || annotations[0].toString().contains("value=[/post/{id}]"));
    }

    @Test
    public void task_4() {
        Method findMethod = null;
        Post post = null;
        try {
            findMethod = PostRepository.class.getMethod("findById", Long.class);
            post = (Post) findMethod.invoke(spyRepository, 1l);
        } catch (Exception e) {
            //e.printStackTrace();
        }
        String message = "Task 4: The method `findById()` does not exist in the PostRepository class.";
        assertNotNull(message, findMethod);

        List<Post> posts = spyRepository.getAllPosts();
        message = "Task 4: The method `getAllPosts()` does not exist in the PostRepository class.";
        assertNotNull(message, posts);

        Post tempPost = null;
        try {
            for (Post p : posts) {
                if (p.getId() == 1)
                    tempPost = p;
            }
        } catch (Exception e) {
            //e.printStackTrace();
        }

        message = "Task 4: The method `findById()` does not return the correct Post.";
        assertEquals(message, tempPost, post);

    }

    @Test
    public void task_5() {
        do_task_5();
    }

    @Test
    public void task_6() {
        do_task_6();
    }

    public void do_task_5() {
        Mockito.when(spyRepository.findById(1l)).thenReturn(ALL_POSTS.get(0));
        ModelMap modelMap = Mockito.mock(ModelMap.class);
        Mockito.when(modelMap.put("post", ALL_POSTS.get(0))).thenReturn(null);

        try {
            // Call postDetails() if found
            method.invoke(blogController, 1l, modelMap);
        } catch (Exception e) {
            //e.printStackTrace();
        }

        boolean calledFind = false;
        try {
            Mockito.verify(spyRepository).findById(1l);
            calledFind = true;
        } catch (Error e) {
            //e.printStackTrace();
        }

        String message = "Task 5: Did not call PostRepository's `findById()` method in BlogController.";
        assertTrue(message, calledFind);
    }

    public void do_task_6() {
        Mockito.when(spyRepository.findById(1l)).thenReturn(ALL_POSTS.get(0));
        ModelMap modelMap = Mockito.mock(ModelMap.class);
        Mockito.when(modelMap.put("post", ALL_POSTS.get(0))).thenReturn(null);

        try {
            // Call postDetails() if found
            method.invoke(blogController, 1l, modelMap);
        } catch (Exception e) {
            //e.printStackTrace();
        }

        boolean calledPut = false;
        try {
            Mockito.verify(modelMap).put("post", ALL_POSTS.get(0));
            calledPut = true;
        } catch (Error e) {
            //e.printStackTrace();
        }

        String message = "Task 6: Did not call ModelMap's `put()` method with the Post.";
        assertTrue(message, calledPut);
    }

    @Test
    public void task_7() {
        //do_tasks_5_and_6();

        Document doc = null;
        String errorInfo = "";
        try {
            MvcResult result = this.mvc.perform(get("/post/1")).andReturn();
            MockHttpServletResponse response = result.getResponse();
            String content = response.getContentAsString();

            doc = Jsoup.parse(content);
        } catch (Exception e) {
            errorInfo = e.getLocalizedMessage();
            //e.printStackTrace();
        }

        String message = "Task 7: The template has errors - " + errorInfo + ".";
        assertNotNull(message, doc);


        Elements h3Elements = doc.getElementsByTag("h3");
        message = "The `<h3>` tags for author and date do not exist in the post-details.html template.";
        assertTrue(message,h3Elements.size() >= 2);
        Element h3Elem = h3Elements.get(0);
        assertNotNull("Task 7: The template doesn't have an `<h3>` tag.", h3Elem);

        boolean h3CorrectText = h3Elem.text().contains("Sarah Holderness");

        message = "Task 7: The first post's first `<h3>` tag should display `Sarah Holderness` as the author.";
        assertTrue(message, h3CorrectText);

        h3Elem = h3Elements.get(1);
        assertNotNull("Task 7: The template doesn't have a second `<h3>` tag.", h3Elem);

        h3CorrectText = h3Elem.text().contains(ALL_POSTS.get(0).getDateStr());

        message = "Task 7: The first post's second `<h3>` tag should display" + ALL_POSTS.get(0).getDateStr() + " as the date.";
        assertTrue(message, h3CorrectText);

    }

    @Test
    public void task_8() {
        //do_tasks_5_and_6();

        Document doc = null;
        String errorInfo = "";

        try {
            MvcResult result = this.mvc.perform(get("/post/1")).andReturn();
            MockHttpServletResponse response = result.getResponse();
            String content = response.getContentAsString();

            doc = Jsoup.parse(content);
        } catch (Exception e) {
            errorInfo = e.getLocalizedMessage();
            //e.printStackTrace();
        }

        String message = "Task 8: The template has errors - " + errorInfo + ".";
        assertNotNull(message, doc);

        Elements divElements = doc.getElementsByClass("col-md-8 post-body");

        message = "The `<div class=\"col-md-8 post-body\">` tag for the body does not exist in the post-details.html template.";
        assertTrue(message,
                divElements.size() == 1);


        message = "Task 8: The Post's Body isn't displayed. It should be - " + ALL_POSTS.get(0).getBody() +
                " - instead it is - " + divElements.get(0).html() + ".";
        assertEquals(message, ALL_POSTS.get(0).getBody(), divElements.get(0).html());


    }

}