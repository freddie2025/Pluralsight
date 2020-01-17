package com.pluralsight.module1;
import com.pluralsight.*;

import static org.junit.Assert.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import java.util.List;
import java.util.ArrayList;
import java.util.Collection;

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

import java.lang.reflect.Method;
import java.io.*;

public class Module1_Task7_and_8_IT extends Mockito{

	static StringWriter stringWriter = new StringWriter();
	static String tempID = "0";
	static boolean called_getParameter = false;
	static boolean called_sendRedirect = false;
	static boolean called_deleteBook = false;
	static HttpServletRequest request = mock(HttpServletRequest.class);
	static HttpServletResponse response = mock(HttpServletResponse.class);
  static Method deleteMethod = null;
  @Mock
  private BookDAO mockBookDAO;

  @InjectMocks
  private ControllerServlet controllerServlet;

  @Before
  public void setUp() throws Exception {
		MockitoAnnotations.initMocks(this);

		request = mock(HttpServletRequest.class);
		response = mock(HttpServletResponse.class);

		when(request.getPathInfo()).thenReturn("/delete");
		when(request.getParameter("id")).thenReturn(tempID);

		try {
			deleteMethod = Whitebox.getMethod(ControllerServlet.class,
								"deleteBook", HttpServletRequest.class, HttpServletResponse.class);
		} catch (Exception e) {}

		// String errorMsg = "private void deleteBook() does not exist in ControllerServlet";
		// assertNotNull(errorMsg, deleteMethod);
		if (deleteMethod != null) {
			try {
				controllerServlet.doGet(request, response);
			} catch (Exception e) {}
		}
  }

		// Verify deleteBook() in ControllerServlet is complete
    @Test
    public void _task7() throws Exception {
			String errorMsg = "private void deleteBook() does not exist in ControllerServlet";
		  assertNotNull(errorMsg, deleteMethod);

			 MockingDetails mockingDetails = Mockito.mockingDetails(mockBookDAO);

			 Collection<Invocation> invocations = mockingDetails.getInvocations();

			 List<String> methodsCalled = new ArrayList<>();
			 for (Invocation anInvocation : invocations) {
			   methodsCalled.add(anInvocation.getMethod().getName());
			 }
			 errorMsg = "The ControllerServlet deleteBook() method was not called.";
			 assertTrue(errorMsg, methodsCalled.contains("deleteBook"));

			 try {
          verify(request, atLeast(1)).getParameter("id");
          called_getParameter = true;
       } catch (Throwable e) {}

			 errorMsg = "In ControllerServlet deleteBook()," +
			 									" did not call getParameter(\"id\").";
			 assertTrue(errorMsg, called_getParameter);
    }

		@Test
		public void _task8() throws Exception {
			String errorMsg = "private void deleteBook() does not exist in ControllerServlet";
		  assertNotNull(errorMsg, deleteMethod);
			try {
				 verify(response, atLeast(1)).sendRedirect("list");
				 called_sendRedirect = true;
			} catch (Throwable e) {}

			errorMsg = "In ControllerServlet deleteBook()," +
												" did not call sendRedirect(\"list\").";
			assertTrue(errorMsg, called_sendRedirect);
		}
}
