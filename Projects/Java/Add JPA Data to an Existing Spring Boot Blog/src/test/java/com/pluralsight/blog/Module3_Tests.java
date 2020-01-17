package com.pluralsight.blog;

import com.pluralsight.blog.data.CategoryRepository;
import com.pluralsight.blog.model.Category;
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
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.mock.web.MockHttpServletResponse;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;

import java.lang.annotation.Annotation;
import java.lang.reflect.Constructor;
import java.lang.reflect.Field;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import static org.junit.Assert.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
@PrepareForTest(BlogController.class)
public class Module3_Tests {

    @Autowired
    private MockMvc mvc;

    @Autowired
    private BlogController blogController;

    @Autowired
    private PostRepository postRepository;
//
//    private PostRepository spyPostRepository;
    private PostRepository mockPostRepository;

    @Autowired
    private CategoryRepository categoryRepository;
//
//    private CategoryRepository spyCategoryRepository;
    private CategoryRepository mockCategoryRepository;

    private Method method = null;

    @Before
    public void setup() {
        Constructor<BlogController> constructor = null;
        try {
            constructor = BlogController.class.getDeclaredConstructor(PostRepository.class, CategoryRepository.class);
        } catch (NoSuchMethodException e) {
            //e.printStackTrace();
        }

//        spyPostRepository = Mockito.spy(postRepository);
//        spyCategoryRepository = Mockito.spy(categoryRepository);
        mockPostRepository = Mockito.mock(PostRepository.class);
        mockCategoryRepository = Mockito.mock(CategoryRepository.class);
        try {
            blogController = constructor.newInstance(mockPostRepository,
                                                     mockCategoryRepository); //new BlogController(spyRepository);
        } catch (Exception e) {
            //e.printStackTrace();
        }

        // Task 6 - setup - Check if method categoryList() exists
        try {
            method = BlogController.class.getMethod("categoryList");
        } catch (Exception e) {
            ////e.printStackTrace();
        }

        // Task 6 - setup - Check if method categoryList() exists
        try {
            method = BlogController.class.getMethod("categoryList", ModelMap.class);
        } catch (Exception e) {
            ////e.printStackTrace();
        }

        // Task 6 - setup - Check if method categoryList() exists
        try {
            method = BlogController.class.getMethod("categoryList", Long.class, ModelMap.class);
        } catch (Exception e) {
            ////e.printStackTrace();
        }
    }


    @Test
    public void task_1() {
        // Task 1 - Add field CategoryRepository categoryRepository; to BlogController
        Field[] fields = BlogController.class.getDeclaredFields();

        boolean categoryRepositoryExists = false;
        boolean annotationExists = false;
        for (Field field : fields) {
            if (field.getName().equals("categoryRepository") && field.getType().equals(CategoryRepository.class)) {
                categoryRepositoryExists = true;
            }
        }

        String message = "Task 3: A field called categoryRepository of type CategoryRepository does not exist in BlogController.";
        assertTrue(message, categoryRepositoryExists);

        // Check for BlogController constructor with CategoryRepository parameter
        Constructor<BlogController> constructor = null;
        try {
            constructor = BlogController.class.getDeclaredConstructor(PostRepository.class, CategoryRepository.class);
        } catch (NoSuchMethodException e) {
            //e.printStackTrace();
        }

        message = "Task 4: A BlogController constructor with a PostRepository parameter and a CategoryRepository parameter does not exist.";
        assertNotNull(message, constructor);
    }
    @Test
    public void task_2() {
        // Setup
        List<Category> categories = categoryRepository.findAll();
        Mockito.when(mockCategoryRepository.findAll()).thenReturn(categories);
        ModelMap modelMap = Mockito.mock(ModelMap.class);
        Mockito.when(modelMap.put("categories", categories)).thenReturn(null);


        try {
            Method listPostsMethod = BlogController.class.getMethod("listPosts", ModelMap.class);
            listPostsMethod.invoke(blogController, modelMap);
        } catch (Exception e) {
            e.printStackTrace();
        }

        // Verify findAll()
        boolean calledFind = false;
        try {
            Mockito.verify(mockCategoryRepository).findAll();
            calledFind = true;
        } catch (Error e) {
            e.printStackTrace();
        }
        String message = "Task 9: Did not call CategoryRepository's findAll() method in BlogController.";
        assertTrue(message, calledFind);

        // Verify ModelMap put()
        boolean putCalledCorrectly = false;
        try {
            Mockito.verify(modelMap).put("categories", categories);
            //Mockito.verify(modelMap, Mockito.atLeast(2)).put(Mockito.anyString(), Mockito.any(List.class));
            putCalledCorrectly = true;
        } catch (Error e) {
            e.printStackTrace();
        }

        assertTrue("Task 10: Did not call put() on the ModelMap with a key of \"categories\" and the result of calling " +
                        "categoryRepository.findAll().",
                putCalledCorrectly);
    }

