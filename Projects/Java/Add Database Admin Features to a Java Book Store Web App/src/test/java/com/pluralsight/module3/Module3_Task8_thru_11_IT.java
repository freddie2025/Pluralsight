package com.pluralsight.module3;
import com.pluralsight.*;

import static org.junit.Assert.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import java.util.List;
import java.util.ArrayList;
import java.util.Collection;
import java.lang.reflect.Method;

import java.sql.Connection;
import java.sql.PreparedStatement;

import org.junit.BeforeClass;
import org.junit.Before;
import org.junit.Test;
import org.mockito.Mockito;

import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.mockito.MockingDetails;
import org.mockito.invocation.Invocation;
import org.powermock.reflect.Whitebox;

import java.io.*;

public class Module3_Task8_thru_11_IT extends Mockito{

	static StringWriter stringWriter = new StringWriter();
	static String tempIDStr = "1";
  static int tempID = 1;
  static String tempTitle = "1984";
  static String tempAuthor = "George Orwell";
  static String tempPriceStr = "1.50";
  static float tempPrice = 1.50f;

	static boolean called_getId = false;
	static boolean called_getTitle = false;
	static boolean called_getAuthor = false;
	static boolean called_getPrice = false;
	static boolean called_updateBook = false;
	static boolean called_sendRedirect = false;
	static HttpServletRequest request;
	static HttpServletResponse response;
	static Book tempBook;
	static Method updateMethod = null;

	@Mock
  private BookDAO mockBookDAO;

  @InjectMocks
  private ControllerServlet controllerServlet;

  @Before
  public void setUp() throws Exception {
    MockitoAnnotations.initMocks(this);

		request = mock(HttpServletRequest.class);
		response = mock(HttpServletResponse.class);
		tempBook = new Book(tempID, tempTitle, tempAuthor, tempPrice);

		when(request.getPathInfo()).thenReturn("/update");
		when(request.getParameter("id")).thenReturn(tempIDStr);
		when(request.getParameter("booktitle")).thenReturn(tempTitle);
		when(request.getParameter("bookauthor")).thenReturn(tempAuthor);
		when(request.getParameter("bookprice")).thenReturn(tempPriceStr);


		try {
			updateMethod = Whitebox.getMethod(ControllerServlet.class,
								"updateBook", HttpServletRequest.class, HttpServletResponse.class);
		} catch (Exception e) {}

		// String errorMsg = "private void updateBook() does not exist in ControllerServlet";
		// assertNotNull(errorMsg, updateMethod);

		if (updateMethod != null) {
			try {
			 controllerServlet.doGet(request, response);
			} catch (Exception e) {}
		}
  }

    @Test
    public void _task8() throws Exception {
			String errorMsg = "private void updateBook() does not exist in ControllerServlet";
			assertNotNull(errorMsg, updateMethod);

			try {
         verify(request).getParameter("id");
         called_getId = true;
       } catch (Throwable e) {}

       errorMsg = "After action \"" + "/update" +
                         "\", did not call getParameter(\"id\").";
       assertTrue(errorMsg, called_getId);
    }

		@Test
    public void _task9() throws Exception {
			String errorMsg = "private void updateBook() does not exist in ControllerServlet";
			assertNotNull(errorMsg, updateMethod);

			try {
         verify(request).getParameter("booktitle");
         called_getTitle = true;
         verify(request).getParameter("bookauthor");
         called_getAuthor = true;
				 verify(request).getParameter("bookprice");
         called_getPrice = true;
       } catch (Throwable e) {}

       errorMsg = "After action \"" + "/update" +
                         "\", did not call getParameter(\"booktitle\").";
       assertTrue(errorMsg, called_getTitle);
       errorMsg = "After action \"" + "/update" +
                         "\", did not call getParameter(\"bookauthor\").";
       assertTrue(errorMsg, called_getAuthor);
			 errorMsg = "After action \"" + "/update" +
                         "\", did not call getParameter(\"bookprice\").";
       assertTrue(errorMsg, called_getPrice);
    }

		@Test
    public void _task10() throws Exception {
			String errorMsg = "private void updateBook() does not exist in ControllerServlet";
			assertNotNull(errorMsg, updateMethod);

			Method method = null;
			try {
				 method =  BookDAO.class.getMethod("updateBook", Book.class);
			} catch (NoSuchMethodException e) {
				 //e.printStackTrace();
			}

			errorMsg = "The method updateBook() doesn't exist in BookDAO.java.";
			assertNotNull(errorMsg, method);

			MockingDetails mockingDetails = Mockito.mockingDetails(mockBookDAO);

			Collection<Invocation> invocations = mockingDetails.getInvocations();

			List<String> methodsCalled = new ArrayList<>();
			for (Invocation anInvocation : invocations) {
				methodsCalled.add(anInvocation.getMethod().getName());
			}
			errorMsg = "After action \"" + "/update" +
												"\", did not updateBook(newBookObject).";
			assertTrue(errorMsg, methodsCalled.contains("updateBook"));
    }

		@Test
    public void _task11() throws Exception {
			String errorMsg = "private void updateBook() does not exist in ControllerServlet";
			assertNotNull(errorMsg, updateMethod);

			try {
         verify(response).sendRedirect("list");
         called_sendRedirect = true;
       } catch (Throwable e) {}

       errorMsg = "In ControllerServlet updateBook()," +
                         " did not call sendRedirect(\"list\").";
       assertTrue(errorMsg, called_sendRedirect);
    }
}
