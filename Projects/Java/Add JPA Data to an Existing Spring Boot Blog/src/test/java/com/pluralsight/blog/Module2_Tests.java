package com.pluralsight.blog;

import com.pluralsight.blog.data.CategoryRepository;
import com.pluralsight.blog.data.PostRepository;
import com.pluralsight.blog.model.Category;
import com.pluralsight.blog.model.Post;
import org.apache.commons.io.IOUtils;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.mock.web.MockHttpServletResponse;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.ManyToOne;
import java.io.*;
import java.lang.annotation.Annotation;
import java.lang.reflect.Constructor;
import java.lang.reflect.Field;
import java.lang.reflect.Method;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;

import static org.junit.Assert.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
public class Module2_Tests {

    @Autowired
    private MockMvc mvc;

    @Test
    public void task_1() {
        // Task 3 - Check for @Entity
        Annotation[] annotations = Category.class.getDeclaredAnnotationsByType(Entity.class);

        boolean entityExists = false;

        for (Annotation methodAnnotation : annotations) {
            if (methodAnnotation instanceof Entity) {
                entityExists = true;
            }
        }
        assertTrue("Task 1: @Entity annotation does not exist before the Category class declaration.",
                entityExists);

        Annotation[] fieldAnnotations = null;

        try {
            Field field = Category.class.getDeclaredField("id");
            fieldAnnotations = field.getDeclaredAnnotations();
        } catch (NoSuchFieldException e) {
            //e.printStackTrace();
        }

        String message = "Task 1: The field id should have two annotations @Id and @GeneratedValue(strategy = GenerationType.IDENTITY).";
        assertTrue(message, fieldAnnotations.length == 2);

        boolean hasIdAnnotation = false;
        boolean hasGeneratedAnnotation = false;

        for (Annotation annotation : fieldAnnotations) {
            if (annotation.annotationType() == Id.class) hasIdAnnotation = true;
            if (annotation.annotationType() == GeneratedValue.class) hasGeneratedAnnotation = true;
            //System.out.println("annotation = " + annotation);
        }

        assertTrue("Task 1: The field id does not have the annotation @Id.", hasIdAnnotation);
        assertTrue("Task 1: The field id does not have the annotation @GeneratedValue(strategy = GenerationType.IDENTITY).", hasGeneratedAnnotation);
    }

    @Test
    public void task_2() {
        // Verify Category property called category exists in the Post class
        Field field = null;
        try {
            field =  Post.class.getDeclaredField("category");
        } catch (NoSuchFieldException e) {
            //e.printStackTrace();
        }

        String message = "Task 2: The Post class does not have a Category field named category.";
        assertNotNull(message, field);
        assertTrue(message, field.getType() == Category.class);

        // Verify that the Category field has the @ManyToOne annotation
        Annotation[] annotations = field.getDeclaredAnnotations();

        message = "Task 2: The field category should have 1 annotation - the @ManyToOne annotation.";
        assertEquals(message, 1, annotations.length);
        assertEquals(message, ManyToOne.class, annotations[0].annotationType());

        // Check for getter
        Method getCategoryMethod = null;
        try {
            getCategoryMethod = Post.class.getMethod("getCategory");
        } catch (NoSuchMethodException e) { }
        assertNotNull("Task 3: The getCategory() method does not exist in the Post class.", getCategoryMethod);

        // Check for setter
        Method setCategoryMethod = null;
        try {
            setCategoryMethod = Post.class.getMethod("setCategory", Category.class);
        } catch (NoSuchMethodException e) { }
        assertNotNull("Task 3: The setCategory() method does not exist in the Post class.", setCategoryMethod);
    }

