package com.pluralsight.module3;
import com.pluralsight.*;

import static org.junit.Assert.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import java.sql.Connection;
import java.sql.PreparedStatement;

import org.junit.BeforeClass;
import org.junit.Before;
import org.junit.Test;
import org.mockito.Mockito;

import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import org.powermock.reflect.Whitebox;
import java.lang.reflect.Method;

import org.junit.runner.RunWith;
import org.powermock.api.mockito.PowerMockito;
import org.powermock.core.classloader.annotations.PrepareForTest;
import org.powermock.modules.junit4.PowerMockRunner;
import org.powermock.reflect.exceptions.*;

import java.io.*;

@RunWith(PowerMockRunner.class)
@PrepareForTest(ControllerServlet.class)
public class Module3_Task6_and_7_IT extends Mockito{
	static String tempID = "0";
	static boolean called_updateBook = false;

	static HttpServletRequest request;
	static HttpServletResponse response;

	private Method method = null;
	private ControllerServlet controllerServlet;

  @Before
  public void setUp() throws Exception {
		try {
			method = Whitebox.getMethod(ControllerServlet.class,
								"updateBook", HttpServletRequest.class, HttpServletResponse.class);
		} catch (Exception e) {}

		if (method != null) {
	    controllerServlet = PowerMockito.spy(new ControllerServlet());

			request = mock(HttpServletRequest.class);
			response = mock(HttpServletResponse.class);
			try {
				when(request.getPathInfo()).thenReturn("/update");
				//PowerMockito.doNothing().when(controllerServlet, "updateBook", request, response);
				when(request.getParameter("id")).thenReturn(tempID);
			} catch (MethodNotFoundException e) {}
			try {
			 controllerServlet.doGet(request, response);
			} catch (Exception e) {}
		}
  }

		// Verify updateBook() exists in ControllerServlet
		@Test
		public void _task6() throws Exception {
			String errorMsg = "private void updateBook() does not exist in ControllerServlet";
			assertNotNull(errorMsg, method);
		}

    @Test
    public void _task7() throws Exception {
			String errorMsg = "private void updateBook() does not exist in ControllerServlet";
			assertNotNull(errorMsg, method);

		  try {
				PowerMockito.verifyPrivate(controllerServlet)
										.invoke("updateBook", request, response);
				called_updateBook = true;
		  } catch (Throwable e) {}

			errorMsg = "After action \"" + "/update" +
												"\", did not call updateBook().";
			assertTrue(errorMsg, called_updateBook);
    }
}
