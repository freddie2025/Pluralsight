package com.pluralsight.module1;
import com.pluralsight.*;

import static org.junit.Assert.*;
import org.junit.Test;

import java.lang.reflect.Method;
import java.io.*;

public class Module1_Task4_IT {

    // Verify the deleteBook() method exists in BookDAO
    @Test
    public void _task4() throws Exception {
      Method method = null;

      try {
         method =  BookDAO.class.getMethod("deleteBook", int.class);
      } catch (NoSuchMethodException e) {
         //e.printStackTrace();
      }

      String message = "The method deleteBook() doesn't exist in BookDAO.java.";
      assertNotNull(message, method);
    }
}
