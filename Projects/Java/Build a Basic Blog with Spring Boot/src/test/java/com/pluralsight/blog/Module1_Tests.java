package com.pluralsight.blog;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
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
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.mock.web.MockHttpServletResponse;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;

import java.lang.annotation.Annotation;
import java.lang.reflect.Method;

import static org.junit.Assert.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc(print = MockMvcPrint.NONE)
@PrepareForTest(BlogController.class)
public class Module1_Tests {

	@Autowired
	private MockMvc mvc;

//	@Autowired(required = false)
//	private PostRepository postRepository;

	@MockBean
	private PostRepository postRepository;

	@Autowired
	private BlogController blogController;

	private Class c = null;
	private Method method = null;
	private boolean methodParametersExist = false;
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

		// Task 2(a) - setup - Check if method listPosts() exists
		try {
			method = c.getMethod("listPosts");
		} catch (Exception e) {
			////e.printStackTrace();
		}

		// Task 4 setup
		try {
			method = c.getMethod("listPosts", ModelMap.class);
			methodParametersExist = true;
		} catch (Exception e) {
			////e.printStackTrace();
		}
	}


	@Test
	public void task_1() {
		// Task 2(a) - Check if method listPosts() exists
		assertNotNull("Task 1: Method `listPosts()` does not exist in BlogController.", method);
	}

	@Test
	public void task_2() {
		// Task 1
		assertNotNull("Task 1: Method `listPosts()` does not exist in BlogController.", method);

		// Task 2 - Check for @ResponseBody and @RequestMapping Annotations
		Annotation[] annotations = method.getDeclaredAnnotations();

		boolean requestMappingExists = false;
		//boolean responseBodyExists = false;

		for (Annotation methodAnnotation : annotations) {
			if (methodAnnotation instanceof RequestMapping) {
				requestMappingExists = true;
			}
		}
		assertTrue("Task 2: `@RequestMapping(\"/\")` annotation does not exist in listPosts().",
				requestMappingExists);

		// TODO Task 2 - Additional test - Check that String "Hello World" is displayed
	}


	@Test
	public void task_3() {
		// Task 3(a)
		assertNotNull("Task 1: Method `listPosts()` does not exist in BlogController.", method);
		int numParameters = method.getParameterCount();
		assertTrue("Task 3: `listPosts()` needs a `ModelMap` parameter.", numParameters >= 1);

		Class[] classes = method.getParameterTypes();
		boolean modelMapExists = false;
		for (Class paramClass : classes) {
			if (paramClass.equals(ModelMap.class))
				modelMapExists = true;
		}

		assertTrue("Task 3: `listPosts()` needs a `ModelMap` parameter.", modelMapExists);

		// Task 3(b) - Verify modelMap.put() is called
		ModelMap modelMap = Mockito.mock(ModelMap.class);
		Mockito.when(modelMap.put("title", "Blog Post 1")).thenReturn(null);
		assertNotNull("Task 1: Method listPosts() does not exist in BlogController.", method);
		//blogController.listPosts(modelMap);
		try {
			method.invoke(blogController, modelMap);
		} catch (IllegalArgumentException e) {
			//e.printStackTrace();
		} catch (Exception e) {
			//e.printStackTrace();
		}

		boolean putCalledCorrectly = false;
		try {
			Mockito.verify(modelMap).put("title", "Blog Post 1");
			putCalledCorrectly = true;
		} catch (Error e) {
			////e.printStackTrace();
		}

		assertTrue("Task 3: Did not call `put()` on the `ModelMap` with a key of `\"title\"` and `\"Blog Post 1\"`",
				putCalledCorrectly);
	}

	@Test
	public void task_4() {
		// TODO This fails after Module 3 is completed
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

		String message = "Task 4: The template has errors - " + errorInfo + ".";
		assertNotNull(message, doc);

		Elements h2Elements = doc.getElementsByTag("h2");

		assertTrue("Task 4: An `<h2>` tag does not exist in the `home.html` template.",
				h2Elements.size() > 0);

		assertEquals("Task 4: An `<h2>` tag does not display the title parameter.",
				"Blog Post 1", h2Elements.first().html());
	}

}