    @Test
    public void task_3() {
        // Task 3 - Display list with foreach loop in template
        // TODO - How to more robustly test?  Right now just confirming 3 <li>'s show up.
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

        Elements liElements = doc.getElementsByTag("li");

        message = "Task 3: A <li> tag does not exist in the header.html template.";
        assertTrue(message,
                liElements.size() > 0);

        // Task 8 - Verify <div class="blog__post"> shows up 5 times
        // Check each h2 content
        int liCount = 0;
        for (int i = 0; i < liElements.size(); i++) {
            Element element = liElements.get(i);
            if (element.className().equals("nav-item"))
                liCount++;
        }

        message = "Task 3: The <li> tag with class \"nav-item\" should appear 3 times.";
        assertEquals(message, 3, liCount);
    }

    @Test
    public void task_4() {
        // Confirming 3 <span>'s show up.
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

        Elements spanElements = doc.getElementsByTag("span");

        List<String> categories = new ArrayList<>(Arrays.asList("Mobile Accessories",
                "Computer Accessories",
                "Smart Home"));

        assertEquals("Task 4: There should be " + categories.size() + " Posts loaded from data-categories.sql.",
                categories.size()+1, spanElements.size());


        boolean categoriesMatch = true;
        for (int i = 0; i<categories.size(); i++) {
            System.out.println("span elem = " + spanElements.get(i+1).text());
            System.out.println("category = " + categories.get(i));
            if (!spanElements.get(i+1).text().equals(categories.get(i).toUpperCase())) {
                categoriesMatch = false;
                break;
            }
        }

        assertTrue("Task 4: The titles loaded from data-categories.sql do not match the expected titles.", categoriesMatch);
    }

    @Test
    public void task_5() {
        // Confirming 3 <span>'s show up.
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
        String message = "Task 5: The template has errors - " + errorInfo + ".";
        assertNotNull(message, doc);

        Elements aElements = doc.getElementsByTag("a");
        assertTrue("Task 5: There should be at least 3 anchor tags.", aElements.size()>=3);

        boolean anchorTagsCorrect = true;
        for (int i = 1; i <=3; i++) {
            assertEquals("Task 5: The href on the anchor tag is not correct.", "/category/"+i, aElements.get(i).attr("href"));
        }


    }

    @Test
    public void task_6() {
        // Task 6 - Check if method categoryList() exists
        assertNotNull("Task 6: Method categoryList() does not exist in BlogController.", method);

        int numParameters = method.getParameterCount();
        assertTrue("Task 6: categoryList() needs a ModelMap parameter and a Long parameter.", numParameters == 2);

        Class[] classes = method.getParameterTypes();
        boolean modelMapExists = false;
        boolean longExists = false;
        for (Class paramClass : classes) {
            System.out.println("paramClass = " + paramClass);
            if (paramClass.equals(ModelMap.class))
                modelMapExists = true;
            if (paramClass.equals(Long.class))
                longExists = true;
        }

        assertTrue("Task 6: categoryList() needs a ModelMap parameter.", modelMapExists);
        assertTrue("Task 6: categoryList() needs a Long id parameter.", longExists);
    }

    @Test
    public void task_7() {
        // Task 1
        assertNotNull("Task 7: Method categoryList() does not exist in BlogController.", method);

        // Task 2 - Check for @RequestMapping Annotations
        Annotation[] annotations = method.getDeclaredAnnotations();
        boolean requestMappingExists = false;

        for (Annotation methodAnnotation : annotations) {
            if (methodAnnotation instanceof RequestMapping) {
                requestMappingExists = true;
            }
        }
        assertTrue("Task 7: @RequestMapping(\"/\") annotation does not exist on categoryList().",
                requestMappingExists);
    }


