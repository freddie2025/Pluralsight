package com.pluralsight.module2;
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

import org.junit.runner.RunWith;
import org.powermock.api.mockito.PowerMockito;
import org.powermock.core.classloader.annotations.PrepareForTest;
import org.powermock.modules.junit4.PowerMockRunner;
import org.powermock.reflect.exceptions.*;

import org.powermock.reflect.Whitebox;
import java.lang.reflect.Method;

import java.io.*;

@RunWith(PowerMockRunner.class)
@PrepareForTest(ControllerServlet.class)
public class Module2_Task1_and_2_IT extends Mockito {

	private ControllerServlet controllerServlet;
  private Method method = null;

  @Before
  public void setUp() throws Exception {
		try {
			method = Whitebox.getMethod(ControllerServlet.class,
								"showEditForm", HttpServletRequest.class, HttpServletResponse.class);
		} catch (Exception e) {}
  }

		// Verify the showEditForm() method exists in ControllerServlet
    @Test
    public void _task1() throws Exception {
      String errorMsg = "private void showEditForm() does not exist in ControllerServlet";
      assertNotNull(errorMsg, method);
    }

		@Test
		public void _task2() throws Exception {
			 String errorMsg = "private void showEditForm() does not exist in ControllerServlet";
			 assertNotNull(errorMsg, method);

			 String tempID = "0";
			 ControllerServlet controllerServlet = PowerMockito.spy(new ControllerServlet());
			 boolean called_showEditForm = false;
			 HttpServletRequest request = mock(HttpServletRequest.class);
			 HttpServletResponse response = mock(HttpServletResponse.class);

			 try {
				 when(request.getPathInfo()).thenReturn("/edit");
				 //PowerMockito.doNothing().when(controllerServlet, "showEditForm", request, response);
				 when(request.getParameter("id")).thenReturn(tempID);
			 } catch (MethodNotFoundException e) {}

			 try {
				controllerServlet.doGet(request, response);
				try {
					 PowerMockito.verifyPrivate(controllerServlet)
											 .invoke("showEditForm", request, response);
					 called_showEditForm = true;
				} catch (Throwable e) {}
			 } catch (Exception e) {}

			 errorMsg = "After action \"" + "/edit" +
												 "\", did not call showEditForm().";
			 assertTrue(errorMsg, called_showEditForm);
		}
}
