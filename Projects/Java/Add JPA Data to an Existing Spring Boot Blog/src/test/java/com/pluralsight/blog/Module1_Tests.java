package com.pluralsight.blog;

import com.pluralsight.blog.model.Post;
import com.pluralsight.blog.data.PostRepository;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.test.context.junit4.SpringRunner;

import javax.persistence.*;
import java.lang.annotation.Annotation;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import static org.junit.Assert.*;

@RunWith(SpringRunner.class)
@SpringBootTest
//@AutoConfigureMockMvc
//@PrepareForTest(BlogController.class)
public class Module1_Tests {

//    @Autowired
//    private MockMvc mvc;

    @Autowired
    private PostRepository postRepository;

//    @Autowired
//    private BlogController blogController;
//
//    private Class c = null;
//    private Method method = null;
//    private boolean methodParametersExist = false;

    @Before
    public void setup() {
//        try {
//            MvcResult result  = this.mvc.perform(get("/")).andReturn();
//        } catch (Exception e) {
//            e.printStackTrace();
//        }
    }


    @Test
    public void task_1() {
        // Verify @Entity annotation
        // TODO All tests will fail with errors if SpringBootTest is used and The Entity doesn't have an @Id
        Annotation[] annotations =  Post.class.getDeclaredAnnotations();

        assertTrue("There should be 1 annotation, @Entity, on the Post class.", annotations.length == 1);

        assertEquals("The annotation on the Post class is not of type @Entity.", Entity.class,annotations[0].annotationType());

        Annotation[] fieldAnnotations = null;

        try {
            Field field = Post.class.getDeclaredField("id");
            fieldAnnotations = field.getDeclaredAnnotations();
        } catch (NoSuchFieldException e) {
            //e.printStackTrace();
        }

        String message = "The field id should have two annotations @Id and @GeneratedValue(strategy = GenerationType.IDENTITY).";
        assertTrue(message, fieldAnnotations.length == 2);

        boolean hasIdAnnotation = false;
        boolean hasGeneratedAnnotation = false;

        for (Annotation annotation : fieldAnnotations) {
            if (annotation.annotationType() == Id.class) hasIdAnnotation = true;
            if (annotation.annotationType() == GeneratedValue.class) hasGeneratedAnnotation = true;
            //System.out.println("annotation = " + annotation);
        }

        assertTrue("The field id does not have the annotation @Id.", hasIdAnnotation);
        assertTrue("The field id does not have the annotation @GeneratedValue(strategy = GenerationType.IDENTITY).", hasGeneratedAnnotation);
    }

    @Test
    public void task_2() {
        Annotation[] fieldAnnotations = null;

        try {
            Field field = Post.class.getDeclaredField("body");
            fieldAnnotations = field.getDeclaredAnnotations();
        } catch (NoSuchFieldException e) {
            //e.printStackTrace();
        }

        String message = "The field body should have 2 annotations @Column(length=1000000) and @Lob.";
        assertTrue(message, fieldAnnotations.length == 2);

        boolean hasColumnAnnotation = false;
        boolean hasLobAnnotation = false;

        for (Annotation annotation : fieldAnnotations) {
            if (annotation.annotationType() == Column.class) {hasColumnAnnotation = true;}
            if (annotation.annotationType() == Lob.class) hasLobAnnotation = true;
            //System.out.println("annotation = " + annotation);
        }

        assertTrue("The field body does not have the annotation @Column(length=1000000).", hasColumnAnnotation);
        assertTrue("The field body does not have the annotation @Lob.", hasLobAnnotation);
    }

    @Test
    public void task_3() {
        Annotation[] fieldAnnotations = null;

        try {
            Field field = Post.class.getDeclaredField("date");
            fieldAnnotations = field.getDeclaredAnnotations();
        } catch (NoSuchFieldException e) {
            //e.printStackTrace();
        }

        String message = "The field date should have 1 annotation @Temporal(TemporalType.DATE).";
        assertTrue(message, fieldAnnotations.length == 1);

        System.out.println("annotation = " + fieldAnnotations[0]);

        message = "The field date does not have the annotation @Temporal(TemporalType.DATE).";
        assertTrue(message, fieldAnnotations[0].annotationType() == Temporal.class);
    }

    @Test
    public void task_4() {
        Class c = PostRepository.class;
        Class[] interfaces = c.getInterfaces();

        assertEquals("PostRepository should extend 1 interface - JpaRepository.",
                1, interfaces.length);

        assertEquals("PostRepository should be an interface that extends JpaRepository<Post, Long>.",
                JpaRepository.class, interfaces[0]);
    }

    @Test
    public void task_5() {
        List<Post> posts = postRepository.findAll();
        assertNotNull("PostRepository's findAll() method returns null.", posts);

        List<String> titles = new ArrayList<>(Arrays.asList("Earbuds",
                "Smart Speakers",
                "Device Charger",
                "Smart Home Lock",
                "Smart Instant Pot",
                "Mobile Tripod",
                "Travel Keyboard",
                "SD Card Reader"));

        assertEquals("There should be " + titles.size() + " Posts loaded from data-categories.sql.", titles.size(), posts.size());


        boolean titlesMatch = true;
        for (int i = 0; i<posts.size(); i++) {
            if (!posts.get(i).getTitle().equals(titles.get(i))) {
                titlesMatch = false;
                break;
            }
        }

        assertTrue("The titles loaded from data-categories.sql do not match the expected titles.", titlesMatch);
    }

}