    @Test
    public void task_8() {
        // Task 8 - Verify categoryRepository.findById(id).orElse(null); is called
        // Verify modelMap.put("category", category)

        // Setup
        Category category = null;
        try {
            category = categoryRepository.findById(1l).orElse(null);
        } catch (Exception e) {
//            e.printStackTrace();
        }
        Mockito.when(mockCategoryRepository.findById(1l)).thenReturn(java.util.Optional.ofNullable(category));
        ModelMap modelMap = Mockito.mock(ModelMap.class);
        Mockito.when(modelMap.put("category", category)).thenReturn(null);

        try {
            method.invoke(blogController, 1l, modelMap);
        } catch (Exception e) {
            //e.printStackTrace();
        }

        // Verify findById()
        boolean calledFind = false;
        try {
            Mockito.verify(mockCategoryRepository).findById(1l);
            calledFind = true;
        } catch (Error e) {
            //e.printStackTrace();
        }
        String message = "Task 8: Did not call CategoryRepository's findById() method in BlogController.";
        assertTrue(message, calledFind);

        // Verify ModelMap put()
        boolean putCalledCorrectly = false;
        try {
            Mockito.verify(modelMap).put("category", category);
            putCalledCorrectly = true;
        } catch (Error e) {
            ////e.printStackTrace();
        }

        assertTrue("Task 8: Did not call put() on the ModelMap with a key of \"category\" and the result of calling " +
                        "category.findById(id).",
                putCalledCorrectly);
    }

@Test
    public void task_9() {
        // Task 9 - Verify PostRepository's findByCategory() is called
        // Confirming 3 <span>'s show up.
        Document doc = null;
        String errorInfo = "";
        try {
            MvcResult result = this.mvc.perform(get("/category/1")).andReturn();
            MockHttpServletResponse response = result.getResponse();
            String content = response.getContentAsString();

            doc = Jsoup.parse(content);
        } catch (Exception e) {
            errorInfo = e.getLocalizedMessage();
            //e.printStackTrace();
        }
        String message = "Task 9: The template has errors - " + errorInfo + ".";
        assertNotNull(message, doc);

        Elements aElements = doc.getElementsByTag("a");
        assertTrue("Task 9: There should be at least 3 anchor tags generated in category-list.html by the passed in posts list.", aElements.size()>=3);

        for (Element elem : aElements) {
            System.out.println("elem = " + elem.text());
            System.out.println("elem href = " + elem.attr("href"));
        }

//        boolean anchorTagsCorrect = true;
//        int j = 4;
//        for (int i = 1; i <=3; i++) {
//            assertEquals("Task 9: The href on the anchor tag is not correct.", "/post/"+i, aElements.get(j).attr("href"));
//            j+=3;
//        }

        assertEquals("Task 9: The href on the anchor tag is not correct.", "/post/"+1, aElements.get(4).attr("href"));
        assertEquals("Task 9: The href on the anchor tag is not correct.", "/post/"+3, aElements.get(7).attr("href"));
        assertEquals("Task 9: The href on the anchor tag is not correct.", "/post/"+6, aElements.get(11).attr("href"));
    }

    @Test
    public void task_10() {
        // Task 10 - Verify CategoryRepository's findAll() is called
        // Verify modelMap.put("categories", categories)

        // Setup
        List<Category> categories = categoryRepository.findAll();
        //Mockito.when(spyCategoryRepository.findAll()).thenReturn(categories);
        ModelMap modelMap = Mockito.mock(ModelMap.class);
        Mockito.when(modelMap.put("categories", categories)).thenReturn(null);

        try {
            method.invoke(blogController, 1l, modelMap);
        } catch (Exception e) {
            //e.printStackTrace();
        }

//        // Verify findAll()
//        boolean calledFind = false;
//        try {
//            Mockito.verify(spyCategoryRepository).findAll();
//            calledFind = true;
//        } catch (Error e) {
//            e.printStackTrace();
//        }
//        String message = "Task 9: Did not call CategoryRepository's findAll() method in BlogController.";
//        assertTrue(message, calledFind);

        // Verify ModelMap put()
        boolean putCalledCorrectly = false;
        try {
            //Mockito.verify(modelMap).put("categories", categories);
            Mockito.verify(modelMap, Mockito.atLeast(2)).put(Mockito.anyString(), Mockito.any(List.class));
            putCalledCorrectly = true;
        } catch (Error e) {
            e.printStackTrace();
        }

        assertTrue("Task 10: Did not call put() on the ModelMap with a key of \"categories\" and the result of calling " +
                        "categoryRepository.findAll().",
                putCalledCorrectly);

    }

    @Test
    public void task_11() {
        // Confirming the <h2> with the Category name shows up
        Document doc = null;
        String errorInfo = "";
        try {
            MvcResult result = this.mvc.perform(get("/category/1")).andReturn();
            MockHttpServletResponse response = result.getResponse();
            String content = response.getContentAsString();

            doc = Jsoup.parse(content);
        } catch (Exception e) {
            errorInfo = e.getLocalizedMessage();
            //e.printStackTrace();
        }
        String message = "Task 11: The template has errors - " + errorInfo + ".";
        assertNotNull(message, doc);

        Elements h2Elements = doc.getElementsByTag("h2");
        Element h2Elem = h2Elements.first();
        assertNotNull("Task 11: The template doesn't have an <h2> tag.", h2Elem);
        assertEquals("Task 11: The category-list page is displaying \"" + h2Elem.text() + "\" instead of \"Mobile Accessories\".",
                "Mobile Accessories", h2Elem.text());
    }
}