    @Test
    public void task_3() {
        // Add the corresponding property to the Category Entity, which will be a List of Posts called posts with
        // the @OneToMany annotation.
        Field field = null;
        try {
            field =  Category.class.getDeclaredField("posts");
        } catch (NoSuchFieldException e) {
            //e.printStackTrace();
        }

        String message = "Task 3: The Category class does not have a List of Posts field named posts.";
        assertNotNull(message, field);
        assertTrue(message, field.getType() == List.class);
        // We also need to do a few additional things in the Category class:
        //  ** Add a default constructor that calls super() and also initializes posts to an empty ArrayList.

        boolean arrayListInit = false;
        try {
             Constructor<Category> constructor = Category.class.getConstructor();
             Category category = constructor.newInstance();
             if (category.getPosts().size() == 0)
                 arrayListInit = true;

        } catch (Exception e) {
            //e.printStackTrace();
        }
        assertTrue("Task 3: The List of posts was not initialized in the default constructor.", arrayListInit);

        //  ** Also add a getter for the List of Posts property.
        Method getPostsMethod = null;
        try {
            getPostsMethod = Category.class.getMethod("getPosts");
        } catch (NoSuchMethodException e) {
            //e.printStackTrace();
        }
        assertNotNull("Task 3: The getPosts() method does not exist in the Category class.", getPostsMethod);

        //  ** Instead of a setter, add a method called public void addPost(Post post) which adds a post to the list.
        Method addPostMethod = null;
        try {
            addPostMethod = Category.class.getMethod("addPost", Post.class);
        } catch (NoSuchMethodException e) {
            //e.printStackTrace();
        }
        assertNotNull("Task 3: The addPost(Post post) method does not exist in the Category class.", getPostsMethod);
    }

    @Test
    public void task_4() {
        // Add CategoryRepository
        // public interface CategoryRepository extends JpaRepository<Category, Long> {}
        Class c = CategoryRepository.class;
        Class[] interfaces = c.getInterfaces();

        assertEquals("Task 4: CategoryRepository should extend 1 interface - JpaRepository.",
                1, interfaces.length);

        assertEquals("Task 4: CategoryRepository should be an interface that extends JpaRepository<Category, Long>.",
                JpaRepository.class, interfaces[0]);
    }

    @Test
    public void task_5() {
        Method method = null;
        try {
            method = PostRepository.class.getMethod("findByCategory", Category.class);
        } catch (Exception e) {
            ////e.printStackTrace();
        }

        assertNotNull("Task 5: The method findByCategory() doesn't exist in the PostRepository class.", method );
    }

    @Test
    public void task_6() {
        // Replace data-categories.sql file to add Categories
        // Open data-categories.sql file and check contents
        Path path = Paths.get("src/main/resources/data.sql");
        String result = "";
        try {
            final String output = "";
            List<String> allLines = Files.readAllLines(path);
            result = String.join("\n", allLines);
        } catch (IOException e) {
            //e.printStackTrace();
        }

        String resultResource = "";
        ClassLoader classLoader = getClass().getClassLoader();
        try (InputStream inputStream = classLoader.getResourceAsStream("data-categories.sql")) {
            resultResource = IOUtils.toString(inputStream, StandardCharsets.UTF_8);
        } catch (IOException e) {
            //e.printStackTrace();
        }

        assertTrue("Task 6: The `data.sql` file is not the same as `data-categories.sql`.", resultResource.equals(result));
    }

    @Test
    public void task_7() {
        // Display Category on details page
        Document doc = null;
        String errorInfo = "";
        try {
            MvcResult result = this.mvc.perform(get("/post/1")).andReturn();
            MockHttpServletResponse response = result.getResponse();
            String content = response.getContentAsString();

            doc = Jsoup.parse(content);
        } catch (Exception e) {
            //errorInfo = e.getLocalizedMessage();
            //e.printStackTrace();
        }
        String message = "Task 7: The template has errors - " + errorInfo + ".";
        assertNotNull(message, doc);

        Elements h3Elements = doc.getElementsByTag("h3");
        Element h3Elem = h3Elements.first();
        assertNotNull("Task 7: The template doesn't have an <h3> tag.", h3Elem);

        boolean catExists = false;
        for (Element elem : h3Elements) {
            if (elem.text().contains("Mobile Accessories"))
            {
                catExists = true;
                break;
            }
        }

        assertTrue("Task 7: The category is not displayed on the post-details page.", catExists);
    }
}