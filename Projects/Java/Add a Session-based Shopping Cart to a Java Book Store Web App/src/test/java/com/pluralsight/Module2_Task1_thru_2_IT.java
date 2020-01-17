package com.pluralsight;

import static org.junit.Assert.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

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


@RunWith(PowerMockRunner.class)
@PrepareForTest(CartController.class)
public class Module2_Task1_thru_2_IT extends Mockito {

  Method method = null;

    @Before
    public void setUp() throws Exception {
      try {
        method = Whitebox.getMethod(CartController.class,
                  "deleteFromCart", HttpServletRequest.class, HttpServletResponse.class);
      } catch (Exception e) {}
    }

		// Verify the deleteFromCart() method exists in CartController
    @Test
    public void _task1() throws Exception {
      String errorMsg = "private void deleteFromCart() does not exist in CartController";
      assertNotNull(errorMsg, method);
    }

    @Test
    public void _task2() throws Exception {
      String errorMsg = "private void deleteFromCart() does not exist in CartController";
      assertNotNull(errorMsg, method);
      CartController cartController = PowerMockito.spy(new CartController());
      boolean called_deleteFromCart = false;
      HttpServletRequest request = mock(HttpServletRequest.class);
      HttpServletResponse response = mock(HttpServletResponse.class);
      HttpSession session = mock(HttpSession.class);
      ShoppingCart shoppingCart = mock(ShoppingCart.class);
      
       try {
         when(request.getPathInfo()).thenReturn("/delete");
         when(request.getSession()).thenReturn(session);
         when(request.getParameter("index")).thenReturn("0");
         when(session.getAttribute("cart")).thenReturn(shoppingCart);
      //   //PowerMockito.doNothing().when(controllerServlet, "deleteBook", request, response);
      //   when(request.getParameter("id")).thenReturn(tempID);
       } catch (MethodNotFoundException e) {}

      try {
       cartController.doGet(request, response);
       try {
          PowerMockito.verifyPrivate(cartController)
                      .invoke("deleteFromCart", request, response);
          called_deleteFromCart = true;
       } catch (Throwable e) {}
      } catch (Exception e) {}

      errorMsg = "After action \"" + "/delete" +
                        "\", did not call deleteFromCart().";
      assertTrue(errorMsg, called_deleteFromCart);
    }


}
