package com.pluralsight;

import static org.junit.Assert.*;
import org.junit.Test;
import org.junit.Before;

import java.lang.reflect.Method;
import java.util.ArrayList;

import org.mockito.Mockito;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

public class Module2_Task3_thru_4_IT extends Mockito {
    @Mock
    private ArrayList<CartItem> mockArrayList;

    @InjectMocks
    private ShoppingCart shoppingCart;

    Method method = null;

    @Before
    public void setUp() throws Exception {
      MockitoAnnotations.initMocks(this);
      try {
         method =  ShoppingCart.class.getMethod("deleteCartItem", int.class);
      } catch (NoSuchMethodException e) {
         //e.printStackTrace();
      }
    }

    // Verify the deleteCartItem() method exists in ShoppingCart
    @Test
    public void _task3() throws Exception {
      String message = "The method deleteCartItem() doesn't exist in ShoppingCart.java.";
      assertNotNull(message, method);
    }

    // Verify the deleteFromCart() method calls ArrayList remove()
    @Test
    public void _task4() throws Exception {
      String message = "The method deleteCartItem() doesn't exist in ShoppingCart.java.";
      assertNotNull(message, method);

      int index = 0;
      boolean called_remove = false;

      Mockito.when(mockArrayList.remove(index)).thenReturn(null);

      try {
        method.invoke(shoppingCart, index);
      } catch (Exception e) {
        System.out.println("method.invoke() exception");
      }

      try {
        Mockito.verify(mockArrayList).remove(index);
        called_remove = true;
      } catch (Throwable e) {System.out.println("ArrayList verify exception");}

      message = "The method deleteCartItem() doesn't call remove() on the ArrayList correctly.";
      assertTrue(message, called_remove);
    }
}
