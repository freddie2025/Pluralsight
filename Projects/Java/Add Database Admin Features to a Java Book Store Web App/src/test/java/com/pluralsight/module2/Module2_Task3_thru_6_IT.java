package com.pluralsight.module2;
import com.pluralsight.*;

import static org.junit.Assert.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.RequestDispatcher;

import java.sql.Connection;
import java.sql.PreparedStatement;

import org.junit.BeforeClass;
import org.junit.Before;
import org.junit.Test;
import org.mockito.Mockito;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import java.lang.reflect.Method;
import java.io.*;



public class Module2_Task3_thru_6_IT extends Mockito{

	static StringWriter stringWriter = new StringWriter();
	static String tempID = "1";
  static int tempIntID = 1;
	static HttpServletRequest request;
	static HttpServletResponse response;
	static RequestDispatcher mockRequestDispatcher;
	static Book mockBook;

  @Mock
  private BookDAO mockBookDAO;

  @InjectMocks
  private ControllerServlet controllerServlet;

  @Before
  public void setUp() throws Exception {
    MockitoAnnotations.initMocks(this);

		request = mock(HttpServletRequest.class);
		response = mock(HttpServletResponse.class);
		mockRequestDispatcher = mock(RequestDispatcher.class);
		mockBook = mock(Book.class);

		when(request.getPathInfo()).thenReturn("/edit");
		when(request.getParameter("id")).thenReturn(tempID);
		when(mockBookDAO.getBook(tempIntID)).thenReturn(mockBook);
		when(request.getRequestDispatcher("/BookForm.jsp"))
								 .thenReturn(mockRequestDispatcher);

		try {
			controllerServlet.doGet(request, response);
	  } catch (Exception e) {}
  }

		// Verify showEditForm() is complete in ControllerServlet
		// Since it's private need to verify the lines of code get called
		// through the /edit action in doGet()
		@Test
    public void _task3() throws Exception {
       boolean called_getParameter = false;
       boolean called_getBook = false;

       try {
          verify(request, atLeast(1)).getParameter("id");
          called_getParameter = true;
       } catch (Throwable e) {}

       try {
          verify(mockBookDAO).getBook(anyInt());
          called_getBook = true;
       } catch (Throwable e) {}

       String errorMsg = "In ControllerServlet showEditForm()," +
                         " did not call getParameter(\"id\").";
       assertTrue(errorMsg, called_getParameter);
       errorMsg = "In ControllerServlet showEditForm()," +
                         " did not call getBook(id).";
       assertTrue(errorMsg, called_getBook);
    }

    @Test
    public void _task4() throws Exception {
       boolean called_getRequestDispatcher = false;

       try {
          verify(request).getRequestDispatcher("/BookForm.jsp");
          called_getRequestDispatcher = true;
       } catch (Throwable e) {}

       String errorMsg = "In ControllerServlet showEditForm()," +
            " did not call request.getRequestDispatcher(\"BookForm.jsp\").";
       assertTrue(errorMsg, called_getRequestDispatcher);
    }

		@Test
    public void _task5() throws Exception {
       boolean called_setAttribute = false;

       try {
          verify(request).setAttribute("book", mockBook);
          called_setAttribute = true;
       } catch (Throwable e) {}

       String errorMsg = "In ControllerServlet showEditForm()," +
                         " did not call request.setAttribute(\"book\", bookObject);.";
       assertTrue(errorMsg, called_setAttribute);
    }

		@Test
    public void _task6() throws Exception {
       boolean called_forward = false;

       try {
          verify(mockRequestDispatcher).forward(request, response);
          called_forward = true;
       } catch (Throwable e) {}

       String errorMsg = "In ControllerServlet showEditForm()," +
                         " did not call dispatcher.forward(request, response);.";
       assertTrue(errorMsg, called_forward);
    }


}
